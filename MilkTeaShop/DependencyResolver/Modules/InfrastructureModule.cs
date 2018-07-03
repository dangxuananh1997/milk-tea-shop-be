using Core.AppService.Database.MilkTea;
using Core.ObjectService.Repositories;
using Infrastructure.Entity.Database;
using Infrastructure.Entity.Repositories;
using Ninject.Modules;

namespace DependencyResolver.Modules
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IMilkteaContext>().To<MilkteaProvider>();
        }
    }
}
