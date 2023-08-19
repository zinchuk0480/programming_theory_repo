using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP = 100;

    public MeshRenderer Renderer;
    public Color startColor;
    public Color enemyDamageBlink = new Color(1f, 1f, 1f, 0.5f);

    private float enemyShootDelay = 2f;
    public GameObject enemyBulet;

    private GameObject explosionParticle;
    private bool shooting = true;



    // Start is called before the first frame update
    void Start()
    {


        Material material = Renderer.material;
        startColor = material.color;
        explosionParticle = GameObject.Find("ExplosionParticle");

        StartCoroutine(StartShuting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float damagePower)
    {
        enemyHP -= damagePower;
        Material material = Renderer.material;
        material.color = enemyDamageBlink;
        Invoke("ResetMaterial", 0.1f);
        Explosion();
    }

    public void ResetMaterial()
    {
        Material material = Renderer.material;
        material.color = startColor;
    }

    public IEnumerator StartShuting()
    {
        while (shooting)
        {
            yield return new WaitForSeconds(enemyShootDelay);
            BulletOut();
        }
    }

    public void BulletOut()
    {
        Instantiate(enemyBulet, transform.position, transform.rotation);
    }






    public void Explosion()
    {
        if (enemyHP <= 0)
        {
            explosionParticle.gameObject.transform.position = transform.position;
            Debug.Log(transform.position);
            explosionParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }
}
