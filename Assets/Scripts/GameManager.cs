using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text playerScore;
    public TMP_Text topPlayerScore;

    public Button backButton;

    public static GameManager Instance;

    private int score;
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

    void Start()
    {
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
                if (topPlayer.Points > 0)
                    topPlayerScore.text = $"{topPlayer.Name}: {topPlayer.Points}";

                Debug.Log(StartMenuManager.Instance.Name);
                playerScore.text = $"{StartMenuManager.Instance.Name}: 0";
            }

            
            // Clear the input field and reattach the listener
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(Back);
        }
    }

    void Back()
    {
        Debug.Log("Back");
        SceneManager.LoadScene((int)SceneName.StartMenu);
    }

    // Update is called once per frame
    void Update()
    {
    }
}