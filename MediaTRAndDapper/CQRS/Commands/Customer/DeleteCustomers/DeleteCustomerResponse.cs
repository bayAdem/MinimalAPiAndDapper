namespace MediaTRAndDapper.CQRS.Commands.Customer.DeleteCustomers
{
    public class DeleteCustomerResponse(bool success, string message)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
    }
}
