using ProductionMan.Domain.Models;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class RawMaterialSiloPageViewModel : BaseViewModel
    {

        private Weight _silosCharge;
        private Weight _ovenConsumption;
        private Weight _millProduction;


        public RawMaterialSiloPageViewModel()
        {
            _millProduction = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _ovenConsumption = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _silosCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };

            _millProduction.PropertyChanged += (sender, args) => FirePropertyChanged(this, "MillProduction");
            _ovenConsumption.PropertyChanged += (sender, args) => FirePropertyChanged(this, "OvenConsumption");
            _silosCharge.PropertyChanged += (sender, args) => FirePropertyChanged(this, "SilosCharge");
        }


        public Weight SilosCharge
        {
            get { return _silosCharge; }
            set
            {
                _silosCharge = value;
                FirePropertyChanged(this, "SilosCharge");
            }
        }

        public Weight OvenConsumption
        {
            get { return _ovenConsumption; }
            set
            {
                _ovenConsumption = value;
                FirePropertyChanged(this, "OvenConsumption");
            }
        }

        public Weight MillProduction
        {
            get { return _millProduction; }
            set
            {
                _millProduction = value;
                FirePropertyChanged(this, "MillProduction");
            }
        }
    }
}
