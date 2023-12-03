using Domain;
using Domain.Account;
using Domain.Transaction;
using Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<AccountOrchestrator>();
builder.Services.AddSingleton<Accounts>();
builder.Services.AddSingleton<TransactionOrchestrator>();
builder.Services.AddSingleton<Transactions>();
builder.Services.AddSingleton<ITransferService, TransferService>();
builder.Services.AddSingleton<TransactionQueries>();
builder.Services.AddSingleton<AccountQueries>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();