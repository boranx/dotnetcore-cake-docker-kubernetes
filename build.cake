var target = Argument("target", "Default");

var projectName = "SampleWebApiAspNetCore";
var solution = "./" + projectName + ".sln";
var deployable_project = "./src/" + projectName + "/" + projectName + ".csproj";
var distDirectory = Directory("./artifacts/");
var configuration = Argument<string>("configuration", "Release");
var framework = Argument<string>("framework", "netcoreapp2.0");

Task("Clean")
    .Does(() =>
    {
        Information("Cleaning Binary folders");
        CleanDirectories("./**/bin");
        CleanDirectories("./**/obj");
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(solution, new DotNetCoreBuildSettings
        {
            Framework = framework,
            Configuration = configuration,
        });
    });

Task("PublishWeb")
    .IsDependentOn("Build")  
    .Does(() =>
    {
    var settings = new DotNetCorePublishSettings
        {
            Framework = framework,
            Configuration = configuration,
            OutputDirectory = distDirectory
        };

     DotNetCorePublish(deployable_project, settings);
    });


Task("Default")
    .IsDependentOn("PublishWeb");

RunTarget(target);