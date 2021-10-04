using System;
using Xunit;
using ShittyPromise;

namespace ShittyPromise.Tests
{
    public class PromiseTests
    {
        [Fact]
        public void Should_resolve_syncronous_action()
        {
            var p = new Promise<int>((res, rej) => {
                res(100);
            });

            p.Then<int>(x => {
                Assert.Equal(100, x);
            });
        }
    }
}
