using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player
{
    public int score;
    public string playerName;
    public Dragon character;
}

// INHERITANCE
public abstract class PlayerCharacter : MonoBehaviour
{
    public abstract void AddForce();
}

public class GameManager : MonoBehaviour
{
    public TMP_Text playerScore;
    public TMP_Text topPlayerScore;

    public Button backButton;

    //ENCAPSULATION
    public static GameManager Instance { get; private set; }

    private Player player = new();
    public Score topPlayer = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)SceneName.GamePlay)
        {
            // Reconnect to UI elements
            topPlayerScore = GameObject.Find("Top Player").GetComponent<TMP_Text>();
            playerScore = GameObject.Find("Score").GetComponent<TMP_Text>();
            backButton = GameObject.Find("Back Button").GetComponent<Button>();

            if (StartMenuManager.Instance != null)
            {
                player.playerName = StartMenuManager.Instance.Name;
                
                if (topPlayer.Points > 0)
                    topPlayerScore.text = $"{topPlayer.Name}: {topPlayer.Points}";

                Debug.Log(StartMenuManager.Instance.Name);
                SetScoreText(0);
            }

            // Clear the input field and reattach the listener
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(Back);
        }
    }

    private void SetScoreText(int points)
    {
        playerScore.text = $"{player.playerName}: {points}";
    }

    void Back()
    {
        Debug.Log("Back");
        SceneManager.LoadScene((int)SceneName.StartMenu);
    }

    // Update is called once per frame
    public void AddScore(int points)
    {
        player.score += points;
        SetScoreText(player.score);
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}