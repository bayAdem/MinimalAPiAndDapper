using FastEndpoints;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediaTRAndDapper.CQRS.Commands.Category.AddCategories;
using MediaTRAndDapper.Database.DPContext;
using MediaTRAndDapper.Services;
using Platform.Api.Database.Repositories.Abstract;
using Platform.Api.Database.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddCategoryCommandHandler>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


builder.Services.AddFastEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseFastEndpoints();

app.Run();
