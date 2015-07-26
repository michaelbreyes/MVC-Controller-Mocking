using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControllerMock.Web.Tests.Mocks
{
    public class MvcControllerContextMocks
    {
        public static void SetContext(ControllerBase onController, NameValueCollection remainingData)
        {
            // Define all the common context objects, plus relationships between them
            var httpContext = new Moq.Mock<HttpContextBase>();
            var request = new Moq.Mock<HttpRequestBase>();
            var response = new Moq.Mock<HttpResponseBase>();

            httpContext.Setup(x => x.Request).Returns(request.Object);
            httpContext.Setup(x => x.Response).Returns(response.Object);
            httpContext.Setup(x => x.Session).Returns(new FakeSessionState());
            response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            request.Setup(x => x.QueryString).Returns(remainingData);
            request.Setup(x => x.Form).Returns(remainingData);
            request.Setup(x => x.ServerVariables).Returns(remainingData);

            // Apply the mock context to the supplied controller instance
            var rc = new RequestContext(httpContext.Object, new RouteData());
            onController.ControllerContext = new ControllerContext(rc, onController);
        }

        public static void SetUrlContext(ControllerBase onController, NameValueCollection remainingData, Uri urlString)
        {
            // Define all the common context objects, plus relationships between them
            var httpContext = new Moq.Mock<HttpContextBase>();
            var request = new Moq.Mock<HttpRequestBase>();
            var response = new Moq.Mock<HttpResponseBase>();

            httpContext.Setup(x => x.Request).Returns(request.Object);
            httpContext.Setup(x => x.Response).Returns(response.Object);
            httpContext.Setup(x => x.Session).Returns(new FakeSessionState());
            response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            request.Setup(x => x.QueryString).Returns(remainingData);
            request.Setup(x => x.Url).Returns(urlString);
            request.Setup(x => x.Form).Returns(remainingData);
            request.Setup(x => x.ServerVariables).Returns(remainingData);

            // Apply the mock context to the supplied controller instance
            var rc = new RequestContext(httpContext.Object, new RouteData());
            onController.ControllerContext = new ControllerContext(rc, onController);
        }

        // Use a fake HttpSessionStateBase, because it's hard to mock it with Moq
        private class FakeSessionState : HttpSessionStateBase
        {
            private readonly Dictionary<string, object> _items = new Dictionary<string, object>();

            public override object this[string name]
            {
                get { return _items[name]; }
                set { _items[name] = value; }
            }
        }
    }
}
