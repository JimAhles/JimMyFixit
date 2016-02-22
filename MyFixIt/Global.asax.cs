//
// Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using MyFixIt.App_Start;
using MyFixIt.Logging;
using MyFixIt.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading.Tasks;

namespace MyFixIt
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            DependenciesConfig.RegisterDependencies();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            PhotoService photoService = new PhotoService(DependencyResolver.Current.GetService<ILogger>());
            // jima changed CreateAndConfigureAsync from a async void to async Task
            // this is how you should 'handle' exceptions thrown from an async method when calling async and not using await
            // i.e. this method is creating the storage resource and can run in background.
            var t = photoService.CreateAndConfigureAsync();
            t.ContinueWith(ta =>
            {
                // won't see a popup!
               foreach(var exception in ta.Exception.Flatten().InnerExceptions)
                {
                    // already been logged - need to convey to user - no access to ui here
                    throw new Exception(exception.Message);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
            DbConfiguration.SetConfiguration(new MyFixit.FixitTaskEntity.EFConfiguration());
        }
    }
}
