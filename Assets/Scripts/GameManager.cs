using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float sideBorder = 14f;
    public float verticalBorder = 8f;

    public float GroundHorizontBorder = 4f;

    public bool gameIsPlay = false;
    public float levelStopPlace = -200f;

    // Objects
    public GameObject player;
    PlayerController playerController;
    public GameObject ground;
    public GameObject enemys_folder;

    public GameObject[] enemyTypesArray;

    // All Enemy and his bullet
    private GameObject[] enemys;
    private GameObject[] bullets;

    // Level
    private Level1 level_1;


    // Audio
    private AudioSource gameManagerAudio;
    public AudioClip playerBulletSmash;
    public AudioClip enemyBulletSmash;
    public AudioClip enemyExplosion;
    public AudioClip weaponSpeedBonus;


    // Side bar
    public int score;
    public TextMeshProUGUI scoreText;

    private bool pause;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Start game
        pauseScreen = GameObject.Find("Pause Screen");
        gameOverScreen = GameObject.Find("Game Over Screen");

        enemys_folder = GameObject.Find("Enemys");
        playerController = player.GetComponent<PlayerController>();

        level_1 = GameObject.Find("Level1").GetComponent<Level1>();

        gameManagerAudio = GetComponent<AudioSource>();


        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsPlay)
        {
            EndLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); 
        }
    }

    private void EndLevel()
    {
        if (gameIsPlay)
        {
            player.transform.Translate(Vector3.forward * playerController.playerSpeed * Time.deltaTime);
            playerController.untouchable = true;
        }
    }

    public void TogglePause()
    {
        pause = !pause;
        Pause();
    }
    private void Pause()
    {
        if (pause)
        {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameIsPlay = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        pause = false;
        gameIsPlay = true;
        pauseScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        SpawnLevelEnemys(level_1.enemys_location);
    }

    public void RestartGame()
    {
        Instantiate(player);
        player.gameObject.transform.position = new Vector3(0, -5, -14.85f);
        Vector3 groundPos = ground.gameObject.transform.position;
        ground.gameObject.transform.position = new Vector3(groundPos.x, 200, groundPos.z);
        SearchAndDestroyEnemysAndBullets();
        StartGame();
    }

    public void SearchAndDestroyEnemysAndBullets()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].gameObject.GetComponent<Enemy>().DestroyGameObject();
        }
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].gameObject.GetComponent<EnemyBulletBehavior>().DestroyBullet();
        }
    }

    public void SpawnLevelEnemys(float[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (i % 3 == 0)
            {
                GameObject prefabInstantiate = Instantiate(enemyTypesArray[1], new Vector3(array[i, 0], array[i, 1], array[i, 2]), transform.rotation);
                prefabInstantiate.transform.SetParent(enemys_folder.transform);
            }
            else
            {
                GameObject prefabInstantiate = Instantiate(enemyTypesArray[0], new Vector3(array[i, 0], array[i, 1], array[i, 2]), transform.rotation);
                prefabInstantiate.transform.SetParent(enemys_folder.transform);
            }
        }
    }

    // Audio functions
    public void playerBulletSmashPlay()
    {
        gameManagerAudio.PlayOneShot(playerBulletSmash, 0.5f);
    }
    //public void playerExplosionPlay()
    //{
    //    gameManagerAudio.PlayOneShot(playerExplosion, 0.5f);
    //}

    public void enemyBulletSmashPlay()
    {
        gameManagerAudio.PlayOneShot(enemyBulletSmash, 0.5f);
    }

    public void enemyExplosionPlay()
    {
        gameManagerAudio.PlayOneShot(enemyExplosion, 0.5f);
    }    
    public void WeaponSpeedBonusPlay()
    {
        gameManagerAudio.PlayOneShot(weaponSpeedBonus, 0.05f);
    }

    // Side bar
    public void ScoreUp()
    {
        score += 1;
        scoreText.text = $"Score: {(score).ToString()}";
    }
}
