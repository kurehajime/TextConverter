using Dajarep.Services;
using Prism.Ioc;
using Prism.Modularity;
using TextConverter.Lib.Interfaces;

namespace Dajarep
{
    public class DajarepModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITextConverterService, DajarepConverter>("Dajarep");
        }
    }
}