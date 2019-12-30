using System;
using System.IO;
using System.Security.AccessControl;
using AdminTools;

namespace Filext.Core
{
    public partial class FileService : IFileService
    {
        public virtual string Read(string filepath)
        {
            return File.ReadAllText(filepath);
        }

        public virtual string[] ReadLines(string filepath)
        {
            return File.ReadAllLines(filepath);
        }

        public virtual void WriteNewFile(string filepath, string content)
        {
            File.WriteAllText(filepath, content);
        }

        public virtual void OverwriteFile(string filepath, string content)
        {
            WriteNewFile(filepath, content);
        }

        public virtual void AppendToFile(string filepath, string content)
        {
            File.AppendAllText(filepath, content);
        }

        public virtual void ReplaceTextInFile(string filepath, string originalText, string replacementText)
        {
            OverwriteFile(filepath, Read(filepath).Replace(originalText, replacementText));
        }

        public virtual bool Exists(string filepath)
        {
            if (Directory.Exists(filepath)) return true;
            if (File.Exists(filepath)) return true;

            return false;
        }

        public virtual void Delete(string filepath, bool exceptionOnFailure)
        {
            if (Directory.Exists(filepath))
            {
                //Empty out files in current directory
                foreach (var file in Directory.GetFiles(filepath))
                {
                    Delete(file, exceptionOnFailure);
                }

                //Delve in to subdirectories and delete their child files.
                foreach (var directory in Directory.GetDirectories(filepath))
                {
                    Delete(directory, exceptionOnFailure);
                }

                try
                {
                    //Clear the directory at the end.
                    Directory.Delete(filepath);
                }
                catch (Exception)
                {
                    if (exceptionOnFailure) throw;
                }
            }
            else if (File.Exists(filepath))
            {
                try
                {
                    //Clear the directory at the end.
                    File.Delete(filepath);
                }
                catch (Exception)
                {
                    if (exceptionOnFailure) throw;
                }
            }
        }

        public virtual void Create(string filePath)
        {
            File.Create(filePath);
        }

        public virtual void CreateDirectory(string filePath)
        {
            Directory.CreateDirectory(filePath);
        }

        public virtual void Copy(string originalPath, string destinationPath, bool overwrite = true)
        {
            if (Directory.Exists(originalPath))
            {
                Directory.CreateDirectory(destinationPath);

                foreach (var file in Directory.GetFiles(originalPath))
                {
                    //Copy the file to be a child of the destination path.
                    Copy(file, Path.Combine(destinationPath, Path.GetFileName(file)), overwrite);
                }

                foreach (var directory in Directory.GetDirectories(originalPath))
                {
                    Copy(directory, directory.Replace(originalPath, destinationPath), overwrite);
                }
            }
            else if (File.Exists(originalPath))
            {
                //Create the parent of the destination file to ensure we have a valid place to copy to.
                Directory.CreateDirectory(Directory.GetParent(destinationPath).ToString());
                File.Copy(originalPath, destinationPath, true);
            }
        }

        public virtual void Move(string originalPath, string destinationPath)
        {
            if (File.Exists(originalPath))
            {
                File.Move(originalPath, destinationPath);
            }
            else if (Directory.Exists(originalPath))
            {
                Directory.Move(originalPath, destinationPath);
            }
            else
            {
                throw new FileNotFoundException($"No item available at {originalPath}");
            }
        }

        public virtual DateTime GetLastAccessTime(string filepath)
        {
            {
                if (Directory.Exists(filepath))
                {
                    Directory.GetLastAccessTime(filepath);
                }
                else if (File.Exists(filepath))
                {
                    return File.GetLastAccessTime(filepath);
                }

                return DateTime.MinValue;
            }
        }

        public DateTime GetLastWriteTime(string filepath, Impersonator authorizingUser = null)
        {
            {
                if (Directory.Exists(filepath))
                {
                    Directory.GetLastWriteTime(filepath);
                }
                else if (File.Exists(filepath))
                {
                    return File.GetLastWriteTime(filepath);
                }

                return DateTime.MinValue;
            }
        }
    }
}