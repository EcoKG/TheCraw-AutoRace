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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nitrowaittime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ecoButton2 = new Eco_Skin.EcoButton();
            this.nitrowaittog = new Eco_Skin.EcoToggleButton();
            this.rightpresstog = new Eco_Skin.EcoToggleButton();
            this.startwaittog = new Eco_Skin.EcoToggleButton();
            this.ecoButton1 = new Eco_Skin.EcoButton();
            this.savebtn = new Eco_Skin.EcoButton();
            this.stopbtn = new Eco_Skin.EcoButton();
            this.startbtn = new Eco_Skin.EcoButton();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.allrevenuslab);
            this.groupBox1.Controls.Add(this.countlab);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // allrevenuslab
            // 
            resources.ApplyResources(this.allrevenuslab, "allrevenuslab");
            this.allrevenuslab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.allrevenuslab.Name = "allrevenuslab";
            // 
            // countlab
            // 
            resources.ApplyResources(this.countlab, "countlab");
            this.countlab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.countlab.Name = "countlab";
            // 
            // bruntimetxt
            // 
            this.bruntimetxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.rarwpress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.rarwpress, "rarwpress");
            this.rarwpress.Name = "rarwpress";
            // 
            // nitrocomb
            // 
            this.nitrocomb.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.nitrocomb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nitrocomb.FormattingEnabled = true;
            this.nitrocomb.Items.AddRange(new object[] {
            resources.GetString("nitrocomb.Items"),
            resources.GetString("nitrocomb.Items1")});
            resources.ApplyResources(this.nitrocomb, "nitrocomb");
            this.nitrocomb.Name = "nitrocomb";
            // 
            // rndmoneytb
            // 
            this.rndmoneytb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.rndmoneytb, "rndmoneytb");
            this.rndmoneytb.Name = "rndmoneytb";
            // 
            // rightcomb
            // 
            this.rightcomb.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.rightcomb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.upcomb.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.upcomb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.timerlab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.timerlab.Name = "timerlab";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.timerlab);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // nitrowaittime
            // 
            this.nitrowaittime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.nitrowaittime, "nitrowaittime");
            this.nitrowaittime.Name = "nitrowaittime";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // ecoButton2
            // 
            this.ecoButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ecoButton2.EdgeRound = 15;
            resources.ApplyResources(this.ecoButton2, "ecoButton2");
            this.ecoButton2.Name = "ecoButton2";
            this.ecoButton2.RoundSize1 = 16;
            this.ecoButton2.RoundSize2 = 10;
            this.ecoButton2.RoundX = 10;
            this.ecoButton2.TextOffset = 0;
            this.ecoButton2.UseVisualStyleBackColor = false;
            this.ecoButton2.Click += new System.EventHandler(this.ecoButton2_Click);
            // 
            // nitrowaittog
            // 
            this.nitrowaittog.BackColor = System.Drawing.Color.Gold;
            resources.ApplyResources(this.nitrowaittog, "nitrowaittog");
            this.nitrowaittog.Name = "nitrowaittog";
            this.nitrowaittog.ToggleBoxWidth = 20;
            this.nitrowaittog.UseVisualStyleBackColor = false;
            this.nitrowaittog.CheckedChanged += new System.EventHandler(this.nitrowaittog_CheckedChanged);
            // 
            // rightpresstog
            // 
            this.rightpresstog.BackColor = System.Drawing.Color.Gold;
            resources.ApplyResources(this.rightpresstog, "rightpresstog");
            this.rightpresstog.Name = "rightpresstog";
            this.rightpresstog.ToggleBoxWidth = 20;
            this.rightpresstog.UseVisualStyleBackColor = false;
            this.rightpresstog.CheckedChanged += new System.EventHandler(this.rightpresstog_CheckedChanged);
            // 
            // startwaittog
            // 
            this.startwaittog.BackColor = System.Drawing.Color.Gold;
            resources.ApplyResources(this.startwaittog, "startwaittog");
            this.startwaittog.Name = "startwaittog";
            this.startwaittog.ToggleBoxWidth = 20;
            this.startwaittog.UseVisualStyleBackColor = false;
            this.startwaittog.CheckedChanged += new System.EventHandler(this.startwaittog_CheckedChanged);
            // 
            // ecoButton1
            // 
            this.ecoButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ecoButton1.EdgeRound = 15;
            resources.ApplyResources(this.ecoButton1, "ecoButton1");
            this.ecoButton1.Name = "ecoButton1";
            this.ecoButton1.RoundSize1 = 16;
            this.ecoButton1.RoundSize2 = 10;
            this.ecoButton1.RoundX = 10;
            this.ecoButton1.TextOffset = 0;
            this.ecoButton1.UseVisualStyleBackColor = false;
            this.ecoButton1.Click += new System.EventHandler(this.ecoButton1_Click);
            // 
            // savebtn
            // 
            this.savebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.savebtn.EdgeRound = 15;
            resources.ApplyResources(this.savebtn, "savebtn");
            this.savebtn.Name = "savebtn";
            this.savebtn.RoundSize1 = 10;
            this.savebtn.RoundSize2 = 5;
            this.savebtn.RoundX = 10;
            this.savebtn.TextOffset = 0;
            this.savebtn.UseVisualStyleBackColor = false;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click);
            // 
            // stopbtn
            // 
            this.stopbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.stopbtn.EdgeRound = 15;
            resources.ApplyResources(this.stopbtn, "stopbtn");
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.RoundSize1 = 16;
            this.stopbtn.RoundSize2 = 10;
            this.stopbtn.RoundX = 10;
            this.stopbtn.TextOffset = 0;
            this.stopbtn.UseVisualStyleBackColor = false;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // startbtn
            // 
            this.startbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.startbtn.EdgeRound = 15;
            resources.ApplyResources(this.startbtn, "startbtn");
            this.startbtn.Name = "startbtn";
            this.startbtn.RoundSize1 = 16;
            this.startbtn.RoundSize2 = 10;
            this.startbtn.RoundX = 10;
            this.startbtn.TextOffset = 0;
            this.startbtn.UseVisualStyleBackColor = false;
            this.startbtn.Click += new System.EventHandler(this.startbtn_Click);
            // 
            // Mainfrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.ecoButton2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nitrowaittog);
            this.Controls.Add(this.rightpresstog);
            this.Controls.Add(this.startwaittog);
            this.Controls.Add(this.ecoButton1);
            this.Controls.Add(this.nitrowaittime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bruntimetxt);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.rarwpress);
            this.Controls.Add(this.startbtn);
            this.Controls.Add(this.rndmoneytb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nitrocomb);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rightcomb);
            this.Controls.Add(this.upcomb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loglistbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Mainfrm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mainfrm_FormClosed);
            this.Load += new System.EventHandler(this.Mainfrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox loglistbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label allrevenuslab;
        private System.Windows.Forms.Label countlab;
        private System.Windows.Forms.TextBox rndmoneytb;
        private System.Windows.Forms.TextBox rarwpress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox nitrocomb;
        private System.Windows.Forms.ComboBox rightcomb;
        private System.Windows.Forms.ComboBox upcomb;
        private System.Windows.Forms.Label timerlab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox bruntimetxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private Eco_Skin.EcoButton startbtn;
        private Eco_Skin.EcoButton stopbtn;
        private Eco_Skin.EcoButton savebtn;
        private System.Windows.Forms.TextBox nitrowaittime;
        private System.Windows.Forms.Label label4;
        private Eco_Skin.EcoButton ecoButton1;
        private Eco_Skin.EcoToggleButton startwaittog;
        private Eco_Skin.EcoToggleButton rightpresstog;
        private Eco_Skin.EcoToggleButton nitrowaittog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Eco_Skin.EcoButton ecoButton2;
    }
}

