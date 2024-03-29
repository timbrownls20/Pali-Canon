

namespace PaliCanon.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Insert(T record);
    }
}