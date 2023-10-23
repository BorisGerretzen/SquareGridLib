using System;
using System.IO;
using System.Linq;
using DefaultNamespace;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter] string NugetApiUrl = "https://api.nuget.org/v3/index.json"; //default
    [Parameter] [Secret] string NugetApiKey;

    [Solution]
    readonly Solution Solution;

    [GitVersion] GitVersion GitVersion;

    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath PackageDirectory => OutputDirectory / "nuget";
    
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s
                .SetProject(Solution.GetProject(Globals.ProjectName))
                .SetConfiguration(Configuration)
            );
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(s => s
                .SetProjectFile(Solution.GetProject(Globals.ProjectName)!.Path)
            );
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution.GetProject(Globals.ProjectName)!.Path)
                .SetConfiguration(Configuration)
            );
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetPack(s => s
                .SetProject(Solution.GetProject(Globals.ProjectName))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .SetDescription("Blazor dashboard layout component that allows for placing panels on a 2d grid.")
                .SetPackageTags("blazor dashboard dashboard-layout")
                .SetNoDependencies(true)
                .SetOutputDirectory(PackageDirectory)
                .SetAuthors("Boris Gerretzen")
                .SetPackageProjectUrl(Globals.RepositoryUrl)
                .SetRepositoryUrl(Globals.RepositoryUrl)
                .SetVersion(GitVersion.NuGetVersionV2)
            );
        });

    Target Push => _ => _
        .DependsOn(Pack)
        .Requires(() => NugetApiUrl)
        .Requires(() => NugetApiKey)
        .Executes(() =>
        {
            PackageDirectory.GlobFiles("*.nupkg")
                .Where(x => !x.Name.EndsWith("symbols.nupkg"))
                .ForEach(x =>
                {
                    DotNetTasks.DotNetNuGetPush(s => s
                        .SetTargetPath(x)
                        .SetSource(NugetApiUrl)
                        .SetApiKey(NugetApiKey)
                        .EnableSkipDuplicate()
                    );
                });
        });
}
