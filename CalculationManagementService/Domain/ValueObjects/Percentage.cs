namespace CalculationManagementService.Domain.ValueObjects
{
    public class Percentage
    {
        public decimal Value { get; }

        public Percentage(decimal value)
        {
            if (value < 0 || value > 100) throw new ArgumentException("Percentage must be between 0 and 100");
            Value = value;
        }

        public Money ApplyTo(Money amount) => new Money(amount.Amount * (Value / 100), amount.Currency);
    }
}
