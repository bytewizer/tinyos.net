
namespace TinyOS.Extension.Debug
{
    public interface ICustomLogger
    {
        void LogError(string v, Exception exception);
        void LogMessage(string message);
    }
}