using Newbe.Mahua.Greenstal.IGrains;
using Orleans;
using System.Threading.Tasks;

namespace Newbe.Mahua.Greenstal.Grains
{
    public class TestGrain : Grain, ITestGrain
    {
        public Task<string> GetId()
        {
            return Task.FromResult(this.GetPrimaryKeyString());
        }
    }
}
