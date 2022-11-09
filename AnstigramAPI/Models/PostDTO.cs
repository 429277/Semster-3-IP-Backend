using System;

namespace AnstigramAPI.Models
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Caption { get; set; }
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
    }
}
