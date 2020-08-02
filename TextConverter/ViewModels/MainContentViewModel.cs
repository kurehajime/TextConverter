using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Collections.Generic;
using System.Linq;
using TextConverter.Lib.Interfaces;
using Unity;

namespace TextConverter.ViewModels
{
    public class MainContentViewModel : BindableBase
    {

        #region properties

        /// <summary>
        /// 入力文字列
        /// </summary>
        private string _InText;
        public string InText
        {
            get { return _InText; }
            set { SetProperty(ref _InText, value); }
        }

        /// <summary>
        /// 出力文字列
        /// </summary>
        private string _OutText;
        public string OutText
        {
            get { return _OutText; }
            set { SetProperty(ref _OutText, value); }
        }

        /// <summary>
        /// 選択インデックス
        /// </summary>
        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { SetProperty(ref _SelectedIndex, value); }
        }

        /// <summary>
        /// 変換コマンド
        /// </summary>
        public DelegateCommand ConvertCommand { get; private set; }

        /// <summary>
        /// 変更イベント
        /// </summary>
        public DelegateCommand<object[]> SelectedCommand { get; private set; }


        /// <summary>
        /// 変換モード
        /// </summary>
        public List<ITextConverterService> Modes = new List<ITextConverterService>();

        /// <summary>
        /// 選択中モード
        /// </summary>
        public ITextConverterService SelectedMode;

        /// <summary>
        /// コンテナ
        /// </summary>
        IContainerRegistry containerRegistry;

        /// <summary>
        /// 変換モード(選択肢)
        /// </summary>
        public IList<string> Items
        {
            get
            {
                return Modes.Select(x => x.Name()).ToList();
            }
        }


        #endregion


        public MainContentViewModel(IContainerRegistry _containerRegistry, IEventAggregator eventAggregator)
        {
            containerRegistry = _containerRegistry;

            SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
            ConvertCommand = new DelegateCommand(Convert);

            eventAggregator.GetEvent<PubSubEvent<string>>().Subscribe(OnMessage);

        }


        /// <summary>
        /// モード変換時イベント
        /// </summary>
        /// <param name="selectedItems"></param>
        private void OnItemSelected(object[] selectedItems)
        {
            var selected = selectedItems?.FirstOrDefault();
            if (selected != null)
            {
                this.SelectedMode = Modes.Where(x => x.Name() == selected.ToString()).FirstOrDefault();
                if(this.SelectedMode is ISampleTextService)
                {
                    var sampleText = ((ISampleTextService)this.SelectedMode)?.SampleText();
                    if (sampleText != null)
                    {
                        InText = string.Join("\n", sampleText);
                    }
                }

            }
        }

        private void Convert()
        {
            if(this.SelectedMode != null)
            {
                OutText = string.Join("\n", this.SelectedMode.Convert(InText.Split("\n").ToList()));
            }
        }

        private void OnMessage(string s)
        {
            if (s == "ModulesLoaded")
            {
                foreach (var item in containerRegistry.GetContainer().ResolveAll<ITextConverterService>())
                {
                    Modes.Add(item);
                }
                RaisePropertyChanged(nameof(Items));
                SelectedIndex = 0;
                OnItemSelected(new string[] { Modes.FirstOrDefault().Name() });
            }
        }
    }
}
