namespace AutoRace
{
    partial class Mainfrm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainfrm));
            this.loglistbox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.allrevenuslab = new System.Windows.Forms.Label();
            this.countlab = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.savebtn = new EcoLib.UI.EcoButton();
            this.bruntimetxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rarwpress = new System.Windows.Forms.TextBox();
            this.nitrocomb = new System.Windows.Forms.ComboBox();
            this.rndmoneytb = new System.Windows.Forms.TextBox();
            this.rightcomb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.upcomb = new System.Windows.Forms.ComboBox();
            this.timerlab = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uiCloseButton1 = new LibAutoHotkeyScriptManager.UICloseButton();
            this.stopbtn = new EcoLib.UI.EcoButton();
            this.startbtn = new EcoLib.UI.EcoButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // loglistbox
            // 
            resources.ApplyResources(this.loglistbox, "loglistbox");
            this.loglistbox.ForeColor = System.Drawing.Color.Black;
            this.loglistbox.FormattingEnabled = true;
            this.loglistbox.Name = "loglistbox";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.allrevenuslab);
            this.groupBox1.Controls.Add(this.countlab);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // allrevenuslab
            // 
            resources.ApplyResources(this.allrevenuslab, "allrevenuslab");
            this.allrevenuslab.Name = "allrevenuslab";
            // 
            // countlab
            // 
            resources.ApplyResources(this.countlab, "countlab");
            this.countlab.Name = "countlab";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.savebtn);
            this.groupBox2.Controls.Add(this.bruntimetxt);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rarwpress);
            this.groupBox2.Controls.Add(this.nitrocomb);
            this.groupBox2.Controls.Add(this.rndmoneytb);
            this.groupBox2.Controls.Add(this.rightcomb);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.upcomb);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // savebtn
            // 
            this.savebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(123)))), ((int)(((byte)(196)))));
            resources.ApplyResources(this.savebtn, "savebtn");
            this.savebtn.ForeColor = System.Drawing.Color.White;
            this.savebtn.Name = "savebtn";
            this.savebtn.UseVisualStyleBackColor = false;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // bruntimetxt
            // 
            resources.ApplyResources(this.bruntimetxt, "bruntimetxt");
            this.bruntimetxt.Name = "bruntimetxt";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // rarwpress
            // 
            resources.ApplyResources(this.rarwpress, "rarwpress");
            this.rarwpress.Name = "rarwpress";
            // 
            // nitrocomb
            // 
            this.nitrocomb.FormattingEnabled = true;
            this.nitrocomb.Items.AddRange(new object[] {
            resources.GetString("nitrocomb.Items"),
            resources.GetString("nitrocomb.Items1")});
            resources.ApplyResources(this.nitrocomb, "nitrocomb");
            this.nitrocomb.Name = "nitrocomb";
            // 
            // rndmoneytb
            // 
            resources.ApplyResources(this.rndmoneytb, "rndmoneytb");
            this.rndmoneytb.Name = "rndmoneytb";
            // 
            // rightcomb
            // 
            this.rightcomb.FormattingEnabled = true;
            this.rightcomb.Items.AddRange(new object[] {
            resources.GetString("rightcomb.Items"),
            resources.GetString("rightcomb.Items1")});
            resources.ApplyResources(this.rightcomb, "rightcomb");
            this.rightcomb.Name = "rightcomb";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // upcomb
            // 
            this.upcomb.FormattingEnabled = true;
            this.upcomb.Items.AddRange(new object[] {
            resources.GetString("upcomb.Items"),
            resources.GetString("upcomb.Items1")});
            resources.ApplyResources(this.upcomb, "upcomb");
            this.upcomb.Name = "upcomb";
            // 
            // timerlab
            // 
            resources.ApplyResources(this.timerlab, "timerlab");
            this.timerlab.ForeColor = System.Drawing.Color.White;
            this.timerlab.Name = "timerlab";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(47)))));
            this.panel1.Controls.Add(this.timerlab);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.uiCloseButton1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // uiCloseButton1
            // 
            this.uiCloseButton1.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.uiCloseButton1, "uiCloseButton1");
            this.uiCloseButton1.Name = "uiCloseButton1";
            this.uiCloseButton1.UseVisualStyleBackColor = false;
            this.uiCloseButton1.Click += new System.EventHandler(this.uiCloseButton1_Click);
            // 
            // stopbtn
            // 
            this.stopbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(123)))), ((int)(((byte)(196)))));
            resources.ApplyResources(this.stopbtn, "stopbtn");
            this.stopbtn.ForeColor = System.Drawing.Color.White;
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.UseVisualStyleBackColor = false;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // startbtn
            // 
            this.startbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(123)))), ((int)(((byte)(196)))));
            resources.ApplyResources(this.startbtn, "startbtn");
            this.startbtn.ForeColor = System.Drawing.Color.White;
            this.startbtn.Name = "startbtn";
            this.startbtn.UseVisualStyleBackColor = false;
            this.startbtn.Click += new System.EventHandler(this.startbtn_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Mainfrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(56)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.startbtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loglistbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Mainfrm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mainfrm_FormClosed);
            this.Load += new System.EventHandler(this.Mainfrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox loglistbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label allrevenuslab;
        private System.Windows.Forms.Label countlab;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox rndmoneytb;
        private System.Windows.Forms.TextBox rarwpress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox nitrocomb;
        private System.Windows.Forms.ComboBox rightcomb;
        private System.Windows.Forms.ComboBox upcomb;
        private System.Windows.Forms.Label timerlab;
        private EcoLib.UI.EcoButton startbtn;
        private EcoLib.UI.EcoButton stopbtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private LibAutoHotkeyScriptManager.UICloseButton uiCloseButton1;
        private System.Windows.Forms.TextBox bruntimetxt;
        private System.Windows.Forms.Label label1;
        private EcoLib.UI.EcoButton savebtn;
        private System.Windows.Forms.Timer timer1;
    }
}

