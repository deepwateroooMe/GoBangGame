using System.Drawing;
using System.Windows.Forms;

namespace GoBangMe {
    partial class Form1 {

        // 必需的设计器变量。
        private System.ComponentModel.IContainer components = null;
        private Ready re = new Ready(17,17); // 【爱表哥，爱生活！！！活宝妹就是一定要嫁给亲爱的表哥！！！】
        // 清理所有正在使用的资源。
        // <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
#region Windows 窗体设计器生成的代码
        // 设计器支持所需的方法 - 不要修改
        // 使用代码编辑器修改此方法的内容。
        // 
        public static int s=0;
        public static bool[,,] wins;
        // 五子棋的所有赢法，前两维代表棋盘，第三位代表所有赢法，每一个棋子都可以有多种赢法。
        public static int[] myWin;// 我方第i中赢法中，有n颗棋子，如果有5颗棋子代表已经赢了，例如：myWin[5]=2;代表第5中赢法中，已经有2颗棋子,
        public static int[] computerWin;// 计算机方第i中赢法中，有n颗棋子
        public static int[,] chessBorad;// 代表棋盘，值0代表没有下棋子，其他值代表已经下棋子了。
        public static bool myFirst = true;
        public bool me = true;// true代表应该我方下棋
        public bool over = false;// 游戏是否结束

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新开始游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            // this.panel1.BackgroundImage = GoBangMe.Properties.Resources.background3; // 
            this.panel1.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Images\background3.png"); // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(10, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 612);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.菜单ToolStripMenuItem,
                    this.重新开始游戏ToolStripMenuItem,
                    this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.退出ToolStripMenuItem});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.退出ToolStripMenuItem.Text = "退出游戏";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 重新开始游戏ToolStripMenuItem
            // 
            this.重新开始游戏ToolStripMenuItem.Name = "重新开始游戏ToolStripMenuItem";
            this.重新开始游戏ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.重新开始游戏ToolStripMenuItem.Text = "重新开始游戏";
            this.重新开始游戏ToolStripMenuItem.Click += new System.EventHandler(this.重新开始游戏ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(260, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 19);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "我优先";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(340, 6);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(104, 19);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "计算机优先";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "悔棋(0)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            // this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 654);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "五子棋";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        void init() {
            wins = re.initChess();
            myWin = re.getMyWin();
            computerWin = re.getConputerWin();
            chessBorad = re.getChessBorad();
        }
#endregion
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 菜单ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripMenuItem 重新开始游戏ToolStripMenuItem;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button button1;
    }
}