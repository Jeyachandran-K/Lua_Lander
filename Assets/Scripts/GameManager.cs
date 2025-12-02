using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    private int score;
    private int coinScoreAmount = 500;
    private float time;
    private bool isTimerActive;
    private static int levelNumber=1;
    [SerializeField] private List<GameLevel> gameLevelList;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Instance_OnCoinPickUp;
        Lander.Instance.OnLanding += Instance_OnLanding;
        Lander.Instance.OnStateChange += Instance_OnStateChange;

        LoadCurrentLevel();
    }

    private void Instance_OnStateChange(object sender, Lander.OnStateChangeEventArgs e)
    {
        if (e.state == Lander.State.Normal)
        {
            isTimerActive = true;
        }
    }

    private void Update()
    {
        if (isTimerActive)
        {
            time += Time.deltaTime;
        }
        
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
    private void LoadCurrentLevel()
    {
        foreach(GameLevel gameLevel in gameLevelList)
        {
            if(gameLevel.GetLevelNumber() == levelNumber)
            {
                GameLevel spawnedGameLevel=Instantiate(gameLevel,Vector3.zero,Quaternion.identity);
                Lander.Instance.transform.position=spawnedGameLevel.GetLanderStartingPosition();
            }
        }
    }
    public void GoNextLevel()
    {
        levelNumber++;
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
