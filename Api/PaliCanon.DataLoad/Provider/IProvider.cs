namespace PaliCanon.DataLoad.Provider
{
    public interface IProvider: INotifier
    {
        void Load();
    }
}