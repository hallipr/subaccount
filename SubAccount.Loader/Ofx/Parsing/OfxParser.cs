namespace SubAccount.Loader.Ofx.Parsing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using SubAccount.Loader.Ofx.Models;

    public static class OfxParser
    {
        public static OfxResponse Parse(string ofxString)
        {
            var jObject = ToJson(ofxString);

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new OfxDateTimeConverter());
            serializer.Converters.Add(new OfxBoolConverter());
            
            return jObject.ToObject<OfxResponse>(serializer);
        }

        public static JObject ToJson(string ofxString)
        {
            var segments = Regex.Matches(ofxString, "<(/)?([^>]+)>([^<]+)?")
                .OfType<Match>()
                .Select(m => new Segment
                {
                    Name = m.Groups[2].Value,
                    Close = m.Groups[1].Success,
                    Value = m.Groups[3].Success ? m.Groups[3].Value : null
                });

            var stack = new Stack<Segment>();
            foreach (var segment in segments)
            {
                if (!segment.Close)
                {
                    stack.Push(segment);
                    continue;
                }

                var children = new Stack<Segment>();

                while (true)
                {
                    var peek = stack.Peek();

                    if (peek.Name == segment.Name)
                    {
                        peek.Children = children.GroupBy(x => x.Name).ToDictionary(x => x.Key, x => x.ToArray());
                        break;
                    }

                    children.Push(stack.Pop());
                }
            }

            var ofx = stack.Single();

            return ofx.ToJson() as JObject;
        }
    }
}
