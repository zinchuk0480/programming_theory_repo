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
    [SerializeField] float playerSpeed = 7.7f;

    // Shooting
    [SerializeField] private bool shooting = true;
    [SerializeField] float playerShootSpeed = 0.2f;
    public GameObject playerBullet;
    public List <GameObject> playerBulletsArray;

    


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        StartCoroutine(StartShuting());
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
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
        }        
        if (Input.GetKey(KeyCode.S) && transform.position.y > -verticalBorder)
        {
            transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
        }
    }

    public IEnumerator StartShuting()
    {
        while (shooting)
        {
            yield return new WaitForSeconds(playerShootSpeed);
            BulletOut();
        }
    }

    public void BulletOut()
    {
        Instantiate(playerBullet, transform.position, transform.rotation);
        playerBulletsArray.Add(playerBullet);
    }

    public void BulletsBehavior()
    {

    }
}
