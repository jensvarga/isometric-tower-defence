using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private int enemyLevel = 1;
    public float speed = 0.3f;
    public Vector3 target;
    public List<Vector3> path;
    private int rewardMoney = 5;
    private int pointReward;
    public float health = 3f;

    private ScoreKeeper scoreKeeper;
    private Vector3 nearestGridPos;
    private AudioPlayer audioPlayer;
    [HideInInspector] public bool armoured;

    public bool dead = false;

    public void Setup(int level, bool armoured)
    {
        this.enemyLevel = level;
        this.health *= (float)level;
        this.rewardMoney *= level;
        this.pointReward = (int)this.health;
        this.armoured = armoured;
    }

    void Start()
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag("audioPlayer");
        if (soundObject != null) audioPlayer = soundObject.GetComponent<AudioPlayer>();
        var obj = GameObject.FindGameObjectWithTag("scoreKeeper");
        scoreKeeper = obj.GetComponent<ScoreKeeper>();
        var grid = GameObject.FindGameObjectWithTag("levelGrid").GetComponent<LevelGrid>();
        path = new List<Vector3>(grid.path);
        nearestGridPos = Vector3.zero;

        InvokeRepeating("AtGoal", 0, 1 - speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (nearestGridPos == Vector3.zero)
        {
            nearestGridPos = GetClosestPathPoint(path);
        }

        // Rotate toward target position
        Vector3 direction = (nearestGridPos - transform.position).normalized;

        var lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 90f, 0f);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, nearestGridPos, step);

        if (Vector3.Distance(transform.position, nearestGridPos) < 0.001f)
        {
            path.Remove(nearestGridPos);
            nearestGridPos = Vector3.zero;
        }

        // Die on 0 hp
        if (health <= 0)
        {
            if (audioPlayer != null) audioPlayer.PlayExplosionSound();
            dead = true;
            scoreKeeper.score += pointReward;
            scoreKeeper.money += rewardMoney;
            Object.Destroy(this.gameObject);
        }
    }

    private void AtGoal()
    {
        RaycastHit hit;
        LayerMask goalLeayer = LayerMask.GetMask("Goal");
        Vector3 thisPos = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);

        if (Physics.Raycast(thisPos, transform.TransformDirection(-Vector3.up), out hit, 20.0f, goalLeayer))
        {
            scoreKeeper.damageTaken++;
            dead = true;
            Object.Destroy(this.gameObject);
        }
    }

    Vector3 GetClosestPathPoint(List<Vector3> pathPoints)
    {
        Vector3 minPoint = new Vector3(0.0f, 0.0f, 0.0f);
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Vector3 point in pathPoints)
        {
            float dist = Vector3.Distance(point, currentPos);
            if (dist < minDist)
            {
                minPoint = point;
                minDist = dist;
            }
        }
        return minPoint;
    }
}
