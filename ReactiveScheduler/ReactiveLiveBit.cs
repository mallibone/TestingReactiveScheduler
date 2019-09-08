using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace ReactiveScheduler
{
    public class ReactiveLiveBit : IDisposable
    {
        private readonly INetworkInterface _networkInterface;
        private readonly IDisposable _timer;

public ReactiveLiveBit(INetworkInterface networkInterface, IScheduler scheduler = null)
{
    var timerScheduler = scheduler ?? Scheduler.Default;
    _networkInterface = networkInterface;
    _timer = Observable
        .Interval(TimeSpan.FromSeconds(1), timerScheduler)
        .Subscribe(x => SendLiveBit());
}

        public void Dispose()
        {
            _timer.Dispose();
        }

        private void SendLiveBit()
        {
            Console.WriteLine("Send Live Bit");
            var liveBit = new byte[]{0xAA}; // Some imaginative payload
            _networkInterface.Send(liveBit);
        }
    }
}
