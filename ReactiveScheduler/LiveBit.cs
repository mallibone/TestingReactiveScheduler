using System;
using System.Reactive.Linq;
using System.Timers;

namespace ReactiveScheduler
{
    public class LiveBit : IDisposable
    {
        private readonly INetworkInterface _networkInterface;
        private readonly Timer _timer;

        public LiveBit(INetworkInterface networkInterface)
        {
            _networkInterface = networkInterface;
            _timer = new Timer(1000);
            _timer.Elapsed += SendLiveBit;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        private void SendLiveBit(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Send Live Bit");
            var liveBit = new byte[]{0xAA}; // Some imaginative payload
            _networkInterface.Send(liveBit);
        }
    }

    public class ReactiveLiveBit : IDisposable
    {
        private readonly INetworkInterface _networkInterface;
        private readonly IDisposable _timer;

        public ReactiveLiveBit(INetworkInterface networkInterface)
        {
            _networkInterface = networkInterface;
            _timer = Observable
                .Interval(TimeSpan.FromSeconds(1))
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
