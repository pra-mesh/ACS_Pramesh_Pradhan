using ACS_DataManager.Authorization;
using ACS_DataManager.Helpers;
using ACS_DataManager.Library.DataAccess.Implementation;
using ACS_DataManager.Library.DataAccess.Interface;
using ACS_DataManager.Library.DataAccess.Internal;
using ACS_DataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ACS_DataManager", Version = "v1" });
});
builder.Services.AddCors();
builder.Services.Configure<ApiBehaviorOptions>(a =>
{
    a.InvalidModelStateResponseFactory = context =>
    {
        BadRequestResponse br = new BadRequestResponse();
        br.statusCode = 2;
        List<string> st = new List<string>();
        br.statusMessage = "Invalid Data";
        foreach (var keyModelStatePair in context.ModelState)
        {
            var key = keyModelStatePair.Key;
            var errors = keyModelStatePair.Value.Errors;
            if (errors != null && errors.Count > 0)
            {
                var errorMessage = string.IsNullOrEmpty(errors[0].ErrorMessage) ?
    "The input was not valid." :
errors[0].ErrorMessage;
                st.Add(errorMessage);
            }
        }
        br.data = st;
        return new BadRequestObjectResult(br);
    };
});
var key = "SuperSecretSaltforACSASSignment1";
builder.Services.AddSingleton<IJwtUtils>(new JwtUtils(key));
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IRegistrationData, RegistrationData>();
builder.Services.AddTransient<ILoginData, LoginData>();
builder.Services.AddTransient<IUserService, UserService>();


var app = builder.Build();

app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
