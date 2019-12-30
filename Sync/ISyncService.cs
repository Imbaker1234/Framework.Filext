using System;
using System.Collections.Generic;
using Filext.Core;

namespace Filext.Sync
{
    public interface ISyncService : IDisposable
    {
        /// <summary>
        /// A dictionary keeping track of all the local files, along with
        /// their corresponding remote paths, that have changed since the
        /// last interval.
        /// </summary>
        Dictionary<string, string> Changes { get; set; }

        /// <summary>
        /// The SyncService is provided by this object which also makes use
        /// of its general file handling capabilities.
        /// </summary>
        IFileService FilextService { get; set; }
        
        /// <summary>
        /// The time, in seconds, between changes being processed/copied from
        /// local directories to their remote counterparts.
        /// </summary>
        int Interval { get; set; }
        /// <summary>
        /// The collection of FileSystemWatchers which monitor the local paths
        /// and populate the SyncService's Changes for processing.
        /// </summary>
        HashSet<Monitor> Monitors { get; set; }

        /// <summary>
        /// <para>
        /// Copies files that have changed since the last interval
        /// before removing the entry from the list of Changes.
        /// </para>
        /// <para>
        /// Takes in a copy of the dictionary keys from Changes
        /// to allow each to avoid running into issues related
        /// with resources being shared across multiple threads.
        /// </para>
        /// </summary>
        void ProcessChanges();
    }
}