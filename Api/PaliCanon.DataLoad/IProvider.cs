namespace PaliCanon.DataLoad
{
    public interface IProvider: INotifier
    {
        void Load();

    }
}