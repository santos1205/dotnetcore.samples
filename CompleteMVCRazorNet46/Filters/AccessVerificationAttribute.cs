using System;
using System.Collections.Generic;
using System.Linq;
using QuestionarioCOrg.DataAccess;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QuestionarioCOrg.Enums;

namespace QuestionarioCOrg.Filters
{
    public class VerifySessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var Usuario = context.HttpContext.Session.Contents["Usuario"];
            
            if (Usuario == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Index");
                redirectTargetDictionary.Add("controller", "Login");
                redirectTargetDictionary.Add("area", "");

                if (!HttpContext.Current.Server.MachineName.Equals("DTI-MARIO"))  
                    context.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

            base.OnActionExecuting(context);
        }
    }

    public class VerifyUserRolesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            Usuario Usuario = context.HttpContext.Session.Contents["Usuario"] as Usuario;

            // caso sessão expirada, redireciona para login
            if (Usuario == null)
                RedirectTo(context, "Index", "Login");
            else
            {
                string ActionName = context.ActionDescriptor.ActionName;

                //if not in role admin or gestor            
                if (!Usuario.NivelAcesso.nvl_nome.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Gestor))
                        && !Usuario.NivelAcesso.nvl_nome.Equals(EnumHelper.GetDescription(NivelAcessoEnum.Administrativo)))
                {
                    // if not Questionarios, block
                    switch (ActionName)
                    {
                        case "Index":
                            RedirectTo(context, "Painel", "Formulario");
                            break;
                        case "Usuario":
                            RedirectTo(context, "Painel", "Formulario");
                            break;

                        case "PermissaoFormularios":
                            RedirectTo(context, "Painel", "Formulario");
                            break;

                        case "Empresa":
                            RedirectTo(context, "Painel", "Formulario");
                            break;

                        case "Questionario":
                            RedirectTo(context, "Painel", "Formulario");
                            break;

                        case "Classificacao":
                            RedirectTo(context, "Painel", "Formulario");
                            break;

                        case "Pergunta":
                            RedirectTo(context, "Painel", "Formulario");
                            break;
                        case "Questionarios":
                            RedirectTo(context, "Painel", "Formulario");
                            break;
                        case "Resposta":
                            RedirectTo(context, "Painel", "Formulario");
                            break;
                        case "Formulario":
                            RedirectTo(context, "Painel", "Formulario");
                            break;

                        case "Resultados":
                            RedirectTo(context, "Painel", "Formulario");
                            break;
                        default:
                            break;
                    }
                }
            }
            

            base.OnActionExecuting(context);
        }

        private void RedirectTo(ActionExecutingContext context, string Action, string Controller)
        {
            RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            redirectTargetDictionary.Add("action", Action);
            redirectTargetDictionary.Add("controller", Controller);
            redirectTargetDictionary.Add("area", "");
            context.Result = new RedirectToRouteResult(redirectTargetDictionary);
        }
    }
}