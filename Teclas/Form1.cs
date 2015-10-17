using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace Teclas
{
    public partial class Form1 : Form
    {
        private KeyHandler F1;
        private KeyHandler F2;
        private bool corriendo,hablando = false;
        private static System.Timers.Timer timerF1, timerF2;

        public Form1()
        {
            F1 = new KeyHandler(Keys.F1, this);
            F2 = new KeyHandler(Keys.F2, this);
            F1.Register();
            F2.Register();
            InitializeComponent();

            f1lbl.ForeColor = Color.Red;
            f2lbl.ForeColor = Color.Red;

            timerF1 = new System.Timers.Timer(2010);
            timerF1.Elapsed += OnTimedEvent;
            timerF1.AutoReset = true;

            timerF2 = new System.Timers.Timer(700);
            timerF2.Elapsed += OnTimedEvent2;
            timerF2.AutoReset = true;
        }
        private void HandleF1()
        {
            if (corriendo)
            {
                timerF1.Stop();
                corriendo = false;
                f1lbl.ForeColor = Color.Red;
                f1lbl.Text = "APAGADO";
            }
            else
            {
                timerF1.Start();
                corriendo = true;
                f1lbl.Text = "ENCENDIDO";
                f1lbl.ForeColor = Color.Green;
            }
        }
        private void HandleF2()
        {
            if (hablando)
            {
                timerF2.Stop();
                hablando = false;
                f2lbl.ForeColor = Color.Red;
                f2lbl.Text = "APAGADO";
            }
            else
            {
                timerF2.Start();
                hablando = true;
                f2lbl.Text = "ENCENDIDO";
                f2lbl.ForeColor = Color.Green;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                if (m.WParam.ToInt32() == F1.GetId())
                    HandleF1();
                else if (m.WParam.ToInt32() == F2.GetId())
                    HandleF2();
            base.WndProc(ref m);
        }


        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SendKeys.SendWait("{UP}");
            SendKeys.SendWait("{UP}");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("{RIGHT}");
            SendKeys.SendWait("{RIGHT}");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("{DOWN}");
            SendKeys.SendWait("{DOWN}");
            System.Threading.Thread.Sleep(500);
            SendKeys.SendWait("{LEFT}");
            SendKeys.SendWait("{LEFT}");
            System.Threading.Thread.Sleep(500);
        }

        private static void OnTimedEvent2(Object source, ElapsedEventArgs e)
        {
            SendKeys.SendWait("z");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            corriendo = false;
            hablando = false;
            timerF1.Stop();
            timerF2.Stop();
            timerF1.Close();
            timerF2.Close();
            timerF1.Dispose();
            timerF2.Dispose();
            this.Close();
        }
    }
}
