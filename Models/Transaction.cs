using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CPFDestination { get; set; } = string.Empty;
        public string WalletNumberDestination { get; set; } = string.Empty;
        public int Bank { get; set; }
        public double Value { get; set; }
        public DateTime TransactionTime { get; set; } = DateTime.UtcNow;
    }
}
