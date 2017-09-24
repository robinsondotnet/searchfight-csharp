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
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Infraestructure;
using Searchfight.Providers.Implementations;
using Searchfight.Providers.Interfaces;
using Searchfight.Utils;

namespace Searchfight
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Welcome to our Searchfight app :)");
            try
            {
                return await ProcessArgs(args);
            }
            catch (SearchfightException ex)
            {
                Console.WriteLine(ex.Message);

                return 1;    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo errores internos");
                Console.WriteLine(ex.Message);

                return 1;
            }
            finally
            {
                // TODO: Write footer
            }
        }

        /// <summary>
        /// Build a list of search engine providers
        /// </summary>
        /// <returns>The list of providers</returns>
        static IEnumerable<IProvider> BuildProviders()
            => new List<IProvider>
            {
                new BingProvider(),
                new GoogleProvider(),
                //new DuckGoProvider()
            };

        internal static async Task<int> ProcessArgs(string[] args)
        {
            var providers = BuildProviders();
            Console.WriteLine("\n");
            Console.WriteLine("Results: ");
            Console.WriteLine("\n");

            var messages = SearchMessage.Factory(args);

            var taskBatch = providers.SearchAll(messages.ToArray());

            var taskResult = await Task.WhenAll(taskBatch);

            foreach (var providerResult in taskResult)
                foreach (var wordResult in providerResult)
                    Console.WriteLine("Provider ({0}) word: {1}, totalItems: {2}", wordResult.EngineName, wordResult.SearchedWord, wordResult.TotalItemsCount);

            Console.WriteLine("\n");
            return 0;

        }

        /// <summary>
        /// Prints current version
        /// </summary>
        private static void PrintVersion()
        {
            // TODO

        }

        /// <summary>
        /// Prints program information
        /// </summary>
        private static void PrintInfo()
        {
            // TODO:

        }

    }
}
