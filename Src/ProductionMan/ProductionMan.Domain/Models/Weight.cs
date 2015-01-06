using System;
using ProductionMan.Common;


namespace ProductionMan.Domain.Models
{

    public class Weight : NotifyPropertyChanged
    {

        private decimal _value;
        private MeasurementUnit _unit;


        public static decimal operator +(Weight c1, Weight c2)
        {
            if (c1.Unit.Id != c2.Unit.Id) throw new InvalidOperationException("Cannot add weights with different units!");
            return c1.Value + c2.Value;
        }


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
