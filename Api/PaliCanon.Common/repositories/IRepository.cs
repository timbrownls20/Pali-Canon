

namespace PaliCanon.Common.Repository
{
    public interface IRepository<T>
    {
        void Insert(T record);

        //T Get(U id);
    }
}