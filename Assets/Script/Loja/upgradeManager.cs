using UnityEngine;
using System;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    public GameObject player; // Arraste o objeto do jogador no Inspector

    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public Projectile projectile;

    [SerializeField] GameObject Panel;
    private bool loja = true;
    private PauseMenu pauseMenu;

    public static event Action onUpgrade;

    // Nova lista para rastrear upgrades comprados
    private List<string> purchasedUpgrades = new List<string>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
             Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (player != null)
        {
            // Obtendo as referências aos componentes
            playerMovement = player.GetComponent<PlayerMovement>();
            playerHealth = player.GetComponent<PlayerHealth>();
            // playerAttack = player.GetComponent<PlayerAttack>();
            // projectile = FindObjectOfType<Projectile>();
        }

        // Carregar upgrades salvos
        LoadUpgrades();
    }

    // Métodos de aplicação de upgrades (mantidos como estavam)
    public void ApplyMaxFuelUpgrade(float fuelIncrease)
    {
        if (player != null)
        {
            playerMovement.maxFuel *= fuelIncrease;
            playerMovement.fuel = playerMovement.maxFuel;  
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
            Debug.Log("Nova vida máxima: " + playerHealth.maxHealth);
        }
    }

    public void ApplyRefilUpgrade(int refilIncrease)
    {
        if (player != null)
        {
            playerMovement.addFuel += refilIncrease;
            Debug.Log("Novo refil máximo: " + playerMovement.addFuel);
        }
    }

    public void ApplyAttackUpgrade(int damageIncrease)
    {
        if (player != null)
        {
            playerAttack.damage += damageIncrease + 1;
            projectile.damage += damageIncrease;
            Debug.Log("Novo dano melee: " + playerAttack.damage + " Novo dano a distância: " + projectile.damage);
        }
        else
        {
            Debug.Log("É PACABA");
        }
    }

    public void ApplySpeedUpgrade(float speedIncrease)
    {
        if (player != null)
        {
            playerMovement.upgradeValue *= speedIncrease;
            Debug.Log("Nova velocidade do jetpack: " + playerMovement.upgradeValue);
        }
    }

    // Novos métodos para gerenciar upgrades salvos
    public void SaveUpgrades()
    {
        // Converte a lista de upgrades para uma string separada por vírgulas
        string upgradesString = string.Join(",", purchasedUpgrades);
        PlayerPrefs.SetString("PurchasedUpgrades", upgradesString);
        PlayerPrefs.Save();
    }

    public void LoadUpgrades()
    {
        // Recupera a string de upgrades salvos
        string savedUpgrades = PlayerPrefs.GetString("PurchasedUpgrades", "");
        
        if (!string.IsNullOrEmpty(savedUpgrades))
        {
            // Divide a string e reconstrói a lista de upgrades
            purchasedUpgrades = new List<string>(savedUpgrades.Split(','));
            
            // Reaplicar todos os upgrades salvos
            foreach (string upgradeName in purchasedUpgrades)
            {
                ReapplyUpgrade(upgradeName);
            }
        }
    }

    // Método para reaplicar um upgrade específico
    private void ReapplyUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Jetpack Speed I":
                ApplySpeedUpgrade(1.2f);
                break;
            case "Jetpack Speed II":
                ApplySpeedUpgrade(1.3f);
                break;
            case "Jetpack Speed III":
                ApplySpeedUpgrade(1.4f);
                break;
            case "Jetpack Fuel I":
                ApplyMaxFuelUpgrade(1.5f);
                break;
            case "Jetpack Fuel II":
                ApplyMaxFuelUpgrade(2f);
                break;
            case "Health I":
                ApplyHealthUpgrade(1);
                break;
            case "Health II":
                ApplyHealthUpgrade(5);
                break;
            case "Jetpack Refil I":
                ApplyRefilUpgrade(2);
                break;
            case "Jetpack Refil II":
                ApplyRefilUpgrade(4);
                break;
            case "Damage I":
                ApplyAttackUpgrade(1);
                break;
            case "Damage II":
                ApplyAttackUpgrade(2);
                break;
        }
    }

    // Método para registrar um upgrade comprado
    public void OnUpgradePurchased(string upgradeName)
    {
        if (!purchasedUpgrades.Contains(upgradeName))
        {
            purchasedUpgrades.Add(upgradeName);
            SaveUpgrades();
        }
    }

    // Método original de Update para a loja
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