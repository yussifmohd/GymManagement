using GymManagement.Domain.Common;
using GymManagment.Infrastructure.Common.Presistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymManagementDbContext dbContext)
        {
            var transaction = dbContext.Database.BeginTransaction();

            context.Response.OnCompleted(async () =>
            {
                try
                {
                    if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
                        value is Queue<IDomainEvent> domainEventsQueue)
                    {
                        while (domainEventsQueue!.TryDequeue(out var domainEvent))
                        {
                            await publisher.Publish(domainEventsQueue);
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // notify the client that even though they got a good response, the changes didn't take place
                    // due to an unexpected error
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            });

            await _next(context);
        }
    }
}
