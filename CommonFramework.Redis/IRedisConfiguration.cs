using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Core.Dependency;
using StackExchange.Redis;
using System.Net;
using System.Security.Authentication;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Redis
{
    public interface IRedisConfiguration : ISingletonDependency
    {
        void setConfigurationOption(ConfigurationOptions option);


        ConfigurationOptions getConfigurationOption();


        //
        // 摘要:
        //     Gets or sets whether connect/configuration timeouts should be explicitly notified
        //     via a TimeoutException
        IRedisConfiguration AbortOnConnectFail(bool AbortOnConnectFail);

        //
        // 摘要:
        //     Indicates whether admin operations should be allowed
        IRedisConfiguration AllowAdmin(bool AllowAdmin);

        //
        // 摘要:
        //     Automatically encodes and decodes channels
        IRedisConfiguration ChannelPrefix(RedisChannel ChannelPrefix);

        //
        // 摘要:
        //     The client name to use for all connections
        IRedisConfiguration ClientName(string ClientName);

        //
        // 摘要:
        //     The command-map associated with this configuration
        IRedisConfiguration CommandMap(CommandMap CommandMap);

        //
        // 摘要:
        //     Check configuration every n seconds (every minute by default)
        IRedisConfiguration ConfigCheckSeconds(int ConfigCheckSeconds);

        //
        // 摘要:
        //     Channel to use for broadcasting and listening for configuration change notification
        IRedisConfiguration ConfigurationChannel(string ConfigurationChannel);
        //
        // 摘要:
        //     The number of times to repeat the initial connect cycle if no servers respond
        //     promptly
        IRedisConfiguration ConnectRetry(int ConnectRetry);

        //
        // 摘要:
        //     Specifies the time in milliseconds that should be allowed for connection (defaults
        //     to 5 seconds unless SyncTimeout is higher)
        IRedisConfiguration ConnectTimeout(int ConnectTimeout);

        //
        // 摘要:
        //     Specifies the default database to be used when calling ConnectionMultiplexer.GetDatabase()
        //     without any parameters
        IRedisConfiguration DefaultDatabase(int? DefaultDatabase);

        //
        // 摘要:
        //     The server version to assume
        IRedisConfiguration DefaultVersion(Version DefaultVersion);


        //
        // 摘要:
        //     Adds a new endpoint to the list
        IRedisConfiguration AddEndPoints(string hostAndPort);

        //
        // 摘要:
        //     Adds a new endpoint to the list
        IRedisConfiguration AddEndPoints(string host, int port = 6379);

        //
        // 摘要:
        //     Adds a new endpoint to the list
        IRedisConfiguration AddEndPoints(IPAddress host, int port = 6379);

        //
        // 摘要:
        //     The endpoints defined for this configuration
        IRedisConfiguration AddEndPoints(EndPoint EndPoint);

        //
        // 摘要:
        //     Use ThreadPriority.AboveNormal for SocketManager reader and writer threads (true
        //     by default). If false, ThreadPriority.Normal will be used.
        IRedisConfiguration HighPrioritySocketThreads(bool HighPrioritySocketThreads);

        //
        // 摘要:
        //     Specifies the time in seconds at which connections should be pinged to ensure
        //     validity
        IRedisConfiguration KeepAlive(int KeepAlive);

        //
        // 摘要:
        //     The password to use to authenticate with the server
        IRedisConfiguration Password(string Password);

        //
        // 摘要:
        //     Type of proxy to use (if any); for example Proxy.Twemproxy
        IRedisConfiguration Proxy(Proxy Proxy);

        //
        // 摘要:
        //     The retry policy to be used for connection reconnects
        IRedisConfiguration ReconnectRetryPolicy(IReconnectRetryPolicy ReconnectRetryPolicy);

        //
        // 摘要:
        //     Indicates whether endpoints should be resolved via DNS before connecting. If
        //     enabled the ConnectionMultiplexer will not re-resolve DNS when attempting to
        //     re-connect after a connection failure.
        IRedisConfiguration ResolveDns(bool ResolveDns);

        //
        // 摘要:
        //     Specifies the time in milliseconds that the system should allow for responses
        //     before concluding that the socket is unhealthy (defaults to SyncTimeout)
        IRedisConfiguration ResponseTimeout(int ResponseTimeout);

        //
        // 摘要:
        //     The service name used to resolve a service via sentinel
        IRedisConfiguration ServiceName(string ServiceName);

        //
        // 摘要:
        //     Gets or sets the SocketManager instance to be used with these options; if this
        //     is null a per-multiplexer SocketManager is created automatically.
        IRedisConfiguration SocketManager(SocketManager SocketManager);

        //
        // 摘要:
        //     Indicates whether the connection should be encrypted
        IRedisConfiguration Ssl(bool Ssl);

        //
        // 摘要:
        //     The target-host to use when validating SSL certificate; setting a value here
        //     enables SSL mode
        IRedisConfiguration SslHost(string SslHost);

        //
        // 摘要:
        //     Configures which Ssl/TLS protocols should be allowed. If not set, defaults are
        //     chosen by the .NET framework.
        IRedisConfiguration SslProtocols(SslProtocols? SslProtocols);

        //
        // 摘要:
        //     Specifies the time in milliseconds that the system should allow for synchronous
        //     operations (defaults to 1 second)
        IRedisConfiguration SyncTimeout(int SyncTimeout);

        //
        // 摘要:
        //     Tie-breaker used to choose between masters (must match the endpoint exactly)
        IRedisConfiguration TieBreaker(string TieBreaker);

        //
        // 摘要:
        //     The size of the output buffer to use
        IRedisConfiguration WriteBuffer(int WriteBuffer);

    }
}
