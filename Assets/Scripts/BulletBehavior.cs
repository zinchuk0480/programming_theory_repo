using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    GameManager gameManager;
    private float playerBulletSpeed = 10f;

    public float bulletDamage = 25;
    private float endDisplay = 10f;

    private GameObject bulletSmashParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //bulletSmashParticle = GetComponent<ParticleSystem>();
        bulletSmashParticle = GameObject.Find("BulletSmashParticle");
    }

    // Update is called once per frame
    void Update()
    {
        bulletMove();
    }

    void bulletMove()
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
            gameManager.playerBulletSmashPlay();

            bulletSmashParticle.gameObject.transform.position = transform.position;
            bulletSmashParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);

            other.gameObject.GetComponent<Enemy>().Damage(bulletDamage);
        }
    }

}
