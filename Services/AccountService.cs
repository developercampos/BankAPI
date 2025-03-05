using BankAPI.Interfaces;
using BankAPI.Models;
using System.Collections.Concurrent;

namespace BankAPI.Services
{
    /// <summary>
    /// Implementação dos serviços de conta bancária.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly ConcurrentDictionary<string, Account> _accounts = new ConcurrentDictionary<string, Account>();

        /// <summary>
        /// Obtém uma conta pelo seu identificador.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <returns>A conta com o identificador especificado.</returns>
        public Account GetAccountById(string accountId)
        {
            _accounts.TryGetValue(accountId, out var account);
            return account;
        }

        /// <summary>
        /// Deposita um valor na conta especificada.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <param name="amount">Quantia a ser depositada.</param>
        public void Deposit(string accountId, decimal amount)
        {
            //var account = _accounts.GetOrAdd(accountId, new Account { Id = accountId, Balance = amount });
            var account = GetAccountById (accountId);

            if (account != null)
            {
                account.Balance += amount;
            }
            else
            {
                account = _accounts.GetOrAdd(accountId, new Account { Id = accountId, Balance = amount });
            }
        }

        /// <summary>
        /// Retira um valor da conta especificada.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <param name="amount">Quantia a ser retirada.</param>
        public void Withdraw(string accountId, decimal amount)
        {
            if (_accounts.TryGetValue(accountId, out var account))
            {
                account.Balance -= amount;
            }
        }

        /// <summary>
        /// Transfere um valor de uma conta para outra.
        /// </summary>
        /// <param name="originAccountId">Identificador da conta de origem.</param>
        /// <param name="destinationAccountId">Identificador da conta de destino.</param>
        /// <param name="amount">Quantia a ser transferida.</param>
        public void Transfer(string originAccountId, string destinationAccountId, decimal amount)
        {
            Withdraw(originAccountId, amount);
            Deposit(destinationAccountId, amount);
        }

        /// <summary>
        /// Reinicia o estado das contas.
        /// </summary>
        public void Reset()
        {
            _accounts.Clear();
        }
    }
}
