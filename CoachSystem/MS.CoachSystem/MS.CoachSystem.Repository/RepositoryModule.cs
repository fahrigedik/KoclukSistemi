using Autofac;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Repository.Repositories;

namespace MS.CoachSystem.Repository;
public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
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
