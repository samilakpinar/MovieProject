using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICastService
    {
        Task<List<Cast>> GetPopulerCast(int movieId);
    }
}
