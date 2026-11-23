namespace LojaEletronicos;


public static class Busca
{
   
    public static List<Produto> LinearPorNome(List<Produto> lista, string termo, out int comparacoes)
    {
        var resultado = new List<Produto>();
        comparacoes = 0;

        for (int i = 0; i < lista.Count; i++)
        {
            comparacoes++;
            if (lista[i].Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                resultado.Add(lista[i]);
        }
        return resultado;
    }

  
    public static int BinariaPorCodigo(List<Produto> ordenada, int codigo, out int comparacoes)
    {
        int inicio = 0, fim = ordenada.Count - 1;
        comparacoes = 0;

        while (inicio <= fim)
        {
            int meio = (inicio + fim) / 2;
            comparacoes++;

            if (ordenada[meio].Codigo == codigo) return meio;
            if (ordenada[meio].Codigo < codigo) inicio = meio + 1;
            else fim = meio - 1;
        }
        return -1;
    }
}
