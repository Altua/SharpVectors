using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using SharpVectors.Dom.Utils;

namespace SharpVectors.Renderers.Utils
{
    public static class WpfApplicationContext
    {
        public static DirectoryInfo ExecutableDirectory
        {
            get {
                DirectoryInfo di;
                try
                {
#if !NET50
                    FileIOPermission f = new FileIOPermission(PermissionState.None);
                    f.AllLocalFiles = FileIOPermissionAccess.Read;

                    f.Assert();
#endif
                    di = new DirectoryInfo(PathUtils.Combine(
                        System.Reflection.Assembly.GetExecutingAssembly()));
                }
                catch (SecurityException)
                {
                    di = new DirectoryInfo(Directory.GetCurrentDirectory());
                }
                return di;
            }
        }

        public static DirectoryInfo DocumentDirectory
        {
            get {
                return new DirectoryInfo(Directory.GetCurrentDirectory());
            }
        }

        public static Uri DocumentDirectoryUri
        {
            get {
                string sUri = DocumentDirectory.FullName + "/";
                sUri = "file://" + sUri.Replace("\\", "/");
                return new Uri(sUri);
            }
        }
    }
}
