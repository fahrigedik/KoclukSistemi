using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Repository.Repositories;

namespace MS.CoachSystem.Repository;
public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        builder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlCon"));
            return new AppDbContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
        builder.RegisterType<CoachingSessionRepository>().As<ICoachingSessionRepository>().InstancePerLifetimeScope();
        builder.RegisterType<GoalRepository>().As<IGoalRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CoachStudentRepository>().As<ICoachStudentRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CoachingResourceRepository>().As<ICoachingResourceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<GoalTypeRepository>().As<IGoalTypeRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ResourceTagRepository>().As<IResourceTagRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ResourceToTagRepository>().As<IResourceToTagRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ResourceTypeRepository>().As<IResourceTypeRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserTaskRepository>().As<IUserTaskRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}
