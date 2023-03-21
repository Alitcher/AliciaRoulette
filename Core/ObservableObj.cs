using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Core
{
    class ObservableObj : INotifyPropertyChanged //interface update ui when binding
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); // If not null then invoke 
        }
    }
}
