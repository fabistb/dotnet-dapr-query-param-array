$daprPlacementContainerId = docker ps -f name=dapr_placement -q

$daprPlacementPort = 50005

if($daprPlacementContainerId) {
    $daprPlacementPort = (docker inspect $daprPlacementContainerId | ConvertFrom-Json).HostConfig.PortBindings[0].psobject.Properties.Value.HostPort
}

daprd --app-id queryparams `
    --app-port 5000 `
    --placement-host-address $("localhost:"+$daprPlacementPort) `
    --log-level debug `
    --components-path ./DaprQueryParamArray/DaprQueryParamArray/components/ `
    --config ./DaprQueryParamArray/DaprQueryParamArray/tracing.yaml `
    --dapr-http-port 3500 `
    --dapr-grpc-port 50001