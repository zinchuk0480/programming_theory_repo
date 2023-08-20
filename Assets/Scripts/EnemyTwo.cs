using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : Enemy
{
    //initialize value (constructor)
    public EnemyTwo() : base(1f) { }
    public GameObject weaponSpeedBonus;

    // Start is called before the first frame update
    void Start()
    {
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
