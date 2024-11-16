using MediatR;

namespace MediaTRAndDapper.CQRS.Query.CustomerQuery.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Models.Customer>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetAllCustomersQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }

}
