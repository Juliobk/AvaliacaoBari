using Newtonsoft.Json.Linq;
using RestSharp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
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
                client.Timeout = TimeSpan.FromMilliseconds(80000);
                try
                {
                    var response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException.Message);
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sResposta;
        }

        public async Task<string> DisplayMessage(string Text, string Url)
        {
            string sResposta = "";
            try
            {
                JObject jsonObject = new JObject
                {
                    { "Text", Text },
                };

                var client = new HttpClient();
                Url = Url + "api/weatherforecast";
                HttpContent content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var uri = new System.Uri(string.Format(Url));
                client.Timeout = TimeSpan.FromMilliseconds(80000);
                try
                {
                    var response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException.Message);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sResposta;
        }
    }
}
