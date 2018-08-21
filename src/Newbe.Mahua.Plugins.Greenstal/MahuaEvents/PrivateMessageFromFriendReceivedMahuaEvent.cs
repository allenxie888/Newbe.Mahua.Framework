using Newbe.Mahua.Greenstal.IGrains;
using Newbe.Mahua.MahuaEvents;
using Orleans;
using Orleans.Runtime;
using Polly;
using System;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Greenstal.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IClientFactory _clientFactory;

        public PrivateMessageFromFriendReceivedMahuaEvent(
            IMahuaApi mahuaApi,
            IClientFactory clientFactory)
        {
            _mahuaApi = mahuaApi;
            _clientFactory = clientFactory;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            var clusterClient = _clientFactory.GetClient();
            var result = clusterClient.GetGrain<ITestGrain>(context.FromQq).GetId().GetAwaiter().GetResult();
            _mahuaApi.SendPrivateMessage(context.FromQq)
                .Text(result)
                .Done();
        }
    }

    public interface IClientFactory
    {
        IClusterClient GetClient();
    }

    public class ClientFactory : IClientFactory
    {
        private static Func<IClientBuilder> _builderFunc;
        private static IClusterClient _client;
        private static readonly Policy RetryPolicy = Policy.Handle<SiloUnavailableException>()
            .WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(5));
        public static async Task<IClusterClient> Build(Func<IClientBuilder> builderFunc)
        {
            _builderFunc = builderFunc;
            await RetryPolicy.ExecuteAsync(() =>
            {
                _client?.Dispose();
                _client = builderFunc().Build();
                return _client.Connect();
            });
            return _client;
        }

        readonly object _connectLock = new object();

        public IClusterClient GetClient()
        {
            if (!_client.IsInitialized)
            {
                lock (_connectLock)
                {
                    if (!_client.IsInitialized)
                    {
                        RetryPolicy.ExecuteAsync(() =>
                        {
                            _client?.Dispose();
                            _client = _builderFunc().Build();
                            return _client.Connect();
                        }).GetAwaiter().GetResult();
                    }
                }
            }
            return _client;
        }

    }
}
