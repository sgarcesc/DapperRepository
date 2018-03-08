using LightInject;

namespace DapperRepository
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry service)
        {
            service.Register(typeof(IRepository<>), typeof(Repository<>));
            service.RegisterAssembly(GetType().Assembly, (serviceType, implementingType) => serviceType.Name.EndsWith("Repository"));
        }
    }
}
