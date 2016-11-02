﻿using Nancy;
using Nancy.Security;

namespace Api2
{
    public sealed class ApiModule : NancyModule
    {
        public ApiModule(PeopleRepository repository): base("/api")
        {
            this.RequiresAuthentication();    
            Get("/{resource}", args => repository.GetAll(args.resource));
        }
    }
}
