using ProductionMan.Common;


namespace ProductionMan.Domain.Models
{

    public class Weight : NotifyPropertyChanged
    {

        private decimal _value;
        private MeasurementUnit _unit;


        public MeasurementUnit Unit
        {
            get { return _unit; }
            set
            {
                _unit = value; 
                FirePropertyChanged(this, "Unit");
            }
        }
        public decimal Value
        {
            get { return _value; }
            set
            {
                _value = value;
                FirePropertyChanged(this, "Value");
            }
        }
    }
}
