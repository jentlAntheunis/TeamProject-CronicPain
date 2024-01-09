using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using Pebbles.Models;
using System.Runtime.CompilerServices;

[ApiController]
[Route("tests")]
public class TestController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public TestController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Get()
    {
        string connectionString = _configuration.GetConnectionString("PebblesDB");
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM test";
            connection.Open();
            using var command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            var tests = new List<Test>();
            while (reader.Read())
            {
                var test = new Test(reader.GetInt32("id"), reader.GetString("data"));
                tests.Add(test);
            }
            connection.Close();

            return Ok(tests);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        string connectionString = _configuration.GetConnectionString("PebblesDB");
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM test WHERE id = @id";
            connection.Open();
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var test = new Test(reader.GetInt32("id"), reader.GetString("data"));
                connection.Close();
                return Ok(test);
            }
            return NotFound("not found");
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] Test test)
    {
        string connectionString = _configuration.GetConnectionString("PebblesDB");
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "INSERT INTO test (data) VALUES (@data)";
            connection.Open();
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@data", test.Data);
            var result = command.ExecuteScalar();
            connection.Close();
            Console.WriteLine(result);
            test.Id = Convert.ToInt32(result);
            var returnobj = JsonConvert.SerializeObject(test);
            return Created(Convert.ToString(result), test);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Test test)
    {
        string connectionString = _configuration.GetConnectionString("PebblesDB");
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "UPDATE test SET data = @data WHERE id = @id";
            connection.Open();
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@data", test.Data);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result == 0)
            {
                return NotFound("not found");
            }
            return Ok(test);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        string connectionString = _configuration.GetConnectionString("PebblesDB");
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM test WHERE id = @id";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result == 0)
            {
                return NotFound("not found");
            }
            return Ok("deleted");
        }
    }
}