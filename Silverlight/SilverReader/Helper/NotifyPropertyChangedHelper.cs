namespace SilverReader.Helper
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public abstract class NotifyPropertyChangedHelper : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = ((MemberExpression) propertyExpression.Body).Member.Name;
            NotifyPropertyChanged(propertyName);
        }
    }
}