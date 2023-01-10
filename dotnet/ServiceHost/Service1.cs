using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHost
{
    public partial class Service1 : ServiceBase
    {
        List<Process> Processes { get; set; }  =  new List<Process>();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Process p = Process.Start("SqlServerDataPush.exe");
        }

        protected override void OnStop()
        {
            foreach (Process process in Processes)
            {
                process.Kill();
            }
        }
    }
}
