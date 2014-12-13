using System;
using System.Data;


namespace ProductionMan.Data.MsAdo
{

    public static class AdoConverter
    {

        public static T Read<T>(IDataReader reader, string fieldName, T defaultValue) where T : IConvertible
        {
            try
            {
                if (reader != null)
                {
                    var value = reader[fieldName];

                    if (value != null)
                    {
                        return (T) Convert.ChangeType(value, typeof (T));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception:{0}", ex);
            }

            return defaultValue;
        }
    }
}