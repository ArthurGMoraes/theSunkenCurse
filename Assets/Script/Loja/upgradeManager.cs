using UnityEngine;
using System;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get;private set; }
    public GameObject player; // Arraste o objeto do jogador no Inspector

    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    [SerializeField] GameObject Panel;
    private bool loja = true;
    private PauseMenu pauseMenu;

    public static event Action onUpgrade;


    private void Start()
    {
        if (player != null)
        {
        // Obtendo as referências aos componentes

        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        }
    }


    public void ApplyMaxFuelUpgrade(float fuelIncrease)
    {
        if (player != null)
        {
        playerMovement.maxFuel *= fuelIncrease;
        playerMovement.fuel =  playerMovement.maxFuel;  
        Debug.Log("Novo combustível máximo: " + playerMovement.maxFuel);
        }
    }
    public void ApplyHealthUpgrade(int healthIncrease)
    {
        if (player != null)
        {
        playerHealth.maxHealth += healthIncrease;
        playerHealth.health = playerHealth.maxHealth;
        onUpgrade?.Invoke();
        Debug.Log("Novo combustível máximo: " + playerMovement.maxFuel);
        }
    }
    public void ApplyRefilUpgrade(int refilIncrease)
    {
        if (player != null)
        {
        playerMovement.addFuel += refilIncrease;
        Debug.Log("Novo combustível máximo: " + playerMovement.maxFuel);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown("e"))
        {
            Panel.SetActive(loja);

            if (loja == false)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

            loja = loja ^ true;
        }
    }



}
