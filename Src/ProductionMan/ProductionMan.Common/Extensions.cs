using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;


namespace ProductionMan.Common
{

    public static class PropertyChangedEventArgsEx
    {

        public static bool NameIs(this PropertyChangedEventArgs source, string toCheck)
        {
            return string.Equals(
                source.PropertyName, 
                toCheck, 
                StringComparison.InvariantCultureIgnoreCase);
        }


        public static string ToUnsecureString(this SecureString source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(source);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
