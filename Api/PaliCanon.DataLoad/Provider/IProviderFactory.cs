using PaliCanon.Common.Enums;

namespace PaliCanon.DataLoad.Provider
{
    public interface IProviderFactory
    {
        IProvider Get(Book book);
    }
}