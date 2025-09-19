// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.EntityFrameworkCore;

// namespace Chat_Application.src.Helpers
// {
//     public class EntityExistsFilter<TSchema> : IEndpointFilter where TSchema : class
//     {
//         private readonly DbContext _db;

//         public EntityExistsFilter(DbContext db)
//         {
//             _db = db;
//         }

//         public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
//         {
//             // Assume "id" is the first argument in the endpoint method
//             var id = context.GetArgument<string>(0);

//             var item = await _db.Set<TSchema>().FindAsync(id);

//             if (item == null)
//             {
//                 return TypedResults.NotFound();
//             }

//             return await next(context);
//         }
//     }
// }
