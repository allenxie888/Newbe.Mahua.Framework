using Orleans;
using System.Threading.Tasks;

namespace Newbe.Mahua.Greenstal.IGrains
{
    public interface ITestGrain : IGrainWithStringKey
    {
        Task<string> GetId();
    }
}
