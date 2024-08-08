using BookStoreAPI.Repositories;
using BookStoreAPI.Repository;
using BookStoreAPI.Services;

namespace SampleCore.Utility
{
    public class DIResolver
    {
        public static void ConfigureServices(IServiceCollection Services)
        {
            //for accessing the http context by interface in view level
            #region Http context
            Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            //for service accesssing
            #region Service

            Services.AddScoped<IBookService, BookService>();
            # endregion 
            //for database accessing 
            #region Repository

            Services.AddScoped<IBookRepository, BookRepository>();

            #endregion

        }
    }
}
