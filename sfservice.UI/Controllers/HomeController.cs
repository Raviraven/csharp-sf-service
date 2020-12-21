using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        //https://docs.microsoft.com/pl-pl/aspnet/core/blazor/globalization-localization?view=aspnetcore-5.0
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));

                //CultureInfo.CurrentCulture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                //    .First(c => c.Name == culture);

            }

            return LocalRedirect(redirectUri);
        }
    }
}
