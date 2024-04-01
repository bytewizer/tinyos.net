using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace TinyOS.NET.Sdk
{
    public class DeployTask : Task
    {
        [Required]
        public ITaskItem[] SourceFiles { get; set; }

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
            foreach (ITaskItem filePath in SourceFiles)
            {
                if (filePath.ItemSpec.Length > 0)
                {
                    string path = filePath.ItemSpec;
                    Log.LogMessage(MessageImportance.High, $"Deploy -> Path: {path}");
                }
            }

            Log.LogMessage(MessageImportance.High, $"Deploy -> RemotePath: {RemotePath}, Host:{Host}, Username: {Username}, Password: {Password}");
            
            return !Log.HasLoggedErrors;
        }
    }
}
