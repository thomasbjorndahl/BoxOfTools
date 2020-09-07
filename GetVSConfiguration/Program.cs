using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VareNo.common;

namespace GetVSConfiguration
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if(args == null || args.Length < 1)
            {
                Loggers.WriteError("You need to specify a folder where I can find the .csproj.user file... dumb ass...");
           
            }
            else
            {
                Loggers.Write(GetConfiguration(args[0]));                
            }
        }

        private static string GetConfiguration(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Loggers.WriteError("No path found");
            }
            else
            {
                if(path == ".")
                {
                    path = Directory.GetCurrentDirectory();
                }                
                else if(!Directory.Exists(path))
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), path);
                }

                if (!Directory.Exists(path))
                {
                    Loggers.WriteError($"Could not find directory \"{path}\"");
                }
                else
                {
                    //Trying to find a .user file
                    var files = new DirectoryInfo(path).GetFiles("*.csproj.user");
                    if(null == files || files.Length == 0)
                    {
                        Loggers.WriteError($"Could not find any .csproj.user files in \"{path}\"");
                    }
                    else
                    {
                        var content = File.ReadAllText(files[0].FullName);
                        var configurationLine = Regex.Match(content, @"LastActiveSolutionConfig\>[a-z0-9\| ]+\<", RegexOptions.IgnoreCase);
                        if (!configurationLine.Success)
                        {
                            Loggers.WriteError($"Found a file, but could not find configuration");
                        }
                        else
                        {
                            var configurationProps = configurationLine.Value.Replace("LastActiveSolutionConfig>", "").Replace("<", "");
                            if (configurationProps.Contains("|"))
                            {
                                return configurationProps.Split('|')[0];
                            }
                            else
                            {
                                return configurationProps;
                            }

                        }
                    }
                }
            }
            return "";
        }
    }
}
