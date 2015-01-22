using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Text;

namespace RnD.KendoUISample.Helpers
{
    public class ConvertHelper
    {
        public string ConvertListObjectToString<T>(IList<T> data, string delimiter, string seperator)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));

            StringBuilder contentBuilder = new StringBuilder();

            foreach (PropertyDescriptor prop in properties)
            {
                if (!prop.Name.Contains("Id") && !prop.Name.Contains("ID"))
                {
                    contentBuilder.Append(prop.Name);
                    contentBuilder.Append(seperator);
                }

            }

            contentBuilder.Append(Environment.NewLine);
            contentBuilder.Append(Environment.NewLine);

            foreach (T item in data)
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (!prop.Name.Contains("Id") && !prop.Name.Contains("ID"))
                    {
                        contentBuilder.Append(prop.GetValue(item) ?? DBNull.Value);
                        contentBuilder.Append(seperator);
                    }
                }
                contentBuilder.Length = contentBuilder.Length - 1;
                contentBuilder.Append(Environment.NewLine);
            }

            return contentBuilder.ToString();

        }
    }
}
