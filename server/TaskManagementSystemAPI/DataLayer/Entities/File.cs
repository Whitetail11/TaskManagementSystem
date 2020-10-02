using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class File
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set;}
        public byte[] Data { get; set; }
        public DateTime AttachedDate { get; set; }

        public virtual Task Task { get; set; }
    }
}
