namespace DigitalWallet.DTO
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Bank { get; set; } // sera um Enum com algumas opções de bancos (itau, nubank, mercadoPago)
        public string WalletNumber { get; set; } = string.Empty;
        public double? Balance { get; set; }
        public string? Token { get; set; }

    }
}
