using BlogRest.Models;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;

namespace BlogRest.Contexts;

public class TestArticleContext : IArticleContext
{
    private string connectionString { get; set; }

    public TestArticleContext(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public IEnumerable<Article> FindAll()
    {
        List<Article> articles = new();

        using (MySqlConnection connection = GetConnection())
        {
            connection.Open();
            MySqlCommand command = new("SELECT * FROM articles", connection);
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
            
            command.CommandText = "INSERT INTO articles (id, post_date, title, subtitle, body) VALUES (@id, FROM_UNIXTIME(@post_date), @title, @subtitle, @body)";

            command.Connection = connection;

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}