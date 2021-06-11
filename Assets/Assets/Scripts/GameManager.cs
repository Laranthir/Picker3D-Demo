using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject startScreen, ingameOverlay, youWon, youLost;
    [SerializeField] private GameObject scoreUI, currentLevelUI, nextLevelUI, cpFirstUI, cpSecondUI, cpLastUI;
    [SerializeField] private GameObject player;
    [SerializeField] private Movement playerScript;

    private TMP_Text tmp_Score, tmp_CurrentLevel, tmp_NextLevel;
    

    public float speedForward = 15f;
    public float speedTouch = 20f;

    public bool gameStart = false;
    public bool controllerActive = true;
    public bool isMoving = false;

    public int score;
    public int currentLevel = 1;
    public int checkpointReached;
    
    
    private void Start()
    {
        tmp_Score = scoreUI.GetComponent<TMP_Text>();
        tmp_CurrentLevel = currentLevelUI.GetComponent<TMP_Text>();
        tmp_NextLevel = nextLevelUI.GetComponent<TMP_Text>();
    }

    // UI Functions Start Here
    public void UpdateScore(bool playerScored)
    {
        if (playerScored)
            tmp_Score.text = Instance.score.ToString();
    }

    public void UpdateLevelBar(bool levelPassed)
    {
        if (levelPassed)
        {
            tmp_CurrentLevel.text = Instance.currentLevel.ToString();
            tmp_NextLevel.text = (Instance.currentLevel + 1).ToString();
        }
    }

    public void UpdateCheckpointBar(bool checkpointPassed)
    {
        if (checkpointPassed)
        {
            if (Instance.checkpointReached == 0)
            {
                cpFirstUI.SetActive(false);
                cpSecondUI.SetActive(false);
                cpLastUI.SetActive(false);
            }
            else if (Instance.checkpointReached == 1)
            {
                cpFirstUI.SetActive(true);
            }
            else if (Instance.checkpointReached == 2)
            {
                cpFirstUI.SetActive(true);
                cpSecondUI.SetActive(true);
            }
            else if (Instance.checkpointReached == 3)
            {
                cpFirstUI.SetActive(true);
                cpSecondUI.SetActive(true);
                cpLastUI.SetActive(true);
            }
        }
    }

    public void StartGame()
    {
        if (!gameStart)
        {
            startScreen.SetActive(false);
            youWon.SetActive(false);
            youLost.SetActive(false);
            ingameOverlay.SetActive(true);

            isMoving = true;
            gameStart = true;
        }
    }
    
    public void StopGame()
    {
        if (gameStart)
        {
            startScreen.SetActive(true);
            ingameOverlay.SetActive(false);

            isMoving = false;
            gameStart = false;
        }
    }

    public void YouWon()
    {
        youWon.SetActive(true);
    }
    
    public void YouLost()
    {
        {
            youLost.SetActive(true);
            ingameOverlay.SetActive(false);
        }
    }
    
    // Player Functions Start Here
    public void SizeUp(bool sizeUp)
    {
        if (sizeUp)
        {
            playerScript.SizeUp();
        }
    }
    
    public void PowerUp(bool powerUpActive)
    {
        if (powerUpActive)
        {
            playerScript.ActivateBlades();
        }
        else if (!powerUpActive)
        {
            playerScript.DeactivateBlades();
        }
    }
    
    public void SpeedBurst(bool isBursting)
    {
        if (isBursting)
        {
            playerScript.Move();
            Debug.Log("BURSTING!");
        }

        if (!isBursting)
        {
            playerScript.StopMoving();
            Debug.Log("STOPPED BURSTING!");
        }
    }

    public void ResetToDefaultSize()
    {
        playerScript.ResetToDefaultSize();
    }
}
