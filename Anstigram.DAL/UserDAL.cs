using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnstigramAPI.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly string connectionString = @"Data Source=LAPTOP-TLNR6N6N\SQLEXPRESS; Initial Catalog=Anstigram; Integrated Security=True; Connection Timeout=5;";

        public User GetUser(int id)
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"SELECT u.Id, u.Name, u.Age FROM Account u WHERE u.Id=@id", connection))
                {
                    Query.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = Query.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Id = (int)reader["Id"];
                        user.Name = (string)reader["Name"];
                        user.Age = (int)reader["Age"];
                    }
                    connection.Close();

                }

                return user;
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"SELECT u.Id, u.Name, u.Age FROM Account u", connection))
                {
                    connection.Open();
                    var reader = Query.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["Id"];
                        user.Name = (string)reader["Name"];
                        user.Age = (int)reader["Age"];
                        users.Add(user);
                    }
                    connection.Close();
                }
                return users;
            }
        }

        public void FollowUser(int followerId, int followingId)
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Query = new SqlCommand($"INSERT INTO FollowerLogic VALUES (@followingId, @followerId)", connection))
                {
                    Query.Parameters.AddWithValue("@followerId", followerId);
                    Query.Parameters.AddWithValue("@followingId", followingId);
                    connection.Open();
                    var reader = Query.ExecuteReader();    
                    connection.Close();
                }
            }
        }
    }
}