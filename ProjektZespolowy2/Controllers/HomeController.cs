using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjektZespolowy2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjektZespolowy2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();


        public ActionResult Index()
        {
            var currentEmail = User.Identity.GetUserName();
            Profile currentProfile = new Profile();
            if(currentEmail != "")
            {
                foreach(Profile profile in context.Profile)
                {
                    if(profile.Email.Equals(currentEmail))
                    {
                        currentProfile = profile;
                    }
                }

                string currentBrowser = Request.Browser.Browser;

                int browserCount = 0;
                foreach (Browser browser in currentProfile.Browsers)
                {
                    if(currentBrowser == browser.Name)
                    {
                        browserCount++;
                    }
                }
                if(browserCount == 0 && StaticTrusted.trustedBrowser == false)
                {
                    return RedirectToAction("ConfirmBrowser", "Home");
                }

                var macAddr = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
                ).FirstOrDefault();

                int MacCount = 0;
                foreach (MAC myMAC in currentProfile.Macs)
                {
                    if (macAddr == myMAC.MACAdress)
                    {
                        MacCount++;
                    }
                }

                if (MacCount == 0 && StaticTrusted.trustedMac == false)
                {
                    return RedirectToAction("ConfirmMac", "Home");
                }
            }


            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ConfirmBrowser()
        {
            return View();
        }

        public ActionResult AddBrowser()
        {
            var currentEmail = User.Identity.GetUserName();

            Profile currentProfile = new Profile();

            foreach (Profile profile in context.Profile)
            {
                if (profile.Email.Equals(currentEmail))
                {
                    currentProfile = profile;
                }
            }

            Browser browser = new Browser();
            browser.Name = Request.Browser.Browser;

            currentProfile.Browsers.Add(browser);

            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmMac()
        {
            return View();
        }

        public ActionResult AddMac()
        {
            var currentEmail = User.Identity.GetUserName();

            Profile currentProfile = new Profile();

            foreach (Profile profile in context.Profile)
            {
                if (profile.Email.Equals(currentEmail))
                {
                    currentProfile = profile;
                }
            }

            var macAddr = (
               from nic in NetworkInterface.GetAllNetworkInterfaces()
               where nic.OperationalStatus == OperationalStatus.Up
               select nic.GetPhysicalAddress().ToString()
               ).FirstOrDefault();

            MAC mac = new MAC();
            mac.MACAdress = macAddr;

            currentProfile.Macs.Add(mac);

            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DoNotAddBrowser()
        {
            StaticTrusted.trustedBrowser = true;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DoNotAddMac()
        {
            StaticTrusted.trustedMac = true;
            return RedirectToAction("Index", "Home");
        }
    }
}