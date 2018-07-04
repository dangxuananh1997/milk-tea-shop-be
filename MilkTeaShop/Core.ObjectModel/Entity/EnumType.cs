namespace Core.ObjectModel.Entity
{
    public enum Size : int
    {
        S, M, L
    }

    public enum PaymentType : int
    {
        Cash, Coupon, Card
    }

    public enum UserType : int
    {
        Administrator, Guess, Member, Shipper
    }
}
