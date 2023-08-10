using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public bool shooting = false;

    [SerializeField] float playerSpeed = 200f;

    public GameObject playerBullet;
    public List <GameObject> playerBulletsArray;
    


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        StartShuting();
        BulletsBehavior();
    }

    public void PlayerMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }         
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        } 
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 5f * Time.deltaTime);
        }
    }

    public void StartShuting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting = true;
            InvokeRepeating("BulletOut", 0, 1);
        } else
        {
            CancelInvoke("BulletOut");
        }
    }

    public void BulletOut()
    {
        Instantiate(playerBullet, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        playerBulletsArray.Add(playerBullet);
    }

    public void BulletsBehavior()
    {

    }
}
