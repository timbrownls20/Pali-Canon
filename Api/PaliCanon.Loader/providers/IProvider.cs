using System;

namespace PaliCanon.Loader.Provider
{
    interface IProvider: INotifier
    {
        void Load();

    }
}