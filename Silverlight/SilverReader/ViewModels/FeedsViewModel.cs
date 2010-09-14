using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;

namespace SilverReader
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Windows.Input;
    using System.Xml;
    using Microsoft.Expression.Interactivity.Core;

    public class FeedsViewModel : INotifyPropertyChanged
    {
        public FeedsViewModel()
        {
            Feeds = new ObservableCollection<SyndicationFeed>();
            AddFeedCommand = new ActionCommand(delegate(object o)
                                                   {
                                                       string url = (string) o;
                                                       Uri feedUri;
                                                       Uri.TryCreate(url, UriKind.Absolute, out feedUri);
                                                       if (feedUri == null)
                                                           return;
                                                       WebClient request = new WebClient();
                                                       request.DownloadStringCompleted +=
                                                           delegate(object sender, DownloadStringCompletedEventArgs e)
                                                           {
                                                               if (e.Error != null)
                                                                   return;

                                                               string xml = e.Result;
                                                               if (xml.Length == 0)
                                                                   return;

                                                               StringReader stringReader = new StringReader(xml);
                                                               XmlReader reader = XmlReader.Create(stringReader);
                                                               SyndicationFeed feed = SyndicationFeed.Load(reader);

                                                               if (Feeds.Where(f => f.Title.Text == feed.Title.Text).ToList().Count > 0)
                                                                   return;

                                                               Feeds.Add(feed);

                                                           };
                                                       request.DownloadStringAsync(feedUri);

                                                   });
        }

        public ObservableCollection<SyndicationFeed> Feeds { get; private set; }
        public ICommand AddFeedCommand { get; private set; }

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