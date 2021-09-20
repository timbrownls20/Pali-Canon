using System;

namespace PaliCanon.DataLoad
{
    interface INotifier
    {
        event EventHandler<NotifyEventArgs> OnNotify;
    }
}