using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GustavoDocSpiderTeste.ViewModels.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Title { get; set; }

        [StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres.")]
        public string Description { get; set; }

        public string FileName { get; set; }

        public IFormFile File { get; set; }
    }
}
