using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        GameManager.Instance.OnGamePaused += Instance_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;

        Hide();
    }

    private void Instance_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.UnPauseGame(); 
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }
    private void Show()
    {
        resumeButton.Select();
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
