using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private GameObject leftArrowKey;
    [SerializeField] private GameObject rightArrowKey;
    [SerializeField] private GameObject upArrowKey;
    [SerializeField] private GameObject downArrowKey;
    [SerializeField] private Image fuelImage;

    private void Update()
    {
        UpdateStatsTextMesh();
    }

    private void UpdateStatsTextMesh()
    {
        leftArrowKey.SetActive(Lander.Instance.GetSpeedX() < 0);
        rightArrowKey.SetActive(Lander.Instance.GetSpeedX()>= 0);
        upArrowKey.SetActive(Lander.Instance.GetSpeedY() >=0);
        downArrowKey.SetActive(Lander.Instance.GetSpeedY()< 0);

        fuelImage.fillAmount = Lander.Instance.GetFuelAmountNormalized();


        statsTextMesh.text = GameManager.Instance.GetScore() + "\n" +
                             Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
                            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f)) + "\n" +
                            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY() * 10f)) ;
    }
}
