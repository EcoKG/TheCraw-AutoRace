using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoRace
{
    public partial class Mainfrm : Form
    {
        Boolean cancel = false;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private infowin ab;
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private TimeSpan ts;
        private StringBuilder sb = new StringBuilder();
        private Eco_Lib.EcoLib Eco = null;
        RegistryKey rkey = Registry.CurrentUser.CreateSubKey(@"TheCrewAutoRace\Setting");

        Task startpart;
        Task timerpart;

        private struct Getsetting
        {
            public InputKeyLib.ScanCodeShort rightkey;
            public InputKeyLib.ScanCodeShort upkey;
            public InputKeyLib.ScanCodeShort nitrokey;
            public InputKeyLib.KEYEVENTF rightkeytype;
            public InputKeyLib.KEYEVENTF upkeytype;
            public InputKeyLib.KEYEVENTF nitrokeytype;
            public int presstime;
            public int burntime;
            public int nitrotime;
            public Getsetting(string rightkey, string upkey, string nitrokey, int presstime, int burntime,int nitrotime)
            {
                this.rightkey = InputKeyLib.ScanCodeShort.RIGHT;
                this.upkey = InputKeyLib.ScanCodeShort.UP;
                this.nitrokey = InputKeyLib.ScanCodeShort.LCONTROL;
                this.rightkeytype = InputKeyLib.KEYEVENTF.EXTENDEDKEY;
                this.upkeytype = InputKeyLib.KEYEVENTF.EXTENDEDKEY;
                this.nitrokeytype = InputKeyLib.KEYEVENTF.EXTENDEDKEY;
                this.presstime = presstime;
                this.burntime = burntime;
                this.nitrotime = nitrotime;
                switch (upkey)
                {
                    case "W":
                        this.upkey = InputKeyLib.ScanCodeShort.KEY_W;
                        this.upkeytype = InputKeyLib.KEYEVENTF.SCANCODE;
                        break;
                    case "UP":
                        this.upkey = InputKeyLib.ScanCodeShort.UP;
                        this.upkeytype = InputKeyLib.KEYEVENTF.EXTENDEDKEY;
                        break;
                }
                switch (rightkey)
                {
                    case "D":
                        this.rightkey = InputKeyLib.ScanCodeShort.KEY_D;
                        this.rightkeytype = InputKeyLib.KEYEVENTF.SCANCODE;
                        break;
                    case "Right":
                        this.rightkey = InputKeyLib.ScanCodeShort.RIGHT;
                        this.rightkeytype = InputKeyLib.KEYEVENTF.EXTENDEDKEY;
                        break;
                }
                switch (nitrokey)
                {
                    case "LCtrl":
                        this.nitrokey = InputKeyLib.ScanCodeShort.LCONTROL;
                        this.nitrokeytype = InputKeyLib.KEYEVENTF.SCANCODE;
                        break;
                    case "LShift":
                        this.nitrokey = InputKeyLib.ScanCodeShort.LSHIFT;
                        this.nitrokeytype = InputKeyLib.KEYEVENTF.SCANCODE;
                        break;
                }
            }
        }
        private Getsetting settinginfo;

        public Mainfrm()
        {
            InitializeComponent();
            Eco = Eco_Lib.EcoLib.GetInstance();
            Task.Factory.StartNew(() => hokeytask());
            if (rkey.ValueCount < 10)
            {
                rkey.SetValue("upkey", upcomb.Text);
                rkey.SetValue("rightkey", rightcomb.Text);
                rkey.SetValue("ntrokey", nitrocomb.Text);
                rkey.SetValue("rightpreetime", rarwpress.Text);
                rkey.SetValue("bruntime", bruntimetxt.Text);
                rkey.SetValue("Roundmoney", rndmoneytb.Text);
                rkey.SetValue("NitroTime", nitrowaittime.Text);
                rkey.SetValue("StartWaitToggle", startwaittog.Checked.ToString());
                rkey.SetValue("RightPressToggle", rightpresstog.Checked.ToString());
                rkey.SetValue("NitroWaitToggle", nitrowaittog.Checked.ToString());
            }
            else
            {
                Eco.CtlSetTxt(upcomb, rkey.GetValue("upkey"));
                Eco.CtlSetTxt(rightcomb, rkey.GetValue("rightkey"));
                Eco.CtlSetTxt(nitrocomb, rkey.GetValue("ntrokey"));
                Eco.CtlSetTxt(rarwpress, rkey.GetValue("rightpreetime"));
                Eco.CtlSetTxt(bruntimetxt, rkey.GetValue("bruntime"));
                Eco.CtlSetTxt(rndmoneytb, rkey.GetValue("Roundmoney"));
                Eco.CtlSetTxt(nitrowaittime, rkey.GetValue("NitroTime"));
                startwaittog.Checked = bool.Parse(rkey.GetValue("StartWaitToggle").ToString());
                rightpresstog.Checked = bool.Parse(rkey.GetValue("RightPressToggle").ToString());
                nitrowaittog.Checked = bool.Parse(rkey.GetValue("NitroWaitToggle").ToString());
            }
        }
        private void Mainfrm_Load(object sender, EventArgs e)
        {
            ab = new infowin();
            startbtn.Enabled = true;
            stopbtn.Enabled = false;
            if (startwaittog.Checked)
                rndmoneytb.Enabled = true;
            else
                rndmoneytb.Enabled = false;
            if (rightpresstog.Checked)
                rarwpress.Enabled = true;
            else
                rarwpress.Enabled = false;
            if (nitrowaittog.Checked)
                nitrowaittime.Enabled = true;
            else
                nitrowaittime.Enabled = false;
            setlog("프로그렘 실행됨");
        }
        private void starttask()
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
                Eco.CtlSetTxt(ab.label1, "정상 작동중");
                Eco.CtlSetTxt(ab.label2, "Finished Round ∞/ 0");
                Eco.CtlSetTxt(ab.infomoneylab, "총 수익 : 0$");
                ab.Show();
                sw.Start();
                startbtn.Enabled = false;
                stopbtn.Enabled = true;
                cancel = true;
                startpart = new Task(new Action(ScriptRun));
                timerpart = new Task(new Action(timertask));
                startpart.Start();
                timerpart.Start();
                //Task.Factory.StartNew(() => timertask());
                //Task.Factory.StartNew(() => ScriptRun());
                //Task.Factory.StartNew(() => processkilltask());
            }
        }
        private void stotask()
        {
            cancel = false;
            sw.Stop();
            startbtn.Enabled = true;
            stopbtn.Enabled = false;
            Eco.CtlSetTxt(ab.label1, "종료됨");
            startpart.Wait();
            startpart.Dispose();
        }
        private void startbtn_Click(object sender, EventArgs e)
        {
            starttask();
            //MessageBox.Show(Eco.GetColor(654,48));
        }
        private void stopbtn_Click(object sender, EventArgs e)
        {
            stotask();
        }
        public void ScriptRun()
        {
            string color = "";
            int giercnt = 1, cnt = 0, finish = 0;
            this.Invoke(new MethodInvoker(delegate ()
            {
                settinginfo = new Getsetting(rightcomb.Text, upcomb.Text, nitrocomb.Text, rightpresstog.Checked ? int.Parse(rarwpress.Text) : 100, startwaittog.Checked ? int.Parse(bruntimetxt.Text) : 4000,nitrowaittog.Checked ? int.Parse(nitrowaittime.Text) : 0);
            }));
            while (cancel)
            {
                while (cancel)
                {
                    if (Eco.GetColor(868, 386) == "0xF9072E")
                    {
                        Eco.Delay(settinginfo.burntime);
                        Eco.SendUp(settinginfo.upkey, settinginfo.upkeytype);
                        break;
                    }
                    if (Eco.GetColor(143, 843) == "0x15E466")
                    {
                        Eco.SendUp(settinginfo.upkey, settinginfo.upkeytype);
                    }
                    else
                    {
                        Eco.SendDown(settinginfo.upkey, settinginfo.upkeytype);
                    }
                } //시작전 서치
                while (cancel)
                {
                    color = Eco.GetColor(868, 386);
                    if (color == "0x15E466" || color == "0x15E366")
                    {
                        Eco.SendDown(settinginfo.upkey, settinginfo.upkeytype);
                        break;
                    }
                } //출발 서치
                while (cancel)
                {
                    if (Eco.GetColor(569, 631) == "0x15E466")
                    {
                        Eco.SendDown(InputKeyLib.ScanCodeShort.KEY_E, InputKeyLib.KEYEVENTF.SCANCODE);
                        Eco.Delay(100);
                        Eco.SendUp(InputKeyLib.ScanCodeShort.KEY_E, InputKeyLib.KEYEVENTF.SCANCODE);
                        giercnt = giercnt + 1;
                        if (giercnt == 5)
                        {
                            Eco.SendDown(settinginfo.rightkey, settinginfo.rightkeytype);
                            Eco.Delay(settinginfo.presstime);
                            Eco.SendUp(settinginfo.rightkey, settinginfo.rightkeytype);
                            Eco.Delay(settinginfo.nitrotime);
                            Eco.SendDown(settinginfo.nitrokey, settinginfo.nitrokeytype);
                            giercnt = 1;
                            break;
                        }
                    }
                }
                while (cancel)
                {
                    if (Eco.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\next.bmp", 20, "TheCrew2"))
                    {
                        cnt += 1;
                        Eco.SendUp(settinginfo.nitrokey, settinginfo.nitrokeytype);
                        Eco.SendUp(settinginfo.upkey, settinginfo.upkeytype);
                        while (cancel)
                        {
                            Eco.SendDown(InputKeyLib.ScanCodeShort.ESCAPE, InputKeyLib.KEYEVENTF.SCANCODE);
                            if (!Eco.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\next.bmp", 20, "TheCrew2"))
                            {
                                Eco.SendUp(InputKeyLib.ScanCodeShort.ESCAPE, InputKeyLib.KEYEVENTF.SCANCODE);
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
                        if (Eco.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\resume.bmp", 20, "TheCrew2"))
                        {
                            Eco.SendDown(InputKeyLib.ScanCodeShort.KEY_N, InputKeyLib.KEYEVENTF.SCANCODE);
                            while (cancel)
                            {
                                if (!Eco.ImgSearch(660, 780, 1260, 1080, @"D:\Scripts\img\resume.bmp", 20, "TheCrew2"))
                                {
                                    this.Invoke(new MethodInvoker(delegate ()
                                    {
                                        finish += 1;
                                        setlog(Eco.CreateString(finish, "회 완료 총수익 : ", int.Parse(rndmoneytb.Text) * finish));
                                        setinfo(finish);
                                        cnt = 0;
                                    }));
                                    Eco.SendUp(InputKeyLib.ScanCodeShort.KEY_N, InputKeyLib.KEYEVENTF.SCANCODE);
                                    break;
                                }
                            }
                            break;
                        }
                    } //다음라운드 서치
                }
            }
            Eco.SendUp(settinginfo.upkey, settinginfo.upkeytype);
            Eco.SendUp(settinginfo.nitrokey, settinginfo.nitrokeytype);
            Eco.SendUp(settinginfo.rightkey, settinginfo.rightkeytype);

        }
        public void setlog(string logmsg)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string msg = Eco.CreateString("[", dt.ToString("M월dd일-HH:mm:ss"), "] ", logmsg);
            this.Invoke(new MethodInvoker(delegate ()
            {
                loglistbox.Items.Add(msg);
            }));
        }
        public void setinfo(int finishcnt)
        {
            Eco.CtlSetTxt(countlab, "판수 : ", finishcnt, "회");
            Eco.CtlSetTxt(allrevenuslab, "총 수익 : ", int.Parse(rndmoneytb.Text) * finishcnt, "$");
            Eco.CtlSetTxt(ab.infomoneylab, "총 수익 : ", int.Parse(rndmoneytb.Text) * finishcnt, "$");
            Eco.CtlSetTxt(ab.label1, "Finished Round ∞/ ", finishcnt);
        }
        public void timertask()
        {
            while (cancel)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ts = sw.Elapsed;
                    string ti = String.Format("Run Time : <{0}H {1}M {2}S> ", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
                    Eco.CtlSetTxt(timerlab, ti);
                    Eco.CtlSetTxt(ab.infotimelab, ti);
                }));
            }
        }
        public void hokeytask()
        {
            while (true)
            {
                if ((Eco.GetAsyncKeyState((int)Keys.F2)) && (startbtn.Enabled == false))
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        stotask();
                    }));
                }
                if ((Eco.GetAsyncKeyState((int)Keys.F1)) && (stopbtn.Enabled == false))
                {

                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        starttask();
                    }));
                }
            }
        }
        private void Mainfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DateTime.Now.ToString("MM");
            setlog(Eco.CreateString("프로그렘종료됨 이유 [", e.CloseReason, "]"));
            if (!Directory.Exists("Log"))
            {
                Directory.CreateDirectory("Log");
            }
            StreamWriter Sw = new StreamWriter(Eco.CreateString(@"Log\", DateTime.Now.ToString(@"yyyy-MM-dd"), "[LOG].txt"), true);
            foreach (String logt in loglistbox.Items)
            {
                Sw.WriteLine(logt);
            }
            Sw.Close();
        }
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Eco.ReleaseCapture();
                Eco.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
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
            rkey.SetValue("NitroTime", nitrowaittime.Text);
            rkey.SetValue("StartWaitToggle", startwaittog.Checked.ToString());
            rkey.SetValue("RightPressToggle", rightpresstog.Checked.ToString());
            rkey.SetValue("NitroWaitToggle", nitrowaittog.Checked.ToString());
            this.Size = new System.Drawing.Size(140, 280);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Eco.SendDown(InputKeyLib.ScanCodeShort.KEY_W);
        }

        private void ecoButton1_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(740, 280);
        }

        private void ecoButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startwaittog_CheckedChanged(object sender, EventArgs e)
        {
            if (startwaittog.Checked)
                rndmoneytb.Enabled = true;
            else
                rndmoneytb.Enabled = false;

        }

        private void rightpresstog_CheckedChanged(object sender, EventArgs e)
        {
            if (rightpresstog.Checked)
                rarwpress.Enabled = true;
            else
                rarwpress.Enabled = false;
        }

        private void nitrowaittog_CheckedChanged(object sender, EventArgs e)
        {
            if (nitrowaittog.Checked)
                nitrowaittime.Enabled = true;
            else
                nitrowaittime.Enabled = false;
        }
    }
}
