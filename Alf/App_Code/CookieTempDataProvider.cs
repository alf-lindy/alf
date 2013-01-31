using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace AppHarbor.Web.Mvc
{
    public class CookieTempDataProvider : ITempDataProvider
    {
        private const string CookieName = "TempData";

        private readonly IFormatter _formatter;

        public CookieTempDataProvider()
        {
            _formatter = new BinaryFormatter();
        }

        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            return GetTempDataFromCookie(controllerContext);
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            var currentValues = GetTempDataFromCookie(controllerContext);
            if (currentValues.SequenceEqual(values))
            {
                return;
            }

            var cookie = new HttpCookie(CookieName);
            cookie.HttpOnly = true;

            if (values.Count == 0)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = string.Empty;
                controllerContext.HttpContext.Response.Cookies.Set(cookie);

                return;
            }

            using (var stream = new MemoryStream())
            {
                _formatter.Serialize(stream, values);
                var bytes = stream.ToArray();

                cookie.Value = Convert.ToBase64String(bytes);
            }
            controllerContext.HttpContext.Response.Cookies.Add(cookie);
        }

        private IDictionary<string, object> GetTempDataFromCookie(ControllerContext controllerContext)
        {
            HttpCookie cookie = controllerContext.HttpContext.Request.Cookies[CookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                byte[] bytes = Convert.FromBase64String(cookie.Value);
                using (var stream = new MemoryStream(bytes))
                {
                    return _formatter.Deserialize(stream) as IDictionary<string, object>;
                }
            }

            return new Dictionary<string, object>();
        }
    }
}
