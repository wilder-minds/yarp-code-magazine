using Yarp.ReverseProxy.Configuration;

namespace DidItAgain.Proxy;

public class YarpProxyConfigProvider
  : IProxyConfigProvider
{
  public IProxyConfig GetConfig()
  {
    return new YarpProxyConfig();
  }

}