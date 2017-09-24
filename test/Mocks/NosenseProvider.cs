using System.Collections.Generic;
using System.Threading.Tasks;
using Searchfight.Infraestructure;
using Searchfight.Providers.Interfaces;

namespace test.Mocks
{
    public class NosenseProvider : CommonProvider, IProvider
    {
        public override SearchResult Search(SearchMessage msg)
        {
            throw new System.NotImplementedException();
        }
    }
}