using System.Text.Json;

namespace HostApp;

public class DeviceCatalogService
{
    private readonly DeviceRepository _repository = new();

    public string Entries()
    {
        return JsonSerializer.Serialize(_repository.Devices, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
