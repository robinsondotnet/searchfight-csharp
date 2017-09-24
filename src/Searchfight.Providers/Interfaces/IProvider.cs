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
using System.Threading.Tasks;
using Searchfight.Infraestructure;

namespace Searchfight.Providers.Interfaces
{
    public interface IProvider
    {
        Task<IEnumerable<SearchResult>> SearchAsync(params SearchMessage[] msgArr);

        IEnumerable<SearchResult> Search(params SearchMessage[] msgArr);
        Task<SearchResult> SearchAsync(SearchMessage msg);

        SearchResult Search(SearchMessage msg);
    }

}