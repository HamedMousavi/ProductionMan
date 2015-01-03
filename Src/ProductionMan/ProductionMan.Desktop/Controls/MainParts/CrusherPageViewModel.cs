using System.ComponentModel;
using ProductionMan.Common;
using ProductionMan.Domain.Models;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class CrusherPageViewModel : BaseViewModel
    {

        private Weight _soilBinCharge;
        private Weight _ironBinCharge;
        private Weight _calciteBinCharge;
        private Weight _mixBinTotalCharge;


        public CrusherPageViewModel()
        {
            _mixBinTotalCharge = new Weight();
            _soilBinCharge = new Weight();
            _calciteBinCharge = new Weight();
            _ironBinCharge = new Weight();

            _ironBinCharge.PropertyChanged += (o, args) => RecalculateTotal();
            _soilBinCharge.PropertyChanged += (o, args) => RecalculateTotal();
            _calciteBinCharge.PropertyChanged += (o, args) => RecalculateTotal();

            PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.NameIs("SoilBinCharge") ||
                    e.NameIs("IronBinCharge") ||
                    e.NameIs("CalciteBinCharge"))
                {
                    RecalculateTotal();
                }
            };
        }


        private void RecalculateTotal()
        {
            MixBinTotalCharge.Value = SoilBinCharge.Value;
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
        public Weight CalciteBinCharge
        {
            get { return _calciteBinCharge; }
            set
            {
                _calciteBinCharge = value; 
                FirePropertyChanged(this, "CalciteBinCharge");
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
