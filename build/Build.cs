using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Serilog;

partial class Build : NukeBuild
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    [GitVersion] GitVersion GitVersion;
    [Parameter] [Secret] string NugetApiKey;

    [Parameter] string NugetApiUrl = "https://api.nuget.org/v3/index.json";

    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath PackageDirectory => OutputDirectory / "nuget";

    Target Clean => _ => _
        .Before(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s
                .SetProject(Solution.GetProject(Globals.ProjectName))
                .SetConfiguration(Configuration)
            );

            // remove old builds
            if (PackageDirectory.Exists())
            {
                var oldBuilds = PackageDirectory.GetFiles("*.nupkg").ToList();
                Log.Information("Deleting {NumOldBuilds} old builds from \"{PackageDirectory}\"", oldBuilds.Count, PackageDirectory);
                foreach (var file in oldBuilds)
                {
                    Log.Debug("Deleting file \"{File}\"", file);
                    file.DeleteFile();
                }
            }
        });

    Target Compile => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(Globals.ProjectName)!.Path)
                .SetConfiguration(Configuration)
            );
        });

    public static int Main() => Execute<Build>(x => x.Compile);
}