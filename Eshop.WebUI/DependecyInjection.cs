namespace Eshop;

public static class DependencyInjection
{
    public static IServiceCollection AddWebUI(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        return services;
    }
}