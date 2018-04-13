using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace myFunctions
{
    /// <summary>
    /// Conversion
    /// Version:    1.0
    /// Date:       2015-06-26    
    /// </summary>
    static class Conv
    {
        /// <summary>
        /// Check if string is Short
        /// </summary>
        /// <param name="text">Number string</param>
        /// <returns>Return True if string is Short</returns>
        public static bool IsShort(string text)
        {
            short value;
            return short.TryParse(text, out value);
        }

        /// <summary>
        /// Check if string is positive Short
        /// </summary>
        /// <param name="text">Number string</param>
        /// <returns>Return True if string is positive short</returns>
        public static bool IsPositiveShort(string text)
        {
            short value;
            bool res = short.TryParse(text, out value);
            if (res)
                if (value < 0) res = false;
            return res;
        }

        /// <summary>
        /// Check if string is Integer
        /// </summary>
        /// <param name="text">Number string</param>
        /// <returns>Return True if string is integer</returns>
        public static bool IsInt(string text)
        {
            int value;
            return int.TryParse(text, out value);
        }

        /// <summary>
        /// Check if string is positive Int
        /// </summary>
        /// <param name="text">Number string</param>
        /// <returns>Return True if string is positive integer</returns>
        public static bool IsPositiveInt(string text)
        {
            int value;
            bool res = int.TryParse(text, out value);
            if (res)
                if (value < 0) res = false;
            return res;
        }

        /// <summary>
        /// Check if string is Float
        /// </summary>
        /// <param name="text">Number string</param>
        /// <returns>Return True if string is float</returns>
        public static bool IsFloat(string text)
        {
            float value;
            return float.TryParse(text, out value);
        }

        /// <summary>
        /// Check if string is positive Float
        /// </summary>
        /// <param name="text">Number string</param>
        /// <returns>Return True if string is positive float</returns>
        public static bool IsPositiveFloat(string text)
        {
            float value;
            bool res = float.TryParse(text, out value);
            if (res)
                if (value < 0) res = false;
            return res;
        }

        /// <summary>
        /// Check if string is DateTime
        /// </summary>
        /// <param name="text">String</param>
        /// <returns>Return True if string is DateTime</returns>
        public static bool IsDate(string text)
        {
            DateTime value;
            return DateTime.TryParse(text, out value);
        }

        /// <summary>
        /// Check if string is number
        /// </summary>
        /// <param name="Expression">Number string</param>
        /// <returns>Return True if string is number</returns>
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// Check if string is integer (INT32)
        /// </summary>
        /// <param name="Expression">Number string</param>
        /// <returns>Return True if string is integer</returns>
        public static bool IsInteger(object Expression)
        {
            bool isNum;
            int retNum;

            isNum = Int32.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// Convert String to Bool with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Short number</returns>
        public static bool ToBoolDef(string text, bool def)
        {
            bool value;
            if (bool.TryParse(text, out value))
                return value;
            else
            {
                if (text == "1")
                {
                    return true;
                }
                else if (text == "0")
                {
                    return false;
                }
                return def;
            }
                
        }

        /// <summary>
        /// Convert String to Short with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Short number</returns>
        public static short ToShortDef(string text, short def)
        {
            short value;
            if (short.TryParse(text, out value))
                return value;
            else
                return def;
        }

        /// <summary>
        /// Convert String to Integer with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Integer number</returns>
        public static int ToIntDef(string text, int def)
        {
            int value;
            if (int.TryParse(text, out value))
                return value;
            else
                return def;
        }

        /// <summary>
        /// Convert String to Int32 with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Integer number</returns>
        public static int ToInt32Def(string text, int def)
        {
            return ToIntDef(text, def);
        }

        /// <summary>
        /// Convert String to Int64 with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Long number</returns>
        public static long ToLongDef(string text, int def)
        {
            long value;
            if (long.TryParse(text, out value))
                return value;
            else
                return def;
        }

        /// <summary>
        /// Convert String to Float with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Float number</returns>
        public static float ToFloatDef(string text, float def)
        {
            float value;
            if (float.TryParse(text, out value))
                return value;
            else
                return def;
        }

        /// <summary>
        /// Convert String to Float with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Float number</returns>
        public static float ToFloatDefI(string text, float def)
        {
            try
            {
                return Convert.ToSingle(text, NumberFormatInfo.InvariantInfo);
            }
            catch (Exception)
            {
                return def;
            }
        }

        /// <summary>
        /// Convert String to Double with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Double number</returns>
        public static double ToDoubleDef(string text, double def)
        {
            double value;
            if (double.TryParse(text, out value))
                return value;
            else
                return def;
        }

        /// <summary>
        /// Convert String to Double with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Double number</returns>
        public static double ToDoubleDef(string text, double def, NumberFormatInfo format)
        {
            if (IsNumeric(text))
                return Convert.ToDouble(text, format);
            else return def;
        }

        /// <summary>
        /// Convert String (Invariant number format - '.')  to Double with default value on convert Error
        /// </summary>
        /// <param name="text">Number string</param>
        /// <param name="def">Default value</param>
        /// <returns>Double number</returns>
        public static double ToDoubleDefI(string text, double def)
        {
            if (IsNumeric(text))
                return Convert.ToDouble(text, NumberFormatInfo.InvariantInfo);
            else return def;
        }

        /// <summary>
        /// Return bool value like "0" or "1"
        /// </summary>
        /// <param name="value">Bool value</param>
        /// <returns>String representation of bool</returns>
        public static string ToString(bool value)
        {
            if (value == true)
                return "1";
            else
                return "0";
        }

        /// <summary>
        /// Convert Image to stream
        /// </summary>
        /// <param name="image">Image</param>
        /// <param name="formaw">Image format</param>
        /// <returns>Image stream</returns>
        public static Stream ToStream(this Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public static Color GetColor(string text)
        {
            Color color;
            try
            {
                color = Color.FromArgb(int.Parse(text, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            catch
            {
                color = Color.FromName(text);
            }
            return color;
        }

        #region Array

        /// <summary>
        /// Array to string separated by defined char
        /// </summary>
        /// <param name="Expression">Array</param>
        /// <param name="separator">Separator</param>
        /// <returns>String</returns>
        public static string ArrToStr(object Expression, string separator = "; ")
        {
            string res = "";
            if (Expression.GetType() == typeof(ushort[]))
            {
                ushort[] exp = (ushort[])Expression;
                if (exp.Length > 0) res = exp[0].ToString();
                for (int i = 1; i < exp.Length; i++) res += separator + exp[i].ToString();
            }
            else if (Expression.GetType() == typeof(uint[]))
            {
                uint[] exp = (uint[])Expression;
                if (exp.Length > 0) res = exp[0].ToString();
                for (int i = 1; i < exp.Length; i++) res += separator + exp[i].ToString();
            }
            else if (Expression.GetType() == typeof(int[]))
            {
                int[] exp = (int[])Expression;
                if (exp.Length > 0) res = exp[0].ToString();
                for (int i = 1; i < exp.Length; i++) res += separator + exp[i].ToString();
            }
            else if (Expression.GetType() == typeof(float[]))
            {
                float[] exp = (float[])Expression;
                if (exp.Length > 0) res = exp[0].ToString();
                for (int i = 1; i < exp.Length; i++) res += separator + exp[i].ToString();
            }
            else if (Expression.GetType() == typeof(bool[]))
            {
                string boolStr;
                bool[] exp = (bool[])Expression;
                if (exp.Length > 0)
                {
                    if (exp[0] == true)
                        boolStr = "1";
                    else
                        boolStr = "0";
                    res = boolStr;
                }
                for (int i = 1; i < exp.Length; i++)
                {
                    if (exp[i] == true)
                        boolStr = "1";
                    else
                        boolStr = "0";
                    res += separator + boolStr;
                }
            }
            else
                res = Expression.ToString();
            return res;
        }
        
        /// <summary>
        /// String to Short array
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="separator">Separator</param>
        /// <returns>Short array</returns>
        public static short[] ToInt16Arr(string value, string separator = ";")
        {
            string[] separate = value.Split(new string[] { separator }, StringSplitOptions.None);
            short[] res = new short[separate.Length];
            try
            {
                for (int i = 0; i < separate.Length; i++)
                {
                    res[i] = Convert.ToInt16(separate[i]);
                }
            }
            catch
            {
                res = new short[0];
            }

            return res;
        }

        /// <summary>
        /// String to UShort array
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="separator">Separator</param>
        /// <returns>UShort array</returns>
        public static ushort[] ToUInt16Arr(string value, string separator = ";")
        {
            string[] separate = value.Split(new string[] { separator }, StringSplitOptions.None);
            ushort[] res = new ushort[separate.Length];
            try
            {
                for (int i = 0; i < separate.Length; i++)
                {
                    res[i] = Convert.ToUInt16(separate[i]);
                }
            }
            catch
            {
                res = new ushort[0];
            }

            return res;
        }

        /// <summary>
        /// String to Int array
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="separator">Separator</param>
        /// <returns>Int array</returns>
        public static int[] ToInt32Arr(string value, string separator = ";")
        {
            string[] separate = value.Split(new string[] { separator }, StringSplitOptions.None);
            int[] res = new int[separate.Length];
            try
            {
                for (int i = 0; i < separate.Length; i++)
                {
                    res[i] = Convert.ToInt32(separate[i]);
                }
            }
            catch
            {
                res = new int[0];
            }

            return res;
        }

        /// <summary>
        /// String to UInt array
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="separator">Separator</param>
        /// <returns>UInt array</returns>
        public static uint[] ToUInt32Arr(string value, string separator = ";")
        {
            string[] separate = value.Split(new string[] { separator }, StringSplitOptions.None);
            uint[] res = new uint[separate.Length];
            try
            {
                for (int i = 0; i < separate.Length; i++)
                {
                    res[i] = Convert.ToUInt32(separate[i]);
                }
            }
            catch
            {
                res = new uint[0];
            }

            return res;
        }

        /// <summary>
        /// String to Float array
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="separator">Separator</param>
        /// <returns>Float array</returns>
        public static float[] ToFloatArr(string value, string separator = ";")
        {
            string[] separate = value.Split(new string[] { separator }, StringSplitOptions.None);
            float[] res = new float[separate.Length];
            try
            {
                for (int i = 0; i < separate.Length; i++)
                {
                    res[i] = Convert.ToSingle(separate[i]);
                }
            }
            catch
            {
                res = new float[0];
            }

            return res;
        }

        /// <summary>
        /// String to Bool array
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="separator">Separator</param>
        /// <returns>Bool array</returns>
        public static bool[] StrToBool(string value, string separator = ";")
        {
            string[] separate = value.Split(new string[] { separator }, StringSplitOptions.None);
            bool[] res = new bool[separate.Length];
            try
            {
                for (int i = 0; i < separate.Length; i++)
                {
                    if (separate[i].ToLower().Trim() == "true" || separate[i].Trim() == "1")
                        res[i] = true;
                    else
                        res[i] = false;
                }
            }
            catch
            {
                res = new bool[0];
            }

            return res;
        }

        #endregion

    }
}
