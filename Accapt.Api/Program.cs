using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.ContextIdentoty;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


#region Microsoft Identity

builder.Services.AddDbContext<ContextIdentity>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionDB")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ContextIdentity>();

#endregion

#region Path dependenct

//dontForgot to use this for patch :)
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;

}).AddNewtonsoftJson();

#endregion

#region DbContext

builder.Services.AddDbContext<AccaptFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionDB")));

#endregion

#region Jwt

var key = Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(oprions =>
{
    oprions.RequireHttpsMetadata = false;
    oprions.SaveToken = true;
    oprions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"]
    };
});

#endregion

#region IOC

builder.Services.AddTransient<IUserServies, UserServies>();
builder.Services.AddTransient<IAuthenticationJwtServies, AuthenticationJwtServies>();
builder.Services.AddTransient<IProductServies, ProductServies>();
builder.Services.AddTransient<IFindeProductServies, FindeProductServies>();
builder.Services.AddTransient<IAddInvoiceServies, AddInvoiceServies>();
builder.Services.AddTransient<IGetInovices, GetInovices>();
builder.Services.AddTransient<IFindInvoices, FindInvoices>();
builder.Services.AddTransient<IDeletInvoices, DeletInvoices>();
builder.Services.AddTransient<IFindPepolServies, FindPepolServies>();
builder.Services.AddTransient<IPepoleServies, PepoleServies>();
builder.Services.AddTransient<IAddBankServies, AddBankServies>();
builder.Services.AddTransient<IFindBankServies, FindBankServies>();
builder.Services.AddTransient<IDeletBankAccount, BankAccountServies>();
builder.Services.AddTransient<IGetBanckAccountServies, GetBanckAccountServies>();
builder.Services.AddTransient<IEditeInvoices, EditeInvoices>();
builder.Services.AddTransient<IDeletChekServies, DeletChekServies>();
builder.Services.AddTransient<IFindChekServies, FindChekServies>();
builder.Services.AddTransient<IUpdateChek, UpdateChek>();
builder.Services.AddTransient<IAddBillanServies, AddBillanServies>();
builder.Services.AddTransient<IDeletBillanServies, DeletBillanServies>();
builder.Services.AddTransient<IAddChekServies, AddChekServies>();
builder.Services.AddTransient<IDebtorCreditorsService, DebtorCreditorsService>();
builder.Services.AddTransient<IEmployeeServies, EmployeeServies>();
builder.Services.AddTransient<ISallaryAndCostsServiec, SallaryAndCostsServiec>();
builder.Services.AddTransient<IProviderService, ProviderService>();
builder.Services.AddTransient<IProviderServiceListS, ProviderServiceListS>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IJwtHelper, JwtHelper>();
builder.Services.AddTransient<IUpdateClientService, UpdateClientService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();

app.Run();
