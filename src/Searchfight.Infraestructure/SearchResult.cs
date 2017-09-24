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

namespace Searchfight.Infraestructure
{
    public class SearchResult
    {
        public static SearchResult Empty() => new SearchResult { TotalItemsCount = 0 };
        public IEnumerable<SearchResulItem> Items { get; set; } = Enumerable.Empty<SearchResulItem>();

        public string EngineName { get; set; }
        public string SearchedWord { get; set; }
        public int AvailableItemsCount => Items.Count();

        public int TotalItemsCount { get; set; }
    }

    public class SearchResulItem
    {

    }
}