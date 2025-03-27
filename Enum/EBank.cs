using System.ComponentModel.DataAnnotations;

namespace DigitalWallet.Enum
{
    public class EBank
    {
        public enum BancoEnum
        {
            [Display(Name = "Itaú")]
            Itau = 1,

            [Display(Name = "Nubank")]
            Nubank = 2,

            [Display(Name = "Mercado Pago")]
            MercadoPago = 3
        }
    }
}
