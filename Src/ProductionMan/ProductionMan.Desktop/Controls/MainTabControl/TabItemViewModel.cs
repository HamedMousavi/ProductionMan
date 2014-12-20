using ProductionMan.Common;


namespace ProductionMan.Desktop.Controls.MainTabControl
{

    public class TabItemViewModel : NotifyPropertyChanged
    {
        private string _headerLabel;
        private string _pageTitle;
        private string _headerIcon;


        public string HeaderLabel
        {
            get { return _headerLabel; }
            set
            {
                _headerLabel = value; 
                FirePropertyChanged(this, "HeaderLabel");
            }
        }


        public string HeaderIcon
        {
            get { return _headerIcon; }
            set
            {
                _headerIcon = value;
                FirePropertyChanged(this, "HeaderIcon");
            }
        }


        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                FirePropertyChanged(this, "PageTitle");
            }
        }


        public bool IsSelected { get; set; }
    }
}
