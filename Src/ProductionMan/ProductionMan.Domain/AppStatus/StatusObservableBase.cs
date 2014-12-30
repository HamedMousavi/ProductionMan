namespace ProductionMan.Domain.AppStatus
{

    public class StatusObservableBase : IStatusObservable
    {

        private StatusChangedEventArgs _statusChangedEventArgs;
        private static readonly object _instanceLock = new object();


        public event StatusChangedEvent StatusChanged;


        //protected virtual void FireStatusChanged(StatusChangedEventArgs e)
        //{
        //    var handler = StatusChanged;
        //    if (handler != null) handler(this, e);
        //}


        protected virtual void FireStatusChanged(Status.Levels level, string message)
        {
            lock (_instanceLock)
            {

                if (_statusChangedEventArgs == null)
                {
                    _statusChangedEventArgs = new StatusChangedEventArgs(new Status());
                }

                _statusChangedEventArgs.NewStatus.Level = level;
                _statusChangedEventArgs.NewStatus.Message = message;

                var handler = StatusChanged;
                if (handler != null) handler(this, _statusChangedEventArgs);
            }
        }
    }
}
