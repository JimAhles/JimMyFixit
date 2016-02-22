$s = @{
"StorageAccountName" = "__STORAGEACCOUNT__";
"StorageAccountAccessKey" = "__STORAGEACCESSKEY__"
}

#Write-Verbose -Verbose "after storage account settings"
Set-AzureWebsite __AZURESITENAME__ -AppSettings $s
#Write-Verbose -Verbose ("after site name __AZURESITENAME__")
$cs1 = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
$cs1.Name = "MyFixItTasksEntities"
$cs1.ConnectionString = "__ENTITYCONNECTION1__" 
$cs1.ConnectionString = $cs1.ConnectionString + "__ENTITYCONNECTION2__"
#Write-Verbose ("Connection string is {0}",$cs1.ConnectionString)
$cs1.Type = "Custom"
$cs2 = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
$cs2.Name = "AzureWebJobsDashboard"
$cs2.ConnectionString = "__JOBDASHBOARD__"
$cs2.Type = "Custom"
$cs3 = New-Object Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebEntities.ConnStringInfo
$cs3.Name = "AzureWebJobsStorage"
$cs3.ConnectionString = "__JOBSTORAGE__"
$cs3.Type = "Custom"
$listOfConnectionStrings = (Get-AzureWebSite __AZURESITENAME__).ConnectionStrings
$listOfConnectionStrings.Clear()
$listOfConnectionStrings.Add($cs1)
$listOfConnectionStrings.Add($cs2)
$listOfConnectionStrings.Add($cs3)
Set-AzureWebsite __AZURESITENAME__ -ConnectionStrings $listOfConnectionStrings