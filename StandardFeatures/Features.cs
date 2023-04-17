

using Interfaces;

namespace StandardFeatures
{
    public class MonitorFeature : IMonitorFeature, IFeature
    {
        public string Name => "MonitorFeature";
        public void Execute()
        {
            Monitor();
        }
        public void Monitor()
        {
            Console.WriteLine("Monitor feature");
        }
    }

    public class CallOutFeature : ICallOutFeature, IFeature
    {
        public string Name => "CallOut Feature";

        public void CallOut()
        {
            Console.WriteLine("Callout feature");
        }

        public void Execute()
        {
            CallOut();
        }
    }

    public class IBrokeFeature : IIBrokeFeature, IFeature
    {
        public string Name => "IBrokeFeature";

        public void Execute()
        {
            IBroke();
            IBrokeAgain();
        }

        public void IBroke()
        {
            Console.WriteLine("I broke.");
        }

        public void IBrokeAgain()
        {
            Console.WriteLine("...again");
        }
    }
}