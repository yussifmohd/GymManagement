using GymManagement.Api.IntegrationTests.Common;

namespace GymManagment.Api.IntegrationTests.Common
{
    [CollectionDefinition(CollectionName)]
    public class GymManagementApiFactoryCollection : ICollectionFixture<GymManagementApiFactory>
    {
        public const string CollectionName = "GymManagementApiFactoryCollection";
    }
}
