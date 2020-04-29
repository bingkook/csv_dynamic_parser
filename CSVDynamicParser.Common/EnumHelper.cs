using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSVDynamicParser.Common
{
    public class EnumHelper
    {
        public static T GetEnumByName<T>(string name)
        {
            try
            {
                foreach (var item in System.Enum.GetValues(typeof(T)))
                {
                    if (item.ToString().ToLower() == name.ToLower())
                    {
                        return (T)item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            throw new ArgumentNullException("Enum name invalid");
        }

        public static String GetEnumDescription(object e)
        {
            try
            {
                FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    return EnumAttributes[0].Description;
                }
                return e.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static List<T> Enum2ObjectList<T>()
        {
            List<T> ol = new List<T>();
            foreach (var item in System.Enum.GetValues(typeof(T)))
            {
                ol.Add((T)item);
            }
            return ol;
        }

        public static List<EnumItem> Enum2List<T>()
        {
            List<EnumItem> ol = new List<EnumItem>();
            foreach (var item in System.Enum.GetValues(typeof(T)))
            {
                ol.Add(new EnumItem()
                {
                    Id = ((int)item).ToString(),
                    Name = item.ToString()
                });
            }
            return ol;
        }

        public static List<EnumItem> Enum2ListWithDescAsName<T>()
        {
            List<EnumItem> ol = new List<EnumItem>();
            foreach (var item in System.Enum.GetValues(typeof(T)))
            {
                ol.Add(new EnumItem()
                {
                    Id = ((int)item).ToString(),
                    Name = GetEnumDescription(item)
                });
            }
            return ol;
        }

    }

    public class EnumItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
