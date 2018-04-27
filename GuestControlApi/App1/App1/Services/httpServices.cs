using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App1.services
{
    public static class Http
    {
        public static async Task<HttpResponseMessage> Get(string address)
        {
            using (var h = new HttpClient())
            {
                var get = await h.GetAsync(address);

                if (get.IsSuccessStatusCode)
                {
                    return get;
                }

                return null;

            }


        }

        public static async Task<HttpResponseMessage> Post(string address, object content)
        {
            using (var h = new HttpClient())
            {
                //h.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //h.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");

                var t = JsonConvert.SerializeObject(content);

                var RoleContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                var post = await h.PostAsync(address, RoleContent);
                
                return post;
            }

        }
    }
}
