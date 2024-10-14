using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

[Route("saml")]
public class HomeController : Controller
{
    [HttpPost("consume")]
    public IActionResult ConsumeSAML()
    {
        var result = HttpContext.AuthenticateAsync(Saml2Defaults.Scheme).Result;
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        // Process SAML claims
        var claims = result.Principal.Claims;
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        return View();
    }
}
