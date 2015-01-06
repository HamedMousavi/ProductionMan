using ProductionMan.Domain.Models;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class RawMillPageViewModel : BaseViewModel
    {
    
        private Weight _limeBinCharge;
        private Weight _ironBinCharge;
        private Weight _mixConsumption;
        private Weight _limeConsumption;
        private Weight _ironConsumption;
        private Weight _humidity;
        private Weight _netProduction;


        public RawMillPageViewModel()
        {
            CreateWeights();
        }


        private void CreateWeights()
        {
            _limeBinCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _ironBinCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };

            _humidity = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _limeConsumption = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _ironConsumption = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _mixConsumption = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _netProduction = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };

            _humidity.PropertyChanged += (sender, args) => FirePropertyChanged(this, "Humidity");
            _limeConsumption.PropertyChanged += (sender, args) => FirePropertyChanged(this, "LimeConsumption");
            _ironConsumption.PropertyChanged += (sender, args) => FirePropertyChanged(this, "IronConsumption");
            _mixConsumption.PropertyChanged += (sender, args) => FirePropertyChanged(this,"MixConsumption");
        }

        public Weight IronBinCharge
        {
            get { return _ironBinCharge; }
            set
            {
                _ironBinCharge = value;
                FirePropertyChanged(this, "IronBinCharge");
            }
        }

        public Weight LimeBinCharge
        {
            get { return _limeBinCharge; }
            set
            {
                _limeBinCharge = value;
                FirePropertyChanged(this, "LimeBinCharge");
            }
        }

        public Weight MixConsumption
        {
            get { return _mixConsumption; }
            set
            {
                _mixConsumption = value;
                FirePropertyChanged(this, "MixConsumption");
            }
        }

        public Weight LimeConsumption
        {
            get { return _limeConsumption; }
            set
            {
                _limeConsumption = value;
                FirePropertyChanged(this, "LimeConsumption");
            }
        }

        public Weight IronConsumption
        {
            get { return _ironConsumption; }
            set
            {
                _ironConsumption = value;
                FirePropertyChanged(this, "IronConsumption");
            }
        }

        public Weight Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                FirePropertyChanged(this, "Humidity");
            }
        }

        public Weight NetProduction
        {
            get { return _netProduction; }
            set
            {
                _netProduction = value;
                FirePropertyChanged(this, "NetProduction");
            }
        }
    }
}