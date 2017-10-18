using System;

namespace PaliCanon.Loader.Provider
{
    interface INotifier
    {
        event EventHandler<NotifyEventArgs> OnNotify;
    }
}