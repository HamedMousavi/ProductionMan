using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using ProductionMan.Common;


namespace ProductionMan.Desktop
{

    public class NameEditorWindowViewModel : BaseViewModel
    {

        private ICommand _cancelCommand;
        private ICommand _saveCommand;
        private string _name;
        private INotifyPropertyChanged _namedEntity;
        private string _namedEntityNameProperty;


        public NameEditorWindowViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("NamedEntity") || e.NameIs("NamedEntityNameProperty"))
            {
                if (NamedEntity != null)
                {
                    LoadName();
                }
            }
            else if (e.NameIs("Name"))
            {
                SaveName();
            }
        }


        private void NamedEntityOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null && !string.IsNullOrWhiteSpace(NamedEntityNameProperty) && e.NameIs(NamedEntityNameProperty))
            {
                LoadName();
            }
        }


        private void SaveName()
        {
            var prop = NamedEntity.GetType().GetProperty(NamedEntityNameProperty, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(NamedEntity, Name, null);
            }
            
            //NamedEntity.GetType()
            //    .InvokeMember(NamedEntityNameProperty,
            //        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder,
            //        NamedEntity, new object[] {Name});
            
            //NamedEntity.Name = Name;
        }


        private void LoadName()
        {
            var prop = NamedEntity.GetType().GetProperty(NamedEntityNameProperty, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                Name = prop.GetValue(NamedEntity) as string;
            }
        }


        public string NamedEntityNameProperty
        {
            get { return _namedEntityNameProperty; }
            set
            {
                _namedEntityNameProperty = value;
                FirePropertyChanged(this, "NamedEntityNameProperty");
            }
        }


        public INotifyPropertyChanged NamedEntity
        {
            get { return _namedEntity; }
            set
            {
                if (_namedEntity != value)
                {
                    if (_namedEntity != null) _namedEntity.PropertyChanged -= NamedEntityOnPropertyChanged;
                    _namedEntity = value;
                    if (_namedEntity != null) _namedEntity.PropertyChanged += NamedEntityOnPropertyChanged;
                }

                FirePropertyChanged(this, "NamedEntity");
            }
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                FirePropertyChanged(this, "Name");
            }
        }


        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set
            {
                _saveCommand = value;
                FirePropertyChanged(this, "SaveCommand");
            }
        }


        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                _cancelCommand = value;
                FirePropertyChanged(this, "CancelCommand");
            }
        }
    }
}
