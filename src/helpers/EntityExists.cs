using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using chat_application.Data;

namespace Chat_Application.src.Helpers
{
    public class EntityExistsFilter<TSchema> : IEndpointFilter where TSchema : class
    {
        private readonly AppDbContext _db;

        public EntityExistsFilter(AppDbContext db)
        {
            _db = db;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
      
            var id = context.GetArgument<string>(0);

            var item = await _db.Set<TSchema>().FindAsync(id);

            if (item == null)
            {
                return TypedResults.NotFound();
            }

            return await next(context);
        }
    }
}
