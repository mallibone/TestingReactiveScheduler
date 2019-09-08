using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using Moq;
using Xunit;

namespace ReactiveScheduler.Test
{
    public class ReactiveLiveBitTest
    {
        [Fact]
        public async Task ReactiveLiveBit_WhenCreated_TheSendMethodWillBeInvokedAfter1Second()
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
            var liveBit = new ReactiveLiveBit(networkInterfaceMock.Object);
            await Task.Delay(TimeSpan.FromMilliseconds(1100));
            // Assert: ... the mocked send method has been invoked once.
            Assert.Equal(1, counter);
        }

    [Fact]
    public void ReactiveLiveBit_WhenCreatedWithATestScheduler_TheSendMethodWillBeInvoked1TestSecond()
    {
        // Arrange: Given a mock interface ...
        int counter = 0;
        // Using Moq https://www.nuget.org/packages/Moq/
        var networkInterfaceMock = new Mock<INetworkInterface>();
        // Write a function that counts the number of invocations
        networkInterfaceMock
            .Setup(n => n.Send(It.IsAny<IEnumerable<Byte>>()))
            .Callback(() => counter++);
        var testScheduler = new TestScheduler();
        // Act: ... start the livebit service and wait for a good second ...
        var liveBit = new ReactiveLiveBit(networkInterfaceMock.Object, testScheduler);
        testScheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        // Assert: ... the mocked send method has been invoked once.
        Assert.Equal(1, counter);
    }
    }
}
