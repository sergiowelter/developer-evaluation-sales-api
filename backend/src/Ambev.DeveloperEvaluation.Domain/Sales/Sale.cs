using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Sales
{
    public class Sale
    {
        public Guid Id { get; private set; }

        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }

        // External Identity - Customer
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }

        // External Identity - Branch
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }

        public IReadOnlyCollection<SaleItem> Items => _items;
        private readonly List<SaleItem> _items = new();

        public decimal TotalAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        protected Sale() { }

        public Sale(
            string saleNumber,
            DateTime saleDate,
            Guid customerId,
            string customerName,
            Guid branchId,
            string branchName)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
        }

        public void AddItem(SaleItem item)
        {
            _items.Add(item);
            RecalculateTotal();
        }

        public void RecalculateTotal()
        {
            TotalAmount = _items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.TotalAmount);
        }

        public void Cancel()
        {
            IsCancelled = true;
            TotalAmount = 0;
        }
    }
}
