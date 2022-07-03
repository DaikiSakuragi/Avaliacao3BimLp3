using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)    
    {
        _databaseConfig = databaseConfig;
    }

    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)", product);

        return product;
    }

    public IEnumerable<Product> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE Active = true");
        
        return products;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id = @Id", new { Id = id });
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Products WHERE id = @Id", new{ Id = id });

        return products;
    }

    public void Enable(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET Active = @Active Where Id = @Id", new { @Id = id, @Active = true });
    }

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET Active = @Active Where Id = @Id", new { @Id = id, @Active = false });

    }

    public IEnumerable<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE Price BETWEEN @InitialPrice AND @EndPrice", new{@InitialPrice = initialPrice, @EndPrice = endPrice});
        
        return products;
    }

    public IEnumerable<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price > @Price", new{@Price = price});
        
        return products;
    }

    public IEnumerable<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price < @Price", new{@Price = price});
        
        return products;
    }

    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var media = connection.ExecuteScalar<Double>("SELECT AVG(price) FROM Products;");

        return media;
    }
}