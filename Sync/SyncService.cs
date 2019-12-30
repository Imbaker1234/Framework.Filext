using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminTools;
using Filext.Core;

namespace Filext.Sync
{
    public class SyncService : ISyncService
    {
        public HashSet<Monitor> Monitors { get; set; }
        public Dictionary<string, string> Changes { get; set; }
        public int Interval { get; set; }
        public IFileService FileService { get; set; }

        public SyncService(string localPath, string remotePath, IFileService fileService, int interval = 60)
        {
            Monitors = new HashSet<Monitor> {new Monitor(this, localPath, remotePath)};
            Changes = new Dictionary<string, string>();
            Interval = interval;
            FileService = fileService;
            Task.Run(() => Synchronize(interval));
        }

        private void Synchronize(int interval)
        {
            while (true) //Replace this with a bool to stop and start service.
            {
                Thread.Sleep(interval * 1000);
                ProcessChanges();
            }
        }

        /// <summary>
        /// Allows for multiple paths, specified via a Dictionary (string(localPath), string(remotePath)).
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="interval"></param>
        /// <param name="fileService"></param>
        /// <param name="authorizingUser"></param>
        public SyncService(Dictionary<string, string> paths, IFileService fileService, int interval = 60)
        {
            Monitors = new HashSet<Monitor>();
            foreach (var kvp in paths)
            {
                Monitors.Add(new Monitor(this, kvp.Key, kvp.Value));
            }

            Changes = new Dictionary<string, string>();
            Interval = interval;
            FileService = fileService;

            while (true)
            {
                Thread.Sleep(interval * 1000);
                ProcessChanges();
            }
        }

        public void ProcessChanges()
        {
            List<string> changedPaths = Changes.Keys.ToList();

            foreach (var localPath in changedPaths)
            {
                //Naming this for readability.
                var remotePath = Changes[localPath];

                //Overwrite the changed file.
                FileService.Copy(localPath, remotePath);

                //Remove this from the dictionary.
                Changes.Remove(localPath);
            }
        }

        /// <summary>
        /// Perform one last copy of the local directory to the remote directory.
        /// </summary>
        public void Dispose()
        {
            foreach (var monitor in Monitors)
            {
                FileService.Copy(monitor.LocalBaseDirectory, monitor.RemoteBaseDirectory);
            }
        }
    }
}