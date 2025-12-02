using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LandedUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMeshPro;
    [SerializeField] private TextMeshProUGUI statsTextMeshPro;
    [SerializeField] private TextMeshProUGUI buttonTextMeshPro;
    [SerializeField] private Button nextButton;

    private Action buttonClickAction;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            buttonClickAction();
        });
    }

    private void Start()
    {
        Lander.Instance.OnLanding += Instance_OnLanding;
        Hide();
    }

    private void Instance_OnLanding(object sender, Lander.OnLandingEventArgs e)
    {
        UpdateTextMesh(e);
        Show();
    }
    private void UpdateTextMesh(Lander.OnLandingEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            titleTextMeshPro.text = "SUCCESFUL LANDING!";
            buttonTextMeshPro.text = "CONTINUE";
            buttonClickAction = GameManager.Instance.GoNextLevel;
        }
        else
        {
            titleTextMeshPro.text = "<color=#ff0000>CRASH!</color>";
            buttonTextMeshPro.text = "RESTART";
            buttonClickAction = GameManager.Instance.Retry;
        }

        statsTextMeshPro.text = Mathf.RoundToInt(e.landingSpeed) + "\n" +
                              Mathf.RoundToInt(e.dotProduct) + "\n" +
                              e.scoreMultiplier + "\n" +
                              GameManager.Instance.GetScore();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    

}
