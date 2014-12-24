using ProductionMan.Common;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class TabItemViewModel : NotifyPropertyChanged
    {

        private string _headerLabel;
        private string _pageTitle;
        private string _headerIcon;
        private bool _isSelected;
        private object _toolbar;


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


        public object Toolbar
        {
            get { return _toolbar; }
            set
            {
                _toolbar = value;
                FirePropertyChanged(this, "Toolbar");
            }
        }


        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                FirePropertyChanged(this, "IsSelected");
            }
        }
    }
}
