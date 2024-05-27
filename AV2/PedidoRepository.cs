using AV2.Database;
using AV2.Models;
using Microsoft.Data.Sqlite;
namespace AV2.Repositories;

class PedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public PedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Pedido> GetAll()
    {
        var pedidos = new List<Pedido>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedidos";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var codpedido = reader.GetInt32(0);
            var prazoentrega = reader.GetString(1);
            var datapedido = reader.GetDateTime(2);
            var pedidocodcliente = reader.GetInt32(3);
            var pedidocodvendedor = reader.GetInt32(4);                                    
            var pedido = ReaderToPedido(reader);
            pedidos.Add(pedido);
        }

        connection.Close();
        
        return pedidos;
    }

    public Pedido Save(Pedido pedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Pedidos VALUES($codpedido, $prazoentrega, $datapedido, $pedidocodcliente, $pedidocodvendedor)";
        command.Parameters.AddWithValue("$codpedido", pedido.CodPedido);
        command.Parameters.AddWithValue("$prazoentrega", pedido.PrazoEntrega);
        command.Parameters.AddWithValue("$datapedido", pedido.DataPedido);
        command.Parameters.AddWithValue("$pedidocodcliente", pedido.PedidoCodCliente);
        command.Parameters.AddWithValue("$pedidocodvendedor", pedido.PedidoCodVendedor);                
    
        command.ExecuteNonQuery();
        connection.Close();

        return pedido;
    }
    public Pedido GetByCodPedido(int codpedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedidos WHERE (codpedido = $codpedido)";
        command.Parameters.AddWithValue("$codpedido", codpedido);

        var reader = command.ExecuteReader();
        reader.Read();

        var pedido = ReaderToPedido(reader);

        connection.Close(); 

        return pedido;
    }
    
        
    public bool ExistByCodPedido(int codpedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codpedido) FROM Pedidos WHERE (codpedido = $codpedido)";
        command.Parameters.AddWithValue("$codpedido", codpedido);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultPedido = reader.GetBoolean(0);

        return resultPedido;
    }
    private Pedido ReaderToPedido(SqliteDataReader reader)
    {
        var pedido = new Pedido(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetInt32(3), reader.GetInt32(4));

        return pedido;
    }
}