using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : Enemy
{
    GameManager gameManager;
    //initialize value (constructor)
    public EnemyTwo() : base(1f) { }
    public GameObject weaponSpeedBonus;
    GameObject ground;
    public float startPos;
    bool MoveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 100;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Material material = Renderer.material;
        startColor = material.color;
        explosionParticle = GameObject.Find("ExplosionParticle");
        ground = GameObject.Find("Ground");
        startPos = transform.localPosition.x;
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
        Move();
        
    }

    // Polymorphism (override enemy class)
    public override void Explosion()
    {
        if (enemyHP <= 0)
        {
            Vector3 lastPos = transform.position;

            explosionParticle.gameObject.transform.position = lastPos;
            weaponSpeedBonus.gameObject.transform.position = transform.position;

            Debug.Log(transform.position);
            explosionParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
            GameObject instantiatedPrefab = Instantiate(weaponSpeedBonus, lastPos, transform.rotation);

            instantiatedPrefab.transform.SetParent(ground.transform);
        }
    }

    public override void Damage(float damagePower)
    {
        enemyHP -= damagePower;
        Material material = Renderer.material;
        material.color = enemyDamageBlink;
        Invoke("ResetMaterial", 0.1f);
        Explosion();
    }
    public void Move()
    {
        float localPos = transform.localPosition.x;

        if (MoveRight)
        {
            transform.Translate(Vector3.left * -0.5f * Time.deltaTime);
            if (localPos >= startPos + 2 || localPos > gameManager.GroundHorizontBorder)
            {
                MoveRight = false;
                Debug.Log(MoveRight);
            }
        }
        if (!MoveRight)
        {
            transform.Translate(Vector3.left * 0.5f * Time.deltaTime);
            if (localPos <= startPos - 2 || localPos < -gameManager.GroundHorizontBorder)
            {
                MoveRight = true;
            }
        }
    }

}
