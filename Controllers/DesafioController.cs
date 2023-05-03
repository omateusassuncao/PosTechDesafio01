using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosTechDesafio01.Data;
using System.Net.Http.Headers;

namespace PosTechDesafio01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesafioController : ControllerBase
    {

        private ApplicationContext _context;
        private IHttpClientFactory _httpClientFactory;

        public DesafioController(ApplicationContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<HttpResponseMessage> DesafioAsync()
        {

            var client = _httpClientFactory.CreateClient();

            //stepTwo
            var query = "select power_name from powers";
            var resultQuery = _context.Powers.FromSql($"SELECT power_name from powers").ToList().FirstOrDefault();

            //stepThree
            var requestKey = new HttpRequestMessage(
                HttpMethod.Get,
                "https://fiap-dotnet.azurewebsites.net/Fiap/"
                + resultQuery.Name
                );
            requestKey.Headers.Add("Accept", "application/json");
            requestKey.Headers.Add("User-Agent", "PosTechDesafio01");

            HttpResponseMessage responseKey = await client.SendAsync(requestKey);
            KeyMessage messageKey = await responseKey.Content.ReadFromJsonAsync<KeyMessage>();

            //stepFour
            var requestImage = new HttpRequestMessage(
            HttpMethod.Get,
                "https://fiap-dotnet.azurewebsites.net/Power/"
                + messageKey.Message
                );
            requestImage.Headers.Add("Accept", "application/json");
            requestImage.Headers.Add("User-Agent", "PosTechDesafio01");

            HttpResponseMessage responseImage = await client.SendAsync(requestImage);
            ImageMessage messageImage = await responseImage.Content.ReadFromJsonAsync<ImageMessage>();
            //Response.Redirect(messageImage.imageUrl);

            //stepFive
            string endpoint = "https://fiap-dotnet.azurewebsites.net/Power/urso/";
            string filePath = @"C:\Users\mateu\source\repos\PosTech\PosTechDesafio01\time.jpg";
            var form = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");
            form.Add(fileContent, "file", "image.jpg");

            HttpResponseMessage responsePost = await client.PostAsync(endpoint, form);

            if (responsePost.IsSuccessStatusCode)
            {
                return responsePost;
            }

            return null;
        }
    }
}

//REFs
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2
