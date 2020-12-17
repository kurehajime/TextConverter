using Anony.Services;
using Prism.Ioc;
using Prism.Modularity;
using TextConverter.Lib.Interfaces;

namespace Anony
{
    public sealed class AnonyModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITextConverterService, AnonyConverter>("Anony");
        }
    }
}