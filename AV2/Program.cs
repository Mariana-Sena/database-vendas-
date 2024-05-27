using Microsoft.Data.Sqlite;
using AV2.Database;
using AV2.Repositories;
using AV2.Models;
var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var clienteRepository = new ClienteRepository(databaseConfig);
var pedidoRepository = new PedidoRepository(databaseConfig);
var produtoRepository = new ProdutoRepository(databaseConfig);
var itempedidoRepository = new ItemPedidoRepository(databaseConfig);
var vendedorRepository = new VendedorRepository(databaseConfig);


var modelNome = args[0];
var modelAcao = args[1];
if(modelNome == "Cliente")
{
    if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Cliente");
        Console.WriteLine("Código Cliente   Nome Cliente          Endereço                    Cidade      CEP         UF   IE");
        foreach (var cliente in clienteRepository.GetAll())
        {
            //Console.WriteLine($"{cliente.ClienteId}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Regiao}, {cliente.CodigoPostal}, {cliente.Pais}, {cliente.Telefone}");
            Console.WriteLine($"{cliente.CodCliente, -16} {cliente.Nome, -21} {cliente.Endereco, -27} {cliente.Cidade, -11} {cliente.Cep, -11} {cliente.Uf, -4} {cliente.Ie}");
        }
    }

    if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Cliente");
        int     codcliente;
        string  nome;
        string  endereco;
        string  cidade;
        string  cep;
        string  uf;
        string  ie;

        Console.Write("Digite o código do cliente   : ");
        codcliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Nome do cliente     : ");
        nome = Console.ReadLine();
        Console.Write("Digite o Endereço do cliente : ");
        endereco = Console.ReadLine();
        Console.Write("Digite a Cidade do cliente   : ");
        cidade = Console.ReadLine();
        Console.Write("Digite o CEP do cliente      : ");
        cep = Console.ReadLine();
        Console.Write("Digite a UF do cliente       : ");
        uf = Console.ReadLine();
        Console.Write("Digite a Inscrição Estadual  : ");
        ie = Console.ReadLine();
        var cliente = new Cliente(codcliente, nome, endereco, cidade, cep, uf, ie);
        clienteRepository.Save(cliente);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Cliente");
	    Console.Write("Digite o código do cliente : ");
        var codcliente = Convert.ToInt32(Console.ReadLine());
       
        if(clienteRepository.ExistByCodCliente(codcliente))
        {
            var cliente = clienteRepository.GetByCodCliente(codcliente);
            Console.WriteLine($"{cliente.CodCliente}, {cliente.Nome}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Cep}, {cliente.Uf}, {cliente.Ie}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com código {codcliente} não existe.");
        }
    }
}

if(modelNome == "Pedido")
{
    if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Pedido");
        Console.WriteLine("Código Pedido   Prazo Entrega          Data Pedido           Código Cliente    Código Vendedor");
        foreach (var pedido in pedidoRepository.GetAll())
        {
            Console.WriteLine($"{pedido.CodPedido, -15} {pedido.PrazoEntrega, -22} {pedido.DataPedido, -21} {pedido.PedidoCodCliente, -17} {pedido.PedidoCodVendedor}");
        }
    }
    if (modelAcao == "MostrarPedidosCliente")
    {
        Console.WriteLine("Mostrar Pedidos do Cliente");
        Console.Write("Digite o código do cliente: ");
       
        var codCliente = Convert.ToInt32(Console.ReadLine());
         if (clienteRepository.ExistByCodCliente(codCliente))
         {
        Console.WriteLine("Código Pedido   Prazo Entrega         Data Pedido           Código Vendedor       Nome Vendedor");
        foreach (var pedido in pedidoRepository.GetAll())
        {

            if (codCliente == pedido.PedidoCodCliente)
            {
                foreach (var vendedor in vendedorRepository.GetAll())
                {
                    if (pedido.PedidoCodVendedor == vendedor.CodVendedor)
                    {
                        Console.WriteLine($"{pedido.CodPedido, -15} {pedido.PrazoEntrega, -21} {pedido.DataPedido, -21} {pedido.PedidoCodVendedor, -21} {vendedor.Nome}");
                    }
                }
            }       
            }    
        }
        else
            {
            Console.WriteLine($"Nenhum pedido encontrado para o cliente com código {codCliente}."); 
            } 
    }
if (modelAcao == "MostrarPedidosVendedor")
    {
        Console.WriteLine("Mostrar Pedidos do Vendedor");
        Console.Write("Digite o código do Vendedor: ");
       
        var codVendedor = Convert.ToInt32(Console.ReadLine());
         if (vendedorRepository.ExistByCodVendedor(codVendedor))
         {
        Console.WriteLine("Código Pedido   Data Pedido");
        foreach (var pedido in pedidoRepository.GetAll())
        {

            if (codVendedor == pedido.PedidoCodVendedor)
            {
                foreach (var vendedor in vendedorRepository.GetAll())
                {
                    if (pedido.PedidoCodVendedor == vendedor.CodVendedor)
                    {
                        Console.WriteLine($"{pedido.CodPedido, -14}  {pedido.DataPedido, -21}");
                    }
                }
            }       
            }    
        }
        else
            {
            Console.WriteLine($"Nenhum pedido encontrado para o Vendedor com código {codVendedor}."); 
            } 
    }
     if (modelAcao == "MostrarQuantidadesProdutoPedido")
    {
        Console.WriteLine("Mostrar Quantidades de Produtos Pedido");
        Console.Write("Digite o código do Produto: ");
       
        var codProduto = Convert.ToInt32(Console.ReadLine());
         if (produtoRepository.ExistByCodProduto(codProduto))
         {
        Console.WriteLine("Código Pedido   Código Produto   Descrição       Valor Unitário   Quantidade   Valor Total");
        foreach (var produto in produtoRepository.GetAll())
        {

            if (codProduto == produto.CodProduto)
            {
                foreach (var itempedido in itempedidoRepository.GetAll())
                {
                    if (produto.CodProduto == itempedido.ItemPedidoCodProduto)
                    {
                        Console.WriteLine($"{itempedido.ItemPedidoCodPedido , -15} {produto.CodProduto, -16} {produto.Descricao, -14}  {produto.ValorUnitario, -17}{itempedido.Quantidade, -12} {produto.ValorUnitario*itempedido.Quantidade}");
                    }
                }
            }       
            }    
        }
        else
            {
            Console.WriteLine($"Nenhum pedido encontrado para o cliente com código {codProduto}."); 
            } 

   if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Pedido");
        int     codpedido;
        string  prazoentrega;
        DateTime datapedido;
        int  pedidocodcliente;
        int  pedidocodvendedor;
       
        Console.Write("Digite o código do pedido: ");
        codpedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a data de entrega do pedido: ");
        prazoentrega = Console.ReadLine();
        datapedido = DateTime.Now;
        Console.Write("Digite o código do cliente do pedido: ");
        pedidocodcliente = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do vendedor do pedido: ");
        pedidocodvendedor = Convert.ToInt32(Console.ReadLine());
       
        
        var pedido = new Pedido(codpedido, prazoentrega, datapedido, pedidocodcliente, pedidocodvendedor);
        pedidoRepository.Save(pedido);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Pedido");
	    Console.Write("Digite o código do pedido : ");
        var codpedido = Convert.ToInt32(Console.ReadLine());
       
        if(pedidoRepository.ExistByCodPedido(codpedido))
        {
            var pedido = pedidoRepository.GetByCodPedido(codpedido);
            Console.WriteLine($"{pedido.CodPedido}, {pedido.PrazoEntrega}, {pedido.DataPedido}, {pedido.PedidoCodCliente}, {pedido.PedidoCodVendedor}");
        } 
        else 
        {
            Console.WriteLine($"O pedido com código {codpedido} não existe.");
        }
    }

}

if (modelNome == "ItensPedido")
    {
          if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Itens do Pedido");
        Console.WriteLine("Código Item Pedido   Quantidade   Código Pedido   Código Produto");
        foreach (var itempedido in itempedidoRepository.GetAll())
        {
            Console.WriteLine($"{itempedido.CodItemPedido, -20} {itempedido.ItemPedidoCodPedido, -12} {itempedido.ItemPedidoCodProduto, -15} {itempedido.Quantidade} ");
        }
    }
     if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Itens do Pedido");
        int  coditempedido;
        int  itempedidocodpedido;
        int  itempedidocodproduto;
        int  quantidade;



        Console.Write("Digite o código do item do pedido            : ");
        coditempedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o códido do pedido do item do pedido  : ");
        itempedidocodpedido = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o código do produto do item do pedido : ");
        itempedidocodproduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a quantidade do item do pedido        : ");
        quantidade = Convert.ToInt32(Console.ReadLine());

        var itempedido = new ItemPedido(coditempedido, itempedidocodpedido, itempedidocodproduto, quantidade);
        itempedidoRepository.Save(itempedido);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Itens do Pedido");
        Console.Write("Digite o código do pedido: ");
        var coditempedido = Convert.ToInt32(Console.ReadLine());

        if(itempedidoRepository.ExistByCodItemPedido(coditempedido))
        {
            var itempedido = itempedidoRepository.GetByCodItemPedido(coditempedido);
            Console.WriteLine($"{itempedido.CodItemPedido}, {itempedido.ItemPedidoCodPedido}, {itempedido.ItemPedidoCodProduto}, {itempedido.Quantidade}");
        } 
        else 
        {
            Console.WriteLine($"O item do pedido com código {coditempedido} não existe.");
        }
    }
    }

if (modelNome == "Produto")
    {
          if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Produto");
        Console.WriteLine("Código Produto   Descrição   Valor Unitário");
        foreach (var produto in produtoRepository.GetAll())
        {
            Console.WriteLine($"{produto.CodProduto, -16} {produto.Descricao, -11} {produto.ValorUnitario} ");
        }
    }
     if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Produto");
        int     codproduto;
        string  descricao;
        int  valorunitario;


        Console.Write("Digite o Código do produto        : ");
        codproduto = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite a Descrição do produto     : ");
        descricao = Console.ReadLine();
        Console.Write("Digite o Valor Unitário do produto: ");
        valorunitario = Convert.ToInt32(Console.ReadLine());

        var produto = new Produto(codproduto, descricao, valorunitario);
        produtoRepository.Save(produto);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Produto");
        Console.Write("Digite o código do produto : ");
        var codproduto = Convert.ToInt32(Console.ReadLine());

        if(produtoRepository.ExistByCodProduto(codproduto))
        {
            var produto = produtoRepository.GetByCodProduto(codproduto);
            Console.WriteLine($"{produto.CodProduto}, {produto.Descricao}, {produto.ValorUnitario}");
        } 
        else 
        {
            Console.WriteLine($"O produto com código {codproduto} não existe.");
        }
    }
    }

if (modelNome == "Vendedor")
    {
          if(modelAcao == "Listar")
    {
        Console.WriteLine("Listar Vendedor");
        Console.WriteLine("Código Vendedor   Nome Vendedor      Salário Fixo     Faixa Comissão");
        foreach (var vendedor in vendedorRepository.GetAll())
        {
            Console.WriteLine($"{vendedor.CodVendedor, -17} {vendedor.Nome, -18} {vendedor.SalarioFixo, -16} {vendedor.FaixaComissao} ");
        }
    }
     if(modelAcao == "Inserir")
    {
        Console.WriteLine("Inserir Vendedor");
        int     codvendedor;
        string  nome;
        decimal salariofixo;
        string  faixacomissao;
       

        Console.Write("Digite o Código do vendedor           : ");
        codvendedor = Convert.ToInt32(Console.ReadLine());
        Console.Write("Digite o Nome do vendedor             : ");
        nome = Console.ReadLine();
        Console.Write("Digite o Salário Fixo do vendedor     : ");
        salariofixo = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Digite a Faixa de Comissão do vendedor: ");
        faixacomissao = Console.ReadLine();

        var vendedor = new Vendedor(codvendedor, nome, salariofixo, faixacomissao);
        vendedorRepository.Save(vendedor);
    }

    if(modelAcao == "Apresentar")
    {
        Console.WriteLine("Apresentar Vendedor");
	    Consolce.Write("Digite o codigo do vendedor : ");
        var codvendedor = Convert.ToInt32(Console.ReadLine());
       
        if(vendedorRepository.ExistByCodVendedor(codvendedor))
        {
            var vendedor = vendedorRepository.GetByCodVendedor(codvendedor);
            Console.WriteLine($"{vendedor.CodVendedor}, {vendedor.Nome}, {vendedor.SalarioFixo}, {vendedor.FaixaComissao}");
        } 
        else 
        {
            Console.WriteLine($"O vendedor com código {codvendedor} não existe.");
        }
    }
    }
}