using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button soundVolumeButton;
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private TextMeshProUGUI soundVolumeTextMesh;
    [SerializeField] private TextMeshProUGUI musicVolumeTextMesh;


    private void Start()
    {
        GameManager.Instance.OnGamePaused += Instance_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;

        soundVolumeTextMesh.text= "SOUND"+SoundManager.Instance.GetSoundVolume();

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
        soundVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundVolumeTextMesh.text = "SOUND" + SoundManager.Instance.GetSoundVolume();
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
        });
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
