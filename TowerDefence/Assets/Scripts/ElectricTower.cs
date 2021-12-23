using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectricTower : TowerBase
{
    [SerializeField] private GameObject torusPivotPoint;
    [SerializeField] private GameObject lightPoint;
    [SerializeField] private GameObject attackLight;

    protected override void Start()
    {
        base.Start();
        ignorsCover = true;
    }

    void Update()
    {
        turretPivotPoint.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
        torusPivotPoint.transform.Rotate(0f, -50f * Time.deltaTime, 0f);
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0.2f && enemiesInRange.Count > 0)
        {
            StartCoroutine(shakeGameObjectCOR(turretPivotPoint.gameObject, 0.2f, 0.1f, false));
        }

        if (cooldownTimer <= 0 && enemiesInRange.Count > 0)
        {
            ShootElectricDischarge();
            cooldownTimer = cooldown;
        }
    }

    void ShootElectricDischarge()
    {
        shakeGameObject(torusPivotPoint, 0.5f, 0.1f, false);
        Instantiate(attackLight, lightPoint.transform.position, Quaternion.identity);
        foreach (enemy enemy in enemiesInRange)
        {
            if (enemy.dead) {
                return;
            }

            enemy.health -= baseDamage * towerLevel;
            Vector3 lightPos = new Vector3(enemy.transform.position.x,
                                enemy.transform.position.y + 0.5f,
                                enemy.transform.position.z);

            Instantiate(attackLight, lightPos, Quaternion.identity);
        }
    }
}