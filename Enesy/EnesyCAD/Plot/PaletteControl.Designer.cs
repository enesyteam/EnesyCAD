using Enesy.Forms;

namespace Enesy.EnesyCAD.Plot
{
    partial class PaletteControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteControl));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabpConfig = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBlockFileName = new System.Windows.Forms.TextBox();
            this.rdoFileName = new System.Windows.Forms.RadioButton();
            this.txtBlockName = new System.Windows.Forms.TextBox();
            this.rdoPath = new System.Windows.Forms.RadioButton();
            this.rdoName = new System.Windows.Forms.RadioButton();
            this.txtBlockPath = new System.Windows.Forms.RichTextBox();
            this.butSpecify = new System.Windows.Forms.Button();
            this.rdoLine = new System.Windows.Forms.RadioButton();
            this.rdoRectangle = new System.Windows.Forms.RadioButton();
            this.rdoBlock = new System.Windows.Forms.RadioButton();
            this.butEditStyle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.rdoSort1 = new System.Windows.Forms.RadioButton();
            this.txtAutoFit = new Enesy.Forms.TextBoxNumeric();
            this.chkAutoFit = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtScaleB = new Enesy.Forms.TextBoxNumeric();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScaleO = new Enesy.Forms.TextBoxNumeric();
            this.chkFit = new System.Windows.Forms.CheckBox();
            this.txtCenterY = new Enesy.Forms.TextBoxNumeric();
            this.chkCenter = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCenterX = new Enesy.Forms.TextBoxNumeric();
            this.cboStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPaper = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPlotter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabpSheets = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvrSheets = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.butRemove = new System.Windows.Forms.Button();
            this.butBelow = new System.Windows.Forms.Button();
            this.butAbove = new System.Windows.Forms.Button();
            this.butCut = new System.Windows.Forms.Button();
            this.butBottom = new System.Windows.Forms.Button();
            this.butDown = new System.Windows.Forms.Button();
            this.butUp = new System.Windows.Forms.Button();
            this.butTop = new System.Windows.Forms.Button();
            this.butPreview = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.butPublish = new System.Windows.Forms.Button();
            this.butRefresh = new System.Windows.Forms.Button();
            this.butSchedule = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.tabpConfig.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabpSheets.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrSheets)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabpConfig);
            this.tabMain.Controls.Add(this.tabpSheets);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(270, 540);
            this.tabMain.TabIndex = 0;
            // 
            // tabpConfig
            // 
            this.tabpConfig.AutoScroll = true;
            this.tabpConfig.BackColor = System.Drawing.Color.White;
            this.tabpConfig.Controls.Add(this.groupBox3);
            this.tabpConfig.Controls.Add(this.butEditStyle);
            this.tabpConfig.Controls.Add(this.groupBox1);
            this.tabpConfig.Controls.Add(this.cboStyle);
            this.tabpConfig.Controls.Add(this.label3);
            this.tabpConfig.Controls.Add(this.cboPaper);
            this.tabpConfig.Controls.Add(this.label2);
            this.tabpConfig.Controls.Add(this.cboPlotter);
            this.tabpConfig.Controls.Add(this.label1);
            this.tabpConfig.Location = new System.Drawing.Point(4, 23);
            this.tabpConfig.Name = "tabpConfig";
            this.tabpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabpConfig.Size = new System.Drawing.Size(262, 513);
            this.tabpConfig.TabIndex = 1;
            this.tabpConfig.Text = "Configuration";
            this.tabpConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.panel5);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.butSpecify);
            this.groupBox3.Controls.Add(this.rdoLine);
            this.groupBox3.Controls.Add(this.rdoRectangle);
            this.groupBox3.Controls.Add(this.rdoBlock);
            this.groupBox3.Location = new System.Drawing.Point(3, 265);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 252);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Indentify";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.comboBox1);
            this.panel5.Location = new System.Drawing.Point(93, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(131, 25);
            this.panel5.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Entire layout",
            "User selection"});
            this.comboBox1.Location = new System.Drawing.Point(0, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImage = global::Enesy.Drawing.Icons._1453733694_Select;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(227, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 27);
            this.button2.TabIndex = 6;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.txtBlockFileName);
            this.groupBox4.Controls.Add(this.rdoFileName);
            this.groupBox4.Controls.Add(this.txtBlockName);
            this.groupBox4.Controls.Add(this.rdoPath);
            this.groupBox4.Controls.Add(this.rdoName);
            this.groupBox4.Controls.Add(this.txtBlockPath);
            this.groupBox4.Location = new System.Drawing.Point(0, 76);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(257, 171);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // txtBlockFileName
            // 
            this.txtBlockFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBlockFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBlockFileName.Location = new System.Drawing.Point(3, 79);
            this.txtBlockFileName.Name = "txtBlockFileName";
            this.txtBlockFileName.ReadOnly = true;
            this.txtBlockFileName.Size = new System.Drawing.Size(254, 13);
            this.txtBlockFileName.TabIndex = 5;
            // 
            // rdoFileName
            // 
            this.rdoFileName.AutoSize = true;
            this.rdoFileName.Location = new System.Drawing.Point(6, 55);
            this.rdoFileName.Name = "rdoFileName";
            this.rdoFileName.Size = new System.Drawing.Size(71, 18);
            this.rdoFileName.TabIndex = 4;
            this.rdoFileName.Text = "File Name";
            this.rdoFileName.UseVisualStyleBackColor = true;
            // 
            // txtBlockName
            // 
            this.txtBlockName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBlockName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBlockName.Location = new System.Drawing.Point(3, 36);
            this.txtBlockName.Name = "txtBlockName";
            this.txtBlockName.ReadOnly = true;
            this.txtBlockName.Size = new System.Drawing.Size(254, 13);
            this.txtBlockName.TabIndex = 2;
            // 
            // rdoPath
            // 
            this.rdoPath.AutoSize = true;
            this.rdoPath.Location = new System.Drawing.Point(6, 98);
            this.rdoPath.Name = "rdoPath";
            this.rdoPath.Size = new System.Drawing.Size(46, 18);
            this.rdoPath.TabIndex = 1;
            this.rdoPath.Text = "Path";
            this.rdoPath.UseVisualStyleBackColor = true;
            // 
            // rdoName
            // 
            this.rdoName.AutoSize = true;
            this.rdoName.Checked = true;
            this.rdoName.Location = new System.Drawing.Point(6, 12);
            this.rdoName.Name = "rdoName";
            this.rdoName.Size = new System.Drawing.Size(52, 18);
            this.rdoName.TabIndex = 0;
            this.rdoName.TabStop = true;
            this.rdoName.Text = "Name";
            this.rdoName.UseVisualStyleBackColor = true;
            // 
            // txtBlockPath
            // 
            this.txtBlockPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBlockPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBlockPath.Location = new System.Drawing.Point(3, 122);
            this.txtBlockPath.Name = "txtBlockPath";
            this.txtBlockPath.ReadOnly = true;
            this.txtBlockPath.Size = new System.Drawing.Size(251, 47);
            this.txtBlockPath.TabIndex = 3;
            this.txtBlockPath.Text = "";
            // 
            // butSpecify
            // 
            this.butSpecify.Location = new System.Drawing.Point(6, 43);
            this.butSpecify.Name = "butSpecify";
            this.butSpecify.Size = new System.Drawing.Size(80, 27);
            this.butSpecify.TabIndex = 3;
            this.butSpecify.Text = "> Specifing...";
            this.butSpecify.UseVisualStyleBackColor = true;
            this.butSpecify.Click += new System.EventHandler(this.butSpecify_Click);
            // 
            // rdoLine
            // 
            this.rdoLine.AutoSize = true;
            this.rdoLine.Location = new System.Drawing.Point(185, 19);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new System.Drawing.Size(59, 18);
            this.rdoLine.TabIndex = 2;
            this.rdoLine.Text = "Line(s)";
            this.rdoLine.UseVisualStyleBackColor = true;
            // 
            // rdoRectangle
            // 
            this.rdoRectangle.AutoSize = true;
            this.rdoRectangle.Location = new System.Drawing.Point(91, 19);
            this.rdoRectangle.Name = "rdoRectangle";
            this.rdoRectangle.Size = new System.Drawing.Size(87, 18);
            this.rdoRectangle.TabIndex = 1;
            this.rdoRectangle.Text = "Rectangle(s)";
            this.rdoRectangle.UseVisualStyleBackColor = true;
            // 
            // rdoBlock
            // 
            this.rdoBlock.AutoSize = true;
            this.rdoBlock.Checked = true;
            this.rdoBlock.Location = new System.Drawing.Point(6, 19);
            this.rdoBlock.Name = "rdoBlock";
            this.rdoBlock.Size = new System.Drawing.Size(75, 18);
            this.rdoBlock.TabIndex = 0;
            this.rdoBlock.TabStop = true;
            this.rdoBlock.Text = "Block/Xref";
            this.rdoBlock.UseVisualStyleBackColor = true;
            // 
            // butEditStyle
            // 
            this.butEditStyle.Location = new System.Drawing.Point(43, 60);
            this.butEditStyle.Name = "butEditStyle";
            this.butEditStyle.Size = new System.Drawing.Size(25, 22);
            this.butEditStyle.TabIndex = 8;
            this.butEditStyle.Text = "...";
            this.butEditStyle.UseVisualStyleBackColor = true;
            this.butEditStyle.Click += new System.EventHandler(this.butEditStyle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rdoSort1);
            this.groupBox1.Controls.Add(this.txtAutoFit);
            this.groupBox1.Controls.Add(this.chkAutoFit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtScaleB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtScaleO);
            this.groupBox1.Controls.Add(this.chkFit);
            this.groupBox1.Controls.Add(this.txtCenterY);
            this.groupBox1.Controls.Add(this.chkCenter);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCenterX);
            this.groupBox1.Location = new System.Drawing.Point(3, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 171);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sheets Arrangement";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 14);
            this.label10.TabIndex = 16;
            this.label10.Text = "Sort Method:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(88, 142);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(140, 18);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Up > Down, Left > Right";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 14);
            this.label9.TabIndex = 15;
            this.label9.Text = "%";
            // 
            // rdoSort1
            // 
            this.rdoSort1.AutoSize = true;
            this.rdoSort1.Checked = true;
            this.rdoSort1.Location = new System.Drawing.Point(88, 118);
            this.rdoSort1.Name = "rdoSort1";
            this.rdoSort1.Size = new System.Drawing.Size(140, 18);
            this.rdoSort1.TabIndex = 0;
            this.rdoSort1.TabStop = true;
            this.rdoSort1.Text = "Left > Right, Up > Down";
            this.rdoSort1.UseVisualStyleBackColor = true;
            // 
            // txtAutoFit
            // 
            this.txtAutoFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutoFit.Location = new System.Drawing.Point(185, 41);
            this.txtAutoFit.Name = "txtAutoFit";
            this.txtAutoFit.Size = new System.Drawing.Size(39, 20);
            this.txtAutoFit.TabIndex = 14;
            this.txtAutoFit.Text = "105";
            // 
            // chkAutoFit
            // 
            this.chkAutoFit.AutoSize = true;
            this.chkAutoFit.Checked = true;
            this.chkAutoFit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoFit.Location = new System.Drawing.Point(6, 43);
            this.chkAutoFit.Name = "chkAutoFit";
            this.chkAutoFit.Size = new System.Drawing.Size(176, 18);
            this.chkAutoFit.TabIndex = 13;
            this.chkAutoFit.Text = "Auto Fit if actual dim diff. paper";
            this.chkAutoFit.UseVisualStyleBackColor = true;
            this.chkAutoFit.CheckedChanged += new System.EventHandler(this.chkAutoFit_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "cm";
            // 
            // txtScaleB
            // 
            this.txtScaleB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScaleB.Location = new System.Drawing.Point(173, 16);
            this.txtScaleB.Name = "txtScaleB";
            this.txtScaleB.Size = new System.Drawing.Size(51, 20);
            this.txtScaleB.TabIndex = 6;
            this.txtScaleB.Text = "1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "/";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(230, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 11;
            this.label7.Text = "cm";
            // 
            // txtScaleO
            // 
            this.txtScaleO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScaleO.Location = new System.Drawing.Point(103, 16);
            this.txtScaleO.Name = "txtScaleO";
            this.txtScaleO.Size = new System.Drawing.Size(51, 20);
            this.txtScaleO.TabIndex = 1;
            this.txtScaleO.Text = "1";
            // 
            // chkFit
            // 
            this.chkFit.AutoSize = true;
            this.chkFit.Location = new System.Drawing.Point(6, 19);
            this.chkFit.Name = "chkFit";
            this.chkFit.Size = new System.Drawing.Size(80, 18);
            this.chkFit.TabIndex = 0;
            this.chkFit.Text = "Fit to Paper";
            this.chkFit.UseVisualStyleBackColor = true;
            this.chkFit.CheckedChanged += new System.EventHandler(this.chkFit_CheckedChanged);
            // 
            // txtCenterY
            // 
            this.txtCenterY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCenterY.Enabled = false;
            this.txtCenterY.Location = new System.Drawing.Point(137, 91);
            this.txtCenterY.Name = "txtCenterY";
            this.txtCenterY.Size = new System.Drawing.Size(87, 20);
            this.txtCenterY.TabIndex = 10;
            this.txtCenterY.Text = "0.000";
            // 
            // chkCenter
            // 
            this.chkCenter.AutoSize = true;
            this.chkCenter.Checked = true;
            this.chkCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCenter.Location = new System.Drawing.Point(6, 67);
            this.chkCenter.Name = "chkCenter";
            this.chkCenter.Size = new System.Drawing.Size(96, 18);
            this.chkCenter.TabIndex = 0;
            this.chkCenter.Text = "Center the Plot";
            this.chkCenter.UseVisualStyleBackColor = true;
            this.chkCenter.CheckedChanged += new System.EventHandler(this.chkCenter_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(114, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "X:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "Y:";
            // 
            // txtCenterX
            // 
            this.txtCenterX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCenterX.Enabled = false;
            this.txtCenterX.Location = new System.Drawing.Point(137, 65);
            this.txtCenterX.Name = "txtCenterX";
            this.txtCenterX.Size = new System.Drawing.Size(87, 20);
            this.txtCenterX.TabIndex = 8;
            this.txtCenterX.Text = "0.000";
            // 
            // cboStyle
            // 
            this.cboStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyle.FormattingEnabled = true;
            this.cboStyle.Location = new System.Drawing.Point(74, 60);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new System.Drawing.Size(187, 22);
            this.cboStyle.TabIndex = 5;
            this.cboStyle.DropDownClosed += new System.EventHandler(this.cboStyle_DropDownClosed);
            this.cboStyle.DropDown += new System.EventHandler(this.cboStyle_DropDown);
            this.cboStyle.Click += new System.EventHandler(this.cboStyle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Style";
            // 
            // cboPaper
            // 
            this.cboPaper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaper.FormattingEnabled = true;
            this.cboPaper.Location = new System.Drawing.Point(43, 32);
            this.cboPaper.Name = "cboPaper";
            this.cboPaper.Size = new System.Drawing.Size(218, 22);
            this.cboPaper.TabIndex = 3;
            this.cboPaper.DropDownClosed += new System.EventHandler(this.cboPaper_DropDownClosed);
            this.cboPaper.DropDown += new System.EventHandler(this.cboPaper_DropDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Paper";
            // 
            // cboPlotter
            // 
            this.cboPlotter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPlotter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlotter.FormattingEnabled = true;
            this.cboPlotter.Location = new System.Drawing.Point(43, 4);
            this.cboPlotter.Name = "cboPlotter";
            this.cboPlotter.Size = new System.Drawing.Size(218, 22);
            this.cboPlotter.TabIndex = 1;
            this.cboPlotter.SelectionChangeCommitted += new System.EventHandler(this.cboPlotter_SelectionChangeCommitted);
            this.cboPlotter.DropDownClosed += new System.EventHandler(this.cboPlotter_DropDownClosed);
            this.cboPlotter.DropDown += new System.EventHandler(this.cboPlotter_DropDown);
            this.cboPlotter.Click += new System.EventHandler(this.cboPlotter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plotter";
            // 
            // tabpSheets
            // 
            this.tabpSheets.Controls.Add(this.panel1);
            this.tabpSheets.Controls.Add(this.panel3);
            this.tabpSheets.Controls.Add(this.panel2);
            this.tabpSheets.Location = new System.Drawing.Point(4, 23);
            this.tabpSheets.Name = "tabpSheets";
            this.tabpSheets.Size = new System.Drawing.Size(262, 513);
            this.tabpSheets.TabIndex = 2;
            this.tabpSheets.Text = "Sheet(s)";
            this.tabpSheets.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.dgvrSheets);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel1.Size = new System.Drawing.Size(262, 465);
            this.panel1.TabIndex = 4;
            // 
            // dgvrSheets
            // 
            this.dgvrSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvrSheets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvrSheets.Location = new System.Drawing.Point(0, 3);
            this.dgvrSheets.Name = "dgvrSheets";
            this.dgvrSheets.Size = new System.Drawing.Size(262, 459);
            this.dgvrSheets.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.butRemove);
            this.panel3.Controls.Add(this.butBelow);
            this.panel3.Controls.Add(this.butAbove);
            this.panel3.Controls.Add(this.butCut);
            this.panel3.Controls.Add(this.butBottom);
            this.panel3.Controls.Add(this.butDown);
            this.panel3.Controls.Add(this.butUp);
            this.panel3.Controls.Add(this.butTop);
            this.panel3.Controls.Add(this.butPreview);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 489);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(262, 24);
            this.panel3.TabIndex = 3;
            // 
            // butRemove
            // 
            this.butRemove.AutoSize = true;
            this.butRemove.BackgroundImage = global::Enesy.Drawing.Icons.Delete;
            this.butRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.butRemove.Location = new System.Drawing.Point(168, 0);
            this.butRemove.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butRemove.Name = "butRemove";
            this.butRemove.Padding = new System.Windows.Forms.Padding(2);
            this.butRemove.Size = new System.Drawing.Size(24, 24);
            this.butRemove.TabIndex = 14;
            this.butRemove.UseVisualStyleBackColor = true;
            // 
            // butBelow
            // 
            this.butBelow.AutoSize = true;
            this.butBelow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butBelow.BackgroundImage")));
            this.butBelow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butBelow.Dock = System.Windows.Forms.DockStyle.Left;
            this.butBelow.Location = new System.Drawing.Point(144, 0);
            this.butBelow.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butBelow.Name = "butBelow";
            this.butBelow.Size = new System.Drawing.Size(24, 24);
            this.butBelow.TabIndex = 13;
            this.butBelow.UseVisualStyleBackColor = true;
            // 
            // butAbove
            // 
            this.butAbove.AutoSize = true;
            this.butAbove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butAbove.BackgroundImage")));
            this.butAbove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butAbove.Dock = System.Windows.Forms.DockStyle.Left;
            this.butAbove.Location = new System.Drawing.Point(120, 0);
            this.butAbove.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butAbove.Name = "butAbove";
            this.butAbove.Size = new System.Drawing.Size(24, 24);
            this.butAbove.TabIndex = 12;
            this.butAbove.UseVisualStyleBackColor = true;
            // 
            // butCut
            // 
            this.butCut.AutoSize = true;
            this.butCut.BackgroundImage = global::Enesy.Drawing.Icons._1453725839_cut;
            this.butCut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butCut.Dock = System.Windows.Forms.DockStyle.Left;
            this.butCut.Location = new System.Drawing.Point(96, 0);
            this.butCut.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butCut.Name = "butCut";
            this.butCut.Size = new System.Drawing.Size(24, 24);
            this.butCut.TabIndex = 11;
            this.butCut.UseVisualStyleBackColor = true;
            // 
            // butBottom
            // 
            this.butBottom.AutoSize = true;
            this.butBottom.BackgroundImage = global::Enesy.Drawing.Icons.bottom;
            this.butBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butBottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.butBottom.Location = new System.Drawing.Point(72, 0);
            this.butBottom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butBottom.Name = "butBottom";
            this.butBottom.Size = new System.Drawing.Size(24, 24);
            this.butBottom.TabIndex = 9;
            this.butBottom.UseVisualStyleBackColor = true;
            // 
            // butDown
            // 
            this.butDown.AutoSize = true;
            this.butDown.BackgroundImage = global::Enesy.Drawing.Icons.down;
            this.butDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.butDown.Location = new System.Drawing.Point(48, 0);
            this.butDown.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butDown.Name = "butDown";
            this.butDown.Size = new System.Drawing.Size(24, 24);
            this.butDown.TabIndex = 8;
            this.butDown.UseVisualStyleBackColor = true;
            // 
            // butUp
            // 
            this.butUp.AutoSize = true;
            this.butUp.BackgroundImage = global::Enesy.Drawing.Icons.up;
            this.butUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.butUp.Location = new System.Drawing.Point(24, 0);
            this.butUp.Margin = new System.Windows.Forms.Padding(0);
            this.butUp.Name = "butUp";
            this.butUp.Size = new System.Drawing.Size(24, 24);
            this.butUp.TabIndex = 7;
            this.butUp.UseVisualStyleBackColor = true;
            // 
            // butTop
            // 
            this.butTop.AutoSize = true;
            this.butTop.BackgroundImage = global::Enesy.Drawing.Icons._1453709979_old_go_top;
            this.butTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.butTop.Location = new System.Drawing.Point(0, 0);
            this.butTop.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.butTop.Name = "butTop";
            this.butTop.Size = new System.Drawing.Size(24, 24);
            this.butTop.TabIndex = 4;
            this.butTop.UseVisualStyleBackColor = true;
            // 
            // butPreview
            // 
            this.butPreview.AutoSize = true;
            this.butPreview.BackgroundImage = global::Enesy.Drawing.Icons.preview;
            this.butPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.butPreview.Location = new System.Drawing.Point(238, 0);
            this.butPreview.Margin = new System.Windows.Forms.Padding(0);
            this.butPreview.Name = "butPreview";
            this.butPreview.Size = new System.Drawing.Size(24, 24);
            this.butPreview.TabIndex = 0;
            this.butPreview.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 24);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.butPublish);
            this.panel4.Controls.Add(this.butRefresh);
            this.panel4.Controls.Add(this.butSchedule);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(124, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(138, 24);
            this.panel4.TabIndex = 4;
            // 
            // butPublish
            // 
            this.butPublish.BackgroundImage = global::Enesy.Drawing.Icons._1453707705_printer;
            this.butPublish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butPublish.Location = new System.Drawing.Point(114, 0);
            this.butPublish.Name = "butPublish";
            this.butPublish.Size = new System.Drawing.Size(24, 24);
            this.butPublish.TabIndex = 2;
            this.butPublish.UseVisualStyleBackColor = true;
            // 
            // butRefresh
            // 
            this.butRefresh.BackgroundImage = global::Enesy.Drawing.Icons._1453707592_arrow_refresh;
            this.butRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butRefresh.Location = new System.Drawing.Point(56, 0);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(24, 24);
            this.butRefresh.TabIndex = 0;
            this.butRefresh.UseVisualStyleBackColor = true;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butSchedule
            // 
            this.butSchedule.BackgroundImage = global::Enesy.Drawing.Icons._1453708145_gtk_sort_descending;
            this.butSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSchedule.Location = new System.Drawing.Point(85, 0);
            this.butSchedule.Name = "butSchedule";
            this.butSchedule.Size = new System.Drawing.Size(24, 24);
            this.butSchedule.TabIndex = 1;
            this.butSchedule.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 14);
            this.label11.TabIndex = 3;
            this.label11.Text = "Sheets to publish";
            // 
            // PaletteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Name = "PaletteControl";
            this.Size = new System.Drawing.Size(270, 540);
            this.tabMain.ResumeLayout(false);
            this.tabpConfig.ResumeLayout(false);
            this.tabpConfig.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabpSheets.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvrSheets)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabpConfig;
        private System.Windows.Forms.ComboBox cboPlotter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPaper;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFit;
        private System.Windows.Forms.Label label4;
        private Enesy.Forms.TextBoxNumeric txtScaleO;
        private TextBoxNumeric txtScaleB;
        private System.Windows.Forms.Button butEditStyle;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button butSpecify;
        private System.Windows.Forms.RadioButton rdoLine;
        private System.Windows.Forms.RadioButton rdoRectangle;
        private System.Windows.Forms.RadioButton rdoBlock;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtBlockFileName;
        private System.Windows.Forms.RadioButton rdoFileName;
        private System.Windows.Forms.RichTextBox txtBlockPath;
        private System.Windows.Forms.TextBox txtBlockName;
        private System.Windows.Forms.RadioButton rdoPath;
        private System.Windows.Forms.RadioButton rdoName;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rdoSort1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private TextBoxNumeric txtCenterY;
        private System.Windows.Forms.CheckBox chkCenter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private TextBoxNumeric txtCenterX;
        private System.Windows.Forms.CheckBox chkAutoFit;
        private System.Windows.Forms.Label label9;
        private TextBoxNumeric txtAutoFit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabpSheets;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butPublish;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button butPreview;
        private System.Windows.Forms.Button butTop;
        private System.Windows.Forms.Button butBottom;
        private System.Windows.Forms.Button butDown;
        private System.Windows.Forms.Button butUp;
        private System.Windows.Forms.Button butCut;
        private System.Windows.Forms.Button butAbove;
        private System.Windows.Forms.Button butBelow;
        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butSchedule;
        private System.Windows.Forms.Button butRefresh;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvrSheets;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
