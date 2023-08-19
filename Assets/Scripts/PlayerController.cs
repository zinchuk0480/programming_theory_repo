using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // World border
    public float sideBorder = 10f;
    public float verticalBorder = 5f;

    // Player
    public Rigidbody playerRb;
    public float playerSpeed = 7.7f;
    private float playerHP = 100f;
    public bool untouchable = false;

    public MeshRenderer Renderer;
    public Color startColor;
    public Color playerDamageBlink = new Color(1f, 1f, 1f, 0.5f);

    // Shooting
    [SerializeField] private bool shooting = true;
    public GameObject playerBullet;
    private float m_playerShootDelay = 1f;
    public float playerShootDelay
    {
        get
        {
            return m_playerShootDelay;
        }
        set
        {
            if (value > 0.4f)
            {
                m_playerShootDelay = value;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        StartCoroutine(StartShuting());

        Material material = Renderer.material;
        startColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        
        BulletsBehavior();
    }

    public void PlayerMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A) && transform.position.x > -sideBorder)
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }         
        if (Input.GetKey(KeyCode.D) && transform.position.x < sideBorder)
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        } 
        if (Input.GetKey(KeyCode.W) && transform.position.y < verticalBorder)
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }        
        if (Input.GetKey(KeyCode.S) && transform.position.y > -verticalBorder)
        {
            transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
    }

    public IEnumerator StartShuting()
    {
        while (shooting)
        {
            yield return new WaitForSeconds(playerShootDelay);
            BulletOut();
        }
    }

    public void BulletOut()
    {
        Instantiate(playerBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    public void BulletsBehavior()
    {

    }

    public void Damage(float damagePower)
    {
        playerHP -= damagePower;
        Material material = Renderer.material;
        material.color = playerDamageBlink;
        Invoke("ResetMaterial", 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponSpeed"))
        {
            playerShootDelay -= 0.3f;
            Destroy(other.gameObject);
        }
    }

}
