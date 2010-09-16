namespace SilverReader.Model
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.ServiceModel.Syndication;
    using Helper;

    public class ReaderModel : NotifyPropertyChangedHelper
    {
        public static ReaderModel Instance = new ReaderModel();
        private SyndicationFeed _selectedFeed;

        private ReaderModel()
        {
            Feeds = new ObservableCollection<SyndicationFeed>();
        }

        public ObservableCollection<SyndicationFeed> Feeds { get; private set; }

        public SyndicationFeed SelectedFeed
        {
            get { return _selectedFeed; }
            set
            {
                _selectedFeed = value;
                NotifyPropertyChanged("SelectedFeed");
                Debug.WriteLine(SelectedFeed.Title.Text);
            }
        }
    }
}