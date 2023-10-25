using LogAPI.Models;
using System.Threading.Tasks;

namespace LogAPI.Interfaces
{
    public interface IRepo
    {
        public  Task<Log?> Add (Log log);
        public Task<Log?> Delete (Log log);
        public Task<Log?> Update (Log log);
        public Task<Log?> Get (int id);
        public Task<ICollection<Log>?> GetAll ();

    }
}
