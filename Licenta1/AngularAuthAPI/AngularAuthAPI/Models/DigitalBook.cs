using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
    public class DigitalBook
    {
        [Key]
        public int Id_digitalBook { get; set; }
        public string Titlu { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] ImageBook { get; set; }
    }
}
