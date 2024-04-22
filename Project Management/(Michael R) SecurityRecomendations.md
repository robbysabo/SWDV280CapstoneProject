# Todos:
*[ ] todo



# SECURITY FIRST Implementations (Tasks)
* Don't be overwhelmed we don't have to implement all these, but If we're going to be minimal then atleast go a bit heavy on security. Could always assign this to 1 or 2 members of the team if anyone feels particularly knowledable in this area.
* I certainly don't mind taking on the task!
## DI Recommended middleware configuration (From a more up-to-date source for implementing into the codebase and best practices)

### UseHsts() 
 - Strict Transport security protocol, through the response headers. 
 - Prevents user from using untrusted/invalid certificates.
 ### UseHttpsRedirection()

 ### HtmlEncoder
```C# 
    public async Task<IActionResult> OnGet(
    [FromServices]HtmlEncoder htmlEncoder,
    string q = "")
    {
        PageResults = await PerformTheSearch(htmlEncoder.Encode(q));
        return Page();
    } 
```

### Configure HTTP request headers

#### Disable Kestral(open source server used in ASP.NET)
**Removes Server Version Information** 
```C# 
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestral(options => options.AddServiceHeader = false);
```
#### Remove X-Powered-By:
* Program.cs
```C# 
app.Use(async(context, next) =>
{
    context.Response.Headers.Remove("Server");
    context.Response.Headers.Remove("X-Powered-By");
    await next();
})
```
#### Remove also inside root/web.config 
* web.config
1. Compression configuration
2. Remove specific IIS headers
3. Custom MIME mappings 
```XML
<?xml version="1.0"
encoding="UTF-8"?>
<configuration>
    <system.webServer>
        <httpProtocol>
            <customHeaders>
                <remove name="X-Powered-by"/>
```
#### Remove X-Aspnet-Version & X-AspnetMvc-Version
**Details about techology used by the application**
```C#
context.Response.Headers.Remove("X-Aspnet-version");
context.Response.Headers.Remove("X-AspnetMvc-version");
```

#### No Sniffing Allowed
1. Avoid attacks based on MIME-type confusion
2. Tells browser to adhere to the MIME-types registered in the Content-type headers. i.e the values don't change.
```C#
context.Response.Headers.Add("X-Content-Type-Options", new StringValues("nosniff"));
```

#### No Framing
1. Add X-Frame-Options
2. Tell browser not to render the webpage in `<iframe> <frame> <embed> or <object>`
3. Prevents clickjacking attacks (embedding your content into other sites using any of the tags above).
```C#
context.Response.Headers.Add("X-Frame-Options", new StringValues("DENY"));
```

#### Middleware Example
```C#
public class RemoveInsecureHeadersMiddleware
{
    private readonly RequestDelegate _next;
    public RemoveInsecureHeadersMiddleWare(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Response.OnStarting((state) =>
        {
            httpContext.Response.Headers.Remove("Server");
            httpContext.Response.Headers.Remove("X-Powered-By");

            httpContext.Response.Headers.Remove("X-Aspnet-version");

            httpContext.Response.Headers.Remove("X-AspnetMvc-version");

            httpContext.Response.Headers.Add("X-Content-Type-Options", new StringValue("nosniff"));

            httpContext.Response.Headers.Add("X-Frame-Options", new StringValues("DENY"));
            return Tasks.CompletedTask;
        }, null!);
        await _next(httpContext);
    }
}

// Middleware Extension
public static class RemoveInsecureHeadersMiddlewareExtensions
{
    public static IApplicationBuilder RemoveInsecureHeaders(this IApplicationBuilder builser)
    {
        return builder.UseMiddleware<RemoveInsecureHeadersMiddleware>();
    }
}

// Program.cs
app.RemoveInsecureHeaders();
```
#### SQL Server's *Encrypt Columns* Feature
- Right click dbo.Table > Encrypt Columns...

#### Prevent XSRF attacks through web forms:
```C#
// Program.cs
services.AddAntiforgery();

// razorview.cshtml
<form @Html.AntiForgeryToken();> ...
```
