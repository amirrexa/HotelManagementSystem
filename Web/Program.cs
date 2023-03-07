using HotelManagementSystem.Business;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HotelContext>();

builder.Host.ConfigureServices( //Bayad ghabl az builder neveshte she
    services =>
        services
            .AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IOperationRepository, OperationRepository>());
var app = builder.Build();

//Hotel Manager
var hotelManager = new HotelManager();
hotelManager.InitializeRooms();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Room}/{action=Index}");
app.Run();