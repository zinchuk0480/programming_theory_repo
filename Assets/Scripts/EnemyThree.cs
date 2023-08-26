using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : Enemy
{
    public EnemyThree() : base(5f) { }
    // Start is called before the first frame update

    public GameObject enemyRocket;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyHP = 25;
        Material material = Renderer.material;
        startColor = material.color;

        explosionParticle = GameObject.Find("ExplosionParticle");
    }

    // Update is called once per frame
    void Update()
    {
        VisualContact();
        if (ceaseFire && openFire)
        {
            StartCoroutine(StartShuting());
            ceaseFire = false;
        }
    }

    public override void rocketOut()
    {
        Instantiate(enemyRocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}
