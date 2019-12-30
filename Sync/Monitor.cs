using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Filext.Sync
{
    public class Monitor : FileSystemWatcher
    {
        public SyncService ParentService { get; set; }

        public string LocalBaseDirectory { get; set; }

        public string RemoteBaseDirectory { get; set; }


        public Monitor(SyncService syncService, string localBaseDirectory, string remoteBaseDirectory)
        {
            ParentService = syncService;
            
            if(!ParentService.FileService.Exists(localBaseDirectory)) throw new DirectoryNotFoundException($"Cannot sync non-existent folder at {localBaseDirectory}");

            LocalBaseDirectory = localBaseDirectory;
            RemoteBaseDirectory = remoteBaseDirectory;

            // Watch for changes in LastAccess and LastWrite times, and
            // the renaming of files or directories.
            NotifyFilter = NotifyFilters.LastAccess
                           | NotifyFilters.Attributes
                           | NotifyFilters.LastWrite
                           | NotifyFilters.FileName
                           | NotifyFilters.DirectoryName
                           | NotifyFilters.Security
                           | NotifyFilters.Size;

            // Only watch text files.
            Filter = "*";

            // Add event handlers.
            Changed += OnChanged;
            Created += OnCreated;
            Deleted += OnDeleted;
            Renamed += OnRenamed;

            // Begin watching.
            EnableRaisingEvents = true;
        }

        /// <summary>
        /// When a file changes it should be added to a list dictionary held by
        /// the parent SyncService of this class for later processing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (ParentService.Changes.ContainsKey(e.FullPath)) return; //This path has already been tagged.

            //else: Add the key and its corresponding remote path to the Changes dictionary
            ParentService.Changes.Add(e.FullPath, GetRemotePath(e.FullPath));
        }

        /// <summary>
        /// When a new file is created it should be immediately copied to the new path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            ParentService.FileService.Copy(e.FullPath, GetRemotePath(e.FullPath));
        }

        /// <summary>
        /// Upon the deletion of a localPath immediately delete its remote counterpart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            ParentService.FileService.Delete(ReplaceFirst(e.FullPath, LocalBaseDirectory, RemoteBaseDirectory), false);
        }

        /// <summary>
        /// Renames/moves the old remote counterpart to that of the
        /// new remote counterpart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            ParentService.FileService.Move(GetRemotePath(e.OldFullPath), GetRemotePath(e.FullPath));
        }

        /// <summary>
        /// Returns the remote counterpart of the provided localPath.
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        private string GetRemotePath(string localPath)
        {
            return ReplaceFirst(localPath, LocalBaseDirectory, RemoteBaseDirectory);
        }

        /// <summary>
        /// Unlike with String.Replace this only removes the very first occurrence of
        /// a string from the provided path.
        /// <para>
        /// This provides safety against filepaths like "C:\Users\Admin\DESKTOP\UserFiles\DESKTOP"
        /// which might otherwise confuse the sync.
        /// </para>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="textToReplace"></param>
        /// <param name="replacementText"></param>
        /// <returns></returns>
        public string ReplaceFirst(string path, string textToReplace, string replacementText)
        {
            var startIndex = path.IndexOf(textToReplace, StringComparison.Ordinal);
            var cleansedPath = path.Remove(startIndex, LocalBaseDirectory.Length);

            return cleansedPath.Insert(startIndex, replacementText);
        }
    }
}