using System;
using System.ComponentModel.DataAnnotations;

namespace GustavoDocSpiderTeste.Models.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileData { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
