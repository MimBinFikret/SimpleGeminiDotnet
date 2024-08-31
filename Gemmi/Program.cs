using System;
using System.Threading.Tasks;
using DotnetGeminiSDK;
using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Client;
using Microsoft.Extensions.DependencyInjection;
using DotnetGeminiSDK.Model.Response;

namespace Gemmi
{
    internal class Program
    {
        private readonly IGeminiClient _geminiClient;


        public Program(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }

        public async Task<GeminiMessageResponse> Example()
        {
            var response = await _geminiClient.TextPrompt("Bana insanlık tarihini anlat.");
            return response;
        }

        public static async Task Main(string[] args)
        {
            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL");
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            var serviceProvider = new ServiceCollection()
                .AddGeminiClient(config =>
                {
                    config.TextBaseUrl = baseUrl;
                    config.ApiKey = apiKey;

                })
                .BuildServiceProvider();

            var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

            Program program = new Program(geminiClient);
            var response = await program.Example();

            Console.WriteLine(response.Candidates[0].Content.Parts[0].Text.ToString());
            Console.WriteLine("Application will exit automatically.");
        }
    }
}
