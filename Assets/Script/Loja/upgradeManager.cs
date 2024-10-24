using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player; // Arraste o objeto do jogador no Inspector

    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    [SerializeField] GameObject Panel;
    private bool loja = true;
    private PauseMenu pauseMenu;

    private void Start()
    {
        // Obtendo as referências aos componentes
        //playerMovement = player.GetComponent<PlayerMovement>();
        //playerHealth = player.GetComponent<PlayerHealth>();
    }


    public void ApplyMaxFuelUpgrade(float fuelIncrease)
    {
        playerMovement.maxFuel *= fuelIncrease; // Supondo que você tenha uma variável maxFuel
        Debug.Log("Novo combustível máximo: " + playerMovement.maxFuel);
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
