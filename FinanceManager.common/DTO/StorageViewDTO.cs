namespace FinanceManager.common.DTO
{
    public class StorageViewDTO
    {
        public string? Name { get; set; }
        public double Value { get; set; }
        public List<TransactionByStorageDTO>? Transactions { get; set; }
    }
}
