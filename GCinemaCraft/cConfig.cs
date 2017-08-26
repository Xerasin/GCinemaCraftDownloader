using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace GCinemaCraft
{
    class cConfig
    {
        public void parseConfig(string config)
        {
            try
            {
                JObject configParse = JObject.Parse(config);
                if (!string.IsNullOrEmpty(configParse.ToString()))
                {
                    cOperation.Config = configParse;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch
            {
                throw;
            }
        }
    }
    
    public static class cLauncher
    {
        static int indexLauncher = 0;
        public static int Index { get { return indexLauncher; } set { indexLauncher = value; } }
        public static JArray Launcher { get { return (JArray)cOperation.Config["launcher"]; } }
        public static JToken Name { get { return Launcher[indexLauncher]["name"]; } }
        public static JToken Link { get { return Launcher[indexLauncher]["link"]; } }
        public static JToken FileName { get { return Launcher[indexLauncher]["filename"]; } }
        public static JToken Executable { get { return Launcher[indexLauncher]["executable"]; } }
        public static class cMod
        {
            static int indexMod = -1;
            public static int Index { get { return indexMod; } set { indexMod = value; } }
            public static JArray Mod { get { return (JArray)Launcher[indexLauncher]["mod"]; } }
            public static JToken Name { get { return Mod[indexMod]["name"]; } }
            public static JToken Link { get { return Mod[indexMod]["link"]; } }
            public static JToken FileName { get { return Mod[indexMod]["filename"]; } }
            public static JToken Path { get { return Mod[indexMod]["path"]; } }
            public static string InstallPath
            {
                get
                {
                    return (string)cLauncher.cMod.Mod[cLauncher.cMod.indexMod][(object)"lastInstall"];
                }
                set
                {
                    cLauncher.cMod.Mod[cLauncher.cMod.indexMod][(object)"lastInstall"] = value;
                    cOperation.saveConfig();

                }
            }
            public static string Type
            {
                get
                {
                    return (string)cLauncher.cMod.Mod[cLauncher.cMod.indexMod][(object)"type"];
                }
            }
        }
    }
    
    public static class cMessage
    {
        public static string About
        {
            get
            {
                string msgAbout = "" +
                "GCinemaCraft - New Year Edition XP Ultimate\n" +
                "\n" +
                "Automatic downloader and updater of the files required to play in the GCinema Minecraft server (although you can set up other launchers and mods in the config)\n" +
                "\n" +
                "Version: 2.0 (01/01/2015)\n" +
                "\n\n" +
                "Licensed to: NOT_SANTA\n" +
                "License valid until: 12/12/9999";
                return msgAbout;
            }
        }
        public static string Instructions
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "GCinemaCraft.instructions.txt";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
        public static string Random
        {
            get
            {
                string[] msgRandom = new string[]
                {
                    "Who says Notch is the only one that can put random messages each time you open a program?",
                    "Did you know? In 2014, the great Ratserini almost beat Imsoreallybored in a Dota 2 match but the noob haxed the game to win?",
                    "'Lorem Ipsum dolor sit a-meth' - W. White",
                    "Did you know? Version 2.0 was finished 5 minutes before January 2?",
                    "There are no easter eggs in this program. No, don\'t even try the Konami code.",
                    "So I herd you wantedz to play Meinkrafts",
                    "Did you know? If you reload this program... nothing will happen? Well, except this text is probably gonna be different",
                    "Don't click this letter A",
                    "You did click the letter A huh? You can\'t fool me"
                };
                return msgRandom[new Random().Next(msgRandom.Length)];
            }
        }
        public static class cDialog
        {
            public static string Download
            {
                get
                {
                    return "Choose the location where you wish to put the files";
                }
            }
            public static string Update
            {
                get
                {
                    return "Select the main folder of the Launcher you wish to update";
                }
            }
        }
    }

    public static class cOperation
    {
        static JObject config;
        static int type = 0;

        public static JObject Config { get { return config; } set { config = value; } }
        public static int Type { get { return type; } set { type = value; } }

        public static void saveConfig()
        {
            using (FileStream fs = File.Open(cPath.Config, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, config);
            }
        }
    }
    
    public static class cPath
    {
        static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        static string config = baseDirectory + "config.json";
        static string dialog = "";
        static string downloaded = baseDirectory + "Downloaded\\";
        static string uncompressed = baseDirectory + "Uncompressed\\";
        
        public static string Config { get { return config; } set { config = value; } }
        public static string Dialog { get { return dialog; } set { dialog = value; } }
        public static string Downloaded { get { return downloaded; } set { downloaded = value; } }
        public static string Uncompressed { get { return uncompressed; } set { uncompressed = value; } }
    }
}
