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
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Searchfight.Infraestructure
{
    public abstract class CommonProvider
    {
        
        public Task<IEnumerable<SearchResult>> SearchAsync(params SearchMessage[] msgArr)
            => Task.FromResult(Search(msgArr));
        public IEnumerable<SearchResult> Search(params SearchMessage[] msgArr)
        {
            foreach (var msg in msgArr)
                yield return Search(msg);
        }

        public virtual Task<SearchResult> SearchAsync(SearchMessage msg) => Task.FromResult(Search(msg));
        public abstract SearchResult Search(SearchMessage msg);

        protected TContract Deserialize<TContract>(Stream stream) where TContract: class
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(TContract));
                return serializer.ReadObject(stream) as TContract;
            }
            catch (Exception ex)
            {
                throw new SearchfightException("Found errors while deserializing response");
            }
        }

        protected decimal GetNumberByPosition(string field, int position, string mode = "left", string separator = ".")
        {
            var numbers = GetNumericFields(field, separator);
            if ((numbers.Count() - 1) < position ) return 0;

            return numbers.ElementAt(position);
        }

        protected IEnumerable<int> GetNumericFields(string field, string separator = ".")
        {
            var result =  Regex.Split(field, @"[^0-9\.]+")
                .Where(c => c != "." && c.Trim() != "");

            return result.Select(c => Convert.ToInt32(c.Replace(separator, string.Empty)))
                .ToList();
        }
        
    }
}