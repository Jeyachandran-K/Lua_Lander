using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    public void Awake()
    {
        Time.timeScale = 1f;
        playButton.onClick.AddListener(() =>
        {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        } );
    }
    private void Start()
    {
        playButton.Select();
    }
}
