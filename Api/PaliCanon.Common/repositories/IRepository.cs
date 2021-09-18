

namespace PaliCanon.Common.Repository
{
    public interface IRepository<T>
    {
        void Insert(T record);
     
    }
}