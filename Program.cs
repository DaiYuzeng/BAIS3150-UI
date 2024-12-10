using ydai5.Domain;

namespace ydai5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ABCPOS>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            //Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment()) // check for production environment
            {
                app.UseDeveloperExceptionPage(); // not for production, remove for final release
                                                 //app.UseExceptionHandler("/Error"); // customized error page, use for final release
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
