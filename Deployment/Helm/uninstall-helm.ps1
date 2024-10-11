# Define the base directory for the Helm charts (current directory by default)
$baseDir = (Get-Location).Path  # This will get the current working directory

# Define your release names
$helmReleases = @(
    "basket",
    "basketdb",
    "catalog",
    "catalogdb",
    "discount",
    "discountdb",
    "elasticsearch",
    "kibana",
    "ocelotapigw",
    "orderdb",
    "ordering",
    "rabbitmq"
)

# Optional: Set namespace if needed (comment out if not using)
$namespace = "microservices"  # Example namespace (replace as needed)

# Iterate through each Helm release and uninstall it
foreach ($releaseName in $helmReleases) {
    Write-Host "Uninstalling Helm release: $releaseName"

    # Construct the Helm uninstall command
    $helmArgs = @("uninstall", $releaseName)

    # Add namespace to the command if set
    if ($namespace) {
        $helmArgs += "--namespace"
        $helmArgs += $namespace
    }

    # Execute the Helm uninstall command
    & helm $helmArgs

    # Check for any errors during execution
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error uninstalling $releaseName. Skipping to the next release."
        continue
    } else {
        Write-Host "Successfully uninstalled $releaseName."
    }
}

Write-Host "Helm uninstallation completed!"
