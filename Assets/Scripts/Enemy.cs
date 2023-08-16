using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP = 100;

    public MeshRenderer Renderer;
    public Color startColor;
    public Color enemyDamageBlink = new Color(1f, 1f, 1f, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        Material material = Renderer.material;
        startColor = material.color;
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
    }

    public void ResetMaterial()
    {
        Material material = Renderer.material;
        material.color = startColor;
    }
}
