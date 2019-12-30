using Filext.Core;

namespace Filext.XT
{
    public interface IFilextService : IFileService
    {
        /// <summary>
        /// <para>
        /// The AdminService contains sensitive information and is private.
        /// </para>
        /// <para>
        /// Additionally COM Standard best practice is to use Factory Methods
        /// exposed via interface to allow for this object to be set.
        /// </para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="domain"></param>
        void InitializeAdminService(string user, string pass, string domain = "");
    }
}