using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Models
{
    [Keyless]
    public class FollowerLogic
    {
        public int UserId { get; set; }
        public int FollowerId { get; set; }
    }
}
