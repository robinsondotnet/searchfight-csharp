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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Searchfight.Infraestructure;
using Searchfight.Providers.Implementations;
using Searchfight.Providers.Interfaces;
using Searchfight.Utils;
using test.Mocks;
using Xunit;

namespace test
{
    public class InputTests
    {
        [Fact]
        public void can_create_message_from_a_single_word()
        {
            var word = "java";
            var message = SearchMessage.Factory(word);
            Assert.IsType(typeof(SearchMessage), message);
        }

        [Fact]
        public void can_create_message_from_multiple_words()
        {
            var words = new string[] {"java", "python", "dotnet"};
            var message = SearchMessage.Factory(words);
            Assert.IsType(typeof(List<SearchMessage>), message);
        }

        [Fact]
        public async void create_provider_without_implAsync()
        {
            var provider = new NosenseProvider();
            var query = new SearchMessage();
            await Assert.ThrowsAsync<NotImplementedException>(async() => await provider.SearchAsync(query));

        }

        [Fact]
        public async void can_retrieve_multiple_results()
        {
            var provider = new BingProvider();
            var query = SearchMessage.Factory("coffee cup");
            var result = await provider.SearchAsync(query);

            Assert.True(result.TotalItemsCount > 0);
        }

        [Fact]
        public async void can_search_as_group()
        {
            var providers = new List<IProvider>()
            {
                new BingProvider(),
                new GoogleProvider(),
            };
            var query = SearchMessage.Factory("coffee cup");
            var result = await Task.WhenAll(providers.SearchAll())
                .ContinueWith(async(providerResultTask) =>
                {
                    var providerResult = await providerResultTask;
                    foreach (var wordsResult in providerResult)
                        foreach (var wordResult in wordsResult)
                            Assert.True(wordResult.TotalItemsCount > 0);
                });
        }
    }
}
