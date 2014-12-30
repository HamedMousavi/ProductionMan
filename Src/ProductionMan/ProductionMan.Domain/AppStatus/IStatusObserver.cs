
namespace ProductionMan.Domain.AppStatus
{
    public interface IStatusObserver
    {
        void Register(IStatusObservable statusObservable);
    }
}
