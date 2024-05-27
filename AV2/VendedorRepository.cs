using AV2.Database;
using AV2.Models;
using Microsoft.Data.Sqlite;
namespace AV2.Repositories;
class VendedorRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public VendedorRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Vendedor> GetAll()
    {
        var vendedores = new List<Vendedor>();
        
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedor";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var codvendedor = reader.GetInt32(0);
            var nome = reader.GetString(1);
            var salariofixo = reader.GetDecimal(2); 
            var faixacomissao = reader.GetString(3);                               
            var vendedor = ReaderToVendedor(reader);
            vendedores.Add(vendedor);
        }

        connection.Close();
        
        return vendedores;
    }

    public Vendedor Save(Vendedor vendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Vendedor VALUES($codvendedor, $nome, $salariofixo, $faixacomissao)";
        command.Parameters.AddWithValue("$codvendedor", vendedor.CodVendedor);
        command.Parameters.AddWithValue("$nome", vendedor.Nome);
        command.Parameters.AddWithValue("$salariofixo", vendedor.SalarioFixo);
        command.Parameters.AddWithValue("$faixacomissao", vendedor.FaixaComissao);
              
        command.ExecuteNonQuery();
        connection.Close();

        return vendedor;
    }
    public Vendedor GetByCodVendedor(int codvendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedor WHERE (codvendedor = $codvendedor)";
        command.Parameters.AddWithValue("$codvendedor", codvendedor);

        var reader = command.ExecuteReader();
        reader.Read();

        var vendedor = ReaderToVendedor(reader);

        connection.Close(); 

        return vendedor;
    }
    
        
    public bool ExistByCodVendedor(int codvendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(codvendedor) FROM Vendedor WHERE (codvendedor = $codvendedor)";
        command.Parameters.AddWithValue("$codvendedor", codvendedor);

        var reader = command.ExecuteReader();
        reader.Read();
        var resultVendedor = reader.GetBoolean(0);

        return resultVendedor;
    }
    private Vendedor ReaderToVendedor(SqliteDataReader reader)
    {
        var vendedor = new Vendedor(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3));

        return vendedor;
    }
}




