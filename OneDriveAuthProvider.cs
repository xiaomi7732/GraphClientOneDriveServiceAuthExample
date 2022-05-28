using Microsoft.Graph;

namespace OneDrive.Sample.WorkerService;

internal class OneDriveAuthProvider : IAuthenticationProvider
{
    public Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        throw new NotImplementedException();
    }
}