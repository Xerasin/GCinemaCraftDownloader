using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

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
        public static class cUpdate
        {
            public static JArray Filter { get { return (JArray)Launcher[indexLauncher]["update"]["filter"]; } }
        }
        public static class cMod
        {
            static int indexMod = -1;
            public static int Index { get { return indexMod; } set { indexMod = value; } }
            public static JArray Mod { get { return (JArray)Launcher[indexLauncher]["mod"]; } }
            public static JToken Name { get { return Mod[indexMod]["name"]; } }
            public static JToken Link { get { return Mod[indexMod]["link"]; } }
            public static JToken FileName { get { return Mod[indexMod]["filename"]; } }
            public static JToken Path { get { return Mod[indexMod]["path"]; } }
            public static class cUpdate
            {
                public static JArray Filter { get { return (JArray)Mod[indexMod]["update"]["filter"]; } }
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
                string msgInstructions = "" +
                "\a\tDownloading:\n" +
                "1.\tClick the 'Download' button, choose which items you want to download and click on 'Begin Operation'\n" +
                "2.\tSelect the location where you wish to install the selected Minecraft Launcher and its Mods\n" +
                "3.\tOnce the files are downloaded and uncompressed, the program will automatically load the Launcher to set it up\n" +
                "3.1.\r\tMultiMC:\n" +
                "3.2.\tSelect which JAVA version you want to use (Recommended: 64-bits)\n" +
                "3.2.1.\r\tMineCinema:\n" +
                "3.2.1.2.\tRight-click on 'MineCinema', select 'Edit Instance'\n" +
                "3.2.1.3.\tSelect the 'Version' option on the list and click on 'Install Forge'\n" +
                "3.2.1.4.\tAfter the application finishes fetching all the available Forge versions, choose which version you want to use (Recommended: Latest version or try different versions if problems occur) and click OK\n" +
                "3.2.1.5.\tSelect the 'Settings' option on the list.\n" +
                "3.2.1.6.\r\tSettings:\n" +
                "3.2.1.6.1.\tThe 'Maximum Memory Allocation' must be higher than '2048 MB'\n" +
                "3.2.1.6.2.\tMake sure the 'PermGen' value is higher than '128 MB'\n" +
                "3.2.1.7.\r\tServer:\n" +
                "3.2.1.7.1.\tIP: join.gcinema.net:25565\n" +
                "3.2.1.7.2.\tRemember to sync your Minecraft account on http://www.gcinema.net/forums/\n" +
                "\n\n" +
                "\a\tUpdating:\n" +
                "1.\tSelect the 'Update' radio button, choose which items you want to update and click on 'Begin Operation'\n" +
                "2.\tSelect the main folder where selected Launcher files are located\n" +
                "3.\tThe program will automatically update any necessary file\n";
                return msgInstructions;
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
