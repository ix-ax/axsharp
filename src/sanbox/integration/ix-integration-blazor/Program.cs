// ix-integration-blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Globalization;
using AXSharp.Connector;
using AXSharp.Presentation.Blazor.Services;
using ix_integration_blazor.Data;
using ix_integration_plc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace ix_integration_blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Entry.Plc.Connector.Translator.SetLocalizationResource(typeof(ix_integration_plc.Resources.PlcStringResources));


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddIxBlazorServices();
            builder.Services.AddLocalization();

            var app = builder.Build();

            Entry.Plc.Connector.BuildAndStart().ReadWriteCycleDelay = 10;

            Entry.Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.Ignore;
            //Entry.Plc.Connector.Translator.SetLocalizationResource(Entry.Plc.GetType(), "Properties.PlcStringResources");
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            
            //app.UseRequestLocalization(new RequestLocalizationOptions()
            //    .AddSupportedCultures(new[] { "en-US", "sk-SK" })
            //    .AddSupportedUICultures(new[] { "en-US", "sk-SK" }));

            //app.UseRequestLocalization("sk-SK");

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}