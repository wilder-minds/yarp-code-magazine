using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace DidItAgain.Proxy;

public class YarpProxyConfig : IProxyConfig
{
  private readonly List<RouteConfig> _routes;
  private readonly List<ClusterConfig> _clusters;
  private readonly CancellationChangeToken _changeToken;
  private readonly CancellationTokenSource _cts = new CancellationTokenSource();

  public YarpProxyConfig()
  {
    _changeToken = new CancellationChangeToken(_cts.Token);
    _routes = GenerateRoutes();
    _clusters = GenerateClusters();
  }

  public IReadOnlyList<RouteConfig> Routes => _routes;
  public IReadOnlyList<ClusterConfig> Clusters => _clusters;
  public IChangeToken ChangeToken => _changeToken;

  private List<ClusterConfig> GenerateClusters()
  {
    var collection = new List<ClusterConfig>();
    collection.Add(new ClusterConfig()
    {
      ClusterId = "FirstCluster",
      Destinations = new Dictionary<string, DestinationConfig>{
          {
            "server",
            new DestinationConfig()
            {
              Address = "https://someserver.com"
            }
          }
        }
    });
    return collection;
  }

  private List<RouteConfig> GenerateRoutes()
  {
    var collection = new List<RouteConfig>();
    collection.Add(new RouteConfig()
    {
      ClusterId = "FirstCluster",
      Match = new RouteMatch()
      {
        Path = "/api/foo/{**catch-all}"
      }
    });

    return collection;
  }
}