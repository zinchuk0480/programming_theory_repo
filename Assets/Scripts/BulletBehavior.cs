using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float playerBulletSpeed = 5f;
    public float bulletDamage = 25;
    private float endDisplay = 10f;

    private GameObject bulletSmashParticle;

    
    // Ray
    public LayerMask layerMask;
    public Ray ray;
    RaycastHit bulletHit;

    // Start is called before the first frame update
    void Start()
    {
        //bulletSmashParticle = GetComponent<ParticleSystem>();
        bulletSmashParticle = GameObject.Find("BulletSmashParticle");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            bulletSmashParticle.gameObject.transform.position = transform.position;
            bulletSmashParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
            
            collision.gameObject.GetComponent<Enemy>().Damage(bulletDamage);
        }
    }
}
