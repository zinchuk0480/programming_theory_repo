using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestPlayerText;
    private string input;

    public void NewNameSelected(string s)
    {
        input = s;
        MainManager.Instance.playerName = input;
    }

    // Start is called before the first frame update
    void Start()
    {
        input = MainManager.Instance.playerName;
        bestScoreText.text = $"Best score: {MainManager.Instance.bestScore.ToString()}";
        bestPlayerText.text = $"Best player {MainManager.Instance.bestPlayer}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        MainManager.Instance.SaveName();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }


}
