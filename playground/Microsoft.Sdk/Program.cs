using Microsoft.Sdk.Processes;

var result = new ProcessRunner("dotnet.exe", new ProcessArgumentBuilder()
          .Append("--list-sdks"))
          .WaitForExit();

if (!result.Success)
    throw new FileNotFoundException("Could not find dotnet tool");

Console.WriteLine(result.GetOutput());