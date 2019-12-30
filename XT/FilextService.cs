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
    [Guid("85B5F49D-A276-4C5C-82F2-5A7D393D4926")]
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

        public override string Read(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Read(filepath);
            }
        }

        public override string[] ReadLines(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.ReadLines(filepath);
            }
        }

        public override void WriteNewFile(string filepath, string content)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.WriteNewFile(filepath, content);
            }
        }

        public override void OverwriteFile(string filepath, string content)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.OverwriteFile(filepath, content);
            }
        }

        public override void AppendToFile(string filepath, string content)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.AppendToFile(filepath, content);
            }
        }

        public override void ReplaceTextInFile(string filepath, string originalText, string replacementText)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.ReplaceTextInFile(filepath, originalText, replacementText);
            }
        }

        public override bool Exists(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Exists(filepath);
            }
        }

        public override void Delete(string filepath, bool exceptionOnFailure)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Delete(filepath, exceptionOnFailure);
            }
        }

        public override void Create(string filePath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Create(filePath);
            }
        }

        public override void CreateDirectory(string filePath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.CreateDirectory(filePath);
            }
        }

        public override void Copy(string originalPath, string destinationPath, bool overwrite = true)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Copy(originalPath, destinationPath, overwrite);
            }
        }

        public override void Move(string originalPath, string destinationPath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.Move(originalPath, destinationPath);
            }
        }

        public override string Encrypt(string plainText)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Encrypt(plainText);
            }
        }

        public override string Decrypt(string obfuscatedText)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.Decrypt(obfuscatedText);
            }
        }

        public override void AddFileSecurity(string fileName, string account, AccessControlType controlType,
            FileSystemRights rights)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.AddFileSecurity(fileName, account, controlType, rights);
            }
        }

        public override void RemoveFileSecurity(string fileName, string account, AccessControlType controlType,
            FileSystemRights rights)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.RemoveFileSecurity(fileName, account, controlType, rights);
            }
        }

        public override void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights,
            AccessControlType ControlType, InheritanceFlags inheritance, PropagationFlags propagation)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.AddDirectorySecurity(FileName, Account, Rights, ControlType);
            }
        }

        public override void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights,
            AccessControlType ControlType)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                base.RemoveDirectorySecurity(FileName, Account, Rights, ControlType);
            }
        }

        public override DateTime GetLastAccessTime(string filepath)
        {
            using (var i = AdminService?.TryImpersonate())
            {
                return base.GetLastAccessTime(filepath);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
