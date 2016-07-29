[T4Scaffolding.Scaffolder(Description = "Enter a description of Portugues.ControllersWithViews here")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ControllerName,   
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$controllerNameWithoutSuffix = [System.Text.RegularExpressions.Regex]::Replace($ControllerName, "Controller$", "", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)

Scaffold Portugues.ViewModel $controllerNameWithoutSuffix -ModelType $foundModelType.FullName -Force:$overwriteFilesExceptController

Scaffold Portugues.RazorViews $controllerNameWithoutSuffix -ModelType $foundModelType.FullName -Force:$overwriteFilesExceptController