namespace MediaTRAndDapper.CQRS.Query.CustomerQuery.GetCustomer;

public class GetCustomerOrdersRequest
{
    public int CustomerId { get; set; }

    public GetCustomerOrdersRequest(int customerId)
    {
        CustomerId = customerId;
    }
}
