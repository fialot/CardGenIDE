namespace CardGenIDE
{
    partial class frmMainIDE
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainIDE));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtDataFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chbLandscape = new System.Windows.Forms.CheckBox();
            this.cbPaper = new System.Windows.Forms.ComboBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chbGenTextWord = new System.Windows.Forms.CheckBox();
            this.chbGenerateBack = new System.Windows.Forms.CheckBox();
            this.cbGenType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSetCardSize = new System.Windows.Forms.Button();
            this.txtDPI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardHeight = new System.Windows.Forms.TextBox();
            this.txtCardWidth = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblCardWidth = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbObjects = new System.Windows.Forms.ComboBox();
            this.propGrid = new System.Windows.Forms.PropertyGrid();
            this.btnForeDown = new System.Windows.Forms.Button();
            this.btnForeUp = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCopyB = new System.Windows.Forms.Button();
            this.btnDelB = new System.Windows.Forms.Button();
            this.btnAddB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbObjectsB = new System.Windows.Forms.ComboBox();
            this.propGridB = new System.Windows.Forms.PropertyGrid();
            this.btnBackDown = new System.Windows.Forms.Button();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuMainFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblTopCard = new System.Windows.Forms.Label();
            this.cardImg = new System.Windows.Forms.PictureBox();
            this.lblBack = new System.Windows.Forms.Label();
            this.cardImgBack = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardImgBack)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(320, 633);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Card deck";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(5, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(308, 599);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(300, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Deck";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtDataFile);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(4, 169);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(287, 94);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data";
            // 
            // txtDataFile
            // 
            this.txtDataFile.Location = new System.Drawing.Point(83, 36);
            this.txtDataFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDataFile.Name = "txtDataFile";
            this.txtDataFile.Size = new System.Drawing.Size(196, 22);
            this.txtDataFile.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Data file:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chbLandscape);
            this.groupBox4.Controls.Add(this.cbPaper);
            this.groupBox4.Controls.Add(this.btnGenerate);
            this.groupBox4.Controls.Add(this.chbGenTextWord);
            this.groupBox4.Controls.Add(this.chbGenerateBack);
            this.groupBox4.Controls.Add(this.cbGenType);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(4, 270);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(287, 184);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Generate";
            // 
            // chbLandscape
            // 
            this.chbLandscape.AutoSize = true;
            this.chbLandscape.Location = new System.Drawing.Point(158, 112);
            this.chbLandscape.Name = "chbLandscape";
            this.chbLandscape.Size = new System.Drawing.Size(100, 21);
            this.chbLandscape.TabIndex = 21;
            this.chbLandscape.Text = "Landscape";
            this.chbLandscape.UseVisualStyleBackColor = true;
            this.chbLandscape.CheckedChanged += new System.EventHandler(this.chbLandscape_CheckedChanged);
            // 
            // cbPaper
            // 
            this.cbPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaper.FormattingEnabled = true;
            this.cbPaper.Items.AddRange(new object[] {
            "A3",
            "A4",
            "A5",
            "A6"});
            this.cbPaper.Location = new System.Drawing.Point(24, 112);
            this.cbPaper.Name = "cbPaper";
            this.cbPaper.Size = new System.Drawing.Size(121, 24);
            this.cbPaper.TabIndex = 20;
            this.cbPaper.SelectedIndexChanged += new System.EventHandler(this.cbPaper_SelectedIndexChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(24, 143);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(113, 28);
            this.btnGenerate.TabIndex = 22;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // chbGenTextWord
            // 
            this.chbGenTextWord.AutoSize = true;
            this.chbGenTextWord.Location = new System.Drawing.Point(24, 84);
            this.chbGenTextWord.Margin = new System.Windows.Forms.Padding(4);
            this.chbGenTextWord.Name = "chbGenTextWord";
            this.chbGenTextWord.Size = new System.Drawing.Size(166, 21);
            this.chbGenTextWord.TabIndex = 19;
            this.chbGenTextWord.Text = "Generate text in Docx";
            this.chbGenTextWord.UseVisualStyleBackColor = true;
            this.chbGenTextWord.CheckedChanged += new System.EventHandler(this.chbGenTextWord_CheckedChanged);
            // 
            // chbGenerateBack
            // 
            this.chbGenerateBack.AutoSize = true;
            this.chbGenerateBack.Location = new System.Drawing.Point(24, 23);
            this.chbGenerateBack.Margin = new System.Windows.Forms.Padding(4);
            this.chbGenerateBack.Name = "chbGenerateBack";
            this.chbGenerateBack.Size = new System.Drawing.Size(155, 21);
            this.chbGenerateBack.TabIndex = 16;
            this.chbGenerateBack.Text = "Generate Back side";
            this.chbGenerateBack.UseVisualStyleBackColor = true;
            this.chbGenerateBack.CheckedChanged += new System.EventHandler(this.chbGenerateBack_CheckedChanged);
            // 
            // cbGenType
            // 
            this.cbGenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGenType.FormattingEnabled = true;
            this.cbGenType.Items.AddRange(new object[] {
            "Docx",
            "Png"});
            this.cbGenType.Location = new System.Drawing.Point(96, 52);
            this.cbGenType.Margin = new System.Windows.Forms.Padding(4);
            this.cbGenType.Name = "cbGenType";
            this.cbGenType.Size = new System.Drawing.Size(160, 24);
            this.cbGenType.TabIndex = 18;
            this.cbGenType.SelectedIndexChanged += new System.EventHandler(this.cbGenType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Format:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSetCardSize);
            this.groupBox3.Controls.Add(this.txtDPI);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtCardHeight);
            this.groupBox3.Controls.Add(this.txtCardWidth);
            this.groupBox3.Controls.Add(this.lblHeight);
            this.groupBox3.Controls.Add(this.lblCardWidth);
            this.groupBox3.Location = new System.Drawing.Point(4, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(287, 155);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Card";
            // 
            // btnSetCardSize
            // 
            this.btnSetCardSize.Location = new System.Drawing.Point(205, 91);
            this.btnSetCardSize.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetCardSize.Name = "btnSetCardSize";
            this.btnSetCardSize.Size = new System.Drawing.Size(61, 28);
            this.btnSetCardSize.TabIndex = 11;
            this.btnSetCardSize.Text = "Set";
            this.btnSetCardSize.UseVisualStyleBackColor = true;
            this.btnSetCardSize.Click += new System.EventHandler(this.btnSetCardSize_Click);
            // 
            // txtDPI
            // 
            this.txtDPI.Location = new System.Drawing.Point(84, 94);
            this.txtDPI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDPI.Name = "txtDPI";
            this.txtDPI.Size = new System.Drawing.Size(100, 22);
            this.txtDPI.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "DPI:";
            // 
            // txtCardHeight
            // 
            this.txtCardHeight.Location = new System.Drawing.Point(84, 64);
            this.txtCardHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCardHeight.Name = "txtCardHeight";
            this.txtCardHeight.Size = new System.Drawing.Size(100, 22);
            this.txtCardHeight.TabIndex = 8;
            // 
            // txtCardWidth
            // 
            this.txtCardWidth.Location = new System.Drawing.Point(84, 37);
            this.txtCardWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCardWidth.Name = "txtCardWidth";
            this.txtCardWidth.Size = new System.Drawing.Size(100, 22);
            this.txtCardWidth.TabIndex = 6;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(12, 68);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(53, 17);
            this.lblHeight.TabIndex = 7;
            this.lblHeight.Text = "Height:";
            // 
            // lblCardWidth
            // 
            this.lblCardWidth.AutoSize = true;
            this.lblCardWidth.Location = new System.Drawing.Point(12, 39);
            this.lblCardWidth.Name = "lblCardWidth";
            this.lblCardWidth.Size = new System.Drawing.Size(48, 17);
            this.lblCardWidth.TabIndex = 5;
            this.lblCardWidth.Text = "Width:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCopy);
            this.tabPage2.Controls.Add(this.btnDel);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cbObjects);
            this.tabPage2.Controls.Add(this.propGrid);
            this.tabPage2.Controls.Add(this.btnForeDown);
            this.tabPage2.Controls.Add(this.btnForeUp);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(300, 570);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fore Side";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::CardGenIDE.Properties.Resources.copy1b;
            this.btnCopy.Location = new System.Drawing.Point(102, 4);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(90, 43);
            this.btnCopy.TabIndex = 26;
            this.btnCopy.Text = "Copy";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = global::CardGenIDE.Properties.Resources.redcross1;
            this.btnDel.Location = new System.Drawing.Point(198, 4);
            this.btnDel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 43);
            this.btnDel.TabIndex = 27;
            this.btnDel.Text = "Del";
            this.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::CardGenIDE.Properties.Resources.document8b;
            this.btnAdd.Location = new System.Drawing.Point(6, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 43);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 28;
            this.label1.Text = "Select item:";
            // 
            // cbObjects
            // 
            this.cbObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjects.FormattingEnabled = true;
            this.cbObjects.Location = new System.Drawing.Point(89, 51);
            this.cbObjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbObjects.Name = "cbObjects";
            this.cbObjects.Size = new System.Drawing.Size(121, 24);
            this.cbObjects.TabIndex = 29;
            this.cbObjects.SelectedIndexChanged += new System.EventHandler(this.cbObjects_SelectedIndexChanged);
            // 
            // propGrid
            // 
            this.propGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propGrid.Location = new System.Drawing.Point(3, 80);
            this.propGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propGrid.Name = "propGrid";
            this.propGrid.Size = new System.Drawing.Size(294, 486);
            this.propGrid.TabIndex = 32;
            this.propGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGrid_PropertyValueChanged);
            // 
            // btnForeDown
            // 
            this.btnForeDown.Image = global::CardGenIDE.Properties.Resources.bluesharp1;
            this.btnForeDown.Location = new System.Drawing.Point(254, 51);
            this.btnForeDown.Name = "btnForeDown";
            this.btnForeDown.Size = new System.Drawing.Size(35, 24);
            this.btnForeDown.TabIndex = 31;
            this.btnForeDown.UseVisualStyleBackColor = true;
            this.btnForeDown.Click += new System.EventHandler(this.btnForeDown_Click);
            // 
            // btnForeUp
            // 
            this.btnForeUp.Image = global::CardGenIDE.Properties.Resources.bluesharp2;
            this.btnForeUp.Location = new System.Drawing.Point(216, 51);
            this.btnForeUp.Name = "btnForeUp";
            this.btnForeUp.Size = new System.Drawing.Size(35, 24);
            this.btnForeUp.TabIndex = 30;
            this.btnForeUp.UseVisualStyleBackColor = true;
            this.btnForeUp.Click += new System.EventHandler(this.btnForeUp_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnCopyB);
            this.tabPage3.Controls.Add(this.btnDelB);
            this.tabPage3.Controls.Add(this.btnAddB);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cbObjectsB);
            this.tabPage3.Controls.Add(this.propGridB);
            this.tabPage3.Controls.Add(this.btnBackDown);
            this.tabPage3.Controls.Add(this.btnBackUp);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(300, 570);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Back side";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnCopyB
            // 
            this.btnCopyB.Image = global::CardGenIDE.Properties.Resources.copy1b;
            this.btnCopyB.Location = new System.Drawing.Point(102, 4);
            this.btnCopyB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyB.Name = "btnCopyB";
            this.btnCopyB.Size = new System.Drawing.Size(90, 43);
            this.btnCopyB.TabIndex = 34;
            this.btnCopyB.Text = "Copy";
            this.btnCopyB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopyB.UseVisualStyleBackColor = true;
            this.btnCopyB.Click += new System.EventHandler(this.btnCopyB_Click);
            // 
            // btnDelB
            // 
            this.btnDelB.Image = global::CardGenIDE.Properties.Resources.redcross1;
            this.btnDelB.Location = new System.Drawing.Point(198, 4);
            this.btnDelB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelB.Name = "btnDelB";
            this.btnDelB.Size = new System.Drawing.Size(90, 43);
            this.btnDelB.TabIndex = 35;
            this.btnDelB.Text = "Del";
            this.btnDelB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelB.UseVisualStyleBackColor = true;
            this.btnDelB.Click += new System.EventHandler(this.btnDelB_Click);
            // 
            // btnAddB
            // 
            this.btnAddB.Image = global::CardGenIDE.Properties.Resources.document8b;
            this.btnAddB.Location = new System.Drawing.Point(6, 4);
            this.btnAddB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddB.Name = "btnAddB";
            this.btnAddB.Size = new System.Drawing.Size(90, 43);
            this.btnAddB.TabIndex = 33;
            this.btnAddB.Text = "Add";
            this.btnAddB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddB.UseVisualStyleBackColor = true;
            this.btnAddB.Click += new System.EventHandler(this.btnAddB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "Select item:";
            // 
            // cbObjectsB
            // 
            this.cbObjectsB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjectsB.FormattingEnabled = true;
            this.cbObjectsB.Location = new System.Drawing.Point(89, 51);
            this.cbObjectsB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbObjectsB.Name = "cbObjectsB";
            this.cbObjectsB.Size = new System.Drawing.Size(121, 24);
            this.cbObjectsB.TabIndex = 37;
            this.cbObjectsB.SelectedIndexChanged += new System.EventHandler(this.cbObjectsB_SelectedIndexChanged);
            // 
            // propGridB
            // 
            this.propGridB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propGridB.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propGridB.Location = new System.Drawing.Point(3, 80);
            this.propGridB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propGridB.Name = "propGridB";
            this.propGridB.Size = new System.Drawing.Size(294, 486);
            this.propGridB.TabIndex = 40;
            this.propGridB.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridB_PropertyValueChanged);
            // 
            // btnBackDown
            // 
            this.btnBackDown.Image = global::CardGenIDE.Properties.Resources.bluesharp1;
            this.btnBackDown.Location = new System.Drawing.Point(254, 51);
            this.btnBackDown.Name = "btnBackDown";
            this.btnBackDown.Size = new System.Drawing.Size(35, 24);
            this.btnBackDown.TabIndex = 39;
            this.btnBackDown.UseVisualStyleBackColor = true;
            this.btnBackDown.Click += new System.EventHandler(this.btnBackDown_Click);
            // 
            // btnBackUp
            // 
            this.btnBackUp.Image = global::CardGenIDE.Properties.Resources.bluesharp2;
            this.btnBackUp.Location = new System.Drawing.Point(216, 51);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(35, 24);
            this.btnBackUp.TabIndex = 38;
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFile,
            this.mnuDeck,
            this.mnuMainHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1103, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuMainFile
            // 
            this.mnuMainFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuSave,
            this.mnuSaveAs,
            this.mnuClose,
            this.toolStripMenuItem1,
            this.mnuExit});
            this.mnuMainFile.Image = global::CardGenIDE.Properties.Resources.floatynote1b;
            this.mnuMainFile.Name = "mnuMainFile";
            this.mnuMainFile.Size = new System.Drawing.Size(64, 24);
            this.mnuMainFile.Text = "File";
            // 
            // mnuNew
            // 
            this.mnuNew.Image = global::CardGenIDE.Properties.Resources.floppynote1b;
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(144, 26);
            this.mnuNew.Text = "New";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = global::CardGenIDE.Properties.Resources.folder5b;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(144, 26);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = global::CardGenIDE.Properties.Resources.disktick1b;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(144, 26);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Image = global::CardGenIDE.Properties.Resources.disk2b;
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(144, 26);
            this.mnuSaveAs.Text = "Save As...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // mnuClose
            // 
            this.mnuClose.Image = global::CardGenIDE.Properties.Resources.crossnote1b;
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(144, 26);
            this.mnuClose.Text = "Close";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Image = global::CardGenIDE.Properties.Resources.redcross2;
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(144, 26);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuDeck
            // 
            this.mnuDeck.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenerate});
            this.mnuDeck.Image = global::CardGenIDE.Properties.Resources.CardGen;
            this.mnuDeck.Name = "mnuDeck";
            this.mnuDeck.Size = new System.Drawing.Size(74, 24);
            this.mnuDeck.Text = "Deck";
            // 
            // mnuGenerate
            // 
            this.mnuGenerate.Image = global::CardGenIDE.Properties.Resources.bars2b;
            this.mnuGenerate.Name = "mnuGenerate";
            this.mnuGenerate.Size = new System.Drawing.Size(144, 26);
            this.mnuGenerate.Text = "Generate";
            this.mnuGenerate.Click += new System.EventHandler(this.mnuGenerate_Click);
            // 
            // mnuMainHelp
            // 
            this.mnuMainHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp,
            this.toolStripMenuItem2,
            this.mnuAbout});
            this.mnuMainHelp.Image = global::CardGenIDE.Properties.Resources.gmindbubble2;
            this.mnuMainHelp.Name = "mnuMainHelp";
            this.mnuMainHelp.Size = new System.Drawing.Size(73, 24);
            this.mnuMainHelp.Text = "Help";
            // 
            // mnuHelp
            // 
            this.mnuHelp.Image = global::CardGenIDE.Properties.Resources.questionmark1;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(125, 26);
            this.mnuHelp.Text = "Help";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(122, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::CardGenIDE.Properties.Resources.yinfo1;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(125, 26);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(339, 31);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(753, 633);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(5, 21);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblTopCard);
            this.splitContainer1.Panel1.Controls.Add(this.cardImg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblBack);
            this.splitContainer1.Panel2.Controls.Add(this.cardImgBack);
            this.splitContainer1.Size = new System.Drawing.Size(741, 606);
            this.splitContainer1.SplitterDistance = 364;
            this.splitContainer1.TabIndex = 3;
            // 
            // lblTopCard
            // 
            this.lblTopCard.AutoSize = true;
            this.lblTopCard.Location = new System.Drawing.Point(3, 6);
            this.lblTopCard.Name = "lblTopCard";
            this.lblTopCard.Size = new System.Drawing.Size(37, 17);
            this.lblTopCard.TabIndex = 4;
            this.lblTopCard.Text = "Top:";
            // 
            // cardImg
            // 
            this.cardImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardImg.Location = new System.Drawing.Point(3, 26);
            this.cardImg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cardImg.Name = "cardImg";
            this.cardImg.Size = new System.Drawing.Size(360, 574);
            this.cardImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cardImg.TabIndex = 3;
            this.cardImg.TabStop = false;
            // 
            // lblBack
            // 
            this.lblBack.AutoSize = true;
            this.lblBack.Location = new System.Drawing.Point(3, 6);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(43, 17);
            this.lblBack.TabIndex = 5;
            this.lblBack.Text = "Back:";
            // 
            // cardImgBack
            // 
            this.cardImgBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardImgBack.Location = new System.Drawing.Point(3, 26);
            this.cardImgBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cardImgBack.Name = "cardImgBack";
            this.cardImgBack.Size = new System.Drawing.Size(373, 574);
            this.cardImgBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cardImgBack.TabIndex = 4;
            this.cardImgBack.TabStop = false;
            // 
            // frmMainIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 674);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMainIDE";
            this.Text = "CardGen IDE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainIDE_FormClosing);
            this.Load += new System.EventHandler(this.frmMainIDE_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardImgBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox cardImgBack;
        private System.Windows.Forms.PictureBox cardImg;
        private System.Windows.Forms.Label lblTopCard;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbObjects;
        private System.Windows.Forms.PropertyGrid propGrid;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem mnuDeck;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerate;
        private System.Windows.Forms.Button btnDelB;
        private System.Windows.Forms.Button btnAddB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbObjectsB;
        private System.Windows.Forms.PropertyGrid propGridB;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chbGenTextWord;
        private System.Windows.Forms.CheckBox chbGenerateBack;
        private System.Windows.Forms.ComboBox cbGenType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDPI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCardHeight;
        private System.Windows.Forms.TextBox txtCardWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblCardWidth;
        private System.Windows.Forms.Button btnSetCardSize;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtDataFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPaper;
        private System.Windows.Forms.CheckBox chbLandscape;
        private System.Windows.Forms.Button btnForeDown;
        private System.Windows.Forms.Button btnForeUp;
        private System.Windows.Forms.Button btnBackDown;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCopyB;
        private System.Windows.Forms.ToolStripMenuItem mnuMainHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    }
}

