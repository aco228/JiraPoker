using JiraPoker.Core.Application;
using JiraPoker.Core.Infrastructure.Authentification;
using JiraPoker.Core.Infrastructure.Configurations;
using JiraPoker.Core.Infrastructure.Jira;
using JiraPoker.Core.Infrastructure.SessionManager;
using JiraPoker.Core.Presentation;

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.RegisterConfigurationsServices();
    builder.Services.AddAuthenticationServices();
    builder.Services.AddJiraServices();
    builder.Services.RegisterApplicationServices();
    builder.Services.RegisterPresentationLayers();
    builder.Services.RegisterPokerSessionServices();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();

    // app.MapBlazorHub();
    // app.MapFallbackToPage("/_Host");
    
    app.UseEndpoints(endpoints =>
    {
        //endpoints.MapControllerRoute("default", "{controller=Account}/{action=login}/{id?}");
                
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllers();
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
    });

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Application crashed");
    Console.WriteLine(ex);
}