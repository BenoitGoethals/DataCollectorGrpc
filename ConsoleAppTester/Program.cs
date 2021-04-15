using System;
using System.Threading.Tasks;
using DataAnalyser.Grpc;
using Grpc.Net.Client;

namespace ConsoleAppTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new IntelData.IntelDataClient(channel);
            var ret = await client.GetIntelAsync(new Keyword(){Name = "Trump"});

            foreach (var intel in ret.IntelData)
            {
                Console.WriteLine(intel.Message);
            }
            

            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}