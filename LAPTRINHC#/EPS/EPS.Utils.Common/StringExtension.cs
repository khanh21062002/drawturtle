using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Common
{
    public static class StringExtension
    {
        public static string RemoveDiacritics(this String s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static bool Contains(this string str, string value, bool accentSensitive, bool caseSensitive)
        {
            if (!caseSensitive)
            {
                str = str.ToUpper();
                value = value.ToUpper();
            }

            if (!accentSensitive)
            {
                str = str.RemoveDiacritics();
                value = value.RemoveDiacritics();
            }

            return str.Contains(value);
        }

        public static bool IsDigitOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static string Truncate(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        public static string XoaKyTuDb(this string str)
        {
            str = str.Trim().ToLower().Replace("–", "-");
            StringBuilder finalstr = new StringBuilder();
            foreach (char c in str)
            {
                int charassci = Convert.ToInt16(c);
                if (!(charassci >= 33 && charassci <= 47) && !(charassci >= 58 && charassci <= 64) && !(charassci >= 91 && charassci <= 96))// special char ???
                {
                    finalstr.Append(c);
                }

            }
            return finalstr.ToString();

        }

        public static string EscapeOracleString(this string str)
        {
            if (str == null) return null;

            return str.Replace("'", "''");
        }

        public static string GetOracleDateString(this DateTime? date)
        {
            return date.HasValue ? ("'" + date.Value.ToString("dd-MMM-yy") + "'") : "NULL";
        }
    }
}
