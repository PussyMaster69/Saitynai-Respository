using System.Threading.Tasks;
using Project.Models;


namespace Project.Services
{
    public interface IWebService
    {
        Task<AuthenticationResult> ValidateGoogleToken(string token);
    }
}