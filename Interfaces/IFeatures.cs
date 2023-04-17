namespace Interfaces
{
    public interface IFeature
    {
        string Name { get; }
        void Execute();
    }

    public interface IIBrokeFeature
    {
        void IBroke();

        void IBrokeAgain();
    }

    public interface IMonitorFeature
    {
        void Monitor();
    }

    public interface ICallOutFeature
    {
        void CallOut();
    }

    public interface IWiggleFeature
    {
        void Wiggle();
    }

    public interface IGuardFeature
    {
        void Guard();
    }

    public interface IRotateFeature
    {
        void Rotate();
    }
}