using System.Diagnostics;

namespace Microsoft.Sdk.Processes
{
    public static class ProcessExtensions {
        private const int ExitTimeout = 5000;
    
        public static void Terminate(this Process process) {
            if (!process.HasExited) {
                process.Kill();
                process.WaitForExit(ExitTimeout);
            }
            process.Close();
        }
    }
}