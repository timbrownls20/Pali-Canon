namespace PaliCanon.DataLoad
{
    interface IProvider: INotifier
    {
        void Load();

    }
}