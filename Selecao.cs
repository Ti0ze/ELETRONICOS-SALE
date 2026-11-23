namespace LojaEletronicos;



public static class Selecao
{
    
    public static List<Produto> PorCategoria(List<Produto> lista, string categoria)
    {
        var resultado = new List<Produto>();

        for (int i = 0; i < lista.Count; i++)
        {
            if (string.Equals(lista[i].Categoria, categoria, StringComparison.OrdinalIgnoreCase))
                resultado.Add(lista[i]);
        }
        return resultado;
    }

   
    public static List<Produto> NMaisBaratos(List<Produto> lista, int n)
    {
        var copia = new List<Produto>(lista);
        n = Math.Min(n, copia.Count);

        for (int i = 0; i < n; i++)
        {
            int menor = i;
            for (int j = i + 1; j < copia.Count; j++)
            {
                if (copia[j].Preco < copia[menor].Preco)
                    menor = j;
            }
            (copia[i], copia[menor]) = (copia[menor], copia[i]);
        }
        return copia.GetRange(0, n);
    }
}
