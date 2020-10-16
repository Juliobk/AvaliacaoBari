using Newtonsoft.Json.Linq;
using Sender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sender.Services
{
    public class MessageService : IMessageService
    {
        public async Task<string> SendMessage(string ID, string Text, string Url)
        {
            string sResposta = "";
            //  string sError = "";

            try
            {
                JObject jsonObject = new JObject
                {
                    { "ID", ID },
                    { "Text", Text },
                };

                var client = new HttpClient();
                Url = Url + "api/message";
                HttpContent content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var uri = new System.Uri(string.Format(Url));
                client.Timeout = TimeSpan.FromMilliseconds(8000);
                var response = client.PostAsync(uri, content);

                if (response.Result.IsSuccessStatusCode)
                {
                    var json = await response.Result.Content.ReadAsStringAsync();
                    //    var result = JsonConvert.DeserializeObject<PostResponse>(json);
                }
            }
            catch(Exception ex)
            {

            }
            return sResposta;
        }
    }
}
