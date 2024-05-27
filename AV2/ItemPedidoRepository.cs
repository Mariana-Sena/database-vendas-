using AV2.Database;
using AV2.Models;
using Microsoft.Data.Sqlite;
namespace AV2.Repositories;

class ItemPedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ItemPedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<ItemPedido> GetAll()
    {
        var itenspedido = new List<ItemPedido>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var coditempedido = reader.GetInt32(0);
            var itempedidocodpedido = reader.GetInt32(1);
            var itempedidocodproduto = reader.GetInt32(2);
            var quantidade = reader.GetInt32(2);

            var itempedido = ReaderToItemPedido(reader);

          
            itenspedido.Add(itempedido);
        }

        connection.Close();
        
        return itenspedido;
    }
    public ItemPedido Save(ItemPedido itempedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO ItensPedido VALUES($coditempedido, $itempedidocodpedido, $itempedidocodproduto, $quantidade)";
        command.Parameters.AddWithValue("$coditempedido", itempedido.CodItemPedido);
        command.Parameters.AddWithValue("$itempedidocodpedido", itempedido.ItemPedidoCodPedido);
        command.Parameters.AddWithValue("$itempedidocodproduto", itempedido.ItemPedidoCodProduto);
        command.Parameters.AddWithValue("$quantidade", itempedido.Quantidade);

  
        command.ExecuteNonQuery();
        connection.Close();

        return itempedido;
    }
    public ItemPedido GetByCodItemPedido(int coditempedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedido WHERE (coditempedido = $coditempedido)";
        command.Parameters.AddWithValue("$coditempedido", coditempedido);

        var reader = command.ExecuteReader();
        reader.Read();

        var itempedido = ReaderToItemPedido(reader);

        connection.Close(); 

        return itempedido;
    }
    
    public bool ExistByCodItemPedido(int coditempedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(coditempedido) FROM ItensPedido WHERE (coditempedido = $coditempedido)";
        command.Parameters.AddWithValue("$coditempedido", coditempedido);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultItemPedido = reader.GetBoolean(0);

        return resultItemPedido;
    }
    
    
    private ItemPedido ReaderToItemPedido(SqliteDataReader reader)
    {
        var itempedido = new ItemPedido(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));

        return itempedido;
    }
}
