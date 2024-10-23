using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player; // Arraste o objeto do jogador no Inspector

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private void Start()
    {
        // Obtendo as referências aos componentes
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    public void ApplyMaxFuelUpgrade(float fuelIncrease)
    {
        playerMovement.maxFuel *= fuelIncrease; // Supondo que você tenha uma variável maxFuel
        Debug.Log("Novo combustível máximo: " + playerMovement.maxFuel);
    }

    

    
}
