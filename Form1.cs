using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace powermode
{
    public partial class Form1 : Form
    {



        Timer t1 = new Timer();
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);
        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Deactivate(object sender, EventArgs e)
        {

            FadeOut(this, 5);

        }

        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   
            else
                Opacity += 0.05;
        }
        private async void FadeOut(Form o, int interval = 5)
        {

            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0; //make fully invisible
            this.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            var pt = new Point();
            GetCursorPos(ref pt);

            var tx = pt.X;
            var ty = pt.Y;

            this.Location = new Point(tx - 125, ty - 260);

            Opacity = 0;      //first the opacity is 0

            t1.Interval = 1; 
            t1.Tick += new EventHandler(fadeIn);  
            t1.Start();

            System.Threading.Thread.Sleep(100);


            this.Focus();

            this.TopMost = true; 
            this.Activate();

            string args = "";


            if (args.Length == 0)
            {
                // Report the current power mode.
                uint result = PowerGetEffectiveOverlayScheme(out Guid currentMode);
                Console.WriteLine(result);
                Console.WriteLine(currentMode);
                if (currentMode == PowerMode.Recommended)
                {
                    Console.WriteLine("Recommended");
                    pictureBox1.Show();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                }
                else if (currentMode == PowerMode.BestPerformance)
                {
                    Console.WriteLine("Best performance");
                    pictureBox3.Show();
                    pictureBox2.Hide();
                    pictureBox1.Hide();
                }
                else if (currentMode == PowerMode.BetterPerformance)
                {
                    Console.WriteLine("Better performance");
                    pictureBox2.Show();
                    pictureBox1.Hide();
                    pictureBox3.Hide();
                }


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guid powerMode;
            powerMode = PowerMode.Recommended;
            Console.WriteLine("Recommended");
            pictureBox1.Show();
            pictureBox2.Hide();
            pictureBox3.Hide();
            PowerSetActiveOverlayScheme(powerMode);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Guid powerMode;
            Console.WriteLine("Better performance");
            pictureBox2.Show();
            pictureBox3.Hide();
            pictureBox1.Hide();
            powerMode = PowerMode.BetterPerformance;
            PowerSetActiveOverlayScheme(powerMode);




        }

        private void button3_Click(object sender, EventArgs e)
        {

            Guid powerMode;
            powerMode = PowerMode.BestPerformance;
            Console.WriteLine("Best performance");
            pictureBox3.Show();
            pictureBox2.Hide();
            pictureBox1.Hide();
            PowerSetActiveOverlayScheme(powerMode);
        }

        private static class PowerMode
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
        /// </summary>      /// <param name="OverlaySchemeGuid">The identifier of the overlay power scheme.</param>
        /// <returns>Returns zero if the call was successful, and a nonzero value if the call failed.</returns>
        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
        private static extern uint PowerSetActiveOverlayScheme(Guid OverlaySchemeGuid);

    }

}
