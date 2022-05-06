using JiraPoker.Data;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

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