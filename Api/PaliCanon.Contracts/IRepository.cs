

namespace PaliCanon.Contracts
{
    public interface IRepository<T>
    {
        void Insert(T record);
    }
}