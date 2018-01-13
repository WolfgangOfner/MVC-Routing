using System;
using System.Web;
using System.Web.Routing;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace MvcRouting.Test
{
    public class RouteTests
    {
        [Fact]
        public void FactMethodName()
        {
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            //TestRouteFail("~/Customer/List/All/Delete");

            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            //TestRouteFail("~/Customer/List/All/Delete");

            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List",
                new { id = "All", catchall = "Delete" });
            TestRouteMatch("~/Customer/List/All/Delete/Perm", "Customer", "List",
                new { id = "All", catchall = "Delete/Perm" });
        }        
        /// <summary>
        ///     url to test, expexted values for the controller and action, object contains any additional variables, URLS have to
        ///     be prefixd with tilde. checks if route can be found but not if controller and method exists
        /// </summary>
        /// <param name="url"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="routeProperties"></param>
        /// <param name="httpMethod"></param>
        [Theory]
        [InlineData("~/", "Home", "Index")]
        [InlineData("~/", "Home", "Index", "DefaultId")]
        [InlineData("~/Customer", "Customer", "Index")]
        [InlineData("~/Customer/List", "Customer", "List")]
        [InlineData("~/Customer/List/All", "Customer", "List", "All")]
        [InlineData("~/Shop/Index", "Home", "Index")]
        //[InlineData("~/bvlbla/lala", "Home", "Index")]
        public void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            routeProperties = routeProperties == null ? null : new {id = routeProperties  };
            
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            var httpContextBase = CreateHttpContext(url, httpMethod);
            var result = routes.GetRouteData(httpContextBase);

            result.Should().NotBe(null);
            TestIncomingRouteResult(result, controller, action, routeProperties).Should().BeTrue();
        }

        // set up
        /// <summary>
        ///     check that a url does not work (maybe no route exists?)
        /// </summary>
        /// <param name="url"></param>
        [Theory]
        [InlineData("~/bvlbla/lala")]
        private void TestRouteFail(string url)
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            var result = routes.GetRouteData(CreateHttpContext(url));
           
            CheckRoute(result).Should().BeTrue();
        }

        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            var fakeRequestBase = A.Fake<HttpRequestBase>();
            var fakeResponseBase = A.Fake<HttpResponseBase>();
            var fakeContextBase = A.Fake<HttpContextBase>();

            A.CallTo(() => fakeRequestBase.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            A.CallTo(() => fakeRequestBase.HttpMethod).Returns(httpMethod);
            A.CallTo(() => fakeResponseBase.ApplyAppPathModifier(A<string>._)).Returns("");
            A.CallTo(() => fakeContextBase.Request).Returns(fakeRequestBase);
            A.CallTo(() => fakeContextBase.Response).Returns(fakeResponseBase);

            return fakeContextBase;
        }

        /// <summary>
        ///     Method uses .net reflection makes testing more convinient
        /// </summary>
        /// <param name="routeResult"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="propertySet"></param>
        /// <returns></returns>
        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            bool ValCompare(object v1, object v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            var result = ValCompare(routeResult.Values["controller"], controller) && ValCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                var propInfo = propertySet.GetType().GetProperties();

                foreach (var pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name) && ValCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private bool CheckRoute(RouteData result)
        {
            return result?.Route == null;
        }
    }
}