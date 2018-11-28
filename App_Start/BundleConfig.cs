using System.Web;
using System.Web.Optimization;

namespace ThePublicNoteBook
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                        "~/Scripts/ckeditor/ckeditor.js"));
            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                        "~/Scripts/DataTables/datatables.js"));
            bundles.Add(new ScriptBundle("~/bundles/ckfinder").Include(
                       "~/Scripts/ckfinder/ckfinder.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                      "~/Assets/plugins/jquery-datatable/jquery.dataTables.js",
                      "~/Assets/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/dataTables.buttons.min.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/buttons.flash.min.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/jszip.min.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/pdfmake.min.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/vfs_fonts.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/buttons.html5.min.js",
                      "~/Assets/plugins/jquery-datatable/extensions/export/buttons.print.min.js"
                      ));



        }
    }
}
