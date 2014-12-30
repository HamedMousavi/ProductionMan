using System;

namespace ProductionMan.Domain.AppStatus
{

    public class StatusChangedEventArgs : EventArgs
    {
        public StatusChangedEventArgs(Status status)
        {
            NewStatus = status;
        }

        public Status NewStatus { get; set; }
    }


    public delegate void StatusChangedEvent(object sender, StatusChangedEventArgs e);


    public interface IStatusObservable
    {
        event StatusChangedEvent StatusChanged;
    }
}
