using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float sideBorder = 10f;
    public float verticalBorder = 6f;

    public float GroundHorizontBorder = 4f;


    // Objects
    public GameObject player;
    //PlayerController playerController;
    public GameObject ground;
    public GameObject enemys_folder;

    public GameObject[] enemyTypesArray;

    // All Enemy and his rocket
    private GameObject[] enemys;
    private GameObject[] bullets;
    private GameObject[] bonus;
    private GameObject[] rockets;

    // Level
    private Level1 level_1;
    public bool gameIsPlay = false;
    public bool endOfLevel = false;
    public float levelStopPlace = -200f;

    // Audio
    private AudioSource gameManagerAudio;
    public AudioClip playerBulletSmash;
    public AudioClip enemyBulletSmash;
    public AudioClip enemyExplosion;
    public AudioClip weaponSpeedBonus;


    // Side bar
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private bool pause;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    public TextMeshProUGUI gameOverScore;

    // Start is called before the first frame update
    void Start()
    {
        // Start game
        pauseScreen = GameObject.Find("Pause Screen");
        gameOverScreen = GameObject.Find("Game Over Screen");

        enemys_folder = GameObject.Find("Enemys");
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        level_1 = GameObject.Find("Level1").GetComponent<Level1>();

        gameManagerAudio = GetComponent<AudioSource>();


        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (endOfLevel && player != null)
        {
            EndLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); 
        }
    }

    public void EndLevel()
    {
        Debug.Log("player: " + player != null);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerEscapeFromLevel();
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

        gameOverScore.text = $"{MainManager.Instance.playerName} / Score: {MainManager.Instance.score.ToString()}";
    }


    public void StartGame()
    {
        pause = false;
        gameIsPlay = true;
        endOfLevel = false;
        pauseScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);

        MainManager.Instance.score = 0;
        scoreText.text = "Score: 0";
        bestScoreText.text = $"Best score: {MainManager.Instance.bestScore.ToString()}";

        Time.timeScale = 1;
        SpawnLevelEnemys(level_1.enemys_location);
    }

    public void RestartGame()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        Instantiate(player);
        var resetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().SearchPlayer();

        resetPlayer.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);

        Vector3 groundPos = ground.gameObject.transform.position;
        ground.gameObject.transform.position = new Vector3(groundPos.x, 200, groundPos.z);
        SearchAndDestroyEnemysAndBullets();
        StartGame();
    }

    public void SearchAndDestroyEnemysAndBullets()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        bonus = GameObject.FindGameObjectsWithTag("WeaponSpeed");
        rockets = GameObject.FindGameObjectsWithTag("WeaponSpeed");


        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].gameObject.GetComponent<Enemy>().DestroyGameObject();
        }
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].gameObject.GetComponent<EnemyBulletBehavior>().DestroyBullet();
        }
        foreach (GameObject rocket in rockets)
        {
            GameObject.Destroy(rocket);
        }
        foreach (GameObject b in bonus)
        {
            GameObject.Destroy(b);
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
            else if (i % 7 == 0 && i != 0)
            {
                GameObject prefabInstantiate = Instantiate(enemyTypesArray[2], new Vector3(array[i, 0], array[i, 1], array[i, 2]), transform.rotation);
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
        MainManager.Instance.score += 1;
        scoreText.text = $"Score: {(MainManager.Instance.score).ToString()}";
        if (MainManager.Instance.score >= MainManager.Instance.bestScore)
        {
            bestScoreText.text = scoreText.text;
            MainManager.Instance.bestScore = MainManager.Instance.score;
            MainManager.Instance.bestPlayer = MainManager.Instance.playerName;
            MainManager.Instance.SaveName();
        }
    }
}
