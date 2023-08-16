using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    float moveSpeed = 2f;
    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        levelMove();

    }

    public void levelMove()
    {
        if (gameManager.gameIsPlay)
        {
            if (transform.position.y > gameManager.levelStopPlace)
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
            else
            {
                gameManager.gameIsPlay = false;
            }
        }
    }
}
