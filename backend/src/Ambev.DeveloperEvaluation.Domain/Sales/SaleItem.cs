using System;

namespace Ambev.DeveloperEvaluation.Domain.Sales
{
    public class SaleItem
    {
        public Guid Id { get; private set; }

        // External Identity
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        // Percentual de desconto (0, 0.10m, 0.20m)
        public decimal Discount { get; private set; }

        public decimal TotalAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        protected SaleItem() { }

        public SaleItem(
            Guid productId,
            string productName,
            int quantity,
            decimal unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            ApplyDiscountAndCalculateTotal();
        }

        public void ApplyDiscountAndCalculateTotal()
        {
            if (Quantity > 20)
                throw new InvalidOperationException("It is not possible to sell more than 20 identical items.");

            if (Quantity < 4)
                Discount = 0m;
            else if (Quantity < 10)
                Discount = 0.10m;
            else
                Discount = 0.20m;

            var grossTotal = Quantity * UnitPrice;
            var discountValue = grossTotal * Discount;

            TotalAmount = grossTotal - discountValue;
        }

        public void Cancel()
        {
            IsCancelled = true;
            TotalAmount = 0;
        }
    }
}
