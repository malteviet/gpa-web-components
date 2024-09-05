namespace HostApp;

public class DeviceRepository
{
    public DeviceRepository()
    {
        Devices =
        [
            new()
            {
                Id = "GIDS01DCIP-1.0.934.0-10",
                Urn = "de.gira.schema.components.DcsIp.CallDisplayModule",
                ImageKey = "browserImage",
                Name = "call display module"
            },
            new()
            {
                Id = "GIDS01DCIP-1.0.934.0-10",
                Urn = "de.gira.schema.components.DcsIp.CallButtonModuleSingle",
                ImageKey = "cameraImage",
                Name = "call button single"
            },
            new()
            {
                Id = "GIDS01DCIP-1.0.934.0-10",
                Urn = "de.gira.schema.components.DcsIp.CallButtonModuleDouble",
                ImageKey = "photoImage",
                Name = "call button double"
            },
            new()
            {
                Id = "GIDS01DCIP-1.0.934.0-10",
                Urn = "de.gira.schema.components.DcsIp.CameraModule",
                ImageKey = "speakerImage",
                Name = "camera module"
            },
        ];
    }

    public List<Device> Devices { get; }

}