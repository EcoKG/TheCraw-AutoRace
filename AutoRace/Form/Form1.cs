using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using sharpAHK;
using AutoRace.AutoHotkeyModul;
using Microsoft.Win32;
using System.Text;
using Eco_Lib;

namespace AutoRace
{
    public partial class Mainfrm : Form
    {
        [DllImport("user32.dll")] static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        [DllImport("user32")] public static extern bool GetAsyncKeyState(Int32 vKey);
        [DllImportAttribute("user32.dll")] public static extern bool ReleaseCapture();
        Boolean cancel = false;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public infowin ab;
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private TimeSpan ts;
        private StringBuilder sb = new StringBuilder();
        RegistryKey rkey = Registry.CurrentUser.CreateSubKey(@"TheCrewAutoRace\Setting");
        Task startpart;
        Task timerpart;
        public EcoHelper EcoLib = EcoHelper.GetInstance();
        public struct Getsetting
        {
            public string rightkey;
            public string upkey;
            public string nitrokey;
            public int waittime;
            public int burntime;
        }
        public Getsetting settinginfo;

        public Mainfrm()
        {
            InitializeComponent();
            Task.Factory.StartNew(() => Hokeytask());
            if (rkey.ValueCount < 6)
            {
                rkey.SetValue("upkey", upcomb.Text);
                rkey.SetValue("rightkey", rightcomb.Text);
                rkey.SetValue("ntrokey", nitrocomb.Text);
                rkey.SetValue("rightpreetime", rarwpress.Text);
                rkey.SetValue("bruntime", bruntimetxt.Text);
                rkey.SetValue("Roundmoney", rndmoneytb.Text);
            }
            else
            {
                EcoLib.SetCtlText(upcomb, EcoLib.ObjToString(rkey.GetValue("upkey")));
                EcoLib.SetCtlText(rightcomb, EcoLib.ObjToString(rkey.GetValue("rightkey")));
                EcoLib.SetCtlText(nitrocomb, EcoLib.ObjToString(rkey.GetValue("ntrokey")));
                EcoLib.SetCtlText(rarwpress, EcoLib.ObjToString(rkey.GetValue("rightpreetime")));
                EcoLib.SetCtlText(bruntimetxt, EcoLib.ObjToString(rkey.GetValue("bruntime")));
                EcoLib.SetCtlText(rndmoneytb, EcoLib.ObjToString(rkey.GetValue("Roundmoney")));
            }
            
        }
        private void Mainfrm_Load(object sender, EventArgs e)
        {
            ab = new infowin();
            startbtn.Enabled = true;
            stopbtn.Enabled = false;
            Setlog("프로그렘 실행됨");
        }
        private void Starttask()
        {
            int ret;
            if (int.TryParse(rarwpress.Text, out ret) == false)
            {
                MessageBox.Show("프레스 타임칸이 비어있거나 정수가 아닙니다.");
            }
            else if (int.TryParse(rndmoneytb.Text, out ret) == false)
            {
                MessageBox.Show("판당 수익칸이 비어있거나 정수가 아닙니다");
            }
            else if (int.TryParse(bruntimetxt.Text, out ret) == false)
            {
                MessageBox.Show("연소시간이 비어있거나 정수가 아닙니다");
            }
            else
            {
                EcoLib.SetCtlText(ab.label1,"정상 작동중");
                EcoLib.SetCtlText(ab.label2, "Finished Round ∞/ 0");
                EcoLib.SetCtlText(ab.infomoneylab, "총 수익 : 0$");
                ab.Show();
                sw.Start();
                startbtn.Enabled = false;
                stopbtn.Enabled = true;
                cancel = true;
                startpart = new Task(new Action(ScriptRun));
                timerpart = new Task(new Action(Timertask));
                startpart.Start();
                timerpart.Start();
                //Task.Factory.StartNew(() => timertask());
                //Task.Factory.StartNew(() => ScriptRun());
                //Task.Factory.StartNew(() => processkilltask());
            }
        }
        private void Stotask()
        {
            cancel = false;
            sw.Stop();
            startbtn.Enabled = true;
            stopbtn.Enabled = false;
            EcoLib.SetCtlText(ab.label1, "종료됨");
            startpart.Wait();
            startpart.Dispose();
        }
        private void startbtn_Click(object sender, EventArgs e)
        {
            Starttask();
        }
        private void stopbtn_Click(object sender, EventArgs e)
        {
            Stotask();
        }
        public void ScriptRun()
        {
            string color = "";
            int giercnt = 1, cnt = 0, finish = 0;
            this.Invoke(new MethodInvoker(delegate ()
            {
                settinginfo.burntime = int.Parse(bruntimetxt.Text);
                settinginfo.waittime = int.Parse(rarwpress.Text);
                settinginfo.upkey = upcomb.Text;
                settinginfo.rightkey = rightcomb.Text;
                settinginfo.nitrokey = nitrocomb.Text;
            }));
            AHKCMD kg = null;
            kg = AHKCMD.GetInstanc();
            while (cancel)
            {
                while (cancel)
                {
                    if (EcoLib.GetColor(868, 386) == "0xF9072E")
                    {
                        kg.Sleep(settinginfo.burntime);
                        kg.SendKeyUP(settinginfo.upkey);
                        break;
                    }
                    if (EcoLib.GetColor(143, 843) == "0x15E466")
                    {
                        kg.SendKeyUP(settinginfo.upkey);
                    }
                    else
                    {
                        kg.SendkeyDown(settinginfo.upkey);
                    } 
                } //시작전 서치
                while (cancel)
                {
                    color = EcoLib.GetColor(868, 386);
                    if (color == "0x15E466" || color == "0x15E366")
                    {
                        kg.SendkeyDown(settinginfo.upkey);
                        break;
                    }
                } //출발 서치
                while (cancel)
                {
                    if (EcoLib.GetColor(569, 631) == "0x15E466")
                    {
                        kg.SendkeyDown("E");
                        kg.Sleep(100);
                        kg.SendKeyUP("E");
                        giercnt = giercnt + 1;
                        if (giercnt == 5)
                        {
                            kg.SendkeyDown(settinginfo.rightkey);
                            kg.Sleep(settinginfo.waittime);
                            kg.SendKeyUP(settinginfo.rightkey);
                            kg.Sleep(4000);
                            kg.SendkeyDown(settinginfo.nitrokey);
                            giercnt = 1;
                            break;
                        }
                    }
                } 
                while (cancel)
                {
                    if (EcoLib.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\next.bmp", 0.55f, "TheCrew2").Equals("0"))
                    {
                        cnt += 1;
                        kg.SendKeyUP(settinginfo.nitrokey);
                        kg.SendKeyUP(settinginfo.upkey);
                        while (cancel)
                        {
                            kg.SendkeyDown("Esc");
                            if (EcoLib.ImgSearch(660, 780, 1260, 1080,@"D:\Scripts\img\next.bmp", 0.55f, "TheCrew2").Equals("1"))
                            {
                                kg.SendKeyUP("Esc");
                                break;
                            }
                        }
                        break;
                    }
                } //런 서치
                if (cnt == 3)
                {
                    while (cancel)
                    {
                        if (EcoLib.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\resume.bmp",0.55f,"TheCrew2").Equals("0"))
                        {
                            kg.SendkeyDown("N");
                            while (cancel)
                            {
                                if (EcoLib.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\resume.bmp",0.55f,"TheCrew2").Equals("1"))
                                {
                                    this.Invoke(new MethodInvoker(delegate ()
                                    {
                                        finish += 1;
                                        Setlog(finish + "회 완료 총수익 : " + int.Parse(rndmoneytb.Text) * finish);
                                        Setinfo(finish);
                                        cnt = 0;
                                    }));
                                    kg.SendKeyUP("N");
                                    break;
                                }
                            }
                            break;
                        }
                    } //다음라운드 서치
                }
            }
            kg.SendKeyUP(settinginfo.upkey);
            kg.SendKeyUP(settinginfo.nitrokey);
            kg.SendKeyUP(settinginfo.rightkey);

        } 
        public void Setlog(string logmsg)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string msg = "[" + dt.ToString("M월dd일-HH:mm:ss") + "] " + logmsg;
            this.Invoke(new MethodInvoker(delegate ()
            {
                loglistbox.Items.Add(msg);
            }));
        }
        public void Setinfo(int finishcnt)
        {
            EcoLib.SetCtlText(countlab, "판수 : ", finishcnt.ToString(), "회");
            EcoLib.SetCtlText(allrevenuslab, "총 수익 : ", (int.Parse(rndmoneytb.Text) * finishcnt).ToString(),"$");
            EcoLib.SetCtlText(ab.infomoneylab, "총 수익 : ",(int.Parse(rndmoneytb.Text) * finishcnt).ToString(),"$");
            EcoLib.SetCtlText(ab.label2, "Finished Round ∞/ ",finishcnt.ToString());
        }
        public void Timertask()
        {
            while(cancel)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ts = sw.Elapsed;
                    string ti = String.Format("Run Time : <{0}H {1}M {2}H> ",sw.Elapsed.Hours,sw.Elapsed.Minutes,sw.Elapsed.Seconds);
                    EcoLib.SetCtlText(timerlab, ti);
                    EcoLib.SetCtlText(ab.infotimelab, ti);
                }));
            }
        }
        public void Hokeytask()
        {
            while(true)
            {
                if ((GetAsyncKeyState((int)Keys.F2)) && (startbtn.Enabled == false))
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        Stotask();
                    }));
                }
                if ((GetAsyncKeyState((int)Keys.F1)) && (stopbtn.Enabled == false))
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        Starttask();
                    }));
                }
            }
        }
        private void Mainfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Setlog("프로그렘종료됨 이유 [" + e.CloseReason.ToString() + "]");
            if (!Directory.Exists("Log"))
            {
                Directory.CreateDirectory("Log");
            }
            StreamWriter Sw = new StreamWriter(@"Log\" + DateTime.Now.ToString(@"yyyy-MM-dd") + "[LOG].txt", true);
            foreach(String logt in loglistbox.Items)
            {
                Sw.WriteLine(logt);
            }
            Sw.Close();
        }
        private void uiCloseButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            rkey.SetValue("upkey", upcomb.Text);
            rkey.SetValue("rightkey", rightcomb.Text);
            rkey.SetValue("ntrokey", nitrocomb.Text);
            rkey.SetValue("rightpreetime", rarwpress.Text);
            rkey.SetValue("bruntime", bruntimetxt.Text);
            rkey.SetValue("Roundmoney", rndmoneytb.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            EcoLib.SetCtlText(savebtn, EcoLib.ImgSearch2(15, 35, 55, 80, @"D:\Scripts\img\test1.bmp", 60, "카카오톡").ToString());
        }
    }
}
