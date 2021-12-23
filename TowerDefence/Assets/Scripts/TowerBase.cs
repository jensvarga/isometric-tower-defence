using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public float towerLevel = 1f;
    public GameObject upgradeBp;
    public GameObject upgradedTower;
    public int sellReward = 15;

    [SerializeField] protected GameObject turretPivotPoint;
    [SerializeField] protected Transform projectile;
    [SerializeField] protected GameObject[] gunPoints;
    [SerializeField] protected float range = 3.0f;
    [SerializeField] protected float cooldown = 1.0f;
    [SerializeField] protected float baseDamage = 0.25f;

    protected enemy targetEnemy;
    protected List<enemy> enemiesInRange;
    protected float cooldownTimer = 0.0f;
    protected bool swap = false;
    protected bool shaking = false;
    protected bool ignorsCover = false;

    protected virtual void Start()
    {
        enemiesInRange = new List<enemy>();
        targetEnemy = null;
        cooldownTimer = cooldown;

        // Find enemies
        InvokeRepeating("FindEnemies", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy != null)
        {
            if (cooldownTimer > 0.0f)
            {
                cooldownTimer -= Time.deltaTime;
            }

            // Rotate turret against target
            Vector3 direction = (targetEnemy.transform.position - turretPivotPoint.transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 90f, 0f);
            turretPivotPoint.transform.rotation = Quaternion.Slerp(turretPivotPoint.transform.rotation, lookRotation, Time.deltaTime * 10f);

            if (cooldownTimer <= 0.0f)
            {
                cooldownTimer = cooldown;
                // shoot
                Vector3 shootPoint;
                if (gunPoints.ToList().Count.Equals(2))
                {
                    if (swap)
                    {
                        shootPoint = gunPoints[0].transform.position;
                        swap = false;
                    }
                    else
                    {
                        shootPoint = gunPoints[1].transform.position;
                        swap = true;
                    }
                }
                else
                {
                    shootPoint = gunPoints[0].transform.position;
                }
                Vector3 shootDir = (targetEnemy.transform.position - shootPoint).normalized;
                Transform beamTransform = Instantiate(projectile, shootPoint, Quaternion.LookRotation(shootDir));
                shakeGameObject(turretPivotPoint.gameObject, 0.2f, 0.1f, false);
                float damage = baseDamage * towerLevel;
                beamTransform.GetComponent<Projectile>().Setup(shootDir, damage);
            }
        }
        else
        {
            if (turretPivotPoint.transform.rotation.x > 0.01f || turretPivotPoint.transform.rotation.z > 0.01f)
            {
                // Return to start position after target is lost
                var slerpTo = Quaternion.Euler(0.0f, turretPivotPoint.transform.rotation.y, 0.0f);
                turretPivotPoint.transform.rotation = Quaternion.Slerp(turretPivotPoint.transform.rotation, slerpTo, Time.deltaTime * 10f);
            }
            else
            {
                turretPivotPoint.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
            }
        }
        // Target enemy with lowest health
        if (!enemiesInRange.Count.Equals(0) || enemiesInRange != null)
        {
            targetEnemy = GetEnemyInRangeWithLowesHealt(enemiesInRange);
        }
        else
        {
            // No enemy in sight, slow rotation
            targetEnemy = null;
        }
    }

    protected void RemoveNullTargets()
    {
        for (var i = enemiesInRange.Count - 1; i > -1; i--)
        {
            if (enemiesInRange[i] == null) enemiesInRange.RemoveAt(i);
        }
    }

    protected void FindEnemies()
    {
        RemoveNullTargets();
        List<enemy> enemies = new List<enemy>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject obj in objs.ToList())
        {
            if (obj.GetComponent<enemy>() != null)
            {
                var e = obj.GetComponent<enemy>();
                if (!enemies.Contains(e) && e.dead == false)
                {
                    enemies.Add(e);
                }
            }
        }

        if (!enemies.Count.Equals(0))
        {
            // Check for enemies in range
            foreach (enemy enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < range &&
                    enemy.transform.position.y <= this.transform.position.y)
                {
                    if (!enemiesInRange.Contains(enemy))
                    {
                        enemiesInRange.Add(enemy);
                    }
                }
                else
                {
                    if (!enemiesInRange.Count.Equals(0) && enemiesInRange.Contains(enemy))
                    {
                        enemiesInRange.Remove(enemy);
                    }
                }
            }
        }

        // Delete local arrays
        enemies = null;
        objs = null;
    }

    protected enemy GetEnemyInRangeWithLowesHealt(List<enemy> enemies)
    {
        enemy lowesEnemy = null;
        var minHealth = float.PositiveInfinity;
        foreach (enemy enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }
            // Skip enemies in cover
            if (ignorsCover == false)
            {
                RaycastHit hit;
                Vector3 enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.5f, enemy.transform.position.z);
                float dis = Vector3.Distance(turretPivotPoint.transform.position, enemy.transform.position);
                LayerMask groundLayer = LayerMask.GetMask("Ground");
                if (Physics.Raycast(turretPivotPoint.transform.position, (enemyPos - turretPivotPoint.transform.position), out hit, dis, groundLayer))
                {
                    // Remove enemies in cover
                    continue;
                }
            }

            if (enemy.health < minHealth && enemy.dead == false)
            {
                minHealth = enemy.health;
                lowesEnemy = enemy;
            }
        }
        return lowesEnemy;
    }

    protected void shakeGameObject(GameObject objectToShake, float shakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (shaking)
        {
            return;
        }
        shaking = true;
        StartCoroutine(shakeGameObjectCOR(objectToShake, shakeDuration, decreasePoint, objectIs2D));
    }

    protected IEnumerator shakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (decreasePoint >= totalShakeDuration)
        {
            Debug.LogError("decreasePoint must be less than totalShakeDuration...Exiting");
            yield break; //Exit!
        }

        //Get Original Pos and rot
        if (objectToShake != null)
        {
            Transform objTransform = objectToShake.transform;
            Vector3 defaultPos = objTransform.position;
            Quaternion defaultRot = objTransform.rotation;

            float counter = 0f;

            //Shake Speed
            const float speed = 0.05f;

            //Angle Rotation(Optional)
            const float angleRot = 2;

            //Do the actual shaking
            while (counter < totalShakeDuration)
            {
                counter += Time.deltaTime;
                float decreaseSpeed = speed;
                float decreaseAngle = angleRot;

                //Shake GameObject
                if (objectIs2D)
                {
                    //Don't Translate the Z Axis if 2D Object
                    Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                    tempPos.z = defaultPos.z;
                    objTransform.position = tempPos;

                    //Only Rotate the Z axis if 2D
                    objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(0f, 0f, 1f));
                }
                else
                {
                    objTransform.position = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                    objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(1f, 1f, 1f));
                }
                yield return null;


                //Check if we have reached the decreasePoint then start decreasing  decreaseSpeed value
                if (counter >= decreasePoint)
                {
                    //Reset counter to 0 
                    counter = 0f;
                    while (counter <= decreasePoint)
                    {
                        counter += Time.deltaTime;
                        decreaseSpeed = Mathf.Lerp(speed, 0, counter / decreasePoint);
                        decreaseAngle = Mathf.Lerp(angleRot, 0, counter / decreasePoint);

                        //Shake GameObject
                        if (objectIs2D)
                        {
                            //Don't Translate the Z Axis if 2D Object
                            Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                            tempPos.z = defaultPos.z;
                            objTransform.position = tempPos;

                            //Only Rotate the Z axis if 2D
                            objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(0f, 0f, 1f));
                        }
                        else
                        {
                            objTransform.position = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                            objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(1f, 1f, 1f));
                        }
                        yield return null;
                    }

                    //Break from the outer loop
                    break;
                }
            }
            objTransform.position = defaultPos; //Reset to original postion
            objTransform.rotation = defaultRot;//Reset to original rotation
        }

        shaking = false; //So that we can call this function next time
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
