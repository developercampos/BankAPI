using BankAPI.Models;

namespace BankAPI.Interfaces
{
    /// <summary>
    /// Interface para os serviços de conta bancária.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Obtém uma conta pelo seu identificador.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <returns>A conta com o identificador especificado.</returns>
        Account GetAccountById(string accountId);

        /// <summary>
        /// Deposita um valor na conta especificada.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <param name="amount">Quantia a ser depositada.</param>
        void Deposit(string accountId, decimal amount);

        /// <summary>
        /// Retira um valor da conta especificada.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <param name="amount">Quantia a ser retirada.</param>
        void Withdraw(string accountId, decimal amount);

        /// <summary>
        /// Transfere um valor de uma conta para outra.
        /// </summary>
        /// <param name="originAccountId">Identificador da conta de origem.</param>
        /// <param name="destinationAccountId">Identificador da conta de destino.</param>
        /// <param name="amount">Quantia a ser transferida.</param>
        void Transfer(string originAccountId, string destinationAccountId, decimal amount);

        /// <summary>
        /// Reinicia o estado das contas.
        /// </summary>
        void Reset();
    }
}
