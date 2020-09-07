using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareNo.common;

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
                Loggers.WriteError(ex.Message);
            }
        }
        

        internal async Task RunApplicationAsync(string[] args)
        {            
            ReadArguments(args);
            await PerformTasksAsync().ConfigureAwait(false);
            if (readAnyKey)
            {
                Loggers.WriteMessage("Press any key");
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
                Loggers.WriteMessage($"{i + 1}: Trying to connect to {catalog}@{connection}");
                connectionOk = await CheckConnectionAsync().ConfigureAwait(false);
                await Task.Delay(1000 * secondsDelay);
                i++;
            }
            if (connectionOk)
            {
                Loggers.WriteMessage("Connection OK...!");
            }
            else
            {
                Loggers.WriteMessage($"Could not connect after {i} etempts");
            }
        }

        private async Task<bool> CheckConnectionAsync()
        {
            try
            {
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
                        Loggers.WriteError(e.Message);
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
                Loggers.WriteError(ex.Message);
                return false;
            }
        }

        private bool AnalyzeInput()
        {
            var valid = true;
            if (string.IsNullOrEmpty(connection))
            {
                Loggers.WriteError("Connection info was not found");
                valid = false;
            }
            return valid;
        }

        private void WriteHelpScreen()
        {
            Loggers.WriteMessage("--------------------------------------");
            Loggers.WriteMessage($"How to use \"{System.AppDomain.CurrentDomain.FriendlyName}\"\n");
            Loggers.WriteMessage(" -c = The connection - example: -c \"dbserver\" or -c dbserver\\instanvce ");
            Loggers.WriteMessage(" -p = Pause after execution");
            Loggers.WriteMessage(" -r = Number of times to repeat on non connection");
            Loggers.WriteMessage(" -d = Database. (Initial catalog)");
            Loggers.WriteMessage(" -? = Show this page");
            
            Loggers.WriteMessage("--------------------------------------");
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
