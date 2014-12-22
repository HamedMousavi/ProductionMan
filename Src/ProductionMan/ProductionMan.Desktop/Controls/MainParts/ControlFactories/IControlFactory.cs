using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{
    public interface IControlFactory
    {
        Task<UserControl> CreateUserControl();
        TabItemViewModel CreateTabItemViewModel();
    }
}
