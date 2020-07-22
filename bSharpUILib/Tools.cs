using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bSharpUILib.Models;

namespace bSharpUILib
{
    
    public static class Tools
    {
        public static List<GameModel> ReadGames (string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException($"Could not find a file called {Path.GetFileName(databasePath)} in path {databasePath}.");
            }

            List<string> lines = File.ReadAllLines(databasePath).ToList();
            List<GameModel> output = new List<GameModel>();

            foreach (string line in lines)
            {
                string[] temp = line.Split(',');
                output.Add(new GameModel(temp[0], temp[1]));
            }
            return output;
        }

        public static void WriteSettingsConfigValue()
        {
            
        }
    }
}
