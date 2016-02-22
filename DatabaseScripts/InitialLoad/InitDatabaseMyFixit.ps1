#
# InitDatabaseMyFixit.ps1
#

$SubscriptionId = " 9a2cbe62-072a-410d-85a6-7885d002bf48"
$ResourceGroupName = "MyFixitMnscuResource"
$Location = "West US"

$ServerName = "myfixitmnscudf1"

$FirewallRuleName = "rule1"
$FirewallStartIP = "199.17.246.149"
$FirewallEndIp = "199.17.246.149"

$DatabaseName = "MyFixitTasks"
$DatabaseEdition = "Basic"
$DatabasePerfomanceLevel = "Basic"



Select-AzureRmSubscription -SubscriptionId $SubscriptionId

$ResourceGroup = New-AzureRmResourceGroup -Name $ResourceGroupName -Location $Location

$Server = New-AzureRmSqlServer -ResourceGroupName $ResourceGroupName -ServerName $ServerName -Location $Location -ServerVersion "12.0"

$FirewallRule = New-AzureRmSqlServerFirewallRule -ResourceGroupName $ResourceGroupName -ServerName $ServerName -FirewallRuleName $FirewallRuleName -StartIpAddress $FirewallStartIP -EndIpAddress $FirewallEndIp

$SqlDatabase = New-AzureRmSqlDatabase -ResourceGroupName $ResourceGroupName -ServerName $ServerName -DatabaseName $DatabaseName -Edition $DatabaseEdition -RequestedServiceObjectiveName $DatabasePerfomanceLevel

$SqlDatabase



