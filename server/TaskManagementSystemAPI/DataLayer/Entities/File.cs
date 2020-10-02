using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations.Models
{
    public class File
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Task Task { get; set; }
        public string Name { get; set;}
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public DateTime AttachedDateTime { get; set; }

    }
}
