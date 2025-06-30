using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OksModule.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "Черновик";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? Deadline { get; set; }
        public int AuthorId { get; set; }
        public int? RecipientDepartmentId { get; set; }
        public int? ProjectId { get; set; }
        public string FilePath { get; set; }
    }
}
