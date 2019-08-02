﻿using System;
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
        private JsonHelp jsonHelp;
        public JsonHelp()
        {

        }
        public string logininfo(string id, string password)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            ret.Add(new JsonStringValue(Jsonname.Password, password));
            return ret.ToString();
        }
        public string passwordcheckinfo(string password){
           JsonObjectCollection ret = new JsonObjectCollection();
           ret.Add(new JsonStringValue(Jsonname.Password,password));
           return ret.ToString();
        }
        public string idcheckinfo(string id)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            return ret.ToString();
        }
        public string plusidcheckinfo(string plusid, string id)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.FID, plusid));
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            return ret.ToString();
        }
        public string joininfo(string id, string password, string nickname, string phone)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.ID, id));
            ret.Add(new JsonStringValue(Jsonname.Password, password));
            ret.Add(new JsonStringValue(Jsonname.Nickname, nickname));
            ret.Add(new JsonStringValue(Jsonname.Phone, phone));
            return ret.ToString();
        }
        public string nicknamecheckinfo(string nickname)
        {
            JsonObjectCollection ret = new JsonObjectCollection();
            ret.Add(new JsonStringValue(Jsonname.Nickname, nickname));
            return ret.ToString();
        }
        public Dictionary<string, string> getlogininfo(string data)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            JsonParse jsonParse = new JsonParse(data);
            ret.Add(Jsonname.ID, jsonParse.GetstringValue(Jsonname.ID));
            ret.Add(Jsonname.Password, jsonParse.GetstringValue(Jsonname.Password));
            return ret;
        }
        public Dictionary<string, string> getidinfo(string data)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            JsonParse jsonParse = new JsonParse(data);
            ret.Add(Jsonname.ID, jsonParse.GetstringValue(Jsonname.ID));
            return ret;
        }
        public Dictionary<string, string> getnickinfo(string data)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            JsonParse jsonParse = new JsonParse(data);
            ret.Add(Jsonname.Nickname, jsonParse.GetstringValue(Jsonname.Nickname));
            return ret;
        }
        public string[] getRefreshnickarray(string data){
            JsonParse jsonParse = new JsonParse(data);
            string[] s=jsonParse.GetstringArrayValue("refreshnickarray");
            return s;
        }
    }
    public class Jsonname
    {
        public const string ID = "ID";
        public const string FID = "Fid";
        public const string Password = "Password";
        public const string Nickname = "Nickname";
        public const string Phone = "Phone";
        public const string Usernumber = "Usernumber";
    }
}
