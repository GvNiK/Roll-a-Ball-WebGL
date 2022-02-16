using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfGame : MonoBehaviour
{
    public GameObject endGame;
    public PlayerController playerController;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        endGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.won)
        {
            Debug.Log(playerController.won);
            endGame.SetActive(true);

            quitButton.onClick.AddListener( () => Application.Quit() );
        }
    }
}
