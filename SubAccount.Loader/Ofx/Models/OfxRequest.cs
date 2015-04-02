namespace SubAccount.Loader.Ofx.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using SubAccount.Loader.Ofx.Models.Banking;
    using SubAccount.Loader.Ofx.Models.Signon;
    using SubAccount.Loader.Ofx.Models.Signup;

    public class OfxRequest
    {
        public SignonMsgsRqV1 SignonMsgsRqV1 { get; set; }
        public SignupMsgsRqV1 SignupMsgsRqV1 { get; set; }
        public BankMsgsRqV1 BankMsgsRqV1 { get; set; }

        public override string ToString()
        {
            return string.Format("<OFX>{0}</OFX>", SerializeProperties(this));
        }

        private static string SerializeProperties(object target)
        {
            var type = target.GetType();
            var outString = new StringBuilder();

            foreach (var prop in GetOrderedProperties(type))
            {
                AddPropertyString(prop, target, outString);
            }

            return outString.ToString();
        }

        private static void AddPropertyString(PropertyInfo prop, object target, StringBuilder stringBuilder)
        {
            object value = prop.GetValue(target);
            
            if (value == null)
                return;

            var type = value.GetType();
            var propertyName = prop.Name.ToUpper();
                           
            stringBuilder.AppendFormat("<{0}>", propertyName);

            if (type == typeof(string) || type == typeof(int))
            {
                stringBuilder.Append(value);
            }
            else if (type == typeof(DateTime))
            {
                var dateValue = (DateTime)value;
                stringBuilder.Append(dateValue.ToString("yyyyMMddHHmmss.fff"));
            }
            else if (type == typeof(bool))
            {
                stringBuilder.Append(((bool)value) ? "Y" : "N");
            }
            else
            {
                foreach (var childProp in GetOrderedProperties(type))
                {
                    AddPropertyString(childProp, value, stringBuilder);
                }

                stringBuilder.AppendFormat("</{0}>", propertyName);
            }
        }

        private static IEnumerable<PropertyInfo> GetOrderedProperties(Type type)
        {
            return type.GetProperties().OrderBy(p => p.DeclaringType == type);
        }
    }
}