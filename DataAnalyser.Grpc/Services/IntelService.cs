using System.Collections.Generic;
using System.Threading.Tasks;
using DataAnalyser.Service;
using Grpc.Core;

namespace DataAnalyser.Grpc.Services
{
    public class IntelService : IntelData.IntelDataBase
    {
        private readonly IDataService _dataService;

        public IntelService(IDataService dataService)
        {
            _dataService = dataService;
        }

        
        public override async Task<IntelDataCollection> GetIntel(Keyword request, ServerCallContext context)
        {
            var list = new List<Intel>();
            var ret = await _dataService.Collect(request.Name);
            ret.ForEach(i => list.Add(new Intel(){Message = i.Description}));
            var reply = new IntelDataCollection(){IntelData = {list}};
            return await Task.FromResult(reply);
        }
    }
}