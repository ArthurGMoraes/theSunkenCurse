using System.Collections.Generic;
using UnityEngine;

public class ListaDeAdjacencia
{
    private Celula[] celulas;
    private int n;

    public ListaDeAdjacencia(int tamanho)
    {
        celulas = new Celula[tamanho];
        n = 0;
    }

    public void AdicionarCelula(Celula celula)
    {
        celulas[celula.Vertice] = celula;
        n++;
    }

    public Celula GetCelula(int vertice)
    {
        return celulas[vertice];
    }

    public int Comprar(int vertice)
    {
        return celulas[vertice].Comprar();
    }

    public bool IsCompravel(int vertice)
    {
        if (celulas[vertice].FoiComprado)
            return false;

        foreach (int c in celulas[vertice].GetPredecessores())
        {
            if (!celulas[c].FoiComprado)
                return false;
        }

        return true;
    }

    public List<int> GetAllCompraveis()
    {
        List<int> compraveis = new List<int>();
        bool[] visitados = new bool[n];

        for (int i = 0; i < n; i++)
        {
            if (celulas[i].GetPredecessores().Length == 0 && !visitados[i])
            {
                DfsCompraveis(i, visitados, compraveis);
            }
        }

        return compraveis;
    }

    private void DfsCompraveis(int vertice, bool[] visitados, List<int> compraveis)
    {
        visitados[vertice] = true;

        if (IsCompravel(vertice))
        {
            compraveis.Add(vertice);
        }

        foreach (int c in celulas[vertice].GetSucessores())
        {
            if (!visitados[c])
            {
                DfsCompraveis(c, visitados, compraveis);
            }
        }
    }

    public int[] GetStore()
    {
        List<int> compraveis = GetAllCompraveis();
        int[] store = new int[3];
        Shuffle(compraveis);

        for (int i = 0; i < 3; i++)
        {
            store[i] = (i < compraveis.Count) ? compraveis[i] : -1;
        }

        return store;
    }

    private void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void AdicionarUpgrade(string nome, int preco)
    {
        Celula celula = new Celula(n, preco, nome);
        AdicionarCelula(celula);
    }

    public void AddAresta(int predecessor, int sucessor)
    {
        celulas[predecessor].AdicionarSucessor(sucessor);
        celulas[sucessor].AdicionarPredecessor(predecessor);
    }

    public void Print()
    {
        for (int i = 0; i < n; i++)
        {
            Debug.Log(celulas[i].ToString());
        }
    }
}
