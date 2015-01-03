using System;
using ProductionMan.Common;


namespace ProductionMan.Domain.Models
{

    public class MeasurementUnit : NotifyPropertyChanged
    {

        private string _name;
        private int _id;


        public Int32 Id
        {
            get { return _id; }
            set
            {
                _id = value;
                FirePropertyChanged(this, "Id");
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


        public override string ToString()
        {
            return Name;
        }
    }
}
