using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenuManager : MonoBehaviour
{
    public TMP_InputField inputName;
    public Button saveButton;
    
    public static StartMenuManager Instance;
    public string Name;
    
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

    //Since the scene is loaded again with destroyed components, we need to fetch the current ones.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)SceneName.StartMenu)
        {
            // Reconnect to UI elements
            inputName = GameObject.Find("Player Name Input").GetComponent<TMP_InputField>();
            saveButton = GameObject.Find("Save Button").GetComponent<Button>();
            
            // Clear the input field and reattach the listener
            inputName.text = "";
            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(SavePlayerName);
        }
    }


    public void SavePlayerName()
    {
        if (Instance != null)
        {
            Instance.Name = inputName.text;
            inputName.text = string.Empty;
            SceneManager.LoadScene((int)SceneName.GamePlay);
        }
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
