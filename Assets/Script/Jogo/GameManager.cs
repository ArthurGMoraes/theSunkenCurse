using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject gamePauseUI;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Coin",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver(){
        gameOverUI.SetActive(true);
        gamePauseUI.SetActive(false);
    }

    public void gameWin()
    {
        gameWinUI.SetActive(true);
        gamePauseUI.SetActive(false);
    }
}
