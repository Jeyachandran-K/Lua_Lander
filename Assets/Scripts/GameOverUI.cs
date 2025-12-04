using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }
    private void Update()
    {
        finalScore.text = "Final Score :"+GameManager.Instance.GetTotalScore().ToString();
    }
}
