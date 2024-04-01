using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace TinyOS.NET.Sdk
{
    public class CleanTask : Task
    {
        [Required]
        public string RemotePath { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, $"Clean -> RemotePath: {RemotePath}, Host:{Host}, Username: {Username}, Password: {Password}");
            
            return !Log.HasLoggedErrors;
        }
    }
}
