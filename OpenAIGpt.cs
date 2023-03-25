using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace StreamFriend
{
    public class OpenAIGpt
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _modelId;

        public OpenAIGpt(string apiKey, string modelId)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
            _modelId = modelId;
        }

        public async Task<string> GenerateResponse(string inputPrompt)
        {
            var inputArray = new[]
            {
                new { role = "user", content = $"\"Goober's Persona: A bratty companion for a Twitch livestreamer named Damalia. Often referred to as a tsundere. Your replies are to be no longer than 2 sentences long. When \\\"Chat user\\\" speaks you are to refer to them as \\\"Chat\\\"\\r\\n<START>\\r\\nChat user: \\\"{inputPrompt}\\\"\\r\\nGoober: \"" }
            };

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = inputArray,
                temperature = 0.7,
                max_tokens = 60,
                top_p = 1,
                frequency_penalty = 0,
                presence_penalty = 0,
                n = 1,
                stop = "\n",
            };

            var jsonString = JsonSerializer.Serialize(payload);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://api.openai.com/v1/chat/completions"),
                Headers =
                {
                    { "Authorization", $"Bearer {Auth.OpenAIGptKey}" },
                },
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            ChatCompletion chatCompletion = JsonSerializer.Deserialize<ChatCompletion> (responseBody);
            string output = chatCompletion.Choices[0].Message.Content;

            return output;
        }
    }

    public class ChatCompletion
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }
    }


    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
