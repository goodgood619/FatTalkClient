using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Json;
namespace WpfApp1.Modules
{
    public class JsonParse
    {
        private JsonTextParser parser;
        private JsonObjectCollection objcollection;

        public JsonParse(string jsondata)
        {
            parser = new JsonTextParser();
            objcollection = (JsonObjectCollection)parser.Parse(jsondata);

        }

        public string GetstringValue(string name)
        {
            string s = string.Empty;
            try
            {
                s = Convert.ToString(objcollection[name].GetValue()); 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return s;
        }

        public string[] GetstringArrayValue(string name)
        {
            string[] array = null;
            try
            {
                JsonArrayCollection jsonArrayCollection = (JsonArrayCollection)objcollection[name];
                int count = jsonArrayCollection.Count;
                for(int i = 0; i < count; i++)
                {
                    array[i] = ((JsonStringValue)jsonArrayCollection[i]).Value;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return array;
        }
    }
}
