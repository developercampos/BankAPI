/// <summary>
/// Representa uma conta bancária na API.
/// </summary>
namespace BankAPI.Models
{
    public class Account
    {
        /// <summary>
        /// Obtém ou define o identificador da conta.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Obtém ou define o saldo da conta.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
