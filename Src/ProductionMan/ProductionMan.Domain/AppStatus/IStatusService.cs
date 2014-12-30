using System.ComponentModel;

namespace ProductionMan.Domain.AppStatus
{
    public interface IStatusService : INotifyPropertyChanged, IStatusObserver
    {

        void SetStatus(Status.Levels level, string message);


        Status Status { get; }
    }
}
