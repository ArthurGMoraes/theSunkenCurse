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

    // Exemplo de método para aplicar um upgrade
    public void ApplySpeedUpgrade(float speedIncrease)
    {
        playerMovement.moveSpeed = playerMovement.moveSpeed * speedIncrease;
        Debug.Log("Nova velocidade de movimento: " + playerMovement.moveSpeed);
    }

    public void ApplyJetpackSpeedUpgrade(float speedIncrease)
    {
        playerMovement.moveSpeed = playerMovement.moveSpeed * speedIncrease;
        Debug.Log("Nova velocidade de movimento: " + playerMovement.moveSpeed);
    }

    public void ApplyMaxFuelUpgrade(float fuelIncrease)
    {
        playerMovement.maxFuel = playerMovement.maxFuel * fuelIncrease;
        Debug.Log("Nova velocidade de movimento: " + playerMovement.maxFuel);
    }

    public void ApplyHealthUpgrade(int healthIncrease)
    {
        playerHealth.ModifyHealth(healthIncrease);
        Debug.Log("Nova saúde do jogador: " + playerHealth.health);
    }
}
