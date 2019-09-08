using System.Collections.Generic;

namespace ReactiveScheduler
{
    public interface INetworkInterface
    {
        void Send(IEnumerable<byte> Payload);
    }
}
