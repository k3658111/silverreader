namespace SilverReader
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ServiceModel.Syndication;
    using Helper;
    using Model;

    public class FeedItemsViewModel : NotifyPropertyChangedHelper
    {
        private readonly ReaderModel _model = ReaderModel.Instance;

        public FeedItemsViewModel()
        {
            _model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
                                          {
                                              if (args.PropertyName == "SelectedFeed")
                                              {
                                                  if (_model.SelectedFeed != null)
                                                  {
                                                      Items = _model.SelectedFeed.Items;
                                                      NotifyPropertyChanged(() => Items);
                                                  }
                                              }
                                          };
        }

        public IEnumerable<SyndicationItem> Items { get; private set; }
    }
}