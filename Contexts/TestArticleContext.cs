using BlogRest.Models;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace BlogRest.Contexts;

public class TestArticleContext : IArticleContext
{
    private string connectionString { get; set; }
    
    private const string databaseName = "blog";
    private const string tableName = "articles";

    public TestArticleContext(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("DefaultConnection");
        InitializeDatabase();
    }

    private MySqlConnection GetConnection()
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        return connection;
    }

    private void InitializeDatabase()
    {
        if (DoesTableExist() == false)
        {
            CreateTable();
        }
    }

    private bool DoesTableExist()
    {
        using (MySqlConnection connection = GetConnection())
        {
            MySqlCommand command = new();
            command.Parameters.AddWithValue("@db", databaseName);
            command.Parameters.AddWithValue("@table", tableName);
            string query = "SELECT COUNT(*) FROM information_schema.TABLES WHERE (TABLE_SCHEMA=@db) AND (TABLE_NAME=@table)";
            command.CommandText = query;
            command.Connection = connection;
            connection.Open();
            int n = int.Parse(command.ExecuteScalar().ToString() ?? "0");
            return n > 0;
        }
    }

    private void CreateTable()
    {
        using (MySqlConnection connection = GetConnection())
        {
            MySqlCommand command = new();
            string query = "CREATE TABLE " + tableName + " (id CHAR(36) PRIMARY KEY NOT NULL, post_date TIMESTAMP NOT NULL, title VARCHAR(255) NOT NULL, subtitle VARCHAR(255) NOT NULL, body TEXT NOT NULL)";
            command.CommandText = query;
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public IEnumerable<Article> FindAll()
    {
        List<Article> articles = new();

        using (MySqlConnection connection = GetConnection())
        {
            try
            {
                connection.Open();
                MySqlCommand command = new();
                command.CommandText = "SELECT * FROM " + tableName;
                command.Connection = connection;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        articles.Add(new Article()
                        {
                            Id = Guid.Parse(reader["id"].ToString() ?? Guid.Empty.ToString()),
                            PostDate = DateTimeOffset.Parse(reader["post_date"].ToString() ?? "0"),
                            Title = reader["title"].ToString() ?? "Title Missing!",
                            SubTitle = reader["subtitle"].ToString() ?? "SubTitle Missing!",
                            Body = reader["body"].ToString() ?? "Body Missing"
                        });
                    }
                }
                connection.Close();
            }
            catch
            {
                Article article = new()
                {
                    Id = Guid.Empty,
                    PostDate = DateTimeOffset.Now,
                    Title = "Error",
                    SubTitle = "One or more errors",
                    Body = connectionString
                };
                articles.Add(article);
                return articles;
            }
            
        }
        return articles;
    }

    public void Add(Article article)
    {
        using (MySqlConnection connection = GetConnection())
        {
            connection.Open();

            MySqlCommand command = new();

            command.Parameters.AddWithValue("@id", article.Id);
            command.Parameters.AddWithValue("@post_date", article.PostDate.ToUnixTimeSeconds());
            command.Parameters.AddWithValue("@title", article.Title);
            command.Parameters.AddWithValue("@subtitle", article.SubTitle);
            command.Parameters.AddWithValue("@body", article.Body);
            
            command.CommandText = "INSERT INTO " + tableName + " (id, post_date, title, subtitle, body) VALUES (@id, FROM_UNIXTIME(@post_date), @title, @subtitle, @body)";

            command.Connection = connection;

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}