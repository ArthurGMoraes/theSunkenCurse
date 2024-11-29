using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
public class Celula
{
    public int Vertice { get; private set; }
    public int Preco { get; private set; }
    public string Nome { get; private set; }
    private HashSet<int> Predecessores { get; set; }
    private HashSet<int> Sucessores { get; set; }
    public bool FoiComprado { get; private set; }

    public Celula(int vertice, int preco, string nome)
    {
        Vertice = vertice;
        Preco = preco;
        Nome = nome;
        Predecessores = new HashSet<int>();
        Sucessores = new HashSet<int>();
        FoiComprado = false;
    }

    public void AdicionarPredecessor(int vertice)
    {
        Predecessores.Add(vertice);
    }

    public void AdicionarSucessor(int vertice)
    {
        Sucessores.Add(vertice);
    }

    public int[] GetPredecessores()
    {
        int[] result = new int[Predecessores.Count];
        Predecessores.CopyTo(result);
        return result;
    }

    public int[] GetSucessores()
    {
        int[] result = new int[Sucessores.Count];
        Sucessores.CopyTo(result);
        return result;
    }

    public int Comprar()
    {
        FoiComprado = true;
        return Preco;
    }

    public void AplicarUpgrade(UpgradeManager upgradeManager)
    {   UnityEngine.Debug.Log(Nome);
        switch (Nome)
        {
             case "Jetpack Speed I":
                 upgradeManager.ApplySpeedUpgrade(1.2f); // Aumenta a velocidade do jetpack em 20%
                 break;
             case "Jetpack Speed II":
                 upgradeManager.ApplySpeedUpgrade(1.3f); // Aumenta a velocidade do jetpack em 30%
                 break;
             case "Jetpack Speed III":
                 upgradeManager.ApplySpeedUpgrade(1.4f); // Aumenta a velocidade do jetpack em 40%
                 break;
             case "Jetpack Fuel I":
                 upgradeManager.ApplyMaxFuelUpgrade(1.5f); // Aumenta o combustível máximo em 50%
                 break;
             case "Jetpack Fuel II":
                 upgradeManager.ApplyMaxFuelUpgrade(2f); // Aumenta o combustível máximo em 100%
                 break;
             case "Health I":
                 upgradeManager.ApplyHealthUpgrade(1); // Aumenta a saúde em 20
                 break;
             case "Health II":
                 upgradeManager.ApplyHealthUpgrade(5); // Aumenta a saúde em 50
                 break;
             case "Jetpack Refil I":
                 upgradeManager.ApplyRefilUpgrade(2);
                 break;
             case "Jetpack Refil II":
                 upgradeManager.ApplyRefilUpgrade(4);
                 break;
             case "Damage I":
                 UnityEngine.Debug.Log("ENTROU NO CASE");
                 upgradeManager.ApplyAttackUpgradeI();
                 break;
             case "Damage II":
                 upgradeManager.ApplyAttackUpgradeII();
                 break;        
            // Adicione mais casos conforme necessário
            default:
                break;
        }
    }

    public override string ToString()
    {
        return $"{Vertice} {Preco} {Nome}";
    }
}
