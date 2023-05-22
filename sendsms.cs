using System;
using System.Net.Http;
using System.Text;

class Program
{
    static void Main()
    {
        string apiUrl = "https://api.umeskiasoftwares.com/api/v1/sms";
        string apiKey = "XXXXXXXXXXXXXXXXXXXX="; // Replace with your API key
        string email = "example7@gmail.com"; // Replace with your email address
        string senderId = "23107"; //If you have a custom sender id, use it here OR Use the default sender id: 23107
        string message = "UMS SMS Api Test Message"; // Replace with your message
        string phoneNumber = "0768168060"; // //Phone number should be in the format: 0768XXXXX60 OR 254768XXXXX60 OR 254168XXXXX60

        // Create the request body as a JSON object
        var requestBody = new
        {
            api_key = apiKey,
            email = email,
            Sender_Id = senderId,
            message = message,
            phone = phoneNumber
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
                HttpResponseMessage response = httpClient.PostAsync(apiUrl, new StringContent(requestBodyJson, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                // Read the response content as a string
                string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                // Check the response status code
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response JSON
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);

                    // Access the response properties
                    string success = responseObject.success;
                    string massage = responseObject.massage;
                    string requestId = responseObject.request_id;
                    if(success == "200")
                    {
                        Console.WriteLine($"Success: {success}");
                        Console.WriteLine($"Message: {massage}");
                        Console.WriteLine($"Request ID: {requestId}");
                    }
                    else
                    {
                        Console.WriteLine($"ERROR {responseObject}");
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