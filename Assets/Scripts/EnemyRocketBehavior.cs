using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketBehavior : MonoBehaviour
{
    GameManager gameManager;
    private float enemyrocketSpeed = 3f;


    public float rocketDamage = 50;
    private float endDisplay = 10f;

    private GameObject bulletSmashParticle;
    private GameObject player;
    private Vector3 rocketDirection;


    // Start is called before the first frame update
    void Start()
    {
        //bulletSmashParticle = GetComponent<ParticleSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        bulletSmashParticle = GameObject.Find("BulletSmashParticle");

        StartCoroutine("destructionRocket");
    }

    // Update is called once per frame
    void Update()
    {
        rocketMove();
        if (gameManager.gameIsPlay)
        {
            rocketDirection = player.gameObject.transform.position - transform.position;
        }
    }

    void rocketMove()
    {
        transform.position += rocketDirection.normalized * enemyrocketSpeed * Time.deltaTime;
        transform.up = Vector3.Slerp(transform.up, rocketDirection, 5 * Time.deltaTime);
        if (transform.position.y > endDisplay || transform.position.y < -endDisplay)
        {
            Destroyrocket();
        }
    }

    IEnumerator destructionRocket()
    {
        yield return new WaitForSeconds(7);
        Destroyrocket();
    }

    public void Destroyrocket()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bulletSmashParticle.gameObject.transform.position = transform.position;
            bulletSmashParticle.gameObject.GetComponent<ParticleSystem>().Play();
            gameManager.enemyBulletSmashPlay();
            Destroyrocket();

            other.gameObject.GetComponent<PlayerController>().Damage(rocketDamage);
        }
    }

    public void SearchPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
