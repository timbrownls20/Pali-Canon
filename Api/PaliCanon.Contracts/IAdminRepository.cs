namespace PaliCanon.Contracts
{
    public interface IAdminRepository
    {
        bool CanConnect();

        bool Migrate();
    }
}