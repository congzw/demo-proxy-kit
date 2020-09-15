using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProxyKit;

namespace ProxySite
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

            //todo：by config
            //ProxySite is a proxy site: "http://localhost:1950"
            //OriginalSite is a demo upstream site: "http://localhost:1956"
            var upstreamServerHost = "http://localhost:1956";
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
