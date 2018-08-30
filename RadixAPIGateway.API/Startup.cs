using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadixAPIGateway.Data.Context;

namespace RadixAPIGateway.API
{
    public class Startup
    {
        //private const string corsPolicyName = "RadixCorsPolicy";

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connection = @"Server=(localdb)\mssqllocaldb;Database=RadixDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<EFContext>(options => options.UseSqlServer(connection));

            IoC.IoCConfiguration.Configure(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            ////app.UseCors(corsPolicyName);

            //app.UseStaticFiles();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<EFContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<EFContext>().Seed();
            }

            app.UseMvc();
        }
    }
}
