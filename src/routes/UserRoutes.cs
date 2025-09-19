
namespace Chat_Application.Routes
{

    public static  class UserRoutes
    {

        public static void MapUserEndPoints(this IEndpointRouteBuilder app)
        {

            var usersGroup = app.MapGroup("/users");
            

        }


        } 



}