﻿using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IList<Scope> _scopes = new List<Scope> {
            new Scope { Name="api1", Description="Api Application Claim", Type = ScopeType.Resource }
        };
        
        private readonly List<Client> _clients = new List<Client> {
            new Client {
                ClientId = "client1",  AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {  new Secret("secret".Sha256()) },
                Claims = new List<Claim> { new Claim( "educationContext", "Admin@email.com") },
                AllowedScopes = { "api1" }
            },
            new Client {
                ClientId = "client2",  AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {  new Secret("secret".Sha256()) },
                Claims = new List<Claim> { new Claim( "educationContext", "AliceSmith@email.com") },
                AllowedScopes = { "api1" }
            },
            new Client {
                ClientId = "client3",  AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {  new Secret("secret".Sha256()) },
                Claims = new List<Claim> { new Claim( "educationContext", "BobSmith@email.com") },
                AllowedScopes = { "api1" }
            }
        };
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDeveloperIdentityServer()
                .AddInMemoryClients(_clients)
                .AddInMemoryScopes(_scopes);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();
            app.UseIdentityServer();
        }
    }
}
