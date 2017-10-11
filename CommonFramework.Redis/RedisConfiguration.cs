using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Security.Authentication;
using System.Net;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Redis
{
    public class RedisConfiguration:IRedisConfiguration
    {
        private ConfigurationOptions _option = new ConfigurationOptions();
        public void setConfigurationOption(ConfigurationOptions option) {
            _option = option.Clone();
        }

        public ConfigurationOptions getConfigurationOption() {
            return _option;
        }
         
        //
        // 摘要:
        //     Gets or sets whether connect/configuration timeouts should be explicitly notified
        //     via a TimeoutException
        public IRedisConfiguration AbortOnConnectFail(bool AbortOnConnectFail) {
            _option.AbortOnConnectFail = AbortOnConnectFail;
            return this;
        }
        //
        // 摘要:
        //     Indicates whether admin operations should be allowed
        public IRedisConfiguration AllowAdmin(bool AllowAdmin) {
            _option.AllowAdmin = AllowAdmin;
            return this;
        }
        //
        // 摘要:
        //     Automatically encodes and decodes channels
        public IRedisConfiguration ChannelPrefix(RedisChannel ChannelPrefix)
        {
            _option.ChannelPrefix = ChannelPrefix;
            return this;
        }
        //
        // 摘要:
        //     The client name to use for all connections
        public IRedisConfiguration ClientName(string ClientName)
        {
            _option.ClientName = ClientName;
            return this;
        }
        //
        // 摘要:
        //     The command-map associated with this configuration
        public IRedisConfiguration CommandMap(CommandMap CommandMap)
        {
            _option.CommandMap = CommandMap;
            return this;
        }
        //
        // 摘要:
        //     Check configuration every n seconds (every minute by default)
        public IRedisConfiguration ConfigCheckSeconds(int ConfigCheckSeconds)
        {
            _option.ConfigCheckSeconds = ConfigCheckSeconds;
            return this;
        }
        //
        // 摘要:
        //     Channel to use for broadcasting and listening for configuration change notification
        public IRedisConfiguration ConfigurationChannel(string ConfigurationChannel)
        {
            _option.ConfigurationChannel = ConfigurationChannel;
            return this;
        }
        //
        // 摘要:
        //     The number of times to repeat the initial connect cycle if no servers respond
        //     promptly
        public IRedisConfiguration ConnectRetry(int ConnectRetry)
        {
            _option.ConnectRetry = ConnectRetry;
            return this;
        }
        //
        // 摘要:
        //     Specifies the time in milliseconds that should be allowed for connection (defaults
        //     to 5 seconds unless SyncTimeout is higher)
        public IRedisConfiguration ConnectTimeout(int ConnectTimeout) {
            _option.ConnectTimeout = ConnectTimeout;
            return this;
        }
        //
        // 摘要:
        //     Specifies the default database to be used when calling ConnectionMultiplexer.GetDatabase()
        //     without any parameters
        public IRedisConfiguration DefaultDatabase(int? DefaultDatabase)
        {
            _option.DefaultDatabase = DefaultDatabase;
            return this;
        }
        //
        // 摘要:
        //     The server version to assume
        public IRedisConfiguration DefaultVersion(Version DefaultVersion)
        {
            _option.DefaultVersion = DefaultVersion;
            return this;
        }

        //
        // 摘要:
        //     Adds a new endpoint to the list
        public IRedisConfiguration AddEndPoints(string hostAndPort) {
            _option.EndPoints.Add(hostAndPort);
            return this;
        }
        //
        // 摘要:
        //     Adds a new endpoint to the list
        public IRedisConfiguration AddEndPoints(string host, int port=6379) {
            _option.EndPoints.Add(host, port);
            return this;
        }
        //
        // 摘要:
        //     Adds a new endpoint to the list
        public IRedisConfiguration AddEndPoints(IPAddress host, int port = 6379) {
            _option.EndPoints.Add(host,port);
            return this;
        }
        //
        // 摘要:
        //     The endpoints defined for this configuration
        public IRedisConfiguration AddEndPoints(EndPoint EndPoint)
        {
            _option.EndPoints.Add(EndPoint);
            return this;
        }
        //
        // 摘要:
        //     Use ThreadPriority.AboveNormal for SocketManager reader and writer threads (true
        //     by default). If false, ThreadPriority.Normal will be used.
        public IRedisConfiguration HighPrioritySocketThreads(bool HighPrioritySocketThreads)
        {
            _option.HighPrioritySocketThreads = HighPrioritySocketThreads;
            return this;
        }
        //
        // 摘要:
        //     Specifies the time in seconds at which connections should be pinged to ensure
        //     validity
        public IRedisConfiguration KeepAlive(int KeepAlive)
        {
            _option.KeepAlive = KeepAlive;
            return this;
        }
        //
        // 摘要:
        //     The password to use to authenticate with the server
        public IRedisConfiguration Password(string Password)
        {
            _option.Password = Password;
            return this;
        }
        //
        // 摘要:
        //     Type of proxy to use (if any); for example Proxy.Twemproxy
        public IRedisConfiguration Proxy(Proxy Proxy)
        {
            _option.Proxy = Proxy;
            return this;
        }
        //
        // 摘要:
        //     The retry policy to be used for connection reconnects
        public IRedisConfiguration ReconnectRetryPolicy(IReconnectRetryPolicy ReconnectRetryPolicy)
        {
            _option.ReconnectRetryPolicy = ReconnectRetryPolicy;
            return this;
        }
        //
        // 摘要:
        //     Indicates whether endpoints should be resolved via DNS before connecting. If
        //     enabled the ConnectionMultiplexer will not re-resolve DNS when attempting to
        //     re-connect after a connection failure.
        public IRedisConfiguration ResolveDns(bool ResolveDns)
        {
            _option.ResolveDns = ResolveDns;
            return this;
        }
        //
        // 摘要:
        //     Specifies the time in milliseconds that the system should allow for responses
        //     before concluding that the socket is unhealthy (defaults to SyncTimeout)
        public IRedisConfiguration ResponseTimeout(int ResponseTimeout)
        {
            _option.ResponseTimeout = ResponseTimeout;
            return this;
        }
        //
        // 摘要:
        //     The service name used to resolve a service via sentinel
        public IRedisConfiguration ServiceName(string ServiceName)
        {
            _option.ServiceName = ServiceName;
            return this;
        }
        //
        // 摘要:
        //     Gets or sets the SocketManager instance to be used with these options; if this
        //     is null a per-multiplexer SocketManager is created automatically.
        public IRedisConfiguration SocketManager(SocketManager SocketManager)
        {
            _option.SocketManager = SocketManager;
            return this;
        }
        //
        // 摘要:
        //     Indicates whether the connection should be encrypted
        public IRedisConfiguration Ssl(bool Ssl)
        {
            _option.Ssl = Ssl;
            return this;
        }
        //
        // 摘要:
        //     The target-host to use when validating SSL certificate; setting a value here
        //     enables SSL mode
        public IRedisConfiguration SslHost(string SslHost)
        {
            _option.SslHost = SslHost;
            return this;
        }
        //
        // 摘要:
        //     Configures which Ssl/TLS protocols should be allowed. If not set, defaults are
        //     chosen by the .NET framework.
        public IRedisConfiguration SslProtocols(SslProtocols? SslProtocols)
        {
            _option.SslProtocols = SslProtocols;
            return this;
        }
        //
        // 摘要:
        //     Specifies the time in milliseconds that the system should allow for synchronous
        //     operations (defaults to 1 second)
        public IRedisConfiguration SyncTimeout(int SyncTimeout)
        {
            _option.SyncTimeout = SyncTimeout;
            return this;
        }
        //
        // 摘要:
        //     Tie-breaker used to choose between masters (must match the endpoint exactly)
        public IRedisConfiguration TieBreaker(string TieBreaker)
        {
            _option.TieBreaker = TieBreaker;
            return this;
        } 
        //
        // 摘要:
        //     The size of the output buffer to use
        public IRedisConfiguration WriteBuffer(int WriteBuffer)
        {
            _option.WriteBuffer = WriteBuffer;
            return this;
        }
    }
}
