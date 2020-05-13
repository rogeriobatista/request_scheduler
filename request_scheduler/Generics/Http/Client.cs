using System.Net.Http;
using System.Text;

namespace request_scheduler.Generics.Http
{
    public class Client
    {
        private string Url { get; set; }

        private string Body { get; set; }

        private string ContentType { get; set; }

        private readonly HttpClient HttpClient;

        public Client(string url, string contentType, string body)
        {
            HttpClient = new HttpClient();
            Url = url;
            ContentType = contentType;
            Body = body;
        }

        public void Post()
        {
            HttpClient.PostAsync(Url, new StringContent(Body, Encoding.UTF8, ContentType));
        }

        public void Get()
        {
            HttpClient.GetAsync(Url);
        }
    }
}
