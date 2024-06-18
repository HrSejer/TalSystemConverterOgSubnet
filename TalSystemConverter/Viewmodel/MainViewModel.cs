using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalSystemConverter.Core;

namespace TalSystemConverter.Viewmodel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand TalSysConViewCommand { get; set; }
       
        public HomeViewModel HomeVm { get; set; }

        public TalSystemConverterViewModel TalSysConVm { get; set; }
        

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }

        }


        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            TalSysConVm = new TalSystemConverterViewModel();
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            TalSysConViewCommand = new RelayCommand(o =>
            {
                CurrentView = TalSysConVm;
            });
        }

    }
}
        
        


        



