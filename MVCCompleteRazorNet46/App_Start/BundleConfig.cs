using System.Web;
using System.Web.Optimization;

namespace QuestionarioCOrg
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region bundle admin
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/fontawesome.js",
                        "~/assets/js/plugins/metisMenu/jquery.metisMenu.js",
                        "~/assets/js/plugins/slimscroll/jquery.slimscroll.min.js",
                        "~/assets/js/plugins/jeditable/jquery.jeditable.js",
                        "~/assets/js/plugins/peity/jquery.peity.min.js",
                        "~/assets/js/plugins/dataTables/jquery.dataTables.js",
                        "~/assets/js/plugins/dataTables/dataTables.bootstrap.js",
                        "~/assets/js/plugins/dataTables/dataTables.responsive.js",
                        "~/assets/js/plugins/dataTables/dataTables.tableTools.min.js",
                        "~/assets/js/plugins/switchery/switchery.js",
                        "~/assets/js/demo/peity-demo.js",
                        "~/assets/js/inspinia.js",
                        "~/assets/js/plugins/pace/pace.min.js",
                        "~/assets/js/demo/sparkline-demo.js",
                        "~/assets/js/plugins/chartJs/Chart.min.js",
                        "~/assets/js/plugins/toastr/toastr.min.js",
                        "~/Scripts/jquery.maskedinput.js",
                        "~/Scripts/ValidacoesCamposForm.js",
                        "~/assets/js/plugins/steps/jquery.steps.min.js",
                        "~/Scripts/vue.js"                        
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/js/jquery-{version}.js",
                        "~/assets/js/plugins/flot/jquery.flot.js",
                        "~/assets/js/plugins/flot/jquery.flot.tooltip.min.js",
                        "~/assets/js/plugins/flot/jquery.flot.spline.js",
                        "~/assets/js/plugins/flot/jquery.flot.resize.js",
                        "~/assets/js/plugins/flot/jquery.flot.pie.js",
                        "~/assets/js/plugins/jquery-ui/jquery-ui.min.js",
                        "~/assets/js/plugins/gritter/jquery.gritter.min.js",
                        "~/assets/js/plugins/sparkline/jquery.sparkline.min.js",
                        "~/assets/js/plugins/validate/jquery.validate.min.js"
                        ));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/bootstrap.min.css",
                      "~/assets/css/plugins/toastr/toastr.min.css",
                      "~/assets/css/plugins/switchery/switchery.css",
                      "~/assets/js/plugins/gritter/jquery.gritter.css",
                      "~/assets/css/plugins/iCheck/custom.css",
                      "~/assets/css/plugins/steps/jquery.steps.css",
                      "~/assets/css/animate.css",                      
                      "~/assets/css/style.css",
                      "~/assets/css/my-style.css"));
            #endregion
        }
    }
}
