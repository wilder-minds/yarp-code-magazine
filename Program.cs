using DidItAgain.Proxy;
using Yarp.ReverseProxy.Configuration;

var bldr = WebApplication.CreateBuilder(args);

var proxy = bldr.Services.AddReverseProxy();
proxy.LoadFromConfig(bldr.Configuration.GetSection("Yarp"));

// For programmatical definition
// bldr.Services.AddTransient<IProxyConfigProvider, YarpProxyConfigProvider>();
// bldr.Services.AddReverseProxy();

var app = bldr.Build();

app.MapReverseProxy(opt => {
  opt.UseLoadBalancing();
  opt.UseSessionAffinity();
});

app.MapGet("/", () => "Hello World!");

app.Run();
