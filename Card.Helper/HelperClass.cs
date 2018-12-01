using Sample.Helper.ExceptionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Helper
{
   public static class HelperClass
    {
        public static ResponseModel Response(bool success, string message, dynamic output)
        {
            return new ResponseModel()
            {
                success = success,
                message = message,
                output = output
            };
        }
        public static ResponseModelAccount Response(bool success, string message, long accountNumber, decimal balance, string currency)
        {
            return new ResponseModelAccount()
            {
                success = success,
                message = message,
                accountNumber=accountNumber,
                balance= balance,
                currency= currency
            };
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }
        public static bool IsLoapYear(int year)
        {
            try
            {
                if ((year % 400) == 0)
                    return true;
                else if ((year % 100) == 0)
                    return true;
                else if ((year % 4) == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                return false;
            }
        }
        public static bool IsPrime(int year)
        {
            if ((year & 1) == 0)
            {
                if (year == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            for (int i = 3; (i * i) <= year; i += 2)
            {
                if ((year % i) == 0)
                {
                    return false;
                }
            }
            return year != 1;
        }
    }
}
