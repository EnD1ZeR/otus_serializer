namespace Reflection
{
    /// <summary> Serializer </summary>
    public static class Serializer
    {
        /// <summary> Serialize from object to CSV </summary>
        /// <param name="obj">any object</param>
        /// <returns>CSV</returns>
        public static string SerializeFromObjectToCSV(object obj)
        {
            return null;
        }

        /// <summary> Deserialize from CSV to object</summary>
        /// <param name="csv">string in CSV format</param>
        /// <returns>object</returns>
        public static object DeserializeFromCSVToObject(string csv)
        {
            return null;
        }

        /// <summary>
        /// Deserialize from CSV to concrete type
        /// </summary>
        /// <typeparam name="T">type of output object</typeparam>
        /// <param name="csv">string in CSV format</param>
        /// <returns></returns>
        public static T DeserializeFromCSVToObject<T>(string csv)
        {
            return (T)DeserializeFromCSVToObject(csv);
        }
    }
}
