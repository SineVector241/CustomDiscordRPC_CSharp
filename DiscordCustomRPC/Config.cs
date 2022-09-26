using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace DiscordCustomRPC
{
    public class Config
    {
        public Config()
        {
            if(!File.Exists("savedsettings.json"))
            {
                File.Create("savedsettings.json").Close();
                JObject obj = new JObject();
                obj["ClientID"] = "Client ID";
                obj["Details"] = "Details";
                obj["State"] = "State";
                obj["AutoStart"] = false;
                obj["LIK"] = "";
                obj["LIT"] = "";
                obj["SIK"] = "";
                obj["SIT"] = "";
                obj["BTN1"] = new JObject();
                obj["BTN1"]["Enabled"] = false;
                obj["BTN1"]["Text"] = "Text";
                obj["BTN1"]["URL"] = "URL";
                obj["BTN2"] = new JObject();
                obj["BTN2"]["Enabled"] = false;
                obj["BTN2"]["Text"] = "Text";
                obj["BTN2"]["URL"] = "URL";
                File.WriteAllText("savedsettings.json", obj.ToString());
            }
        }

        public void SaveSettings(JObject obj)
        {
            File.WriteAllText("savedsettings.json", obj.ToString());
        }

        public JObject LoadSettings()
        {
            return JObject.Parse(File.ReadAllText("savedsettings.json"));
        }
    }
}
