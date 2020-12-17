using Markov.Services;
using Prism.Ioc;
using Prism.Modularity;
using TextConverter.Lib.Interfaces;

namespace Markov
{
    public sealed class MarkovModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITextConverterService, MarkovConverter>("Markov");
        }
    }
}