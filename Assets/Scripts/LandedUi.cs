using TMPro;
using UnityEngine;

public class LandedUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMeshPro;
    [SerializeField] private TextMeshProUGUI statsTextMeshPro;

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
        }
        else
        {
            titleTextMeshPro.text = "<color=#ff0000>CRASH!</color>";
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
