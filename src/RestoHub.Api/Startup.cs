using RestoHub.Api.Presenters;
using RestoHub.Api.Bootstrap;
using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace RestoHub.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApi(Configuration);

            services.AddTransient<IPresenter, Presenter>();

            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureApi(env);
        }
    }
}
