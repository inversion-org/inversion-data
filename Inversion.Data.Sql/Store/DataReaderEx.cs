using System;
using System.Data;

namespace Inversion.Data.Store
{
    public static class DataReaderEx
    {
        public static void ForEach(this IDataReader self, Action<IDataRecord> action)
        {
            while (self.Read())
            {
                action(self);
            }
        }

        public static bool HasField(this IDataReader self, string columnName)
        {
            for (int i = 0; i < self.FieldCount; i++)
            {
                if (self.GetName(i).Equals(columnName, StringComparison.InvariantCulture))
                {
                    return true;
                }
            }
            return false;
        }

        public static char ReadChar(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? default(char) : self.GetChar(ordinal);
        }

        public static char? ReadCharOrNull(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? (char?) null : self.GetChar(ordinal);
        }

        public static string ReadString(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? null : self.GetString(ordinal);
        }

        public static Guid ReadGuid(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? Guid.Empty : self.GetGuid(self.GetOrdinal(columnName));
        }

        public static decimal ReadDecimal(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? default(decimal) : self.GetDecimal(ordinal);
        }

        public static bool ReadBool(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return !self.IsDBNull(ordinal) && self.GetBoolean(ordinal);
        }

        public static bool? ReadBoolOrNull(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? (bool?) null : self.GetBoolean(ordinal);
        }

        public static int ReadInt(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? default(int) : self.GetInt32(ordinal);
        }

        public static int? ReadIntOrNull(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? (int?) null : self.GetInt32(ordinal);
        }

        public static long ReadLong(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? default(long) : self.GetInt64(ordinal);
        }

        public static long? ReadLongOrNull(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? (long?) null : self.GetInt64(ordinal);
        }

        public static float ReadFloat(this IDataReader self, string columnName)
        {
            //return self.GetFloat(self.GetOrdinal(columnName));
            return Convert.ToSingle(self.GetDouble(self.GetOrdinal(columnName)));
        }

        public static float? ReadFloatOrNull(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            return self.IsDBNull(ordinal) ? (float?) null : Convert.ToSingle(self.GetDouble(self.GetOrdinal(columnName)));
        }

        /// <summary>
        /// Reads the date/time from the column specified.
        /// </summary>
        /// <param name="self">The reader being used.</param>
        /// <param name="columnName">The name of the field to read.</param>
        /// <returns>
        /// Returns the value of the field if it has one; otherwise, returns `default(DateTime)`.
        /// </returns>
        public static DateTime ReadDateTime(this IDataReader self, string columnName)
        {
            int ord = self.GetOrdinal(columnName);
            return self.IsDBNull(ord) ? default(DateTime) : self.GetDateTime(ord);
        }

        /// <summary>
        /// Reads the date/time from the column specified.
        /// </summary>
        /// <param name="self">The reader being used.</param>
        /// <param name="columnName">The name of the field to read.</param>
        /// <returns>
        /// Returns the value of the field if it has one; otherwise, returns `null`.
        /// </returns>
        public static DateTime? ReadDateTimeOrNull(this IDataReader self, string columnName)
        {
            int ord = self.GetOrdinal(columnName);
            return self.IsDBNull(ord) ? (DateTime?) null : self.GetDateTime(ord);
        }

        public static byte[] ReadBinaryData(this IDataReader self, string columnName)
        {
            int ordinal = self.GetOrdinal(columnName);
            if (self.IsDBNull(ordinal))
            {
                return new byte[0];
            }
            else
            {
                byte[] data = (byte[]) self.GetValue(ordinal); // this might be implementation specific
                return data;
            }
        }
    }
}