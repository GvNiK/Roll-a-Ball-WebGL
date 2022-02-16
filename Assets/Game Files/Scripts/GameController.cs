using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public float setTimer;
    public float endTimer;
    public TextMeshProUGUI timer;
    public Button restartBtn;
    public Button nextLevelBtn;
    public GameObject looseText;
    public Animator animator;
    public PlayerController playerController;
    public bool isNextLevel;
    private int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        looseText.SetActive(false);
        restartBtn.gameObject.SetActive(false);
        nextLevelBtn.gameObject.SetActive(false);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene Index: " + sceneIndex);

    }

    // Update is called once per frame
    void Update()
    {
        if(setTimer > 0)
        {
            setTimer -= Time.deltaTime;
            timer.text = string.Format("{0:0.0}", setTimer);

            playerController.Won += RoundClear;
            ChangerTimerColor();       
            //isNextLevel = playerController.won;
        }

        else
        {
            GameLost();
        }

    }

    private void RoundClear()
    {
        if(playerController.won && isNextLevel)
        {
            nextLevelBtn.gameObject.SetActive(true);

            nextLevelBtn.onClick.AddListener( () => 
            {   
                SceneManager.LoadScene(sceneIndex + 1);
            });

        }
        else
        {
            nextLevelBtn.gameObject.SetActive(false);
        }
    }

    private void ChangerTimerColor()
    {
        if(setTimer <= endTimer)
        {
            timer.color = Color.red;
            animator.SetBool("Ending", true);
        }

    }

    private void GameLost()
    {
        looseText.SetActive(true);
        setTimer = 0;
        Time.timeScale = 0;

        restartBtn.gameObject.SetActive(true);
        restartBtn.onClick.AddListener( () => RestartLevel());
    }

    private void RestartLevel()
    {
        playerController.Won -= RoundClear;    
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnDestroy() 
    {
        playerController.Won -= RoundClear;    
    }
}
