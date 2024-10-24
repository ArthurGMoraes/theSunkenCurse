using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] GameObject deathMenu;

    public void Pause()
    {
        Time.timeScale = 0;
        deathMenu.SetActive(true);
    }

    public void Home(){
        SceneManager.LoadSceneAsync(0);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
