using System.IO;
using System.Web;
using System.Web.SessionState;

namespace API.Tests
{
    public static class MockHelper
    {
        public static HttpContext FakeHttpContext(string url)
        {
            var httpRequest = new HttpRequest("", "https://localhost:44307/" + url, "");


            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), 10, true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

            return httpContext;
        }
    }
}