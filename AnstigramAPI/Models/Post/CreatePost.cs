using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Models.Post
{
    public class CreatePost
    {
        public string Caption { get; set; }
        public string AuthId { get; set; }
        public byte[] Image { get; set; }
        
        public CreatePost(string caption, string authId, IFormFile image)
        {
            Caption = caption;
            AuthId = authId;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                 Image = fileBytes;
            }
        }
    }
}
