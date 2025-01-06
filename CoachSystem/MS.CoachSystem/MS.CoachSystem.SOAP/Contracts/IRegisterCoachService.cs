using System.ServiceModel;

namespace MS.CoachSystem.SOAP.Contracts
{
    [ServiceContract]
    public interface IRegisterCoachService
    {
        [OperationContract]
        string RegisterCoachUser(
            string userName,
            string email,
            string name,
            string surname,
            string password,
            string city,
            string birthDate,
            string tckn,
            int gender);
    }
}
