using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    private float enemyBulletSpeed = 0.5f;


    public float bulletDamage = 25;
    private float endDisplay = 10f;

    private GameObject bulletSmashParticle;
    private GameObject player;
    private Vector3 bulletDirection;


    // Start is called before the first frame update
    void Start()
    {
        //bulletSmashParticle = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
        bulletSmashParticle = GameObject.Find("BulletSmashParticle");
        bulletDirection = player.gameObject.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        
        transform.Translate(bulletDirection * enemyBulletSpeed * Time.deltaTime);
        if (transform.position.y > endDisplay || transform.position.y < -endDisplay)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Crash");
        if (other.gameObject.CompareTag("Player"))
        {
            bulletSmashParticle.gameObject.transform.position = transform.position;
            bulletSmashParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);

            other.gameObject.GetComponent<PlayerController>().Damage(bulletDamage);
        }
    }
}
