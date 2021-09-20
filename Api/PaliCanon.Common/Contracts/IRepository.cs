

namespace PaliCanon.Common.Contracts
{
    public interface IRepository<T>
    {
        void Insert(T record);
     
    }
}