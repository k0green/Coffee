using Coffee;
using Coffee.Interfaces;
using Coffee.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Coffee.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CoffeeContext>(options => options.UseMySql("server=127.0.0.1;database=coffee;uid=root;pwd=12345678", ServerVersion.Parse("8.0.30-mysql")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDbContext, CoffeeContext>();
builder.Services.AddTransient<ICrudlDrinkService, DrinkCrudlServices>();
builder.Services.AddTransient<ICrudlIngredientService, IngredientCrudlServices>();
builder.Services.AddTransient<ICrudlIngredientOfDrinkService, IngredientOfDrinkCrudlServices>();
builder.Services.AddTransient<ICrudlWalletService, WalletCrudlServices>();
builder.Services.AddTransient<ICrudlBillsService, BillsCrudlServices>();
builder.Services.AddTransient<ICrudlBillDrinkService, BillDrinkCrudlServices>();
builder.Services.AddTransient<ICrudlRoleService, RoleCrudlServices>();
builder.Services.AddTransient<ICrudlUserService, UserCrudlServices>();
builder.Services.AddTransient<IRegistrationService, RegistrationService>();

//builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<IDbContext, CoffeeContext>();
//builder.Services.AddScoped<ICrudlDrinkService, DrinkCrudlServices>();
//builder.Services.AddScoped<ICrudlIngredientService, IngredientCrudlServices>();
//builder.Services.AddScoped<ICrudlIngredientOfDrinkService, IngredientOfDrinkCrudlServices>();
//builder.Services.AddScoped<ICrudlWalletService, WalletCrudlServices>();
//builder.Services.AddScoped<ICrudlBillsService, BillsCrudlServices>();
//builder.Services.AddScoped<ICrudlBillDrinkService, BillDrinkCrudlServices>();
//builder.Services.AddScoped<ICrudlRoleService, RoleCrudlServices>();
//builder.Services.AddScoped<ICrudlUserService, UserCrudlServices>();
//builder.Services.AddScoped<IRegistrationService, RegistrationService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

IServiceCollection services = new ServiceCollection();
{
    services.AddDbContext<CoffeeContext>(options => options.UseMySql("server=127.0.0.1;database=coffee;uid=root;pwd=12345678", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => 
        {
            options.LoginPath = new PathString("/Account/Login");
        });
    services.AddControllersWithViews();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GeneralMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");
});

app.Run();