using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;

namespace SilverReader
{
    using System.Collections.ObjectModel;
    using System.ServiceModel.Syndication;

    public class FeedsViewModel : INotifyPropertyChanged
    {
        public FeedsViewModel()
        {
            
        }

        public ObservableCollection<SyndicationFeed> Feeds { get; private set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}