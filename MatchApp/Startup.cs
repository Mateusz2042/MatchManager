﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Routing;
using Application;
using Application.Actors;
using Autofac;
using DotNETCore.Repository.Mongo;
using Hangfire;
using Hangfire.Mongo;
using Infrastructure.Redis;
using MatchApp.Settings;
using MatchManager.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace MatchApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "MatchApp Api", Version = "v1" });
            });

            services.AddResponseCaching();

            services.AddHangfire(config =>
            config.UseMongoStorage("mongodb://localhost:27017/MatchDatabase", "MatchDatabase"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowApi",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowApi"));
            });

            services.AddTransient<IRediseDataAgent, RedisDataAgent>();

            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "127.0.0.1";
            //    option.InstanceName = "matchapp";
            //});

            //ContainerBuilder builder = new ContainerBuilder();
            //var repoPlayer = builder.RegisterGeneric(typeof(Repository<>))
            //                                            .As(typeof(IRepository<>)).WithParameter("connectionString", Configuration.GetConnectionString("MongoConnection")).SingleInstance();

            //var container = builder.Build();


            //-------------------Remote Akka----------------------

            var builderHocon = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");

            var configuration = builderHocon.Build();

            string configurationAkka = configuration.GetSection("configurationAkka").Value;

            var configHocon = ConfigurationFactory.ParseString(@"akka {  
                actor {
                    provider = remote
                }
                remote {
                    dot-netty.tcp {
                        port = 0 # bound to a dynamic port assigned by the OS
                        hostname = localhost
                    }
                }
                loglevel=INFO,  
                loggers=[""Akka.Logger.Serilog.SerilogLogger, Akka.Logger.Serilog""]
            }");

            var system = ActorSystem.Create("ActorSystem", configHocon);

            var timeOut = TimeSpan.FromSeconds(30);

            var playerActor = await system.ActorSelection("akka.tcp://RemoteActorSystem@localhost:8090/user/playerActor").ResolveOne(timeOut);
            var teamActor = await system.ActorSelection("akka.tcp://RemoteActorSystem@localhost:8090/user/teamActor").ResolveOne(timeOut);
            var matchActor = await system.ActorSelection("akka.tcp://RemoteActorSystem@localhost:8090/user/matchActor").ResolveOne(timeOut);
            
            ActorModelWrapper.PlayerActor = playerActor;
            ActorModelWrapper.TeamActor = teamActor;
            ActorModelWrapper.MatchActor = matchActor;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseResponseCaching();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MatchApp Api v1");
            });

            app.UseCors("AllowApi");

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
