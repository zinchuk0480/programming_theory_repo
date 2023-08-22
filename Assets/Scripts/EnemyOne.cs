using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : Enemy
{
    //initialize value (constructor)
    public EnemyOne() : base(2f) { }
    
    void Start()
    {
        enemyHP = 50;
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

}
