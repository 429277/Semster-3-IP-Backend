using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.DAL
{
    public class PostDAL : IPostDAL
    {
        private readonly string connectionString = @"Data Source=LAPTOP-TLNR6N6N\SQLEXPRESS; Initial Catalog=Anstigram; Integrated Security=True; Connection Timeout=5;";

        public Post GetPost(int id)
        {
            Post post = new Post();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"SELECT p.Id, p.UserId, p.Caption, p.PostDate, p.Likes FROM Post p WHERE u.Id=@id", connection))
                {
                    Query.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = Query.ExecuteReader();
                    while (reader.Read())
                    {
                        post.Id = (int)reader["Id"];
                        post.UserId = (int)reader["UserId"];
                        post.Caption = (string)reader["Caption"];
                        post.PostDate = (DateTime)reader["PostDate"];
                        post.Likes = (int)reader["Likes"];
                    }
                    connection.Close();

                }
                return post;
            }
        }

        public List<Post> GetPosts()
        {
            List<Post> Posts = new List<Post>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"SELECT p.Id, p.UserId, p.Caption, p.PostDate, p.Likes, u.Name FROM Post p INNER JOIN Account u ON u.Id = p.Id", connection))
                {
                    connection.Open();
                    var reader = Query.ExecuteReader();
                    while (reader.Read())
                    {
                        Post post = new();
                        post.Id = (int)reader["Id"];
                        post.UserId = (int)reader["UserId"];
                        post.UserName = (string)reader["Name"];
                        post.Caption = (string)reader["Caption"];
                        post.PostDate = (DateTime)reader["PostDate"];
                        post.Likes = (int)reader["Likes"];
                        Posts.Add(post);
                    }
                    connection.Close();
                }
                return Posts;
            }
        }
    }
}
