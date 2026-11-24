using System.Globalization;
using System.Text;
using LojaEletronicos;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

// Estrutura de dados LINEAR: lista sequencial de produtos
var estoque = new List<Produto>
{
    new(105, "Notebook Gamer X15",     "Notebook",   6499.90m,  5),
    new(101, "Smartphone Galaxy S",    "Celular",    3299.00m, 12),
    new(112, "Fone Bluetooth Pro",     "Acessorio",   349.90m, 30),
    new(103, "Smart TV 55 4K",         "TV",         2799.00m,  8),
    new(109, "Mouse Sem Fio",          "Acessorio",    89.90m, 50),
    new(107, "Tablet Ultra 10",        "Tablet",     1899.00m, 10),
    new(102, "Smartphone Moto G",      "Celular",    1599.00m, 20),
    new(110, "Teclado Mecanico RGB",   "Acessorio",   299.90m, 25),
    new(104, "Notebook Office 14",     "Notebook",   3199.00m,  7),
    new(108, "Smart TV 43 Full HD",    "TV",         1699.00m, 14),
    new(111, "Carregador Turbo 65W",   "Acessorio",   129.90m, 40),
    new(106, "Smartwatch Fit",         "Acessorio",   599.00m, 18),
};

int opcao;
do
{
    Console.Clear();
    Console.WriteLine("===== LOJA DE ELETRÔNICOS =====");
    Console.WriteLine("1 - Listar produtos");
    Console.WriteLine("2 - Cadastrar produto");
    Console.WriteLine("3 - Ordenar (Insertion Sort / Quick Sort)");
    Console.WriteLine("4 - Buscar (Linear / Binária)");
    Console.WriteLine("5 - Selecionar (Categoria / N mais baratos)");
    Console.WriteLine("0 - Sair");
    opcao = LerInt("Escolha: ");

    Console.Clear(); // limpa o terminal ao selecionar uma opção

    switch (opcao)
    {
        case 1: Listar(estoque, "ESTOQUE"); break;
        case 2: Cadastrar(); break;
        case 3: MenuOrdenar(); break;
        case 4: MenuBuscar(); break;
        case 5: MenuSelecionar(); break;
        case 0: Console.WriteLine("Encerrando..."); break;
        default: Console.WriteLine("Opção inválida."); break;
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }
} while (opcao != 0);

// ---------------------------------------------------------------
void MenuOrdenar()
{
    Console.WriteLine("\nAlgoritmo: 1 - Insertion Sort | 2 - Quick Sort");
    int alg = LerInt("Escolha: ");
    Console.WriteLine("Critério: 1 - Preço | 2 - Nome | 3 - Código");
    int crit = LerInt("Escolha: ");

    Console.Clear();

    Comparison<Produto> cmp = crit switch
    {
        1 => Ordenacao.PorPreco,
        2 => Ordenacao.PorNome,
        3 => Ordenacao.PorCodigo,
        _ => Ordenacao.PorPreco
    };

    long comparacoes = alg == 2
        ? Ordenacao.QuickSort(estoque, cmp)
        : Ordenacao.InsertionSort(estoque, cmp);

    Listar(estoque, "ESTOQUE ORDENADO");
    Console.WriteLine($"Comparações realizadas: {comparacoes}");
}

void MenuBuscar()
{
    Console.WriteLine("\nBusca: 1 - Linear por nome | 2 - Binária por código");
    int tipo = LerInt("Escolha: ");
    Console.Clear();

    if (tipo == 2)
    {
        // A busca binária exige lista ordenada por código: ordenamos uma CÓPIA
        var copia = new List<Produto>(estoque);
        Ordenacao.QuickSort(copia, Ordenacao.PorCodigo);

        int codigo = LerInt("Código do produto: ");
        int idx = Busca.BinariaPorCodigo(copia, codigo, out int comp);

        Console.WriteLine(idx >= 0
            ? $"\nEncontrado (posição {idx}):\n{copia[idx]}"
            : "\nProduto não encontrado.");
        Console.WriteLine($"Comparações realizadas: {comp}");
    }
    else
    {
        Console.Write("Nome (ou parte do nome): ");
        string termo = Console.ReadLine() ?? "";
        var achados = Busca.LinearPorNome(estoque, termo, out int comp);

        Listar(achados, "RESULTADO DA BUSCA");
        Console.WriteLine($"Comparações realizadas: {comp}");
    }
}

void MenuSelecionar()
{
    Console.WriteLine("\nSeleção: 1 - Por categoria | 2 - N mais baratos");
    int tipo = LerInt("Escolha: ");

    if (tipo == 2)
    {
        int n = LerInt("Quantos produtos? ");
        Listar(Selecao.NMaisBaratos(estoque, n), $"{n} MAIS BARATOS");
    }
    else
    {
        Console.Write("Categoria (Notebook, Celular, TV, Tablet, Acessorio): ");
        string cat = Console.ReadLine() ?? "";
        Listar(Selecao.PorCategoria(estoque, cat), $"CATEGORIA: {cat}");
    }
}

void Cadastrar()
{
    int codigo = LerInt("Código: ");
    if (estoque.Any(p => p.Codigo == codigo))
    {
        Console.WriteLine("Já existe um produto com esse código.");
        return;
    }

    Console.Write("Nome: ");
    string nome = Console.ReadLine() ?? "";
    Console.Write("Categoria: ");
    string categoria = Console.ReadLine() ?? "";
    decimal preco = LerDecimal("Preço: ");
    int qtd = LerInt("Estoque: ");

    estoque.Add(new Produto(codigo, nome, categoria, preco, qtd));
    Console.WriteLine("Produto cadastrado!");
}

void Listar(List<Produto> lista, string titulo)
{
    Console.WriteLine($"\n--- {titulo} ({lista.Count}) ---");
    if (lista.Count == 0) Console.WriteLine("Nenhum produto.");
    foreach (var p in lista) Console.WriteLine(p);
}

int LerInt(string msg)
{
    while (true)
    {
        Console.Write(msg);
        if (int.TryParse(Console.ReadLine(), out int v)) return v;
        Console.WriteLine("Valor inválido. Digite um número inteiro.");
    }
}

decimal LerDecimal(string msg)
{
    while (true)
    {
        Console.Write(msg);
        if (decimal.TryParse(Console.ReadLine(), out decimal v)) return v;
        Console.WriteLine("Valor inválido. Digite um número (ex.: 199,90).");
    }
}
