using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace ReactiveScheduler.Test
{
    public class LiveBitTest
    {
        [Fact]
        public async Task LiveBit_WhenCreated_TheSendMethodWillBeInvokedAfter1Second()
        {
            // Arrange: Given a mock interface ...
            int counter = 0;
            // Using Moq https://www.nuget.org/packages/Moq/
            var networkInterfaceMock = new Mock<INetworkInterface>();
            // Write a function that counts the number of invocations
            networkInterfaceMock
                .Setup(n => n.Send(It.IsAny<IEnumerable<Byte>>()))
                .Callback(() => counter++);
            // Act: ... start the livebit service and wait for a good second ...
            var liveBit = new LiveBit(networkInterfaceMock.Object);
            await Task.Delay(TimeSpan.FromMilliseconds(1100));
            // Assert: ... the mocked send method has been invoked once.
            Assert.Equal(1, counter);
        }
    }
}
