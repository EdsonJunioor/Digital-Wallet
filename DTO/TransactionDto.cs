namespace DigitalWallet.DTO
{
    public class TransactionDto
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string? CPFDestination { get; set; }
        public string? WalletNumberDestination { get; set; }
        public decimal Value { get; set; }
        public DateTime TransactionTime { get; set; } = DateTime.UtcNow;
    }
}
