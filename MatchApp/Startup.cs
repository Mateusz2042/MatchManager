using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Application.Actors;
using Autofac;
using DotNETCore.Repository.Mongo;
using MatchApp.Settings;
using MatchManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace MatchApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //ContainerBuilder builder = new ContainerBuilder();
            //var repoPlayer = builder.RegisterGeneric(typeof(Repository<>))
            //                                            .As(typeof(IRepository<>)).WithParameter("connectionString", Configuration.GetConnectionString("MongoConnection")).SingleInstance();

            //var container = builder.Build();

            var system = ActorSystem.Create("MyActor");
            //var propsResolver = new AutoFacDependencyResolver(container, system);
            var playerActor = system.ActorOf<PlayerActor>();
            var teamActor = system.ActorOf<TeamActor>();
            var matchActor = system.ActorOf<MatchActor>();

            ActorModelWrapper.PlayerActor = playerActor;
            ActorModelWrapper.TeamActor = teamActor;
            ActorModelWrapper.MatchActor = matchActor;

            //services.AddSingleton(typeof(ActorSystem), (ServiceProvider) => actorSystem);


            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "MatchApp Api", Version = "v1"});
            });

            //services.Configure<SettingsConnection>(options =>
            //{
            //    options.ConnectionString
            //        = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            //    options.Database
            //        = Configuration.GetSection("MongoConnection:Database").Value;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MatchApp Api v1");
            });
        }
    }
}
