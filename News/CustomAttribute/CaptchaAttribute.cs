using News.Common;
using News.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace News.CustomAttribute
{
    public class CaptchaAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            const string url = "https://www.google.com/recaptcha/api/siteverify";
            const string secretKey = Captcha.GoogleRecaptchaSecretKey_v2;
            var captchaResponse = filterContext.HttpContext.Request.Form["g-recaptcha-response"];

            if (string.IsNullOrWhiteSpace(captchaResponse))
                AddErrorAndRedirectToGetAction(filterContext);

            var validateResult = ValidateFromGoogle(url, secretKey, captchaResponse);
            if (!validateResult.Success)
                AddErrorAndRedirectToGetAction(filterContext);

            base.OnActionExecuting(filterContext);
        }
        private static void AddErrorAndRedirectToGetAction(ActionExecutingContext filterContext)
        {
            filterContext.Controller.TempData["InvalidCaptcha"] = "Invalid Captcha !";
            filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
        }

        private static CatchaRespose ValidateFromGoogle(string urlToPost, string secretKey, string captchaResponse)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                CatchaModel model = new CatchaModel
                {
                    secret = Captcha.GoogleRecaptchaSecretKey_v2,
                    response = captchaResponse
                };

                string url = "https://www.google.com/recaptcha/api/siteverify";
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", model.secret),
                    new KeyValuePair<string, string>("response", model.response),
                });

                var response = httpClient.PostAsync(url, content);
                var json = response.Result.Content.ReadAsStringAsync().Result;
                //Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<CatchaRespose>(json);
            }

            
        }
    }
}