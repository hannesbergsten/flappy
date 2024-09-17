using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text playerName;

    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        if (StartMenuManager.Instance != null)
            playerName.text = StartMenuManager.Instance.Name;
        
        backButton.onClick.AddListener(Back);
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