using System.Data.OleDb;

namespace DGA001.Extensions
{
    public static class DataReaderExtensions
    {
        public static string GetStringSafe(this OleDbDataReader reader, string column)
        {
            return reader[column] != DBNull.Value ? reader[column].ToString() : string.Empty;
        }

        public static int GetIntSafe(this OleDbDataReader reader, string column)
        {
            return reader[column] != DBNull.Value ? Convert.ToInt32(reader[column]) : 0;
        }

        public static decimal GetDecimalSafe(this OleDbDataReader reader, string column)
        {
            return reader[column] != DBNull.Value ? Convert.ToDecimal(reader[column]) : 0m;
        }

        public static DateOnly? GetDateOnlySafe(this OleDbDataReader reader, string column)
        {
            return reader[column] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader[column])) : (DateOnly?)null;
        }

    }
}
