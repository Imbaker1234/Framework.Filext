using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using AdminTools;
using FileService = Filext.Core.FileService;

namespace Filext.XT
{
    /// <summary>
    /// <para>
    /// Exposes the Microsoft Files API via the COM standard with additional
    /// utility methods.
    /// </para>
    /// <para>
    /// Once the AdminService is set using the InitializeAdminService method all
    /// methods of this class will be executed as the supplied user.
    /// </para>
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("CAFBCB71-2F61-44D8-BFC5-3C4FC75A51D5")]
    [ProgId("Prometric.UAS.FilextService")]
    [ComDefaultInterface(typeof(IFilextService))]
    [AutomationProxy(true)]
    public class FilextService : FileService, IFilextService
    {
        private ImpersonatorService AdminService { get; set; } = null;

        public void InitializeAdminService(string user, string pass, string domain = "")
        {
            AdminService = new ImpersonatorService(user, pass, domain);
        }

        /// <summary>
        /// <para>
        /// Returns the contents of the file at the specified filepath.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public override string Read(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Read(filepath);
            }
        }

        /// <summary>
        /// Returns the files contents as an array of string
        /// corresponding to the individual lines of the file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public override string[] ReadLines(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.ReadLines(filepath);
            }
        }

        /// <summary>
        /// <para>
        /// Creates a new file, writing the content to the specified
        /// filepath.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="content"></param>
        public override void WriteNewFile(string filepath, string content)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.WriteNewFile(filepath, content);
            }
        }

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
        public override void OverwriteFile(string filepath, string content)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.OverwriteFile(filepath, content);
            }
        }

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
        public override void AppendToFile(string filepath, string content)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.AppendToFile(filepath, content);
            }
        }

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
        public override void ReplaceTextInFile(string filepath, string originalText, string replacementText)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.ReplaceTextInFile(filepath, originalText, replacementText);
            }
        }

        /// <summary>
        /// Determines whether an item, file or directory, exists at the specified file
        /// path.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public override bool Exists(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Exists(filepath);
            }
        }

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
        public override void Delete(string filepath, bool exceptionOnFailure)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Delete(filepath, exceptionOnFailure);
            }
        }


        /// <summary>
        /// Creates a file at the specified path.
        /// </summary>
        /// <param name="filePath"></param>
        public override void CreateFile(string filePath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.CreateFile(filePath);
            }
        }

        /// <summary>
        /// <para>
        /// Creates a directory at the specified path, recursively creating any
        /// parent folders necessary in order to do so.
        /// </para>
        /// </summary>
        /// <param name="filePath"></param>
        public override void CreateDirectory(string filePath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.CreateDirectory(filePath);
            }
        }

        /// <summary>
        /// Copies a file or recursively copies a directory from the specified originalPath
        /// to the destinationPath.
        /// <para>
        /// Preemptively creates parent folders for files to be copied in this way.
        /// </para>
        /// </summary>
        /// <param name="originalPath"></param>
        /// <param name="destinationPath"></param>
        public override void Copy(string originalPath, string destinationPath, bool overwrite = true)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Copy(originalPath, destinationPath, overwrite);
            }
        }

        /// <summary>
        /// Moves the specified file/directory from the originalPath to the destinationPath.
        /// </summary>
        /// <param name="originalPath"></param>
        /// <param name="destinationPath"></param>
        public override void Move(string originalPath, string destinationPath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Move(originalPath, destinationPath);
            }
        }

        /// <summary>
        /// <para>
        /// Encrypts the file. Allowing only the user who encrypted it to read the file
        /// by calling the Decrypt method.
        /// </para>
        /// <param name="filePath"></param>
        /// </summary>
        public override string Encrypt(string plainText)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Encrypt(plainText);
            }
        }

        /// <summary>
        /// Decrypts a file previously encrypted with user level encryption using this
        /// library/the MS Files API. Only the user who previously encrypted the file
        /// will be able to retrieve usable information from files encrypted this way.
        /// </summary>
        /// <param name="obfuscatedText"></param>
        /// <returns></returns>
        public override string Decrypt(string obfuscatedText)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Decrypt(obfuscatedText);
            }
        }

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
        public override void AddFileSecurity(string fileName, string account, AccessControlType controlType,
            FileSystemRights rights)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.AddFileSecurity(fileName, account, controlType, rights);
            }
        }


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
        public override void RemoveFileSecurity(string fileName, string account, AccessControlType controlType,
            FileSystemRights rights)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.RemoveFileSecurity(fileName, account, controlType, rights);
            }
        }

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
        public override void AddDirectorySecurity(string fileName, string account,
            AccessControlType ControlType, FileSystemRights rights,
            InheritanceFlags inheritance, PropagationFlags propagation)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.AddDirectorySecurity(fileName, account,
                ControlType, rights, inheritance, propagation);
            }
        }

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
        public override void RemoveDirectorySecurity(string fileName, string account,
            AccessControlType controlType, FileSystemRights rights,
            InheritanceFlags inheritance = InheritanceFlags.ContainerInherit,
            PropagationFlags propagation = PropagationFlags.None)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.RemoveDirectorySecurity(fileName, account,
                    controlType, rights, inheritance, propagation);
            }
        }

        /// <summary>
        /// Returns the last time the file/directory at this location was accessed.
        /// <para>
        /// If nothing exists at the specified path the lowest possible DateTime value
        /// will be returned.
        /// </para>
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public override DateTime GetLastAccessTime(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.GetLastAccessTime(filepath);
            }
        }
    }
}
