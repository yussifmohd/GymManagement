using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.SubcutaneousTests.Common
{
    [CollectionDefinition(CollectionName)]
    public class MediatorFactoryCollection : ICollectionFixture<MediatorFactory>
    {
        public const string CollectionName = "MediatorFactoryCollection";
    }
}
