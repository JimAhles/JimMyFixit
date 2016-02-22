param(
    [string]$setParamsFilePath
)
Write-Verbose -Verbose "Entering script Replace-SetParameters.ps1"
Write-Verbose -Verbose ("Path to SetParametersFile: {0}" -f $setParamsFilePath)

# get the environment variables
gci -path env:*

# read in the setParameters file
$contents = gc -Path $setParamsFilePath
 gc -Path $setParamsFilePath
# perform a regex replacement
$newContents = "";
$contents | % {
    $line = $_
	#$line | Out-String
	if ($_ -match "__(\w+)__") {
	$Matches[0] | Out-String
        $setting = gci -path env:* | ? { $_.Name -eq $Matches[0]  }
	    if ($setting) {
            Write-Verbose -Verbose ("Replacing key {0} with value {1} from environment" -f $setting.Name, $setting.Value)
            $line = $_ -replace "__(\w+)__", $setting.Value
        }
    }
    $newContents += $line + [Environment]::NewLine
}

Write-Verbose -Verbose "Overwriting SetParameters file with new values"
sc $setParamsFilePath -Value $newContents
 $newContents | Out-String
Write-Verbose -Verbose "Exiting script Replace-SetParameters.ps1"
#Start-Sleep -Seconds 30