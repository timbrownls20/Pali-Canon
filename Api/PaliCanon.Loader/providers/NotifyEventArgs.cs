namespace PaliCanon.Loader.Provider
{

    public class NotifyEventArgs : System.EventArgs
    {
        public NotifyEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

}