using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderCode { get; set; } = null!;

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal ShippingFee { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public string ReceiverName { get; set; } = null!;

    public string ReceiverPhone { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
