﻿using System;
using System.Net;
using System.Web.Mvc;
using SeoAnalyzer.Core;
using SeoAnalyzer.Models;

namespace SeoAnalyzer.Controllers
{
    public class AnalyzerController : Controller
    {   
        [HttpGet]
        public ViewResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(AnalysisParameters parameters)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return View("Result", new ContentProcessor().Process(parameters));
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                    {
                        ModelState.AddModelError("Content", "Provided host name can not be resolved");
                    }

                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        ModelState.AddModelError("Content",
                            "No response was received during the time-out period for a request");
                    }
                }

                catch (UriFormatException)
                {
                    ModelState.AddModelError("Content", "Please, insert a correct URI");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("Content", ex.Message);
                }
            }

            return View();
        }
    }
}
