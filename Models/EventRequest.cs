namespace BankAPI.Models
{
    public class EventRequest
    {
        public string Type { get; set; }  // "deposit" , "withdraw" , "transfer"
        public decimal Amount { get; set; }
        public decimal Destination { get; set; }
        public decimal Origin { get; set; }
    }
}
