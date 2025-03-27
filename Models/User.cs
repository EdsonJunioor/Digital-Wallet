using DigitalWallet.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Bank { get; set; }
        public string WalletNumber { get; set; } = string.Empty;
        public double? Balance { get; set; }

    }
}
