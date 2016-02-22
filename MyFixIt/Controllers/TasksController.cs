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

using MyFixIt.Persistence;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using MyFixIt.Logging;
using MyFixit.FixitTaskEntity;
using Microsoft.ApplicationInsights;

namespace MyFixIt.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ILogger log;
        private IFixItTaskRepository fixItRepository = null;
        private IPhotoService photoService = null;
        private IFixItQueueManager queueManager = null;

        public TasksController(IFixItTaskRepository repository, IPhotoService photoStore, IFixItQueueManager queueManager, ILogger log)
        {
            fixItRepository = repository;
            photoService = photoStore;
            this.queueManager = queueManager;
            this.log = log;
            log.Error(" in constructor");
        }

        // GET: /FixIt/
        public async Task<ActionResult> Status()
        {
            var properties = new Dictionary<string, string>
   {{"texttest1", "some test stuff"}, {"secondtexttest", "random stuff"}};
            var measurements = new Dictionary<string, double>
   {{"testnumber1", 312}, {"numbertest2", 17}};

            // Send the event:
            var telemetry = new TelemetryClient();
            telemetry.TrackEvent("AppInsightEventTest", properties, measurements);
            string currentUser = User.Identity.Name;
            var result = await fixItRepository.FindTasksByCreatorAsync(currentUser);
            
            return View(result);
        }

        //
        // GET: /Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FixItTaskId,CreatedBy,Owner,Title,Notes,PhotoUrl,IsDone")]FixItTask fixittask, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                fixittask.CreatedBy = User.Identity.Name;
                fixittask.PhotoUrl = await photoService.UploadPhotoAsync(photo);

                if (ConfigurationManager.AppSettings["UseQueues"] == "true")
                {
                    await queueManager.SendMessageAsync(fixittask);
                }
                else
                {
                    await fixItRepository.CreateAsync(fixittask);
                }

                return RedirectToAction("Success");
            }

            return View(fixittask);
        }

        //
        // GET: /Tasks/Success
        public ActionResult Success()
        {
            return View();
        }
    }
}