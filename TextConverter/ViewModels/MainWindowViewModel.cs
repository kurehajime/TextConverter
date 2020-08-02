using Prism.Mvvm;
using Prism.Regions;
using TextConverter.Views;

namespace TextConverter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "Text Converter";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(MainContent));
        }
    }
}
