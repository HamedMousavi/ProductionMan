using ProductionMan.Domain.Models;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class CrusherPageViewModel : BaseViewModel
    {

        private Weight _soilBinCharge;
        private Weight _ironBinCharge;
        private Weight _limeBinCharge;
        private Weight _mixBinTotalCharge;


        public CrusherPageViewModel()
        {
            _mixBinTotalCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _soilBinCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _limeBinCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };
            _ironBinCharge = new Weight { Unit = new MeasurementUnit { Name = Localized.Resources.Ton } };

            _ironBinCharge.PropertyChanged += (o, args) => FirePropertyChanged(this, "IronBinCharge");
            _soilBinCharge.PropertyChanged += (o, args) => FirePropertyChanged(this, "SoilBinCharge");
            _limeBinCharge.PropertyChanged += (o, args) => FirePropertyChanged(this, "LimeBinCharge");
        }


        public Weight SoilBinCharge
        {
            get { return _soilBinCharge; }
            set
            {
                _soilBinCharge = value;
                FirePropertyChanged(this, "SoilBinCharge");
            }
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
        public Weight MixBinTotalCharge
        {
            get { return _mixBinTotalCharge; }
            set
            {
                _mixBinTotalCharge = value;
                FirePropertyChanged(this, "MixBinTotalCharge");
            }
        }
    }
}
