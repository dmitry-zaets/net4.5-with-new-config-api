using Microsoft.Extensions.Options;
using Net45WithNetCoreConfigApi.Configurations.Feed;
using Net45WithNetCoreConfigApi.Configurations.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Net45WithNetCoreConfigApi.Controllers
{
    public class HomeController : Controller
    {
        private MailingOptions _mailingOptions;
        private FeedOptions _feedOptions;

        public HomeController(IOptionsSnapshot<MailingOptions> mailingOptions, IOptions<FeedOptions> feedOptions)
        {
            _mailingOptions = mailingOptions.Value;
            _feedOptions = feedOptions.Value;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.BatchDelay = _mailingOptions.BatchDelay;
            ViewBag.BatchSize = _mailingOptions.BatchSize;
            ViewBag.FeedEndpoints = string.Join(",", _feedOptions.Endpoints.Select(_=>_.Name));
            return View();
        }
    }
}
