using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Talent.Data.Entities;
using Talent.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Talent.Models;
using Talent.Models.Boolean;
using Talent.Models.JsonSettings;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Talent
{

  
    public class Program
    {

        class A
        {
            public int balance;
        }
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static BooleanPhraseModel BooleanPhraseModel(string a)
        {
            BooleanPhraseModel booleanPhrase = new BooleanPhraseModel();
            booleanPhrase.OperatorType = BooleanOperator.AND;
            var bool1 = new BooleanExpressionModel("name", a);
            booleanPhrase.Children.AddRange(new[] {bool1});
            return booleanPhrase;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
