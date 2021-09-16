using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using DataAccess.Repositories;
using Entities.Concrete;


namespace DataAccess.Concrete
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(MovieStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
