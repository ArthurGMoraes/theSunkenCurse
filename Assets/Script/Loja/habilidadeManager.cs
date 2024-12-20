using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HabilidadeManager : MonoBehaviour
{
    public GameObject botaoPrefab; // Referência ao prefab do botão
    public Transform painelHabilidades; // Referência ao painel onde os botões serão exibidos
    private ListaDeAdjacencia lista;

    public UpgradeManager upgradeManager;

    void Start()
    {
        lista = new ListaDeAdjacencia(100);
        InicializarHabilidades();

        // Carregar estado dos upgrades
        CarregarEstadoUpgrades();

        ExibirHabilidades();
    }

    // Novo método para carregar o estado dos upgrades
    private void CarregarEstadoUpgrades()
    {
        // Iterar por todas as habilidades e verificar se já foram compradas
        for (int i = 0; i < 12; i++) // Supondo que você tenha 12 habilidades
        {
            string upgradeName = PlayerPrefs.GetString($"Upgrade_{i}", "");
            if (!string.IsNullOrEmpty(upgradeName))
            {
                // Marcar a habilidade como comprada
                lista.Comprar(i);

                // Reaplicar o upgrade
                Celula habilidade = lista.GetCelula(i);
                habilidade.AplicarUpgrade(upgradeManager);

                Debug.Log($"Carregando upgrade comprado: {upgradeName}");
            }
        }
    }

    private void InicializarHabilidades()
    {
        // Adicionando habilidades
        lista.AdicionarUpgrade("Iniciar loja", 0);
        lista.AdicionarUpgrade("Jetpack Speed I", 1);
        lista.AdicionarUpgrade("Jetpack Speed II", 2); // Depende de Jetpack Speed I e Jetpack Fuel I
        lista.AdicionarUpgrade("Jetpack Speed III", 3); // Depende de Jetpack Speed II
        lista.AdicionarUpgrade("Jetpack Fuel I", 1);
        lista.AdicionarUpgrade("Jetpack Fuel II", 3); // Depende de Jetpack Fuel I
        lista.AdicionarUpgrade("Health I", 2);
        lista.AdicionarUpgrade("Health II", 4); // Depende de Health I e Jetpack Speed II
        lista.AdicionarUpgrade("Damage I", 2);
        lista.AdicionarUpgrade("Damage II", 3); // Depende de Damage I e Health II
        lista.AdicionarUpgrade("Jetpack Refil I", 1);
        lista.AdicionarUpgrade("Jetpack Refil II", 3);
        // Definindo as dependências
        lista.AddAresta(0, 1); // Upgrade 1 -> Jetpack Speed I
        lista.AddAresta(1, 2); // Jetpack Speed I -> Jetpack Speed II
        lista.AddAresta(4, 2); // Jetpack Fuel 1 -> Jetpack Speed II
        lista.AddAresta(2, 3); // Jetpack Speed II -> Jetpack Speed III
        lista.AddAresta(5, 3); // Jetpack Fuel II -> Jetpack Speed III
        lista.AddAresta(0, 4); // Upgrade 1 -> Jetpack Fuel I
        lista.AddAresta(4, 5); // Jetpack Fuel I -> Jetpack Fuel II
        lista.AddAresta(0, 6); // Upgrade 1 -> Health I
        lista.AddAresta(6, 7); // Health I -> Health II
        lista.AddAresta(1, 2); // Jetpack Speed I -> Jetpack Speed II (2 dependências)
        lista.AddAresta(2, 7); // Jetpack Speed II -> Health II (2 dependências)
        lista.AddAresta(0, 8); // Upgrade 1 -> Damage I
        lista.AddAresta(8, 9); // Damage I -> Damage II
        lista.AddAresta(1, 10); // Jetpack Speed I -> Jetpack Refil I
        lista.AddAresta(4, 10); // Jetpack Fuel I -> Jetpack Refil I
        lista.AddAresta(2, 11);// Jetpack Speed II -> Jetpack Refil II
        lista.AddAresta(5, 11); // Jetpack Fuel II -> Jetpack Refil II
    }

    private void ComprarHabilidade(int indice)
    {
        int preco = lista.GetCelula(indice).Preco;
        int balanco = PlayerPrefs.GetInt("Coin");

        if (lista.IsCompravel(indice) && preco <= balanco)
        {
            lista.Comprar(indice);
            Debug.Log($"Comprou a habilidade: {lista.GetCelula(indice).Nome} por {preco} moedas.");

            // Aplicar o upgrade ao jogador
            Celula habilidade = lista.GetCelula(indice);
            habilidade.AplicarUpgrade(upgradeManager);

            // Salvar o nome do upgrade com seu índice
            PlayerPrefs.SetString($"Upgrade_{indice}", habilidade.Nome);
            PlayerPrefs.Save(); // Garantir que seja salvo imediatamente

            // Notificar o UpgradeManager
            upgradeManager.OnUpgradePurchased(habilidade.Nome);

            PlayerPrefs.SetInt("Coin", balanco - preco);
            ExibirHabilidades(); // Atualiza a lista de habilidades disponíveis
        }
        else
        {
            Debug.Log($"A habilidade {lista.GetCelula(indice).Nome} não pode ser comprada ainda, faltam {preco - balanco} moedas");
        }
    }
    private void ExibirHabilidades()
    {
        foreach (Transform child in painelHabilidades)
        {
            Destroy(child.gameObject);
        }

        // Obtém as habilidades compráveis
        List<int> compraveis = lista.GetAllCompraveis();

        foreach (int indice in compraveis)
        {
            Celula habilidade = lista.GetCelula(indice);

            // Cria um novo botão a partir do prefab
            GameObject novoBotao = Instantiate(botaoPrefab, painelHabilidades);
            Button buttonComponent = novoBotao.GetComponent<Button>();
            TMP_Text buttonText = novoBotao.GetComponentInChildren<TMP_Text>();

            // Define o texto do botão como o nome da habilidade
            buttonText.text = habilidade.Nome;

            // Adiciona um listener para o botão
            buttonComponent.onClick.AddListener(() => ComprarHabilidade(indice));
        }

        // Ajusta a disposição dos botões
        AjustarDisposicaoBotoes();
    }

    private void AjustarDisposicaoBotoes()
    {
        HorizontalLayoutGroup layoutGroup = painelHabilidades.GetComponent<HorizontalLayoutGroup>();

        if (layoutGroup != null)
        {
            // Configura o layout para centralizar os botões
            layoutGroup.childControlWidth = true;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;

            layoutGroup.spacing = 10;
        }
    }

}