using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);

        #if UNITY_WEBGL
            quitButton.gameObject.SetActive(false); 
        #else
            quitButton.onClick.AddListener(OnQuitClicked);
        #endif
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene(1); 
    }

    void OnQuitClicked()
    {
        Application.Quit();
    }
}
