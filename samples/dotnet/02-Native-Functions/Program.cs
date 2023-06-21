﻿using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Plugins;

var kernelSettings = KernelSettings.LoadSettings();
IKernel kernel = new KernelBuilder()
    .WithCompletionService(kernelSettings)
    .Build();

var pluginsDirectory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "plugins");

// Import the semantic functions
kernel.ImportSemanticSkillFromDirectory(pluginsDirectory, "OrchestratorPlugin");
kernel.ImportSemanticSkillFromDirectory(pluginsDirectory, "SummarizeSkill");

// Import the native functions 
var mathPlugin = kernel.ImportSkill(new MathPlugin(), "MathPlugin");
var orchestratorPlugin = kernel.ImportSkill(new OrchestratorPlugin(kernel), "OrchestratorPlugin");

// Make a request that runs the Sqrt function
var result1 = await orchestratorPlugin["RouteRequest"].InvokeAsync("What is the square root of 634?");
Console.WriteLine(result1);

// Make a request that runs the Add function
var result2 = await orchestratorPlugin["RouteRequest"].InvokeAsync("What is 42 plus 1513?");
Console.WriteLine(result2);