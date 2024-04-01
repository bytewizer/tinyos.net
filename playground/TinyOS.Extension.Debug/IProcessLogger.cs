namespace TinyOS.Extension.Debug
{
    public interface IProcessLogger
    {
        void OnOutputDataReceived(string stdout);
        void OnErrorDataReceived(string stderr);
    }
}