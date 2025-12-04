using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    private static int levelNumber = 1;
    private static int totalScore=0;

    private int score;
    private int coinScoreAmount = 500;
    private float time;
    private bool isTimerActive;
    private float currentLevelOrthographicSize;

    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Instance_OnCoinPickUp;
        Lander.Instance.OnLanding += Instance_OnLanding;
        Lander.Instance.OnStateChange += Instance_OnStateChange;

        GameInput.Instance.OnMenuButtonPressed += Instance_OnMenuButtonPressed;
        LoadCurrentLevel();
    }

    private void Instance_OnMenuButtonPressed(object sender, System.EventArgs e)
    {
        PauseUnpauseGame();
    }

    private void Instance_OnStateChange(object sender, Lander.OnStateChangeEventArgs e)
    {
        if (e.state == Lander.State.Normal)
        {
            isTimerActive = true;
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
        }
        else
        {
            isTimerActive= false;
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
        GameLevel gameLevel=GetGameLevel();
        GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnedGameLevel.GetLanderStartingPosition();
        cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GetCameraStartingPositionTransform();
        currentLevelOrthographicSize = spawnedGameLevel.GetZoomOutOrthographicSize();
        CinemachineCameraZoom2D.Instance.SetTargetOrthographicSize(currentLevelOrthographicSize);
    }

    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                return gameLevel;
                
            }

        }
        return null;
    }

    public void GoNextLevel()
    {
        levelNumber++;
        totalScore += score;

        if (GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }           
    }
    public void Retry()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public float GetCurrentLevelOrthoGraphicsize()
    {
        return currentLevelOrthographicSize;
    }
    private void PauseUnpauseGame()
    {
        if (Time.timeScale == 0)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this,EventArgs.Empty); 
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        OnGameUnpaused?.Invoke(this,EventArgs.Empty);
    }
    public int GetTotalScore()
    {
        return totalScore;
    }

    public static void ResetStaticData()
    {
        levelNumber = 1;

        totalScore = 0;
    }
}
