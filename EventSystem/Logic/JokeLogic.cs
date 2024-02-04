
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Helpers;


namespace EventSystem.Logic
{
    public class JokeLogic
    {

        public async static Task<string> GetRandomJoke()
        {
            string json = "";

            var url = Joke.GenerateURLRandom();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                json = await response.Content.ReadAsStringAsync();
            }
            return json;
        }
    }
}
