using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private float damage;
    private Vector3 dir;
    public ParticleSystem lazerHit;
    [SerializeField] private GameObject paticlePoint;
    [SerializeField] private GameObject lazerLight;
    private float failsafeLifetime = 2.0f;
    private AudioPlayer audioPlayer;
 
    public void Setup(Vector3 dir, float damage, AudioPlayer audioPlayer)
    {
        this.dir = dir;
        this.damage = damage;
        this.audioPlayer = audioPlayer;
    }

    // Update is called once per frame
    private void Update()
    {
        // Kill if falsafe exeeded:
        failsafeLifetime -= Time.deltaTime;
        if (failsafeLifetime <= 0)
        {
            Object.Destroy(this.gameObject);
        }
        var lookRotation = Quaternion.LookRotation(dir); //* Quaternion.Euler(0f, 90f, 0f);
        this.transform.rotation = lookRotation;
        transform.position += dir * speed * Time.deltaTime;

        // Detect collision
    }

    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "enemy")
        {
            audioPlayer.PlayHitSound();
            var paticleRot = Quaternion.LookRotation( new Vector3(dir.x, -dir.y, dir.z));
            var instance = Instantiate(lazerHit, paticlePoint.transform.position, paticleRot);
            Destroy(instance.gameObject, 3.0f);
            var light = Instantiate(lazerLight, paticlePoint.transform.position, paticleRot);
            
            enemy enemy = other.gameObject.GetComponent<enemy>();
            enemy.health -= damage;

            Object.Destroy(this.gameObject);
        }
    }
}
