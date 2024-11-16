namespace MediaTRAndDapper.CQRS.Commands.Customer.AddCustomers;

public class AddCustomerResponse(int id)
{
    public int Id { get; } = id;
}
