﻿namespace FinanceManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsIncome { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
