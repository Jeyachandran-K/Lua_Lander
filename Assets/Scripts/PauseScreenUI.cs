using UnityEngine;
using UnityEngine.UI;

public class PauseScreenUI : MonoBehaviour
{
    [SerializeField] private Button pauseButton;

    private void Awake()
    {
        pauseButton.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
        });
    }
}
