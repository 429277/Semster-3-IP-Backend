using System;

namespace AnstigramAPI.Models.Post
{
    public class ReadPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Caption { get; set; }
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
        public string Image { get; set; }

        public ReadPost(PostDTO postDTO, string username)
        {
            Id = postDTO.Id;
            UserId = postDTO.UserId;
            Username = username;
            Caption = postDTO.Caption;
            PostDate = postDTO.PostDate;
            Likes = postDTO.Likes;
            try { Image = "data:image/jpg;base64," + Convert.ToBase64String(postDTO.Image); } 
            catch { Console.WriteLine("Image was not found"); }
        }
    }
}
