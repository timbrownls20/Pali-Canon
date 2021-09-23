using PaliCanon.Common.Enums;

namespace PaliCanon.DataLoad.Provider.Factory
{
    public interface IProviderFactory
    {
        IProvider Get(Book book);
    }
}