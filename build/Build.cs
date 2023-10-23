using System;
using System.Linq;
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

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] 
    readonly Solution Solution;

    [GitVersion] GitVersion GitVersion;
    
    AbsolutePath OutputDirectory => RootDirectory / "output";

    const string RepositoryUrl = "https://github.com/BorisGerretzen/SquareGridLib";
    
    
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetPack(s => s
                .SetProject(Solution.GetProject("SquareGridLib"))
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetDescription("Blazor dashboard layout component that allows for placing panels on a 2d grid.")
                .SetPackageTags("blazor dashboard dashboard-layout")
                .SetNoDependencies(true)
                .SetOutputDirectory(OutputDirectory / "nuget")
                .SetAuthors("Boris Gerretzen")
                .SetPackageProjectUrl(RepositoryUrl)
                .SetRepositoryUrl(RepositoryUrl)
                .SetVersion(GitVersion.NuGetVersionV2)
            );
        });
}
