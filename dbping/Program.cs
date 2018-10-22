using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VareNo.dbping
{
    class Program
    {
        private bool showHelp;
        private bool readAnyKey;
        private string connection;
        private int numbOfTries = 5;
        private int secondsDelay = 1;
        private string catalog = "master";
        static void Main(string[] args)
        {
            try
            {
                new Program().RunApplicationAsync(args).Wait();
            }
            catch(Exception ex)
            {
                WriteError(ex.Message);
            }
        }
        #region loggers
        private static void WriteError(string message)
        {
            var cl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error occured: {message}");
            Console.ForegroundColor = cl;
        }

        private static void WriteMessage(string message)
        {           
            Console.WriteLine($"{message}");
           
        }
        #endregion

        internal async Task RunApplicationAsync(string[] args)
        {            
            ReadArguments(args);
            await PerformTasksAsync().ConfigureAwait(false);
            if (readAnyKey)
            {
                WriteMessage("Press any key");
                Console.ReadKey();
            }
        }

        private async Task PerformTasksAsync()
        {
            if (showHelp)
            {
                WriteHelpScreen();
            }
            else
            {
                if (AnalyzeInput())
                {
                    await PingDatabaseAsync().ConfigureAwait(false);
                }
                else
                {
                    WriteHelpScreen();
                }
            }
        }

        private async Task PingDatabaseAsync()
        {
            int i = 0;
            var connectionOk = false;
            while(i < numbOfTries && !connectionOk)
            {
                WriteMessage($"{i + 1}: Trying to connect to {catalog}@{connection}");
                connectionOk = await CheckConnectionAsync().ConfigureAwait(false);
                await Task.Delay(1000 * secondsDelay);
                i++;
            }
            if (connectionOk)
            {
                WriteMessage("Connection OK...!");
            }
            else
            {
                WriteMessage($"Could not connect after {i} etempts");
            }
        }

        private async Task<bool> CheckConnectionAsync()
        {
            try
            {
                //Data Source=KBG1DBDEV01\SQL2012;Initial Catalog=Interfaces;Integrated Security=True
                using (var conn = new SqlConnection($"Data Source={connection};Initial Catalog={catalog};Integrated Security=True"))
                {
                    SqlCommand command = null;
                    try
                    {
                        await conn.OpenAsync().ConfigureAwait(false);
                        command = new SqlCommand("select 1",conn);

                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            command.CommandTimeout = 20;
                            var data = await command.ExecuteScalarAsync().ConfigureAwait(false);
                            return null != data;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch(Exception e)
                    {
                        WriteError(e.Message);
                        return false;
                    }
                    finally
                    {
                        if (null != command) { command.Dispose(); }
                        if (null != conn) conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                WriteError(ex.Message);
                return false;
            }
        }

        private bool AnalyzeInput()
        {
            var valid = true;
            if (string.IsNullOrEmpty(connection))
            {
                WriteError("Connection info was not found");
                valid = false;
            }
            return valid;
        }

        private void WriteHelpScreen()
        {
            WriteMessage("--------------------------------------");
            WriteMessage($"How to use \"{System.AppDomain.CurrentDomain.FriendlyName}\"\n");
            WriteMessage(" -c = The connection - example: -c \"dbserver\" or -c dbserver\\instanvce ");
            WriteMessage(" -p = Pause after execution");
            WriteMessage(" -r = Number of times to repeat on non connection");
            WriteMessage(" -d = Database. (Initial catalog)");
            WriteMessage(" -? = Show this page");
            
            WriteMessage("--------------------------------------");
        }

        private void ReadArguments(string[] args)
        {
            Action<string> nextParameter = null;
            foreach (var arg in args)
            {
                if (null != nextParameter)
                {
                    nextParameter(arg);
                    nextParameter = null;
                }
                else
                {
                    switch (arg.ToLowerInvariant())
                    {
                        case "-d":
                            nextParameter = (value) =>
                            {
                                catalog = (value ?? "").Replace("\"", "");
                            };
                            break;
                        case "-c":
                            nextParameter = (value) =>
                            {
                                connection = (value ?? "").Replace("\"","");
                            };
                            break;
                        case "-r":
                            nextParameter = (value) =>
                            {
                                var val = (value ?? "5").Replace("\"", "");
                                int.TryParse(val, out numbOfTries);                                
                            };
                            break;
                        case "-p":
                            readAnyKey = true;
                            nextParameter = null;
                            break;
                        case "?":
                        case "-?":
                            showHelp = true;
                            nextParameter = null;
                            break;
                    }
                }
            }
            
        }
    }
}
