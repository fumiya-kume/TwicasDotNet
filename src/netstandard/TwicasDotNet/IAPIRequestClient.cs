using System.IO;
using System.Threading.Tasks;
using TwicasDotNet.Model;

namespace TwicasDotNet
{
    public interface IAPIRequestClient
    {
        Task<Stream> GetLiveThinbnal(string userID);
        Task<movieObject> GetMovieInfo(string movieID);
        Task<UserObject> GetUserInfo(string userName);
    }
}