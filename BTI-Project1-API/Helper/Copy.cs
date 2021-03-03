using BTI_Project1_API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Helper
{
    public class Copy
    {
        public static K Action<T, K>(T from, K to)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttributes(typeof(IgnoreCopy), false).Length > 0) continue;

                var Prop = typeof(K).GetProperty(property.Name);
                Prop.SetValue(to, property.GetValue(from));
            }
            return to;
        }
    }
}