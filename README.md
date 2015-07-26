# MVC Controller Mocking

Ever needed to unit test your MVC controller actions?  This sample solution includes a helper library for testing controllers when you need to access the following objects:

- Request
- Response
- Cookies (both request and response)
- QueryString
- Form
- ServerVariables
- Session
- Url

Relies on the Moq NuGet package.


An example of a unit test that reads posted form data:

```C#
// Controller code

[HttpPost]
public ActionResult ReceiveFormData()
{
    var name = Request.Form["Name"];
    if (name == "Mock") return Content("Ok");

    return Content("Not ok");
}


// Unit test

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
```


An example of a unit test that checks modifications to Session:

```C#
// Controller code

public ActionResult ModifySession()
{
    Session["IsModified"] = true;

    return Content("Ok");
}


// Unit test
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
```