//    Copyright 2017 Kento <robinsondotnet@hotmail.com>
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Searchfight.Domain.DuckGo;
using Searchfight.Infraestructure;
using Searchfight.Providers.Interfaces;

namespace Searchfight.Providers.Implementations
{
    public class DuckGoProvider : CommonProvider, IProvider
    {
        const string API_URI = "https://duckduckgo.com/api";

        public override Task<SearchResult> SearchAsync(SearchMessage msg)
            => Task.FromResult(Search(msg));
        public override SearchResult Search(SearchMessage msg)
        {

            var searchResult = new SearchResult();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                

                var stream = client.GetStreamAsync(API_URI).Result;
                var result = Deserialize<ResponseContract>(stream);

                // TODO: agregar propiedades del cuerpo del response json al ResponseContract correspondiente
                // y mapear con el searchResult que se devolvera
                searchResult.Items.Append(new SearchResulItem());

                return searchResult;

            }
        }
    }
}