using System;
using SimpleCache.Core;

namespace SimpleCache.CLI
{
    public sealed class CLIParser
    {
        private static readonly SimpleCacheDb db = new SimpleCacheDb();
        static CLIParser()
        {
        }

        private CLIParser()
        {
        }

        public static string Cmd(string cmd){
            string[] param = cmd.Split(' ');
            string stringResult = "False";
            switch(param[0].ToUpper())
            {
                case "SET":
                    var key = param[1];
                    var value = param[2];
                    bool result = db.Set(key, value);
                    stringResult = result.ToString();
                    break;
            }
            return stringResult;
        }
    }
}
