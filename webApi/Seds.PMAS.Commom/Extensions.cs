using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Seds.PMAS.Common
{
    public static class Extensions
    {
        public static String GetFirstName(this String str, Int32 maxChars)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            var name = str.Trim();
            var ind = name.IndexOf(' ');
            if (ind == -1)
                return (maxChars != -1 && name.Length > maxChars) ? name.Remove(maxChars - 3).Trim() + "..." : name.Trim();
            else
            {
                name = name.Substring(0, ind);
                if (maxChars != -1 && name.Length > maxChars)
                    return name.Remove(maxChars - 3).Trim() + "...";
            }
            return name.Trim();
        }

        public static String GetLastName(this String str, Int32 maxChars)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            var name = str.Trim();
            var ind = name.LastIndexOf(' ');
            if (ind == -1)
                return (maxChars != -1 && name.Length > maxChars) ? name.Remove(maxChars - 3).Trim() + "..." : name.Trim();
            else
            {
                name = name.Substring(ind + 1);
                if (maxChars != -1 && name.Length > maxChars)
                    return name.Remove(maxChars - 3).Trim() + "...";
            }
            return name.Trim();
        }

        public static IEnumerable<t> Except<t>(this IEnumerable<t> lst1, IEnumerable<t> lst2, Func<t, System.Object> func)
        {
            return lst1.Where(obj1 => !lst2.Any(obj2 =>
            {
                var v1 = func(obj1);
                var v2 = func(obj2);
                if (v1 == null && v2 == null)
                    return true;
                if (v1 == null || v2 == null)
                    return false;
                return v1.Equals(v2);
            }));
        }

        public static Boolean EndsWith(this String obj, params String[] str)
        {
            if (String.IsNullOrEmpty(obj))
                return false;
            foreach (var s in str)
                if (obj.EndsWith(s))
                    return true;
            return false;
        }

        public static Boolean IsMatch(this String str, String pattern)
        {
            if (String.IsNullOrEmpty(str))
                return false;
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        public static String Concat<t>(this IEnumerable<t> lst, Func<t, System.Object> valueFunction)
        {
            return lst.Concat(valueFunction, ", ", null, null);
        }

        public static string Concat(this IList list, string separator)
        {
            var s = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                s.Append(item);
                if (i < list.Count - 1)
                    s.Append(separator);
            }
            return s.ToString();
        }

        public static String Concat<t>(this IEnumerable<t> lst, Func<t, System.Object> valueFunction, String separator)
        {
            return lst.Concat(valueFunction, separator, null, null);
        }
        public static String Concat<t>(this IEnumerable<t> lst, Func<t, System.Object> valueFunction, String separator, String format)
        {
            return lst.Concat(valueFunction, separator, format, null);
        }

        public static String Concat<t>(this IEnumerable<t> lst, Func<t, System.Object> valueFunction, String separator, String format, String defaultValue)
        {
            if (lst == null || lst.Count() == 0)
                return defaultValue;

            StringBuilder str = new StringBuilder();
            foreach (t obj in lst)
            {
                System.Object value = valueFunction(obj);
                String valuestr = Convert.ToString(value);

                if (String.IsNullOrEmpty(valuestr))
                    continue;

                if (String.IsNullOrEmpty(format))
                    str.Append(valuestr + separator);
                else
                    str.Append(String.Format("{0:" + format + "}", value) + separator);
            }
            return String.IsNullOrEmpty(str.ToString()) ? defaultValue : str.Remove(str.Length - separator.Length, separator.Length).ToString();
        }

        public static String RemoveAccents(this String str)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            String lst1 = "áéíóúàèìòùäëïöüãõâêîôûçÁÉÍÓÚÀÈÌÒÙÄËÏÖÜÃÕÂÊÎÔÛÇ";
            String lst2 = "aeiouaeiouaeiouaoaeioucAEIOUAEIOUAEIOUAOAEIOUC";
            return str.ReplaceAll(lst1.ToCharArray().ToList(), lst2.ToCharArray().ToList());
        }

        public static String RemoveAll(this String str, params String[] strToRemove)
        {
            var aux = str;
            foreach (var x in strToRemove)
                aux = aux.Replace(x, "");
            return aux;
        }

        public static String ReplaceAll(this String str, List<Char> oldChars, List<Char> newChars)
        {
            if (String.IsNullOrEmpty(str) || oldChars == null || newChars == null)
                return str;

            StringBuilder builder = new StringBuilder(str);
            foreach (var c in oldChars)
                builder.Replace(c, newChars[oldChars.FindIndex(cc => cc == c)]);

            return builder.ToString();
        }

        public static IEnumerable<t> RemoveAll<t>(this IEnumerable<t> lst, Func<t, Boolean> func)
        {
            List<t> newlst = new List<t>();
            //foreach (t o in lst)
            //    newlst.Add(o);
            foreach (t o in lst)
                if (!func(o))
                    newlst.Add(o);
            return newlst;
        }

        public static IEnumerable<t> UnionAll<t>(this IEnumerable<t> lst, IEnumerable<t> second)
        {
            var newlst = new List<t>();
            newlst.AddRange(lst);
            newlst.AddRange(second);
            return newlst;
        }

        public static IEnumerable<t> RemoveAllExcept<t>(this IEnumerable<t> lst, params t[] array)
        {
            List<t> newlst = new List<t>();
            foreach (t o in lst)
                newlst.Add(o);
            foreach (t o in newlst)
                if (!array.Contains(o))
                    newlst.Remove(o);
            return newlst;
        }

        public static List<t> ToList<t>(this ICollection lst)
        {
            return lst.Cast<t>().ToList();
        }

        public static Boolean Any(this String obj, params String[] str)
        {
            if (String.IsNullOrEmpty(obj))
                return false;

            foreach (String s in str)
                if (obj.Contains(s))
                    return true;
            return false;
        }

        public static Boolean AllEquals<t>(this IEnumerable<t> lst, Func<t, System.Object> func)
        {
            if (lst == null || lst.Count() == 0)
                return true;

            var value = func(lst.First());
            if (value == null && lst.All(obj => func(obj) == null))
                return true;
            return lst.All(obj => value.Equals(func(obj)));
        }

        public static String Encode(this String obj)
        {
            if (String.IsNullOrEmpty(obj))
                return obj;

            String abb = "";
            foreach (Char c in obj.ToCharArray())
            {
                Int32 value = Convert.ToInt32(c);
                abb += (value > 255) ? "#(#" + value + "#)#" : Convert.ToString(c);
            }
            return abb;
        }

        public static String Decode(this String obj)
        {
            if (String.IsNullOrEmpty(obj))
                return obj;

            String pattern = @"#\({1}#\d+#\){1}#";
            if (!obj.IsMatch(pattern))
                return obj;

            String abb = obj;
            while (abb.IsMatch(pattern))
            {
                Int32 ind1 = abb.IndexOf("#(#");
                Int32 ind2 = abb.IndexOf("#)#");

                Int32 value = Convert.ToInt32(abb.Substring(ind1 + 3, ind2 - (ind1 + 3)));
                abb = abb.Remove(ind1, ind2 - ind1 + 3);
                abb = abb.Insert(ind1, Convert.ToString((Char)value));
            }
            return abb;
        }

        public static Boolean ContainsIgnoreCase(this String obj, String str)
        {
            if (String.IsNullOrEmpty(obj) || String.IsNullOrEmpty(str))
                return false;
            return obj.ToLower().Contains(str.ToLower());
        }

        public static IEnumerable<t> Distinct<t>(this IEnumerable<t> lst, Func<t, System.Object> func)
        {
            if (lst == null || lst.Count() == 0)
                return lst;
            var newlst = new List<t>();
            foreach (var obj in lst)
            {
                if (!newlst.Any(o => func(obj).Equals(func(o))))
                    newlst.Add(obj);
            }
            return newlst;
        }

        public static IEnumerable<t> Distinct<t>(this IEnumerable<t> lst, Func<t, System.Object> func1, Func<t, System.Object> func2)
        {
            if (lst == null || lst.Count() == 0)
                return lst;
            var newlst = new List<t>();
            foreach (var obj in lst)
            {
                if (!newlst.Any(o => func1(obj).Equals(func1(o)) && func2(obj).Equals(func2(o))))
                    newlst.Add(obj);
            }
            return newlst;
        }

        public static IEnumerable<t> Distinct<t>(this IEnumerable<t> lst, Func<t, System.Object> func1, Func<t, System.Object> func2, Func<t, System.Object> func3)
        {
            if (lst == null || lst.Count() == 0)
                return lst;
            var newlst = new List<t>();
            foreach (var obj in lst)
            {
                if (!newlst.Any(o => func1(obj).Equals(func1(o)) && func2(obj).Equals(func2(o)) && func3(obj).Equals(func3(o))))
                    newlst.Add(obj);
            }
            return newlst;
        }

        public static T Clone<T>(this T obj)
        {
            if (obj == null)
                throw new NullReferenceException("Can't clone null objects.");

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);
            ms.Position = 0;
            object clone = bf.Deserialize(ms);
            ms.Close();
            return (T)clone;
        }

        public static Boolean In<T>(this T obj, params T[] lst)
        {
            if (obj == null || lst == null || lst.Length == 0)
                return false;
            foreach (var v in lst)
                if (obj.Equals(v))
                    return true;
            return false;
        }

        public static Boolean ContainsAny(this String obj, params String[] lst)
        {
            if (obj == null || lst == null || lst.Length == 0)
                return false;
            foreach (var v in lst)
                if (obj.Contains(v))
                    return true;
            return false;
        }

        public static Boolean ContainsAll(this IEnumerable<String> obj, params String[] lst)
        {
            if (obj == null || obj.Count() == 0 || lst == null || lst.Length == 0)
                return false;
            foreach (var v in lst)
                if (!obj.Contains(v))
                    return false;
            return true;
        }

        public static Boolean ContainsAny(this IEnumerable<String> obj, params String[] lst)
        {
            if (obj == null || obj.Count() == 0 || lst == null || lst.Length == 0)
                return false;
            foreach (var v in lst)
                if (obj.Contains(v))
                    return true;
            return false;
        }

        public static List<List<T3>> GetList<T1, T2, T3>(this List<T1> lst, Func<T1, List<T2>> func1, Func<T2, T3> func2)
        {
            var newlst = new List<List<T3>>();
            foreach (var obj in lst)
            {
                var sublst = new List<T3>();
                foreach (var val in func1(obj))
                    sublst.Add(func2(val));
                newlst.Add(sublst);
            }
            return newlst;
        }

        public static Int32 GetDecimalPlaces(this Decimal value)
        {
            return Convert.ToDouble(value).GetDecimalPlaces();
        }

        public static Int32 GetDecimalPlaces(this Decimal? value)
        {
            return value == null ? 0 : Convert.ToDouble(value.Value).GetDecimalPlaces();
        }

        public static Int32 GetDecimalPlaces(this Double value)
        {
            var s = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            var str = Convert.ToString(Convert.ToDecimal(value));
            var ind = str.LastIndexOf(s);
            if (ind == -1)
                return 0;
            return str.Length - ind - 1;
        }

        public static String OnlyChars(this String str, String charListToKeep)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            var newStr = new StringBuilder();
            str.ToCharArray().Where(c => charListToKeep.Any(cc => cc == c)).ToList().ForEach(c => newStr.Append(c));
            return newStr.ToString();
        }

        public static String OnlyNumbers(this String str)
        {
            return str.OnlyChars("0123456789");
        }

        public static String OnlyChars(this String str)
        {
            return str.OnlyChars("abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ");
        }

        public static String OnlyCharsAndNumbers(this String str)
        {
            return str.OnlyChars("0123456789abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ");
        }

        public static String OnlyConsonants(this String str)
        {
            return str.OnlyChars("bcdfghjklmnpqrstvxwyzBCDFGHJKLMNPQRSTVXWYZ");
        }

        public static String WithoutAccents(this String str)
        {
            var chars1 = "áéíóúçãõàèìòùäëïöüÁÉÍÓÚÇÃÕÀÈÌÒÙÄËÏÖÜ";
            var chars2 = "aeioucaoaeiouaeiouAEIOUCAOAEIOUAEIOU";
            for (var x = 0; x < chars1.Length; x++)
                str = str.Replace(chars1[x], chars2[x]);
            return str;
        }

        public static Boolean StartsWithAny(this String obj, params String[] str)
        {
            if (String.IsNullOrEmpty(obj) || str == null || str.Length == 0 || str.All(s => String.IsNullOrEmpty(s)))
                return false;
            foreach (var s in str)
                if (obj.StartsWith(s, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

        public static List<Char> ToList(this String str)
        {
            var lst = new List<Char>();
            for (var x = 0; x < str.Length; x++)
                lst.Add(str[x]);
            return lst;
        }

        public static String GetStringDiff(this DateTime datetime2, DateTime datetime1)
        {
            var diff = datetime2.Subtract(datetime1);
            return String.Format("{0:00}:{1:000}", diff.Seconds, diff.Milliseconds);
        }

        public static String Repeat(this String str, Int32 n)
        {
            var rs = "";
            for (var x = 0; x < n; x++)
                rs += str;
            return rs;
        }

        public static String SHA256Encrypt(this String str)
        {
            if (str == null)
                str = "";
            Byte[] bytes = Encoding.Default.GetBytes(str);
            SHA256Managed sha = new SHA256Managed();
            bytes = sha.ComputeHash(bytes);
            String rs = Convert.ToBase64String(bytes);
            sha.Clear();
            return rs;
        }

        public static Boolean isCNPJ(this String cnpj)
        {
            if (!String.IsNullOrEmpty(cnpj))
                cnpj = cnpj.Trim();
            if (!cnpj.IsMatch(@"(^(\d{2,3}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14,15})$)") && !cnpj.IsMatch(@"^\d{14,15}$"))
                return false;
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace(".", "");
            cnpj = cnpj.Replace("-", "");

            String digits;
            if (!(cnpj.Length >= 14 && cnpj.Length <= 15) || cnpj == "0".Repeat(cnpj.Length))
                return false;

            digits = cnpj.Substring(cnpj.Length - 2);
            return verifyDigit(Convert.ToInt32(digits.Substring(0, 1)), cnpj.Substring(0, cnpj.Length - 2)) && verifyDigit(Convert.ToInt32(digits.Substring(1, 1)), cnpj.Substring(0, cnpj.Length - 1));
        }

        public static Boolean isCPF(this String cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * (multiplicador1[i]);
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            int soma2 = 0;
            for (int i = 0; i < 10; i++)
                soma2 += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma2 % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        private static Boolean verifyDigit(Int32 digit, String numbers)
        {
            Int32 sum = 0, i, result;
            Int32 pos = numbers.Length - 7;
            for (i = 0; i < numbers.Length; i++)
            {
                sum += Convert.ToInt32(numbers.Substring(i, 1)) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            result = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            return result == digit;
        }

        public static Int32? ToNullableInt32(this System.Object obj)
        {
            if (obj == null)
                return null;
            return Convert.ToInt32(obj);
        }

        public static Int32 ToInt32(this System.Object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static Decimal? ToNullableDecimal(this System.Object obj)
        {
            if (obj == null)
                return null;
            return Convert.ToDecimal(obj);
        }

        public static Decimal ToDecimal(this System.Object obj)
        {
            return Convert.ToDecimal(obj);
        }

        public static Double? ToNullableDouble(this System.Object obj)
        {
            if (obj == null)
                return null;
            return Convert.ToDouble(obj);
        }

        public static Double ToDouble(this System.Object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static DateTime AddWeekDays(this DateTime date, Int32 days)
        {
            while (days > 0)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek == DayOfWeek.Saturday)
                    date = date.AddDays(2);
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    date = date.AddDays(1);
                days--;
            }
            return date;
        }

        public static t CastTo<t>(this Object o, t type)
        {
            return (t)o;
        }

        public static Boolean IsNumber(this Object obj)
        {
            if (obj is Int16 || obj is Int32 || obj is Int64)
                return true;
            if (obj is Decimal || obj is Single || obj is Double)
                return true;
            if (obj is String && !String.IsNullOrEmpty(obj as String) && (obj as String).IsMatch("^((\\+|\\-)?[0-9]+([0-9]*(\\.|\\" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + ")?[0-9]+)?)?$"))
                return true;
            return false;
        }

        public static Boolean IsEmail(this String str)
        {
            if (String.IsNullOrEmpty(str))
                return false;
            var array = str.Split(new String[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim(' ', '.')).ToArray();
            foreach (var s in array)
                if (!s.IsMatch(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$"))
                    return false;
            return true;
        }

        public static List<MailAddress> GetMailAddresses(this String str)
        {
            return GetMailAddresses(str, null);
        }

        public static List<MailAddress> GetMailAddresses(this String str, String toName)
        {
            var lstMail = new List<MailAddress>();
            var lst = str.Split(new String[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries).Where(obj => !String.IsNullOrEmpty(obj)).Select(obj => obj.Trim(' ', '.')).Where(obj => !String.IsNullOrEmpty(obj)).ToList();
            foreach (var mail in lst)
            {
                var chars = @"[a-zA-Z0-9 _\-\.áéíóúàèìòùãõçüÁÉÍÓÚÀÈÌÒÙÃÕÇÜ]+";
                if (mail.IsMatch(@"^(" + chars + @")\s*\<{1}(" + chars + @"\@{1}" + chars + @"(\.\w+)+)\>{1}$"))
                {
                    var m = Regex.Match(mail, @"^(" + chars + @")\s*\<{1}(" + chars + @"\@{1}" + chars + @"(\.\w+)+)\>{1}$", RegexOptions.IgnoreCase);
                    var obj = new MailAddress(m.Groups[2].Value, toName ?? m.Groups[1].Value);
                    lstMail.Add(obj);
                }
                else if (!String.IsNullOrEmpty(toName) && mail.IsMatch(@"^(" + chars + @"\@{1}" + chars + @"(\.\w+)+)$"))
                {
                    var m = Regex.Match(mail, @"^(" + chars + @"\@{1}" + chars + @"(\.\w+)+)$", RegexOptions.IgnoreCase);
                    var obj = new MailAddress(m.Groups[1].Value, toName);
                    lstMail.Add(obj);
                }
                else if (mail.IsMatch(@"^(" + chars + @"\@{1}" + chars + @"(\.\w+)+)$"))
                {
                    var m = Regex.Match(mail, @"^(" + chars + @"\@{1}" + chars + @"(\.\w+)+)$", RegexOptions.IgnoreCase);
                    var obj = new MailAddress(m.Groups[1].Value);
                    lstMail.Add(obj);
                }
            }
            return lstMail;
        }

        public static List<T> ElementAt<T>(this List<T> lst, params Int32[] indexes)
        {
            var newlst = new List<T>();
            foreach (var i in indexes)
                newlst.Add(lst[i]);
            return newlst;
        }

        public static List<T> ElementAt<T>(this List<T> lst, IList<Int32> indexes)
        {
            var newlst = new List<T>();
            foreach (var i in indexes)
                newlst.Add(lst[i]);
            return newlst;
        }

        public static Int32 GetWeekDays(this TimeSpan ts, DateTime date)
        {
            var count = 0;
            for (var x = 0; x < ts.TotalDays; x++)
            {
                var d = date.AddDays(x);
                if (d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
                    count++;
            }
            return count;
        }

        public static XmlNode GetNode(this XmlNodeList lst, String name)
        {
            if (lst == null || lst.Count == 0)
                return null;
            var list = lst.Cast<XmlNode>().ToList();
            if (list == null || list.Count == 0)
                return null;
            return list.FirstOrDefault(n => n.Name == name);
        }

        public static XmlNode GetNode(this XmlNode node, String name)
        {
            if (node == null)
                return null;
            return node.ChildNodes.GetNode(name);
        }

        public static List<T> ConvertAnonymousType<T>(this IEnumerable list)
        {
            var stronglyTypedList = (List<T>)Activator.CreateInstance(typeof(List<T>), null);
            System.Reflection.PropertyInfo[] properties = new System.Reflection.PropertyInfo[] { };
            foreach (var item in list)
            {
                var obj = (T)Activator.CreateInstance(typeof(T), null);
                stronglyTypedList.Add((T)FillObject(obj, item));
            }

            return stronglyTypedList;
        }

        public static object FillObject(object obj, object item)
        {
            var type = item.GetType();
            var typeObj = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo pi in typeObj.GetProperties())
            {
                if (properties.Any(p => p.Name == pi.Name))
                {
                    var value = type.GetProperty(pi.Name).GetValue(item, null);
                    if (value == null || value is Int32 || value is Decimal || value is Double || value is String || value is DBNull)
                        typeObj.GetProperty(pi.Name).SetValue(obj, value, null);
                    else
                    {
                        var objValue = Activator.CreateInstance(pi.PropertyType, null);
                        typeObj.GetProperty(pi.Name).SetValue(obj, FillObject(objValue, value), null);
                    }
                }
            }
            return obj;
        }

        public static String GetExceptionMessage(Exception ex)
        {
            var msg = ex.Message;
            if (ex.InnerException != null)
                msg += System.Environment.NewLine + GetExceptionMessage(ex.InnerException);
            return msg;
        }

        public static List<String> GetExceptionMessageList(Exception ex)
        {
            var lst = ex.Message.Split('\n').ToList();
            if (ex.InnerException != null)
                lst.AddRange(GetExceptionMessageList(ex.InnerException));
            return lst;
        }

        public static string AbreviarNome(string nomeCompleto)
        {
            string nomeAbreviado = "";
            string[] nome = nomeCompleto.Split(Convert.ToChar(" "));

            if (nome.Length > 0)
                nomeAbreviado = nome[0] + " " + nome[nome.Length - 1];

            return nomeAbreviado;
        }

        public static String FormatarCNPJ(String cnpj)
        {
            cnpj = "00000000000000" + cnpj;
            cnpj = cnpj.Substring(cnpj.Length - 14, 14);
            cnpj = cnpj.Insert(2, ".");
            cnpj = cnpj.Insert(6, ".");
            cnpj = cnpj.Insert(10, "/");
            cnpj = cnpj.Insert(15, "-");
            return cnpj;
        }

        public static String FormatarCPF(String cpf)
        {
            cpf = "00000000000" + cpf;
            cpf = cpf.Substring(cpf.Length - 11, 11);
            cpf = cpf.Insert(3, ".");
            cpf = cpf.Insert(7, ".");
            cpf = cpf.Insert(11, "-");

            return cpf;
        }

        public static String FormatarCEP(String cep)
        {
            cep = "00000000" + cep;
            cep = cep.Substring(cep.Length - 8, 8);
            cep = cep.Insert(5, "-");
            return cep;
        }

        public static String GetMonthName(Int32 month)
        {
            switch (month)
            {
                case 1: return "Janeiro";
                case 2: return "Fevereiro";
                case 3: return "Março";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";
            }
            return "";
        }

        public static string GetTokenCurrentUser()
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identities.FirstOrDefault();
            var id = identity.Claims.Where(c => c.Type == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault();
            var login = identity.Claims.Where(c => c.Type == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault();
            var role = identity.Claims.Where(c => c.Type == "http://seds.sp.gov.br/identity/claims/role" && c.Value.Contains(ConfigurationManager.AppSettings["Aplicativo"] + "@")).FirstOrDefault();
            return Genericos.clsCrypto.Encrypt(id.Value + ";" + login.Value + ";" + role.Value);
        }

        public static Int32 GetIdFromUri(Uri uri)
        {
            var array = uri.AbsoluteUri.Split('/');
            return Convert.ToInt32(array[array.Count() - 1]);
        }

    }
}
