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

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Searchfight.Domain.Bing;
using Searchfight.Infraestructure;
using Searchfight.Providers.Interfaces;

namespace Searchfight.Providers.Implementations
{
    public class BingProvider: CommonProvider, IProvider
    {
        const string NAME = "Bing Search Engine";
        const string API_URI = "https://www.bing.com/search?q={0}";

        public override SearchResult Search(SearchMessage msg)
        {
            var searchResult = new SearchResult();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                var stream = client.GetStreamAsync(string.Format(API_URI, msg.word)).Result;
                var doc = new HtmlDocument();
                doc.Load(stream);
                var node = doc.GetElementbyId("b_tween");

                var resultCountNode = node.ChildNodes.FirstOrDefault(e => e.Attributes["class"].Value == "sb_count");
                if (resultCountNode == null ) return SearchResult.Empty();

                var totalItems = GetNumberByPosition(node.InnerHtml, 0);
                
                // TODO: we are not going to store results. not yet
                // searchResult.Items.Append(new SearchResulItem());

                // TODO: safe cast
                return new SearchResult
                {
                    SearchedWord = msg.word,
                    TotalItemsCount = (int) totalItems,
                    EngineName = NAME
                };
            }
        }

    }
}