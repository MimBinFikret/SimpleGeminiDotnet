# Gemmi - .NET Client for Google Gemini AI Content Generation

This repository contains a .NET console application that demonstrates how to use the Google Gemini API to generate text-based content. The application uses the DotnetGeminiSDK to interact with the API, providing a simple example of how to integrate AI content generation into a .NET project.

## Requirements

- .NET 6.0 or higher
- DotnetGeminiSDK
- A valid Google Gemini API key

## Installation

1. Clone this repository:

```bash
git clone https://github.com/yourusername/gemmi.git
```

2. Navigate to the project directory:

```bash
cd gemmi
```

3. Open the project in Visual Studio 2022 (or your preferred .NET IDE).

## Configuration

The project requires two environment variables to be set:

- `API_BASE_URL`: The base URL for the Google Gemini API.
- `API_KEY`: Your API key for accessing the Google Gemini API.

These environment variables can be configured in the `launchSettings.json` file as shown below:

```json
"profiles": {
  "Gemmi": {
    "commandName": "Project",
    "environmentVariables": {
      "API_BASE_URL": "https://generativelanguage.googleapis.com/v1/models/gemini-pro",
      "API_KEY": "YOUR_API_KEY_HERE"
    }
  },
  "Container (Dockerfile)": {
    "commandName": "Docker",
    "environmentVariables": {
      "API_BASE_URL": "https://generativelanguage.googleapis.com/v1/models/gemini-pro",
      "API_KEY": "YOUR_API_KEY_HERE"
    }
  }
}
```

Replace `"YOUR_API_KEY_HERE"` with your actual API key.

## Usage

To run the application:

1. Build and run the project in Visual Studio.

2. The application will send a prompt to the Google Gemini API and output the generated content to the console.

## Example Code

The core logic is contained in `Program.cs`:

```csharp
using System;
using System.Threading.Tasks;
using DotnetGeminiSDK;
using DotnetGeminiSDK.Client.Interfaces;
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
```

## Docker Support

The project includes a `Dockerfile` for containerization:

1. To build the Docker image:

```bash
docker build -t gemmi .
```

2. To run the container:

```bash
docker run --rm -it gemmi
```

Make sure your environment variables are set correctly in the Docker environment.

## Contributing

Feel free to fork this repository, make changes, and submit pull requests. Any contributions are welcome!

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
