using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClusterEnv
{
    class Program
    {
       
        public class CliOptions
        {
            [Value(0, Required = true,MetaName ="Resource Name")]
            public string ClusterResourceName { get; set; }

            [Value(1, Required = true, MetaName ="Command")]
            public string Command { get; set; }

            [Value(2, MetaName = "Arguments")]
            public IList<string> Arguments { get; set; }

            [Usage(ApplicationAlias = "clusterenv")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    yield return new Example("Launching hostname with name set as myresourcce", new CliOptions { Command = "hostname", Arguments = null, ClusterResourceName ="myresource" });
                    yield return new Example("Launching Computer Management for resource MQSomething", new CliOptions { Command = "mmc", ClusterResourceName = "MQSomething", Arguments = new[] { "compmgmt.msc" } });
                }
            }
        }
        static void Main(string[] args)
        {
            var parser = new Parser(with => {
                with.EnableDashDash = true;
            });
            parser.ParseArguments<CliOptions>(args)
                   .WithParsed<CliOptions>(o => StartCommand(o));
        }

        

        

        private static void StartCommand(CliOptions o)
        {

            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = o.Command;
            si.UseShellExecute = false;
          

            si.EnvironmentVariables.Add("_CLUSTER_NETWORK_NAME_", o.ClusterResourceName);
            Console.WriteLine($"_CLUSTER_NETWORK_NAME_ set to {o.ClusterResourceName}");

            si.CreateNoWindow = false;
            si.WindowStyle = ProcessWindowStyle.Normal;
            
            si.Arguments = String.Join(" ",o.Arguments);
            Process p = new Process();
            p.StartInfo = si;

            try
            {
                p.Start();
                p.WaitForExit();
            }
            catch
            {
                Console.WriteLine($"Process {si.FileName} could not be started");
            }
           
        }
    }
}
