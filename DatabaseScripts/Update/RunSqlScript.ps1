param(
    [string]$setParamsScript #,[string]$connectionString
)

 $connectionString = "__SQLCONNSTRING__"
Write-Verbose -Verbose $connectionString
$setParamsScript | Out-String

$lines = Get-Childitem $setParamsScript
$lines | Out-String
ForEach($line in $lines )
{
	#$out = $line.split(".")[0] + ".sql";
	#Write-Verbose -Verbose $out
	#$line | Out-String
	$query = (Get-Content $line) | Out-String 
	Write-Verbose -Verbose $query
	$connection = New-Object -TypeName System.Data.SqlClient.SqlConnection($connectionString)
#	$query = [IO.File]::ReadAllText($setParamsScript)
	$command = New-Object -TypeName System.Data.SqlClient.SqlCommand($query, $connection)
	$connection.Open()
	$command.ExecuteNonQuery()
	$connection.Close()
}