using System.IO;
using System.Threading.Tasks;
using TwicasDotNet.Model;

namespace TwicasDotNet
{
    public interface IAPIRequestClient
    {
        Task<Stream> getLiveThinbnal(string userID);
        Task<movieObject> getMovieInfo(string movieID);
        Task<UserObject> getUserInfo(string userName);
    }
}