using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace siteControl.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/bootstrap/js/jquery.slim.min.js",
                "~/Scripts/bootstrap/js/jquery.min.js"));
       
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/tether.min.js",
               "~/Scripts/bootstrap/js/bootstrap.min.js",
               "~/Scripts/bootstrap/js/bootstrap.bundle.min.js",
                "~/Scripts/bootstrap-datepicker.js"));




            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
               "~/Content/bootstrap/css/mystyle.css",
               "~/Content/fonts/font-awesome/css/font-awesome.min.css",
             
               "~/Content/bootstrap/css/sidebar.css"));
           
  
            BundleTable.EnableOptimizations = true;

        }
    }
}