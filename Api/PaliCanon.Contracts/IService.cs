

namespace PaliCanon.Contracts
{
    public interface IService<T> where T : class
    {
        void Insert(T record);
    }
}