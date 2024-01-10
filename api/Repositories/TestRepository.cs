using MySqlConnector;
using Pebbles.Models;

namespace Pebbles.Repositories;

public interface ITestRepository
{
    Task<List<Test>> GetTestsAsync();
    Task<Test> GetTestAsync(int id);
    Task<Test> AddTestAsync(Test test);
    Task<Test> UpdateTestAsync(Test test);
    Task DeleteTestAsync(int id);
}

public class TestRepository : ITestRepository
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public TestRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("PebblesDB");
        Console.WriteLine("Connection string: " + connectionString);
    }

    public async Task<List<Test>> GetTestsAsync()
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM test";
                await connection.OpenAsync();
                using var command = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = command.ExecuteReader();
                var tests = new List<Test>();
                while (reader.Read())
                {
                    var id = int.Parse(reader["id"].ToString());
                    var data = reader["data"].ToString();
                    tests.Add(new Test(id, data));
                }
                await connection.CloseAsync();

                return tests;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Test> GetTestAsync(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM test WHERE id = @id";
                await connection.OpenAsync();
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using MySqlDataReader reader = command.ExecuteReader();

                reader.Read();
                var test = new Test(int.Parse(reader["id"].ToString()), reader["data"].ToString());
                await connection.CloseAsync();

                return test;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Test> AddTestAsync(Test test)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO test (data) VALUES (@data)";
                await connection.OpenAsync();
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@data", test.Data);
                command.ExecuteNonQuery();
                await connection.CloseAsync();
                return test;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Test> UpdateTestAsync(Test test)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE test SET data = @data WHERE id = @id";
                await connection.OpenAsync();
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", test.Id);
                command.Parameters.AddWithValue("@data", test.Data);
                command.ExecuteNonQuery();
                await connection.CloseAsync();
                return test;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task DeleteTestAsync(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM test WHERE id = @id";
                await connection.OpenAsync();
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                await connection.CloseAsync();
                return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }
    }
}