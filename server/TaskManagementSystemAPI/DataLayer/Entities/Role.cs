using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
