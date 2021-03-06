﻿namespace OpenNETCF
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Reflection;

    public sealed class EnumEx
    {
        private EnumEx()
        {
        }

        public static string Format(Type enumType, object value, string format)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The argument enumType must be an System.Enum.");
            }
            if (string.Compare(format, "G", true, CultureInfo.InvariantCulture) == 0)
            {
                return InternalFormat(enumType, value);
            }
            if (string.Compare(format, "F", true, CultureInfo.InvariantCulture) == 0)
            {
                return InternalValuesFormat(enumType, value, false);
            }
            if (string.Compare(format, "V", true, CultureInfo.InvariantCulture) == 0)
            {
                return InternalValuesFormat(enumType, value, true);
            }
            if (string.Compare(format, "X", true, CultureInfo.InvariantCulture) == 0)
            {
                return InternalFormattedHexString(value);
            }
            if (string.Compare(format, "D", true, CultureInfo.InvariantCulture) != 0)
            {
                throw new FormatException("Invalid format.");
            }
            return Convert.ToUInt64(value).ToString();
        }

        public static string GetName(Type enumType, object value)
        {
            if (enumType.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("enumType parameter is not an System.Enum");
            }
            foreach (FieldInfo info in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                object obj2 = 0;
                try
                {
                    obj2 = Convert.ChangeType(info.GetValue(null), value.GetType(), null);
                }
                catch
                {
                    throw new ArgumentException();
                }
                if (obj2.Equals(value))
                {
                    return info.Name;
                }
            }
            return null;
        }

        public static string[] GetNames(Type enumType)
        {
            if (enumType.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("enumType parameter is not an System.Enum");
            }
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            string[] strArray = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                strArray[i] = fields[i].Name;
            }
            return strArray;
        }

        public static Type GetUnderlyingType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }

        public static Enum[] GetValues(Type enumType)
        {
            if (enumType.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("enumType parameter is not an System.Enum");
            }
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            Enum[] enumArray = new Enum[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                enumArray[i] = (Enum) fields[i].GetValue(null);
            }
            return enumArray;
        }

        private static string InternalFlagsFormat(Type enumType, object value)
        {
            string name = GetName(enumType, value);
            if (name == null)
            {
                return value.ToString();
            }
            return name;
        }

        private static string InternalFormat(Type enumType, object value)
        {
            if (enumType.IsDefined(typeof(FlagsAttribute), false))
            {
                return InternalFlagsFormat(enumType, value);
            }
            string name = GetName(enumType, value);
            if (name == null)
            {
                return value.ToString();
            }
            return name;
        }

        private static string InternalFormattedHexString(object value)
        {
            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.SByte:
                {
                    sbyte num = (sbyte) value;
                    return num.ToString("X2", (IFormatProvider) null);
                }
                case TypeCode.Byte:
                {
                    byte num2 = (byte) value;
                    return num2.ToString("X2", null);
                }
                case TypeCode.Int16:
                {
                    short num3 = (short) value;
                    return num3.ToString("X4", (IFormatProvider) null);
                }
                case TypeCode.UInt16:
                {
                    ushort num4 = (ushort) value;
                    return num4.ToString("X4", null);
                }
                case TypeCode.Int32:
                {
                    int num5 = (int) value;
                    return num5.ToString("X8", null);
                }
                case TypeCode.UInt32:
                {
                    uint num6 = (uint) value;
                    return num6.ToString("X8", null);
                }
            }
            throw new InvalidOperationException("Unknown enum type.");
        }

        private static string InternalValuesFormat(Type enumType, object value, bool showValues)
        {
            string[] names = null;
            if (!showValues)
            {
                names = GetNames(enumType);
            }
            ulong num = Convert.ToUInt64(value);
            Enum[] values = GetValues(enumType);
            ArrayList list = new ArrayList();
            for (int i = 0; i < values.Length; i++)
            {
                ulong num3 = (ulong) Convert.ChangeType(values[i], typeof(ulong), null);
                if (((i != 0) || (num3 != 0L)) && ((num & num3) == num3))
                {
                    num -= num3;
                    if (showValues)
                    {
                        list.Add(num3.ToString());
                    }
                    else
                    {
                        list.Add(names[i]);
                    }
                }
            }
            if (num != 0L)
            {
                return value.ToString();
            }
            string[] strArray2 = (string[]) list.ToArray(typeof(string));
            return string.Join(", ", strArray2);
        }

        public static bool IsDefined(Type enumType, object value)
        {
            return Enum.IsDefined(enumType, value);
        }

        public static object Parse(Type enumType, string value)
        {
            return Parse(enumType, value, false);
        }

        public static object Parse(Type enumType, string value, bool ignoreCase)
        {
            if (value.TrimEnd(new char[] { ' ' }) == "")
            {
                throw new ArgumentException("value is either an empty string (\"\") or only contains white space.");
            }
            if (enumType.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("enumType parameter is not an System.Enum");
            }
            string[] strArray = value.Replace(" ", "").Split(new char[] { ',' });
            long num = 0L;
            foreach (string str in strArray)
            {
                if (str != "")
                {
                    try
                    {
                        if (ignoreCase)
                        {
                            num += (long) Convert.ChangeType(enumType.GetField(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase).GetValue(null), num.GetType(), null);
                        }
                        else
                        {
                            num += (long) Convert.ChangeType(enumType.GetField(str, BindingFlags.Public | BindingFlags.Static).GetValue(null), num.GetType(), null);
                        }
                    }
                    catch
                    {
                        try
                        {
                            num += (long) Convert.ChangeType(Enum.ToObject(enumType, Convert.ChangeType(str, Enum.GetUnderlyingType(enumType), null)), typeof(long), null);
                        }
                        catch
                        {
                            throw new ArgumentException("value is a name, but not one of the named constants defined for the enumeration.");
                        }
                    }
                }
            }
            return Enum.ToObject(enumType, num);
        }

        public static object ToObject(Type enumType, object value)
        {
            return Enum.ToObject(enumType, value);
        }
    }
}

