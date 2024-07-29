using VoucherShop.Server.Data;
using VoucherShop.Server.Data_Layer;
using VoucherShop.Server.Interfaces;
using VoucherShop.Server.Model;
using VoucherShop.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Add(new ServiceDescriptor(typeof(IProductService<Voucher>), new VoucherService()));
builder.Services.Add(new ServiceDescriptor(typeof(IRepository<CheckOut>), new CheckoutRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IRepository<CartItem>), new CartRepository()));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
