using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public string playerName;
    public int score;
    public int bestScore;
    public string bestPlayer;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadName();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
        public int bestScore;
        public string bestPlayer;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.score = score;
        data.bestPlayer = bestPlayer;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            score = data.score;
            bestPlayer = data.bestPlayer;
            bestScore = data.bestScore;

        }
    }
}
