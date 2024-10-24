using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void gameOver(){
        gameOverUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
