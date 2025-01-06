using Autofac;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Repository.Repositories;
using MS.CoachSystem.Service.Client;
using MS.CoachSystem.Service.Services;

namespace MS.CoachSystem.Service;
public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register your services here
        builder.RegisterGeneric(typeof(GenericService<,>)).As(typeof(IGenericService<,>)).InstancePerLifetimeScope();
        builder.RegisterType<CoachingSessionService>().As<ICoachingSessionService>().InstancePerLifetimeScope();
        builder.RegisterType<GoalService>().As<IGoalService>().InstancePerLifetimeScope();
        builder.RegisterType<CoachStudentService>().As<ICoachStudentService>().InstancePerLifetimeScope();
        builder.RegisterType<CoachingResourceService>().As<ICoachingResourceService>().InstancePerLifetimeScope();
        builder.RegisterType<GoalTypeService>().As<IGoalTypeService>().InstancePerLifetimeScope();
        builder.RegisterType<ResourceTagService>().As<IResourceTagService>().InstancePerLifetimeScope();
        builder.RegisterType<ResourceToTagService>().As<IResourceToTagService>().InstancePerLifetimeScope();
        builder.RegisterType<ResourceTypeService>().As<IResourceTypeService>().InstancePerLifetimeScope();
        builder.RegisterType<UserTaskService>().As<IUserTaskService>().InstancePerLifetimeScope();

        builder.RegisterType<StudentService>().InstancePerLifetimeScope();
        builder.RegisterType<SOAPCoachRegisterClient>().InstancePerLifetimeScope();
        builder.RegisterType<AuthService>().InstancePerLifetimeScope();
    }
}
