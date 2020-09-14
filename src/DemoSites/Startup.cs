using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProxyKit;

namespace DemoSites
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProxy();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //proxy sites: http://localhost:5066
            var upstreamServerHost = "http://localhost:5136";
            //todo config
            app.RunProxy(context => context
                .ForwardTo(upstreamServerHost)
                .AddXForwardedHeaders()
                .Send());

            //app.UseWebSockets();
            //app.UseWebSocketProxy(
            //    context => new Uri("ws://upstream-host:80/"),
            //    options => options.AddXForwardedHeaders());
        }
    }
}
