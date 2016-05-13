namespace ChatApplication
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.lbRooms = new System.Windows.Forms.ListBox();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.lbMessages = new System.Windows.Forms.ListBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeNickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMessage = new System.Windows.Forms.Button();
            this.userContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.partToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNickname = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLocalIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbTopic = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.userContextMenu.SuspendLayout();
            this.roomContextMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbRooms
            // 
            this.lbRooms.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbRooms.FormattingEnabled = true;
            this.lbRooms.Location = new System.Drawing.Point(6, 35);
            this.lbRooms.Name = "lbRooms";
            this.lbRooms.Size = new System.Drawing.Size(219, 576);
            this.lbRooms.TabIndex = 3;
            this.lbRooms.SelectedIndexChanged += new System.EventHandler(this.lbRooms_SelectedIndexChanged);
            this.lbRooms.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbRooms_MouseDown);
            // 
            // lbUsers
            // 
            this.lbUsers.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(775, 33);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(219, 576);
            this.lbUsers.TabIndex = 4;
            this.lbUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbUsers_MouseDown);
            // 
            // lbMessages
            // 
            this.lbMessages.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbMessages.FormattingEnabled = true;
            this.lbMessages.Location = new System.Drawing.Point(226, 35);
            this.lbMessages.Name = "lbMessages";
            this.lbMessages.Size = new System.Drawing.Size(543, 550);
            this.lbMessages.TabIndex = 5;
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(226, 588);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(543, 22);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.roomToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeNickToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // changeNickToolStripMenuItem
            // 
            this.changeNickToolStripMenuItem.Name = "changeNickToolStripMenuItem";
            this.changeNickToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.changeNickToolStripMenuItem.Text = "Change Nick";
            this.changeNickToolStripMenuItem.Click += new System.EventHandler(this.changeNickToolStripMenuItem_Click);
            // 
            // roomToolStripMenuItem
            // 
            this.roomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createRoomToolStripMenuItem,
            this.joinRoomToolStripMenuItem,
            this.partRoomToolStripMenuItem});
            this.roomToolStripMenuItem.Name = "roomToolStripMenuItem";
            this.roomToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.roomToolStripMenuItem.Text = "Room";
            // 
            // createRoomToolStripMenuItem
            // 
            this.createRoomToolStripMenuItem.Name = "createRoomToolStripMenuItem";
            this.createRoomToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.createRoomToolStripMenuItem.Text = "Create Room";
            this.createRoomToolStripMenuItem.Click += new System.EventHandler(this.createRoomToolStripMenuItem_Click);
            // 
            // joinRoomToolStripMenuItem
            // 
            this.joinRoomToolStripMenuItem.Name = "joinRoomToolStripMenuItem";
            this.joinRoomToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.joinRoomToolStripMenuItem.Text = "Join Room";
            this.joinRoomToolStripMenuItem.Click += new System.EventHandler(this.joinRoomToolStripMenuItem_Click);
            // 
            // partRoomToolStripMenuItem
            // 
            this.partRoomToolStripMenuItem.Name = "partRoomToolStripMenuItem";
            this.partRoomToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.partRoomToolStripMenuItem.Text = "Part Room";
            this.partRoomToolStripMenuItem.Click += new System.EventHandler(this.partRoomToolStripMenuItem_Click);
            // 
            // btnMessage
            // 
            this.btnMessage.Location = new System.Drawing.Point(555, 704);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(75, 23);
            this.btnMessage.TabIndex = 12;
            this.btnMessage.Text = "button1";
            this.btnMessage.UseVisualStyleBackColor = true;
            this.btnMessage.Visible = false;
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            // 
            // userContextMenu
            // 
            this.userContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem});
            this.userContextMenu.Name = "userContextMenu";
            this.userContextMenu.Size = new System.Drawing.Size(97, 48);
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.kickToolStripMenuItem.Text = "Kick";
            this.kickToolStripMenuItem.Click += new System.EventHandler(this.kickToolStripMenuItem_Click);
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.banToolStripMenuItem.Text = "Ban";
            this.banToolStripMenuItem.Click += new System.EventHandler(this.banToolStripMenuItem_Click);
            // 
            // roomContextMenu
            // 
            this.roomContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.partToolStripMenuItem});
            this.roomContextMenu.Name = "roomContextMenu";
            this.roomContextMenu.Size = new System.Drawing.Size(96, 26);
            // 
            // partToolStripMenuItem
            // 
            this.partToolStripMenuItem.Name = "partToolStripMenuItem";
            this.partToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.partToolStripMenuItem.Text = "Part";
            this.partToolStripMenuItem.Click += new System.EventHandler(this.partToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Rooms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(861, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Users";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblNickname,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel2,
            this.label1,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.lblLocalIP});
            this.statusStrip1.Location = new System.Drawing.Point(0, 749);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1012, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel1.Text = "Nickname:";
            // 
            // lblNickname
            // 
            this.lblNickname.BackColor = System.Drawing.Color.Transparent;
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(74, 17);
            this.lblNickname.Text = "lblNickname";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel3.Text = " | ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel4.Text = " | ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabel5.Text = "Local IP Address:";
            // 
            // lblLocalIP
            // 
            this.lblLocalIP.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalIP.Name = "lblLocalIP";
            this.lblLocalIP.Size = new System.Drawing.Size(118, 17);
            this.lblLocalIP.Text = "toolStripStatusLabel6";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbTopic);
            this.groupBox1.Controls.Add(this.lbRooms);
            this.groupBox1.Controls.Add(this.lbMessages);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbUsers);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMessage);
            this.groupBox1.Location = new System.Drawing.Point(6, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 616);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // lbTopic
            // 
            this.lbTopic.AutoSize = true;
            this.lbTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTopic.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbTopic.Location = new System.Drawing.Point(233, 12);
            this.lbTopic.Name = "lbTopic";
            this.lbTopic.Size = new System.Drawing.Size(51, 16);
            this.lbTopic.TabIndex = 19;
            this.lbTopic.Text = "label4";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnMessage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1012, 771);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnMessage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Simple Chat";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.userContextMenu.ResumeLayout(false);
            this.roomContextMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbRooms;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.ListBox lbMessages;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnMessage;
        private System.Windows.Forms.ContextMenuStrip userContextMenu;
        private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip roomContextMenu;
        private System.Windows.Forms.ToolStripMenuItem partToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeNickToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem roomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem partRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRoomToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblNickname;
        private System.Windows.Forms.ToolStripStatusLabel label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lblLocalIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTopic;

    }
}

