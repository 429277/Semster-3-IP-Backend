using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Caption { get; set; }
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
        public string Image { get; set; }
        //Image type is a placeholder^
        

        public Post(PostDTO postDTO)
        {
            Id = postDTO.Id;
            UserId = postDTO.UserId;
            Caption = postDTO.Caption;
            PostDate = postDTO.PostDate;
            Likes = postDTO.Likes;
        }
    }
}
