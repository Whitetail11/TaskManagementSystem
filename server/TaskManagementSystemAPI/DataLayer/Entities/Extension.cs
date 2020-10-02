using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Extension
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
