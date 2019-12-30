using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace Filext.Core
{
    public partial class FileService
    {
        public virtual string Encrypt(string plainText)
        {
            return new string(plainText.Select(x => (char) (x + 1)).ToArray());
        }

        public virtual string Decrypt(string obfuscatedText)
        {
            return new string(obfuscatedText.Select(x => (char) (x - 1)).ToArray());
        }

        public virtual void AddFileSecurity(string fileName, string account,
            AccessControlType controlType, FileSystemRights rights)
        {
            // Get a FileSecurity object that represents the
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Add the FileSystemAccessRule to the security settings.
            fSecurity.AddAccessRule(new FileSystemAccessRule(account,
                rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);
        }

        public virtual void RemoveFileSecurity(string fileName, string account,
            AccessControlType controlType, FileSystemRights rights)
        {
            // Get a FileSecurity object that represents the
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Remove the FileSystemAccessRule from the security settings.
            fSecurity.RemoveAccessRule(new FileSystemAccessRule(account,
                rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);
        }

        public virtual void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights,
            AccessControlType ControlType, InheritanceFlags inheritance, PropagationFlags propagation)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                Rights,
                ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        public virtual void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights,
            AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                Rights,
                ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }
    }
}