using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// abstraction
// Inheritance 
public abstract class Enemy : MonoBehaviour
{

    public float enemyHP = 100;

    public MeshRenderer Renderer;
    public Color startColor;
    public Color enemyDamageBlink = new Color(1f, 1f, 1f, 0.5f);

    protected float enemyShootDelay;

    //initialize value (constructor)
    public Enemy(float initialEnemyShootDelay)
    {
        enemyShootDelay = initialEnemyShootDelay;
    }
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
        Explosion();
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
            BulletOut();
        }
    }

    public void BulletOut()
    {
        Instantiate(enemyBulet, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
    }

    public virtual void Explosion()
    {
        if (enemyHP <= 0)
        {
            explosionParticle.gameObject.transform.position = transform.position;
            Debug.Log(transform.position);
            explosionParticle.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }


    public void VisualContact()
    {
        if (transform.position.y < fireRangeArea && transform.position.y > -fireRangeArea)
        {
            openFire = true;
        }
    }

}
