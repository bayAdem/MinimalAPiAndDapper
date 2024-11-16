namespace MediaTRAndDapper.CQRS.Query.CustomerQuery.GetAllCustomers
{
    public class GetAllCustomersRequest
    {
        public int Page { get; set; } = 1;  // Sayfa numarası
        public int PageSize { get; set; } = 10;  // Sayfa başına gösterilecek müşteri sayısı
    }

}
