namespace Core.ObjectModel.ConstantManager
{
    public class ConstantDataManager
    {
        public const int PAGESIZE = 20;

        public partial class OrderStatus
        {
            public const string PENDING = "Pending";
            public const string ACCEPTED = "Accepted";
            public const string DENIED = "Denied";
            public const string DELIVERED = "Delivered";
        }

        public partial class WorldTime
        {
            public const int VIETNAM = 7;
        }

    }
}