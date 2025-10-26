using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;



namespace WebApplication1
{
    public class BundleConfig
    {
        // 🔹 Registro de todos los bundles CSS y JS del proyecto
        public static void RegisterBundles(BundleCollection bundles)
        {
            // --- 📦 ESTILOS (CSS) ---
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/Site.css"));

            // --- 💡 MODERNIZR ---
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // --- 🧩 JQUERY ---
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // --- 🧰 BOOTSTRAP JS ---
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));

            // --- ⚙️ WEBFORMS JS (ya lo tenías) ---
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                        "~/Scripts/WebForms/WebForms.js",
                        "~/Scripts/WebForms/WebUIValidation.js",
                        "~/Scripts/WebForms/MenuStandards.js",
                        "~/Scripts/WebForms/Focus.js",
                        "~/Scripts/WebForms/GridView.js",
                        "~/Scripts/WebForms/DetailsView.js",
                        "~/Scripts/WebForms/TreeView.js",
                        "~/Scripts/WebForms/WebParts.js"));

            // --- ⚙️ AJAX JS (ya lo tenías) ---
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                        "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                        "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                        "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                        "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // --- 🧱 ACTIVAR OPTIMIZACIÓN (false para debug, true para producción) ---
            BundleTable.EnableOptimizations = false;
        }
    }
}
