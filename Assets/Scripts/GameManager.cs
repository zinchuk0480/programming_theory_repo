using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameIsPlay = false;
    public float levelStopPlace = -200f;
    public GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPlay = true;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsPlay)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        gameIsPlay = false;
        player.transform.Translate(Vector3.up * playerController.playerSpeed * Time.deltaTime);
        playerController.untouchable = true;
    }
}
