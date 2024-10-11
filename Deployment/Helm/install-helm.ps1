# Define the base directory for the Helm charts (current directory by default)
$baseDir = (Get-Location).Path

# Define your release names and folder paths
$helmReleases = @{
    "basket"        = "$baseDir/Deployment/Helm/basket/"
    "basketdb"      = "$baseDir/Deployment/Helm/basketdb/"
    "catalog"       = "$baseDir/Deployment/Helm/catalog/"
    "catalogdb"     = "$baseDir/Deployment/Helm/catalogdb/"
    "discount"      = "$baseDir/Deployment/Helm/discount/"
    "discountdb"    = "$baseDir/Deployment/Helm/discountdb/"
    "elasticsearch" = "$baseDir/Deployment/Helm/elasticsearch/"
    "kibana"        = "$baseDir/Deployment/Helm/kibana/"
    "ocelotapigw"   = "$baseDir/Deployment/Helm/ocelotapigw/"
    "orderdb"       = "$baseDir/Deployment/Helm/orderdb/"
    "ordering"      = "$baseDir/Deployment/Helm/ordering/"
    "rabbitmq"      = "$baseDir/Deployment/Helm/rabbitmq/"
}

# Optional: Set namespace if needed
$namespace = "microservices"
$createNamespace = $true

# Helm chart default values (use only when required)
$defaultValues = @{
    "elasticsearch" = ""  # Remove autoscaling flag as it's not required
}

# Iterate through each Helm release and install/upgrade
foreach ($release in $helmReleases.GetEnumerator()) {
    $releaseName = $release.Key
    $chartPath = $release.Value

    if (-Not (Test-Path $chartPath)) {
        Write-Host "Warning: Chart path $chartPath not found for release $releaseName. Skipping this release."
        continue
    }

    # Construct the Helm command
    $helmArgs = @("upgrade", "--install", $releaseName, $chartPath)

    if ($namespace) {
        $helmArgs += "--namespace"
        $helmArgs += $namespace
        if ($createNamespace) {
            $helmArgs += "--create-namespace"
        }
    }

    # Add default values for specific releases
    if ($defaultValues.ContainsKey($releaseName)) {
        $helmArgs += $defaultValues[$releaseName]
    }

    Write-Host "Deploying Helm chart: $releaseName from path: $chartPath"
    & helm $helmArgs

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error deploying $releaseName. Skipping to the next release."
        continue
    }
}

Write-Host "Helm deployments completed successfully!"
