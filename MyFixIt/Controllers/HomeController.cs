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

using System.Web.Mvc;
using MyFixIt.Logging;
using Microsoft.ApplicationInsights;

namespace MyFixIt.Controllers
{
    public class HomeController : Controller
    {
        private ILogger logger;

        public HomeController(ILogger log)
        {
            logger = log;
        }
        public ActionResult Index()
        {
            var tc = new TelemetryClient();
            tc.TrackPageView("Tracking Page View");
            tc.TrackEvent("Tacking event");

            logger.Error("write a log thru log4net");
            return View();
        }
    }
}