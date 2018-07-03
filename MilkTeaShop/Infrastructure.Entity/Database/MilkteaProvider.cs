using Core.AppService.Database.MilkTea;

namespace Infrastructure.Entity.Database
{
    public class MilkteaProvider : IMilkteaContext
    {
        public object GetContext()
        {
            return new MilkteaContext();
        }
    }
}
