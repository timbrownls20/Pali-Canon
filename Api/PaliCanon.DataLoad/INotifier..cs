using System;

namespace PaliCanon.DataLoad
{
    public interface INotifier
    {
        event EventHandler<NotifyEventArgs> OnNotify;
    }
}