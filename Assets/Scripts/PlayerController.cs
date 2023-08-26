using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    // Player
    public float playerSpeed = 7.7f;
    public float playerHP;
    public bool untouchable = false;
    
    public GameObject explosionParticle;
    public ParticleSystem damageSmoke;

    public MeshRenderer Renderer;
    public Color startColor;
    public Color playerDamageBlink = new Color(1f, 1f, 1f, 0.5f);



    // Shooting
    public bool shooting;
    public GameObject playerBullet;

    private float minimumShootDelay = 0.1f;
    private float stepShootDelay = 0.1f;


    // ENCAPSULATION
    private float m_playerShootDelay = 0.9f;
    public float playerShootDelay
    {
        get
        {
            return m_playerShootDelay;
        }
        set
        {
            if (value > minimumShootDelay)
            {
                m_playerShootDelay = value;
            }
            else
            {
                m_playerShootDelay = minimumShootDelay;
            }
        }
    }

    // Audio
    private AudioSource playerAudio;
    public AudioClip playerShoot;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shooting = true;
        playerHP = 100;
        StartCoroutine(StartShuting());

        Material material = Renderer.material;
        startColor = material.color;

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        ToggleShooting();
    }

    public void PlayerMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A) && transform.position.x > -gameManager.sideBorder)
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }         
        if (Input.GetKey(KeyCode.D) && transform.position.x < gameManager.sideBorder)
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        } 
        if (Input.GetKey(KeyCode.W) && transform.position.y < gameManager.verticalBorder)
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }        
        if (Input.GetKey(KeyCode.S) && transform.position.y > -gameManager.verticalBorder)
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
    public void ToggleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting = !shooting;
            if (shooting)
            {
                StartCoroutine(StartShuting());
            }
            else
            {
                StopCoroutine(StartShuting());
            }
        }
    }

    public void BulletOut()
    {
        Instantiate(playerBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        playerAudio.PlayOneShot(playerShoot, 0.05f);
    }


    public void Damage(float damagePower)
    {
        playerHP -= damagePower;
        Material material = Renderer.material;
        material.color = playerDamageBlink;
        Invoke("ResetMaterial", 0.3f);

        if (playerHP <= 50)
        {
            damageSmoke.Play();
        }
        else
        {
            damageSmoke.Stop();
        }

        if (playerHP <= 0)
        {
            PlayerExplosion();
        }
    }
    public void ResetMaterial()
    {
        Material material = Renderer.material;
        material.color = startColor;
    }

    public void PlayerExplosion()
    {
        explosionParticle.gameObject.transform.position = transform.position;
        explosionParticle.gameObject.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        gameManager.GameOver();

    }

    public void PlayerEscapeFromLevel()
    {
        if (transform.position.y < gameManager.verticalBorder + 17)
        {
            shooting = false;
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
            untouchable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponSpeed"))
        {
            playerShootDelay -= stepShootDelay;
            gameManager.WeaponSpeedBonusPlay();
            Destroy(other.gameObject);
        }
    }

}
