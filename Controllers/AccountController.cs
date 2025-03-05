using BankAPI.Interfaces;
using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar contas bancárias.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Inicializa uma nova instância de AccountController.
        /// </summary>
        /// <param name="accountService">Serviço de conta bancária.</param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Obtém o saldo da conta especificada.
        /// </summary>
        /// <param name="accountId">Identificador da conta.</param>
        /// <returns>O saldo da conta, ou 404 se a conta não existir.</returns>
        [HttpGet("balance")]
        public IActionResult GetBalance([FromQuery] string accountId)
        {
            var account = _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound(0);
            }
            return Ok(account.Balance);
        }

        /// <summary>
        /// Processa eventos relacionados a contas bancárias (depósito, retirada, transferência).
        /// </summary>
        /// <param name="payload">Dados do evento.</param>
        /// <returns>O resultado do processamento do evento.</returns>
        [HttpPost("event")]
        public IActionResult PostEvent([FromBody] EventRequest request)
        {
            switch ((string)request.Type)
            {
                case "deposit":
                    _accountService.Deposit(request.Destination.ToString(), (decimal)request.Amount);
                    var destination = _accountService.GetAccountById(request.Destination.ToString());
                    return Ok(destination);
                
                case "withdraw":
                    _accountService.Withdraw(request.Origin.ToString(), (decimal)request.Amount);
                    var origin = _accountService.GetAccountById(request.Origin.ToString());

                    if (origin == null)
                    {
                        return NotFound ("Insufficient funds");
                    }

                    return Ok (origin);
                   
                case "transfer":
                    _accountService.Transfer(request.Origin.ToString(), request.Destination.ToString(), (decimal)request.Amount);
                    var originAcc = _accountService.GetAccountById(request.Origin.ToString());

                    if (originAcc == null)
                    {
                        return NotFound ("Insufficient funds");
                    }

                    var destinationAcc = _accountService.GetAccountById(request.Destination.ToString());

                    if (destinationAcc == null)
                    {
                        return NotFound("Insufficient funds");
                    }

                    return Ok(destinationAcc);
                
                default:
                    return BadRequest();
            }
        }    

    /// <summary>
    /// Reinicia o estado das contas bancárias.
    /// </summary>
    /// <returns>Confirmação do reinício do estado.</returns>
    [HttpPost("reset")]
        public IActionResult Reset()
        {
            _accountService.Reset();
            return Ok();
        }
    }
}
