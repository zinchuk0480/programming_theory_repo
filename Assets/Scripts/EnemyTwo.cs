using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : Enemy
{
    //initialize value (constructor)
    public EnemyTwo() : base(1f) { }

    public GameObject weaponSpeedBonus;
    private GameObject ground;
    public float startPos;
    private bool MoveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        ground = GameObject.Find("Ground");

        enemyHP = 100;
        startPos = transform.localPosition.x;

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
        Move();
    }

    // POLYMORPHISM
    public override void Explosion()
    {
        if (enemyHP <= 0)
        {
            explosionParticle.gameObject.transform.position = transform.position;
            explosionParticle.gameObject.GetComponent<ParticleSystem>().Play();

            weaponSpeedBonus.gameObject.transform.position = transform.position;
            GameObject instantiatedPrefab = Instantiate(weaponSpeedBonus, transform.position, transform.rotation);
            instantiatedPrefab.transform.SetParent(ground.transform);

            gameManager.enemyExplosionPlay();
            DestroyGameObject();
        }
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
