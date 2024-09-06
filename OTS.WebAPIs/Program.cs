using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OTS.Common.Contants;
using OTS.Common.RequestModels.Email;
using OTS.Core.Entities.Account;
using OTS.Core.Repositories;
using OTS.Infrastructure.Data;
using OTS.Repository.Interface.Account;
using OTS.Repository.Interface.Conversation;
using OTS.Repository.Interface.Email;
using OTS.Repository.Interface.Ticket;
using OTS.Repository.Services;
using OTS.Repository.Services.Account;
using OTS.Repository.Services.Conversation;
using OTS.Repository.Services.Email;
using OTS.Repository.Services.Tickets;
using OTS.WebAPIs.Helper;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"),
                         b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

//SMTP Configuration
builder.Services.Configure<EmailConfigurationRequestModel>(builder.Configuration.GetSection("EmailConfiguration"));

//AutoMapper for mapping profiles

//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddTransient<ApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<ITicketRepository, TicketService>();
builder.Services.AddScoped<IConversationRepository, ConversationService>();
builder.Services.AddTransient<IEmailRepository, EmailService>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//for identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();


//configure applicationrole in place of identityrole
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Adding authorization using JWT

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 3.1 Web API"
    });

    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

// Adding Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });



// Adding Authorization using Identity Principle Policy

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("UserRolePolicy", policy =>
//    {
//        policy.RequireRole(Constant.User);
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Add Authorization using Identity


app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable static file serving for wwwroot
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        DbInitilizer.Initialize(context);
    }
    catch (Exception ex)
    {
    }

}
app.Run();
