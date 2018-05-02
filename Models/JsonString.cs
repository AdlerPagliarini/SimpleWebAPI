using System.ComponentModel.DataAnnotations;

namespace SimpleWebApi.Models
{
    public class JsonString
    {
        public int Id { get; set; }
        [Required]
        public string Projeto { get; set; }
        [Required]
        public string Texto { get; set; }
    }
}