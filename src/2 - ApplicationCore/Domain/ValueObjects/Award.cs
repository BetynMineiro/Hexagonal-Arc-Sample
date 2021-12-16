using System;
namespace Domain.ValueObjects
{
    public class Award
    {
        public Award(string id, decimal quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public string Id { get; private set; }
        public decimal Quantity { get; private set; }

        public void Add(decimal quantity, int round)
        {
            Quantity = Math.Round(Quantity + quantity, round, MidpointRounding.ToNegativeInfinity);
        }

        public void Remove(decimal quantity, int round)
        {
            if (Quantity >= quantity)
            {
                Quantity = Math.Round(Quantity - quantity, round,MidpointRounding.ToNegativeInfinity);
            }
            else Quantity = 0;
        }
    }
}
