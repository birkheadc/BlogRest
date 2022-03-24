using BlogRest.Dtos;
using BlogRest.Models;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace BlogRest.Contexts;

public class ArticleContext : IArticleContext
{
    private string connectionString { get; set; }
    
    private const string databaseName = "blog";
    private const string tableName = "articles";

    public ArticleContext(IConfiguration configuration)
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

            command.CommandText = "SELECT COUNT(*) FROM information_schema.TABLES WHERE (TABLE_SCHEMA=@db) AND (TABLE_NAME=@table)";

            command.Connection = connection;

            connection.Open();

            int n = GetCountFromScalarCommand(command);
            return n > 0;
        }
    }

    private void CreateTable()
    {
        using (MySqlConnection connection = GetConnection())
        {
            MySqlCommand command = new();
            
            command.CommandText = "CREATE TABLE " + tableName + " (id CHAR(36) PRIMARY KEY NOT NULL, post_date TIMESTAMP NOT NULL, title VARCHAR(255) NOT NULL, subtitle VARCHAR(255) NOT NULL, body TEXT NOT NULL, UNIQUE (title))";
            
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
            connection.Open();

            MySqlCommand command = new();

            command.CommandText = "SELECT * FROM " + tableName;

            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Article article = GetArticleFromReader(reader);
                    articles.Add(article);
                }
            }

            connection.Close();
        }

        return articles;
    }

    public bool DoesArticleExistByTitle(string title)
    {
        using (MySqlConnection connection = GetConnection())
        {
            connection.Open();

            MySqlCommand command = new();

            command.Parameters.AddWithValue("@title", title);

            command.CommandText = "SELECT COUNT(title) FROM " + tableName + " WHERE title=@title";

            int n = GetCountFromScalarCommand(command);
            connection.Close();
            return n > 0;
        }
    }

    public bool Add(Article article)
    {
        // Title is a unique field, so check if an article of the same title already exists.
        if (DoesArticleExistByTitle(article.Title) == true)
        {
            return false;
        }
        
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

        return true;
    }

    public IEnumerable<ArticleProfileDto> FindAllArticleProfilesByPostDateDesc()
    {
        List<ArticleProfileDto> profiles = new();

        using (MySqlConnection connection = GetConnection())
        {
            connection.Open();

            MySqlCommand command = new();
            command.CommandText = "SELECT title, subtitle, post_date FROM " + tableName + " ORDER BY post_date DESC";
            command.Connection = connection;
            Console.WriteLine("SELECT title, subtitle, post_date FROM " + tableName + " ORDER BY post_date DESC");
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    profiles.Add(GetArticleProfileFromReader(reader));
                }
            }

            connection.Close();
        }

        return profiles;
    }

    public Article FindByTitle(string title)
    {
        Article article;

        using (MySqlConnection connection = GetConnection())
        {
            connection.Open();

            MySqlCommand command = new();
            command.Parameters.AddWithValue("@title", title);
            command.CommandText = "SELECT * FROM " + tableName + " WHERE title = @title LIMIT 1";
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read() == false)
                {
                    connection.Close();
                    return null;
                }
                article = GetArticleFromReader(reader);
            }

            connection.Close();
        }

        return article;
    }

    private Article GetArticleFromReader(MySqlDataReader reader)
    {
        Article article = new()
        {
            Id = Guid.Parse(reader["id"].ToString() ?? Guid.Empty.ToString()),
            PostDate = DateTimeOffset.Parse(reader["post_date"].ToString() ?? "0"),
            Title = reader["title"].ToString() ?? "Title Missing!",
            SubTitle = reader["subtitle"].ToString() ?? "SubTitle Missing!",
            Body = reader["body"].ToString() ?? "Body Missing"
        };
        
        return article;
    }

    private ArticleProfileDto GetArticleProfileFromReader(MySqlDataReader reader)
    {
        ArticleProfileDto profile = new()
        {
            PostDateUnixTimeSeconds = DateTimeOffset.Parse(reader["post_date"].ToString() ?? "0").ToUnixTimeSeconds(),
            Title = reader["title"].ToString() ?? "Title Missing!",
            SubTitle = reader["subtitle"].ToString() ?? "SubTitle Missing!",
        };

        return profile;
    }

    private int GetCountFromScalarCommand(MySqlCommand command)
    {
        return int.Parse(command.ExecuteScalar().ToString() ?? "0");
    }
}