namespace SilverReader
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Windows;
    using System.Windows.Input;
    using System.Xml;
    using Helper;
    using Microsoft.Expression.Interactivity.Core;
    using Model;

    public class FeedsViewModel : NotifyPropertyChangedHelper
    {
        public FeedsViewModel()
        {
            Model = ReaderModel.Instance;
            AddFeedFormVisibility = Visibility.Collapsed;
            AddFeedCommand = new ActionCommand(delegate(object o)
                                                   {
                                                       var url = (string) o;
                                                       Uri feedUri;
                                                       Uri.TryCreate(url, UriKind.Absolute, out feedUri);
                                                       if (feedUri == null)
                                                           return;
                                                       var request = new WebClient();
                                                       request.DownloadStringCompleted +=
                                                           delegate(object sender, DownloadStringCompletedEventArgs e)
                                                               {
                                                                   if (e.Error != null)
                                                                       return;
                                                                   AddFeedFormVisibility = Visibility.Collapsed;
                                                                   string xml = e.Result;
                                                                   if (xml.Length == 0)
                                                                       return;

                                                                   var stringReader = new StringReader(xml);
                                                                   XmlReader reader = XmlReader.Create(stringReader);
                                                                   SyndicationFeed feed = SyndicationFeed.Load(reader);

                                                                   if (Feeds.Where(f => f.Title.Text == feed.Title.Text).ToList().Count > 0)
                                                                       return;

                                                                   Feeds.Add(feed);
                                                               };
                                                       request.DownloadStringAsync(feedUri);
                                                   });
        }

        public ObservableCollection<SyndicationFeed> Feeds
        {
            get { return Model.Feeds; }
        }

        public ICommand AddFeedCommand { get; private set; }

        public SyndicationFeed SelectedFeed
        {
            get { return Model.SelectedFeed; }
            set { Model.SelectedFeed = value; }
        }

        public ReaderModel Model { get; set; }

        private Visibility _addFeedFormVisibility;
        public Visibility AddFeedFormVisibility
        {
            get { return _addFeedFormVisibility; }
            set
            {
                _addFeedFormVisibility = value;
                NotifyPropertyChanged(() => AddFeedFormVisibility);
            }
        }
    }
}