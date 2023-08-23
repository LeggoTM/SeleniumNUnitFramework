using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitFramework.Utilities
{
    internal class JsonReader
    {
        public string ExtractJsonData(string tokenName)
        {

            var myJsonStr = File.ReadAllText("Utilities/TestData.json");
            var jsonObject = JToken.Parse(myJsonStr);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
    }
}
