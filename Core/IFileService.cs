using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using AdminTools;
using Filext.Sync;

namespace Filext.Core
{
    public interface IFileService
    {
        /// <summary>
        /// <para>
        /// Returns the contents of the file at the specified filepath.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        string Read(string filepath);

        /// <summary>
        /// Returns the files contents as an array of string
        /// corresponding to the individual lines of the file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        string[] ReadLines(string filepath);

        /// <summary>
        /// <para>
        /// Creates a new file, writing the content to the specified
        /// filepath.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content"></param>
        void WriteNewFile(string filepath, string content);

        /// <summary>
        /// <para>
        /// Overwrites the file at the specified file path with the provided content
        /// </para>
        /// <para>
        /// If a file does not already exist at that path, one is created.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content"></param>
        void OverwriteFile(string filepath, string content);

        /// <summary>
        /// <para>
        /// Appends content to the end of the specified file.
        /// </para>
        /// <para>
        /// If a file does not already exist at that path, one is created.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content"></param>
        void AppendToFile(string filepath, string content);

        /// <summary>
        /// <para>
        /// Replaces all instances of the originalText within the specified file
        /// with the replacementText.
        /// </para>
        /// <para>
        /// Uses the permissions of the authorizing user to gain access
        /// to the specified file.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="originalText"></param>
        /// <param name="replacementText"></param>
        void ReplaceTextInFile(string filepath, string originalText, string replacementText);

        /// <summary>
        /// Determines whether an item, file or directory, exists at the specified file
        /// path.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        bool Exists(string filepath);

        /// <summary>
        /// Deletes the file or recursively deletes the directory at the specified filepath
        /// after checking if an item exists there.
        /// <para>
        /// If exceptionOnFailure is false then deleting then failures to delete the file
        /// will silently occur. Allowing the execution to proceed.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="exceptionOnFailure"></param>
        void Delete(string filepath, bool exceptionOnFailure);

        /// <summary>
        /// Creates a file at the specified path.
        /// </summary>
        /// <param name="filePath"></param>
        void CreateFile(string filePath);

        /// <summary>
        /// <para>
        /// Creates a directory at the specified path, recursively creating any
        /// parent folders necessary in order to do so.
        /// </para>
        /// </summary>
        /// <param name="filePath"></param>
        void CreateDirectory(string filePath);

        /// <summary>
        /// Copies a file or recursively copies a directory from the specified originalPath
        /// to the destinationPath.
        /// <para>
        /// Preemptively creates parent folders for files to be copied in this way.
        /// </para>
        /// </summary>
        /// <param name="originalPath"></param>
        /// <param name="destinationPath"></param>
        void Copy(string originalPath, string destinationPath, bool overwrite = true);

        /// <summary>
        /// Moves the specified file/directory from the originalPath to the destinationPath.
        /// </summary>
        /// <param name="originalPath"></param>
        /// <param name="destinationPath"></param>
        void Move(string originalPath, string destinationPath);

        /// <summary>
        /// Returns the last time the file/directory at this location was accessed.
        /// <para>
        /// If nothing exists at the specified path the lowest possible DateTime value
        /// will be returned.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        DateTime GetLastAccessTime(string filepath);

        /// <summary>
        /// Returns the last time the file/directory at this location was written to.
        /// <para>
        /// If nothing exists at the specified path the lowest possible DateTime value
        /// will be returned.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="authorizingUser"></param>
        /// <returns></returns>
        DateTime GetLastWriteTime(string filepath, Impersonator authorizingUser = null);

        SyncService Sync(string localPath, string remotePath, int interval = 60);

        SyncService Sync(Dictionary<string, string> paths, int interval = 60,
            Impersonator authorizingUser = null);

        /// <summary>
        /// <para>
        /// Encrypts the file. Allowing only the user who encrypted it to read the file
        /// by calling the Decrypt method.
        /// </para>
        /// <param name="filePath"></param>
        /// </summary>
        string Encrypt(string plainText);

        /// <summary>
        /// Decrypts a file previously encrypted with user level encryption using this
        /// library/the MS Files API. Only the user who previously encrypted the file
        /// will be able to retrieve usable information from files encrypted this way.
        /// </summary>
        /// <param name="obfuscatedText"></param>
        /// <returns></returns>
        string Decrypt(string obfuscatedText);

        /// <summary>
        /// <para>
        /// Adds ACL attributes to the specified file for the specified user.
        /// </para>
        /// <para>
        /// EXAMPLE:
        /// </para>
        /// <para>
        /// The system administrator wants to allow a new employee, GaryJ, to read and add to
        /// a file, but not rewrite the file.
        /// </para>
        /// <para>
        /// AddFileSecurity(@"C:\Users\Admin\ReadAndAddOnlyFile.txt", "GaryJ", AccessControlType.Allow,
        /// FileSystemRights.AppendData | FileSystemRights.Read);
        /// </para>
        /// <para>
        /// This allows GaryJ the desired permissions without allowing him to rewrite the file.
        /// </para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="controlType"></param>
        /// <param name="rights"></param>
        void AddFileSecurity(string fileName, string account,
            AccessControlType controlType, FileSystemRights rights);

        /// <summary>
        /// <para>
        /// Removes ACL attributes from the specified file for the specified user.
        /// </para>
        /// <para>
        /// EXAMPLE:
        /// </para>
        /// <para>
        /// The system administrator wants to remove a users ability to write to or
        /// delete a file.
        /// </para>
        /// <para>
        /// RemoveFileSecurity(@"C:\Temp\MyFile.txt", "Frank.Pippin", AccessControlType.Allow,
        /// FileSystemRights.Write | FileSystemRights.Delete);
        /// </para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="controlType"></param>
        /// <param name="rights"></param>
        void RemoveFileSecurity(string fileName, string account,
            AccessControlType controlType, FileSystemRights rights);


        /// <summary>
        /// <para>
        /// Adds ACL attributes to a directory. By default these attributes
        /// are inherited by the files and subdirectories.
        /// </para>
        /// <para>
        /// EXAMPLE:
        /// </para>
        /// <para>
        /// The following call would grant George.Rhinemann the ability to read files and
        /// call EXEs from the Archives folder.
        /// </para>
        /// <para>
        /// AddDirectorySecurity(@"C:\Users\Admin\Archives", "George.Rhinemann",
        /// AccessControlType.Allow, FileSystemRights.Read | FileSystemRights.ExecuteFile,
        /// InheritanceFlags.ContainerInherit, PropagationFlags.None);
        /// </para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="ControlType"></param>
        /// <param name="rights"></param>
        /// <param name="inheritance"></param>
        /// <param name="propagation"></param>
        void AddDirectorySecurity(string fileName, string account,
            AccessControlType ControlType, FileSystemRights rights,
            InheritanceFlags inheritance = InheritanceFlags.ContainerInherit,
            PropagationFlags propagation = PropagationFlags.None);

        /// <summary>
        /// <para>
        /// Remove ACL attributes from a directory. By default these attributes
        /// are inherited by the files and subdirectories.
        /// </para>
        /// <para>
        /// EXAMPLE:
        /// </para>
        /// <para>
        /// The following call would remove George.Rhinemann's ability to Delete
        /// or Write to the Archives directory and all of its files and subfolders.
        /// </para>
        /// <para>
        /// RemoveDirectorySecurity(@"C:\Users\Admin\Archives",
        /// "George.Rhinemann", AccessControlType.Allow, FileSystemRights.Delete
        /// | FileSystemRights.Write, InheritanceFlags.ContainerInherit,
        /// PropagationFlags.None);
        /// </para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="controlType"></param>
        /// <param name="rights"></param>
        /// <param name="inheritance"></param>
        /// <param name="propagation"></param>
        void RemoveDirectorySecurity(string fileName, string account,
            AccessControlType controlType, FileSystemRights rights,
            InheritanceFlags inheritance = InheritanceFlags.ContainerInherit,
            PropagationFlags propagation = PropagationFlags.None);
    }
}