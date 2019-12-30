using System.Collections.Generic;
using AdminTools;
using Filext.Sync;

namespace Filext.Core
{
    public partial class FileService
    {
        public SyncService Sync(string localPath, string remotePath, int interval = 60)
        {
            return new SyncService(localPath, remotePath, this, interval);
        }

        public SyncService Sync(Dictionary<string, string> paths, int interval = 60,
            Impersonator authorizingUser = null)
        {
            return new SyncService(paths, this, interval);
        }
    }
}