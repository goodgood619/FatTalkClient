using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Json;
using WpfApp1.Models;

namespace WpfApp1.Modules
{
    public class JsonHelp
    {
        public string logininfo(string id,string password)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            ret.Add(new JsonStringValue(Jsonname.Password, password));
            return ret.ToString();
        }

        public string idcheckinfo(string id)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            return ret.ToString();
        }
        public string joininfo(string id,string password,string nickname,string phone)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            ret.Add(new JsonStringValue(Jsonname.Password, password));
            ret.Add(new JsonStringValue(Jsonname.Nickname, nickname));
            ret.Add(new JsonStringValue(Jsonname.Phone, phone));
            return ret.ToString();
        }
        public class Jsonname
        {
            public const string ID = "ID";
            public const string Password = "Password";
            public const string Nickname = "Nickname";
            public const string Phone = "Phone";
        }
    }
}
