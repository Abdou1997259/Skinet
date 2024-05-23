namespace Skinet.API.Extentions
{
    public static class SwaggerServicesExtentions
    {
        public static IServiceCollection AddSwaggerDocumention(this IServiceCollection services)
        {

            services.AddSwaggerGen();
            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumention(this IApplicationBuilder app )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
