using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// ABSTRACTION
// INHERITANCE
public abstract class Enemy : MonoBehaviour
{
    //initialize value (constructor)
    protected float enemyShootDelay;
    public Enemy(float initialEnemyShootDelay)
    {
        enemyShootDelay = initialEnemyShootDelay;
    }

    public GameManager gameManager;
    public GameObject player;
    
    public float enemyHP;

    public MeshRenderer Renderer;
    public Color startColor;
    public Color enemyDamageBlink = new Color(1f, 1f, 1f, 0.5f);

    public GameObject enemyBulet;
    public bool openFire = false;
    public bool ceaseFire = true;
    private float fireRangeArea = 8f;

    public GameObject explosionParticle;

    public virtual void Damage(float damagePower)
    {
        enemyHP -= damagePower;
        Material material = Renderer.material;
        material.color = enemyDamageBlink;
        Invoke("ResetMaterial", 0.1f);
        
        if (enemyHP <= 0)
        {
            Explosion();
        }
    }

    public void ResetMaterial()
    {
        Material material = Renderer.material;
        material.color = startColor;
    }

    public IEnumerator StartShuting()
    {
        while (openFire)
        {
            yield return new WaitForSeconds(enemyShootDelay);
            rocketOut();
        }
    }

    public virtual void rocketOut()
    {
        Instantiate(enemyBulet, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
    }

    public virtual void Explosion()
    {
        gameManager.enemyExplosionPlay();
        explosionParticle.gameObject.transform.position = transform.position;
        explosionParticle.transform.parent = GameObject.Find("Ground").transform;
        explosionParticle.gameObject.GetComponent<ParticleSystem>().Play();
        DestroyGameObject();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
        if (gameManager.gameIsPlay)
        {
            gameManager.ScoreUp();
        }
    }

    public void VisualContact()
    {
        var enemyInDisplay = (transform.position.y < fireRangeArea && transform.position.y > -fireRangeArea);
        if (GameObject.FindGameObjectWithTag("Player") && enemyInDisplay)
        {
            openFire = true;
        }
        else
        {
            openFire = false;
        }
    }

    public void SearchPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
