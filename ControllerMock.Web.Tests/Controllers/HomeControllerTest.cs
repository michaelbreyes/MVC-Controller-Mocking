using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ControllerMock.Web.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllerMock.Web;
using ControllerMock.Web.Controllers;

namespace ControllerMock.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void About()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void UserInfo_action_returns_ok_when_passed_the_correct_name_querystring_param()
        {
            // Arrange
            var ctrl = new HomeController();
            var qs = new NameValueCollection();
            qs["Name"] = "Mock";
            MvcControllerContextMocks.SetContext(ctrl, qs);

            // Act
            var result = ctrl.UserInfo() as ContentResult;

            // Assert
            Assert.AreEqual("Ok", result.Content);
        }

        [TestMethod]
        public void ModifySession_action_modifies_the_Session_object()
        {
            // Arrange
            var ctrl = new HomeController();
            MvcControllerContextMocks.SetContext(ctrl, null);

            // Act
            ctrl.ModifySession();

            // Assert
            Assert.IsTrue((bool)ctrl.Session["IsModified"]);
        }

        [TestMethod]
        public void ReceiveFormData_action_returns_ok_when_passed_the_correct_name_in_form_data()
        {
            // Arrange
            var ctrl = new HomeController();
            var formCol = new NameValueCollection();
            formCol["Name"] = "Mock";
            MvcControllerContextMocks.SetContext(ctrl, formCol);

            // Act
            var result = ctrl.ReceiveFormData() as ContentResult;

            // Assert
            Assert.AreEqual("Ok", result.Content);
        }
    }
}
