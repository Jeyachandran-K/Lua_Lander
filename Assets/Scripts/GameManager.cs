using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    private int score;
    private int coinScoreAmount = 500;
    private float time;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Instance_OnCoinPickUp;
        Lander.Instance.OnLanding += Instance_OnLanding;
    }

    private void Update()
    {
        time += Time.deltaTime;
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

    public int GetScore()
    {
        return score;
    }
    public float GetTime()
    {
        return time;
    }
}
