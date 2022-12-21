using System;

namespace AnstigramAPI.Models.Post
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Caption { get; set; }
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
        public string AuthId { get; set; }
        public byte[] Image { get; set; }


        public PostDTO() { }

        public PostDTO(CreatePost post)
        {
            Id = 0;
            UserId = 0;
            Caption = post.Caption;
            PostDate = DateTime.Now;
            Likes = 0;
            AuthId = post.AuthId;
            Image = post.Image;
        }
    }
}
