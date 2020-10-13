using System;
using FlubuCore.Context;
using FlubuCore.Context.Attributes.BuildProperties;
using FlubuCore.IO;
using FlubuCore.Scripting;
using FlubuCore.Tasks.Docker.Service;

namespace Build
{
    public class BuildScript : DefaultBuildScript
    {
        public FullPath OutputDir => RootDirectory.CombineWith("output");

        protected override void ConfigureBuildProperties(IBuildPropertiesContext context)
        {
            context.Properties.Set(BuildProps.ProductId, "FlubuCore.Teams");
            context.Properties.Set(BuildProps.SolutionFileName, "FlubuCore.TeamsPlugin.sln");
            context.Properties.Set(BuildProps.BuildConfiguration, "Release");
        }

        protected override void ConfigureTargets(ITaskContext context)
        {
            var buildVersion = context.CreateTarget("buildVersion")
                .SetAsHidden()
                .SetDescription("Fetches flubu version from FlubuCore.Gitter.ProjectVersion.txt file.")
                .AddTask(x => x.FetchBuildVersionFromFileTask());

            var compile = context.CreateTarget("Compile")
                .AddCoreTask(x => x.Restore())
                .AddCoreTask(x => x.UpdateNetCoreVersionTask("FlubuCore.TeamsPlugin/FlubuCore.Teams.csproj"))
                .AddCoreTask(x => x.Build())
                .DependsOn(buildVersion);

            var nugetPublish = context.CreateTarget("Nuget.Publish")
                .DependsOn(compile)
                .AddCoreTask(x => x.Pack().Project("FlubuCore.TeamsPlugin")
                    .NoBuild()
                    .IncludeSymbols()
                    .OutputDirectory(OutputDir))
                .Do(PublishNuGetPackage);

            context.CreateTarget("Rebuild")
                .SetAsDefault()
                .DependsOn(compile, nugetPublish);
        }

        private void PublishNuGetPackage(ITaskContext context)
        {
            var version = context.Properties.GetBuildVersion();
            var nugetVersion = version.Version.ToString(3);

            context.CoreTasks().NugetPush(OutputDir.CombineWith($"FlubuCore.Teams.{nugetVersion}.nupkg"))
                .ForMember(x => x.ApiKey("Not provided"), "nugetKey", "Nuget api key.")
                .ServerUrl("https://www.nuget.org/api/v2/package")
                .Execute(context);
        }
    }
}
