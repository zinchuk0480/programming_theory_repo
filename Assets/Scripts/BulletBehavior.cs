using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float playerBulletSpeed = 10f;

    public float bulletDamage = 25;
    private float endDisplay = 10f;

    private GameObject bulletSmashParticle;
    private GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        //bulletSmashParticle = GetComponent<ParticleSystem>();
        bulletSmashParticle = GameObject.Find("BulletSmashParticle");
        ground = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        transform.Translate(Vector3.up * playerBulletSpeed * Time.deltaTime);
        if (transform.position.y > endDisplay)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            bulletSmashParticle.gameObject.transform.position = transform.position;
            bulletSmashParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);

            if (other.gameObject.GetComponent<EnemyTwo>())
            {
                other.gameObject.GetComponent<Enemy>().Damage(bulletDamage, other.gameObject.GetComponent<EnemyTwo>().weaponSpeedBonus, ground);
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().Damage(bulletDamage);
            }
        }
    }
}
