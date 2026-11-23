namespace LojaEletronicos;

public static class Ordenacao
{
    
    public static readonly Comparison<Produto> PorPreco =
        (a, b) => a.Preco.CompareTo(b.Preco);

    public static readonly Comparison<Produto> PorNome =
        (a, b) => string.Compare(a.Nome, b.Nome, StringComparison.OrdinalIgnoreCase);

    public static readonly Comparison<Produto> PorCodigo =
        (a, b) => a.Codigo.CompareTo(b.Codigo);

    
    public static long InsertionSort(List<Produto> lista, Comparison<Produto> cmp)
    {
        long comparacoes = 0;

        for (int i = 1; i < lista.Count; i++)
        {
            Produto chave = lista[i];
            int j = i - 1;

            while (j >= 0)
            {
                comparacoes++;
                if (cmp(lista[j], chave) <= 0) break;
                lista[j + 1] = lista[j];
                j--;
            }
            lista[j + 1] = chave;
        }
        return comparacoes;
    }

   
    public static long QuickSort(List<Produto> lista, Comparison<Produto> cmp)
    {
        long comparacoes = 0;
        QuickSort(lista, 0, lista.Count - 1, cmp, ref comparacoes);
        return comparacoes;
    }

    private static void QuickSort(List<Produto> l, int ini, int fim,
                                  Comparison<Produto> cmp, ref long comparacoes)
    {
        if (ini >= fim) return;

        int p = Particionar(l, ini, fim, cmp, ref comparacoes);
        QuickSort(l, ini, p - 1, cmp, ref comparacoes);
        QuickSort(l, p + 1, fim, cmp, ref comparacoes);
    }

    private static int Particionar(List<Produto> l, int ini, int fim,
                                   Comparison<Produto> cmp, ref long comparacoes)
    {
        Produto pivo = l[fim];
        int i = ini - 1;

        for (int j = ini; j < fim; j++)
        {
            comparacoes++;
            if (cmp(l[j], pivo) <= 0)
            {
                i++;
                (l[i], l[j]) = (l[j], l[i]);
            }
        }
        (l[i + 1], l[fim]) = (l[fim], l[i + 1]);
        return i + 1;
    }
}
