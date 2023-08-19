using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
using PowerMode.Properties;
using powermode;
using System.Threading;

namespace powrmod
{ 

    class MyApplicationContext : ApplicationContext
    {


        //Component declarations
        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        private ToolStripMenuItem DirMenuItem;


        public MyApplicationContext()
        {

               InitializeComponent();

        }

        static void Main()
        { 
            Application.Run(new MyApplicationContext());
        }
        private void InitializeComponent()
        {


            TrayIcon = new NotifyIcon();
            TrayIcon.Visible = true;

            TrayIcon.Text = "Change the PowerMode from the Taskbar";

            TrayIcon.Icon = PowerMode.Properties.Resources.powermode;

            TrayIcon.MouseDown += TrayIcon_Click;

            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            DirMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();

            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[]
            {this.CloseMenuItem,this.DirMenuItem});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new Size(153, 70);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new Size(152, 22);
            this.CloseMenuItem.Text = "Close the tray icon program";
            this.CloseMenuItem.Click += new EventHandler(this.CloseMenuItem_Click);
            //
            // Open file location
            //
            this.DirMenuItem.Name = "DirMenuItem";
            this.DirMenuItem.Size = new Size(152, 22);
            this.DirMenuItem.Text = "Open the location of the program";
            this.DirMenuItem.Click += new EventHandler(this.DirMenuItem_Click);

            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;


        }


        private void TrayIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                new Form1().Show();

            }




        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to quit PowerMode?",
                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void DirMenuItem_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;

            Process.Start("explorer.exe", @path);
        }

        private static class ppmod
        {
            /// Better Battery mode.
            public static Guid BetterBattery = new Guid("961cc777-2547-4f9d-8174-7d86181b8a7a");

            /// Better Performance mode.
            public static Guid BetterPerformance = new Guid("3af9B8d9-7c97-431d-ad78-34a8bfea439f");

            /// Best Performance mode.
            public static Guid BestPerformance = new Guid("ded574b5-45a0-4f42-8737-46345c09c238");

            /// Balanced mode.
            public static Guid Recommended = new Guid("00000000-0000-0000-0000-000000000000");
        }


        /// Retrieves the active overlay power scheme and returns a GUID that identifies the scheme.
        /// <param name="EffectiveOverlayPolicyGuid">A pointer to a GUID structure.</param>
        /// <returns>Returns zero if the call was successful, and a nonzero value if the call failed.</returns>
        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerGetEffectiveOverlayScheme")]
        private static extern uint PowerGetEffectiveOverlayScheme(out Guid EffectiveOverlayPolicyGuid);


        /// Sets the active power overlay power scheme.
        /// <param name="OverlaySchemeGuid">The identifier of the overlay power scheme.</param>
        /// <returns>Returns zero if the call was successful, and a nonzero value if the call failed.</returns>
        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
        private static extern uint PowerSetActiveOverlayScheme(Guid OverlaySchemeGuid);
    }

}
