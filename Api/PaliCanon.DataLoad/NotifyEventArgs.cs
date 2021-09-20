namespace PaliCanon.DataLoad
{
    public class NotifyEventArgs : System.EventArgs
    {

        public NotifyEventArgs(string message)
        {
            IsError = false;
            Message = message;
        }

        public NotifyEventArgs(string message, bool isError)
        {
            IsError = isError;
            Message = message;
        }

        public string Message { get; set; }

        public bool IsError { get; set; }
    }

}