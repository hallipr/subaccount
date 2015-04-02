namespace SubAccount.Loader.Ofx.Parsing
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class Segment
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Close { get; set; }

        public Dictionary<string, Segment[]> Children;

        public JToken ToJson()
        {
            if (this.Value != null)
            {
                return JValue.CreateString(this.Value);
            }

            var value = new JObject();

            foreach (var child in this.Children)
            {
                var childValues = child.Value.Select(x => x.ToJson()).ToArray();

                value[child.Key.ToLower()] = childValues.Length == 1 ? childValues[0] : JArray.FromObject(childValues);
            }

            return value;
        }
    }
}