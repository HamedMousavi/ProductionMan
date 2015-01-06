using System.ComponentModel;
using ProductionMan.Common;
using ProductionMan.Desktop.Controls.MainParts;


namespace ProductionMan.Desktop.Logic
{

    public class ProductionCalculator : NotifyPropertyChanged
    {

        private RawMillPageViewModel _rawMillViewModel;
        private CrusherPageViewModel _crusherViewModel;
        private RawMaterialSiloPageViewModel _rawMaterialSiloViewModel;
        private volatile bool _isCalculating;


        public ProductionCalculator()
        {
            _isCalculating = false;
            PropertyChanged += delegate { ResetBindings(); };
        }


        private void ResetBindings()
        {
            if (RawMillViewModel != null && 
                CrusherViewModel != null && 
                RawMaterialSiloViewModel != null)
            {
                RawMillViewModel.PropertyChanged += (sender, args) => Recalculate(args);
                CrusherViewModel.PropertyChanged += (sender, args) => Recalculate(args);
                RawMaterialSiloViewModel.PropertyChanged += (sender, args) => Recalculate(args);
            }
        }

        private void Recalculate(PropertyChangedEventArgs e)
        {
            if (_isCalculating) return;
            _isCalculating = true;

            var sum = RawMillViewModel.LimeConsumption.Value + RawMillViewModel.MixConsumption.Value + RawMillViewModel.IronConsumption.Value;
            
            if (!e.NameIs("NetProduction")) RawMillViewModel.NetProduction.Value = sum - ((sum * RawMillViewModel.Humidity.Value) / 100);
            if (!e.NameIs("LimeBinCharge")) RawMillViewModel.LimeBinCharge.Value = CrusherViewModel.LimeBinCharge.Value - RawMillViewModel.LimeConsumption.Value;
            if (!e.NameIs("IronBinCharge")) RawMillViewModel.IronBinCharge.Value = CrusherViewModel.IronBinCharge.Value - RawMillViewModel.IronConsumption.Value;

            if (!e.NameIs("MixBinTotalCharge")) CrusherViewModel.MixBinTotalCharge.Value = CrusherViewModel.SoilBinCharge.Value + RawMillViewModel.MixConsumption.Value;

            if (!e.NameIs("MillProduction")) RawMaterialSiloViewModel.MillProduction.Value = RawMillViewModel.NetProduction.Value;
            if (!e.NameIs("SilosCharge")) RawMaterialSiloViewModel.SilosCharge.Value = RawMillViewModel.NetProduction.Value - RawMaterialSiloViewModel.OvenConsumption.Value;

            _isCalculating = false;
        }


        public RawMillPageViewModel RawMillViewModel
        {
            get { return _rawMillViewModel; }
            set
            {
                _rawMillViewModel = value; 
                FirePropertyChanged(this, "RawMillViewModel");
            }
        }

        public CrusherPageViewModel CrusherViewModel
        {
            get { return _crusherViewModel; }
            set
            {
                _crusherViewModel = value;
                FirePropertyChanged(this, "CrusherViewModel");
            }
        }

        public RawMaterialSiloPageViewModel RawMaterialSiloViewModel
        {
            get { return _rawMaterialSiloViewModel; }
            set
            {
                _rawMaterialSiloViewModel = value;
                FirePropertyChanged(this, "RawMaterialSiloViewModel");
            }
        }
    }
}
