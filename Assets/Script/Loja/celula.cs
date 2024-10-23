using System.Collections.Generic;

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

    public override string ToString()
    {
        return $"{Vertice} {Preco} {Nome}";
    }
}
