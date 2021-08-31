using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Talent.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Talent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BooleanPhraseModel booleanPhraseModel = new BooleanPhraseModel();
            BooleanExpressionModel<int> expressionModel = new BooleanExpressionModel<int>("ali",123);
            BooleanExpressionModel<string> expressionModel1 = new BooleanExpressionModel<string>("mmd","123");
            booleanPhraseModel.OperatorType = BooleanOperator.AND;
            expressionModel.OperatorType = BooleanOperator.OR;
            expressionModel1.OperatorType = BooleanOperator.AND;
            booleanPhraseModel.Children.Add(expressionModel);
            booleanPhraseModel.Children.Add(expressionModel1);
            var serialize = JsonSerializer.Serialize(booleanPhraseModel);
            Console.WriteLine(serialize);
            
            // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
