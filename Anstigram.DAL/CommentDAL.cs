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
    public class CommentDAL : ICommentDAL
    {
        private readonly string connectionString = @"Data Source=LAPTOP-TLNR6N6N\SQLEXPRESS; Initial Catalog=Anstigram; Integrated Security=True; Connection Timeout=5;";

        public Comment GetComment(int id)
        {
            Comment comment = new Comment();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"SELECT c.Id, c.UserId, c.UserName, c.Message, c.PostDate, c.Likes FROM Comment c WHERE u.Id=@id", connection))
                {
                    Query.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = Query.ExecuteReader();
                    while (reader.Read())
                    {
                        comment.Id = (int)reader["Id"];
                        comment.UserId = (int)reader["UserId"];
                        comment.UserName = (string)reader["UserName"];
                        comment.Message = (string)reader["Message"];
                        comment.PostDate = (DateTime)reader["PostDate"];
                        comment.Likes = (int)reader["Likes"];
                    }
                    connection.Close();
                }
                return comment;
            }
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"SELECT c.Id, c.UserId, c.UserName, c.Message, c.PostDate, c.Likes FROM Comment c", connection))
                {
                    connection.Open();
                    var reader = Query.ExecuteReader();
                    while (reader.Read())
                    {
                        Comment comment = new();
                        comment.Id = (int)reader["Id"];
                        comment.UserId = (int)reader["UserId"];
                        comment.UserName = (string)reader["UserName"];
                        comment.Message = (string)reader["Message"];
                        comment.PostDate = (DateTime)reader["PostDate"];
                        comment.Likes = (int)reader["Likes"];
                        comments.Add(comment);
                    }
                    connection.Close();
                }
                return comments;
            }
        }
    }
}
