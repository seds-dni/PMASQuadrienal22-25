using System.Web;
using System.Web.Optimization;

namespace Seds.PMAS.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
             "~/Scripts/Vendors/modernizr.js"));


            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
             "~/Scripts/Vendors/jquery.js",
             "~/Scripts/Vendors/jquery.mask.js",
             "~/Scripts/Vendors/angular.js",
              "~/Scripts/Vendors/angular.min.js",
             "~/Scripts/Vendors/angular-route.js",
             "~/Scripts/Vendors/angular-cookies.js",
             "~/Scripts/Vendors/angular-messages.js",
             "~/Scripts/Vendors/angular-resource.js",
             "~/Scripts/Vendors/ui-bootstrap-tpls-0.12.0.js",
             "~/Scripts/Vendors/ui-bootstrap-tpls.min.js",
             "~/Scripts/Vendors/select2.min.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/widgets").Include(
              "~/Scripts/widgets/metro.js"
                 ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                 "~/Scripts/bootstrapMenu/tether.js",
                 "~/Scripts/bootstrapMenu/bootstrap-min.js"
                 ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
               "~/Scripts/spa/modules/common.core.js",
               "~/Scripts/spa/modules/common.ui.js",
               "~/Scripts/spa/pmas.js",
                "~/Scripts/spa/services/loginService.js",
                 "~/Scripts/spa/services/authenticationService.js",
                 "~/Scripts/spa/services/prefeitoService.js",
               "~/Scripts/spa/services/prefeitoService.js",
               "~/Scripts/spa/services/usuarioService.js",
               "~/Scripts/spa/home/homeController.js",
               "~/Scripts/spa/home/menuController.js",
               "~/Scripts/spa/prefeitura/prefeituraController.js",
                 "~/Scripts/spa/prefeitura/prefeitoController.js"
               ));


            bundles.Add(new StyleBundle("~/Content/bootstrap")
           .Include("~/Content/BootstrapMenu/bootstrap.css",
                        "~/Content/BootstrapMenu/bootstrap-min.css"
           ));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site/site.css"));

            bundles.Add(new StyleBundle("~/Content/metro")
                .Include("~/Content/metro/css/metro-icons.css",
                            "~/Content/metro/css/style.css"

                ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}