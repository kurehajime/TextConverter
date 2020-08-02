using Prism.Ioc;
using TextConverter.Views;
using System.Windows;
using TextConverter.Lib.Interfaces;
using TextConverter.Services;
using Prism.Modularity;
using Prism.Events;

namespace TextConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITextConverterService, DefaultConverter>("Default");
            containerRegistry.RegisterInstance<IContainerRegistry>(containerRegistry);
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Plugins" };
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<PubSubEvent<string>>().Publish("ModulesLoaded");
        }
    }
}
