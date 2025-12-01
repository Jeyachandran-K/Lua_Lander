using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private int coinScoreAmount = 500;

    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Instance_OnCoinPickUp;
        Lander.Instance.OnLanding += Instance_OnLanding;
    }

    private void Instance_OnLanding(object sender, Lander.OnLandingEventArgs e)
    {
        addScore(e.score);
    }

    private void Instance_OnCoinPickUp(object sender, System.EventArgs e)
    {
        addScore(coinScoreAmount);
    }

    public void addScore(int scoreAmount)
    {
        score += scoreAmount;
        Debug.Log(score);

    }
}
