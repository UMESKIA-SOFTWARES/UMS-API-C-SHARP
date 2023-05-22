using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string apiUrl = "https://api.umeskiasoftwares.com/api/v1/smsbalance";
        string apiKey = "XXXXXXXXXXXXXXX=";
        string email = "example@gmail.com";

        // Create the request body as a JSON object
        var requestBody = new
        {
            api_key = apiKey,
            email = email
        };

        // Serialize the request body to JSON
        string requestBodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

        // Create the HttpClient
        using (var httpClient = new HttpClient())
        {
            // Set the Content-Type header
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {
                // Send the POST request
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response JSON
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);

                    // Access the response properties
                    string success = responseObject.success;
                    if (success == "200")
                    {
                        string creditBalance = responseObject.creditBalance;
                        Console.WriteLine($"Sms Balance retrieved successfully, with creditBalance: {creditBalance}");
                    }
                    else
                    {
                        string resultCode = responseObject.ResultCode;
                        string errorMessage = responseObject.errorMessage;
                        Console.WriteLine($"Sms not sent, with ResultCode: {resultCode} and errorMessage: {errorMessage}");
                    }
                }
                else
                {
                    Console.WriteLine($"Request failed with status code {response.StatusCode}: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
