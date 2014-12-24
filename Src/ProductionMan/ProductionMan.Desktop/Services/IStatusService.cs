using System.ComponentModel;


namespace ProductionMan.Desktop.Services
{

    public interface IStatusService : INotifyPropertyChanged
    {

        void SetStatus(Status.Levels level, string message);


        Status Status { get; }
    }
}
