using System.Collections.Generic;
using ProductionMan.Common;

namespace ProductionMan.Domain.AppStatus
{

    public class DefaultStatusService : NotifyPropertyChanged, IStatusService
    {

        private readonly List<IStatusObservable> _observers;


        public DefaultStatusService()
        {
            _observers = new List<IStatusObservable>();
            Status = new Status();
        }


        public void Register(IStatusObservable statusObservable)
        {
            if (!_observers.Contains(statusObservable))
            {
                _observers.Add(statusObservable);
                statusObservable.StatusChanged += delegate(object sender, StatusChangedEventArgs e)
                {
                    if (e.NewStatus != null) SetStatus(e.NewStatus.Level, e.NewStatus.Message);
                };
            }
        }


        public void SetStatus(Status.Levels level, string message)
        {
            Status.Level = level;
            Status.Message = message;
            FirePropertyChanged(this, "Status");
        }


        public Status Status { get; private set; }
    }
}
