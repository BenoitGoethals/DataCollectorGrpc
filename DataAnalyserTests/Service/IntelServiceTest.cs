using DataAnalyser.Grpc;
using FluentAssertions;
using Xunit;
using XUnitTestProjectServiceUsers.Fixture;

namespace DataAnalyserTests.Service
{
    public class IntelServiceTest: FunctionalTestBase
    {
        public IntelServiceTest(GrpcServerFactory<Startup> factory) : base(factory)
        {
        }
        
        [Fact()]
        public async void GetBooksTest()
        {
            var client = new IntelData.IntelDataClient(_channel);
            var ret = await client.GetIntelAsync(new Keyword(){Name = "Trump"});
            client.Should().NotBeNull();
            ret.IntelData.Should().NotBeNull();
        }
    }
}