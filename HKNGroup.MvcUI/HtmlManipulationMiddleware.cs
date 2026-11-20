using System.Text.RegularExpressions;

public class HtmlManipulationMiddleware
{
    private readonly RequestDelegate _next;

    public HtmlManipulationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // İstek öncesi işlemler

        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            // İstek sonrası işlemler

            responseBody.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(responseBody))
            {
                var content = await reader.ReadToEndAsync();
                var modifiedContent = ModifyHtmlContent(content);

                context.Response.Body = originalBodyStream;

                await context.Response.WriteAsync(modifiedContent);
            }
        }
    }

    private string ModifyHtmlContent(string content)
    {
        // manipüle etme işlemleri 
        var modifiedContent = Regex.Replace(content, @"\s+", " ");
        var modifiedContent2 = modifiedContent.Replace("> <","><");

        return modifiedContent2;
    }

}


// public class HtmlManipulationMiddleware
// {
//     private readonly RequestDelegate _next;
//     private readonly Dictionary<string, Func<string, string>> _pageManipulations;

//     public HtmlManipulationMiddleware(RequestDelegate next)
//     {
//         _next = next;
//         _pageManipulations = new Dictionary<string, Func<string, string>>();
//         ConfigurePageManipulations();
//     }

//     public async Task Invoke(HttpContext context)
//     {
//         var originalBodyStream = context.Response.Body;

//         using (var responseBody = new MemoryStream())
//         {
//             context.Response.Body = responseBody;

//             await _next(context);

//             responseBody.Seek(0, SeekOrigin.Begin);

//             using (var reader = new StreamReader(responseBody))
//             {
//                 var content = await reader.ReadToEndAsync();
//                 if (_pageManipulations.TryGetValue(context.Request.Path, out var manipulation))
//                 {
//                     var modifiedContent = manipulation(content);
//                     await context.Response.WriteAsync(modifiedContent);
//                 }
//                 else
//                 {
//                     await context.Response.WriteAsync(content);
//                 }
//             }
//         }

//         context.Response.Body = originalBodyStream;
//     }

//     private void ConfigurePageManipulations()
//     {
//         _pageManipulations["/Teacher"] = ModifyTeacherPage;
//         _pageManipulations["/Student"] = ModifyStudentPage;
//     }

//     private string ModifyTeacherPage(string content)
//     {
//         var modifiedContent = content.Replace("<h2 class=\"", "<h6 class=\"bg-primary ").Replace("</h2>", "</h6>");
//         return modifiedContent;
//     }

//     private string ModifyStudentPage(string content)
//     {
//         var modifiedContent = content.Replace("<h2 class=\"", "<h3 class=\"bg-danger ").Replace("</h2>", "</h3>");
//         return modifiedContent;
//     }
// }
