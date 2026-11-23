namespace LojaEletronicos;


public class Produto
{
    public int Codigo { get; }
    public string Nome { get; }
    public string Categoria { get; }
    public decimal Preco { get; }
    public int Estoque { get; }

    public Produto(int codigo, string nome, string categoria, decimal preco, int estoque)
    {
        Codigo = codigo;
        Nome = nome;
        Categoria = categoria;
        Preco = preco;
        Estoque = estoque;
    }

    public override string ToString() =>
        $"{Codigo,4} | {Nome,-26} | {Categoria,-12} | {Preco,12:C2} | Estoque: {Estoque,3}";
}
