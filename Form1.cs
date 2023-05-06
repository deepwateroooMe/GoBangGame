using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoBangMe {

    public partial class Form1 : Form {
        /**
         * 记录三步状态
         * */
        MyList<State> states = new MyList<State>();
        public Form1() {
            InitializeComponent();
            init();
            if (!myFirst) {
                me = !me;
                winJudge(8,8);
                MessageBox.Show(me + "");
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e) {
        }
        private void pictureBox1_Click(object sender, EventArgs e) {
        }
        private void button1_Click(object sender, EventArgs e) {
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e) {
            if (!me || over) {// 如果不该我方下棋或已结束，则返回
                return;
            }
            int i = e.X;// 棋盘位置（像素）
            int j = e.Y;// 棋盘位置（像素）
            int px = i % 36;// 通过鼠标点击的位置计算棋子应该下的位置
            int py = j % 36;// 通过鼠标点击的位置计算棋子应该下的位置
            i = i - (px) + (px <= 18 ? -18 : 18);// 通过鼠标点击的位置计算棋子应该下的位置
            j = j - (py) + (py <= 18 ? -18 : 18);// 通过鼠标点击的位置计算棋子应该下的位置
            int x = (i + 18) / 36;// 坐标位置（数组）
            int y = (j + 18) / 36;// 坐标位置（数组）
            if (x == 0 || y == 0 || x >= 17 || y >= 17) {// 规则：第一行，第一列不能下棋。
                MessageBox.Show(i + "," + j + ":" + x + "," + y);
                return;
            }
            // paintNode(i, j);
            myStep(i, j, x, y);
        }
        public void myStep(int i, int j, int x, int y) {
            if (chessBorad[x,y] == 0) {
                // 下棋之前记住改变的状态
                State s;
                // s = new State();
                if (states.count>=6) {
                    s = states.getAndRemoveLast();
                    s.k_valueList = new ArrayList();
                }
                else {
                    s = new State();
                }
                s.x = x;
                s.y = y;
                // 先记录坐标
                s.pictureBox = paintNode(i, j);
                chessBorad[x,y] = 1;
                for (int k = 0; k < re.getCount(); k++) {
                    if (wins[x,y,k]) {
                        // 直接之前的k中赢发的状态值
                        s.k_valueList.Add(new K_Value(k,myWin[k],computerWin[k]));
                        myWin[k]++;
                        computerWin[k] = 999;
                        if (myWin[k] == 5)
                        {
                            MessageBox.Show("你赢了");
                            over = true;
                            return;
                        }
                    }
                }
                states.add(s);
            }
            me = !me;
            computerStep();
            if (hui<3) {
                hui++;
                button1.Text = "悔棋("+hui+")";
            }
        }
        /**
         * 计算机下棋
         */
        public void computerStep() {
            if (over) {// 游戏结束
                return;
            }
            int[,] myScore = new int[17, 17];// 评估我方评分
            int[,] computerScore = new int[17, 17];// 评估计算机评分
            int max = 0;
            int x = 0;
            int y = 0;
            // 遍历棋盘
            for (int i = 1; i< 17; i++) {// x轴
                for (int j = 1; j< 17; j++) {// y轴
                    if (chessBorad[i,j] == 0) {// 当前可落子，如果不等于0代表已经有棋子
                        for (int k = 0; k<re.getCount(); k++) { // 每个点都在 多种赢法中，所以要遍历所有赢法
                            if (wins[i,j,k]) {// 计算他在K赢法中的 重要性
                                switch (myWin[k]) {// 我方棋路，当前赢法中已经连上了几颗棋子
                                case 1:
                                    myScore[i,j] += 200;
                                    break;
                                case 2:
                                    myScore[i,j] += 400;
                                    break;
                                case 3:
                                    myScore[i,j] += 2000;
                                    break;
                                case 4:
                                    myScore[i,j] += 10000;
                                    break;
                                }
                                switch (computerWin[k]) {// 计算机棋路，当前赢法中已经连上了几颗棋子
                                case 1:
                                    computerScore[i,j] += 300;
                                    break;
                                case 2:
                                    computerScore[i,j] += 500;
                                    break;
                                case 3:
                                    computerScore[i,j] += 3000;
                                    break;
                                case 4:
                                    computerScore[i,j] += 20000;
                                    break;
                                }
                            }
                        }
                        // 玩家最重要的落点
                        if (myScore[i,j] > max) {
                            max = myScore[i,j];
                            x = i;
                            y = j;
                        } else if (myScore[i,j] == max) {
                            if (computerScore[i,j] > computerScore[x,y]) { // 
                                x = i;
                                y = j;
                            }
                        }
                        // AI最重要的落点
                        if (computerScore[i,j] > max) {
                            max = computerScore[i,j];
                            x = i;
                            y = j;
                        } else if (computerScore[i,j] == max) {
                            if (myScore[i,j] > myScore[x,y]) {
                                x = i;
                                y = j;
                            }
                        }
                    }
                }
            }
            winJudge(x,y);
        }
        void winJudge(int x,int y) {
            // 下棋之前记住改变的状态
            State s;
            // s = new State();
            if (states.count >= 6) {
                s = states.getAndRemoveLast();
                s.k_valueList = new ArrayList();
            } else {
                s = new State();
            }
            s.x = x;
            s.y = y;
            // 记住坐标
            s.pictureBox=paintNode(x * 36 - 18, y * 36 - 18);// 下棋
            chessBorad[x, y] = 2;// 棋盘标记已下棋
            for (int k = 0; k < re.getCount(); k++) {// 遍历所有的赢法
                if (wins[x, y, k]) {// 如果（x,y）这个点在某一种赢法中
                    // 记录之前的k中赢发的状态值
                    s.k_valueList.Add(new K_Value(k, myWin[k], computerWin[k]));
                    computerWin[k]++;  // 那么该种赢法中有多了一个棋子
                    myWin[k] = 999;  // 那么我方的这种赢法就不可能赢了，设一个异常的值
                    if (computerWin[k] == 5) { // 如果计算机在某种赢法上连上了五个子，那么计算机就赢了，我方就输了
                        MessageBox.Show("你输了");
                        over = true; // 结束游戏
                        return;
                    }
                }
            }
            states.add(s);
            if (!over) {// 如果没有结束游戏
                me = !me;// 换我方下棋
            }
        }

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        public PictureBox paintNode(int x,int y) {
            PictureBox pictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
            pictureBox.BackColor = System.Drawing.Color.Transparent;
            if (me) {
                pictureBox.Image = Image.FromFile(Environment.CurrentDirectory + @"\Images\black.png"); // ;
            } else {
                pictureBox.Image = Image.FromFile(Environment.CurrentDirectory + @"\Images\white.png");
            }
            pictureBox.Location = new System.Drawing.Point(x, y);
            pictureBox.Size = new System.Drawing.Size(36, 36);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            this.panel1.Controls.Add(pictureBox);
            ((System.ComponentModel.ISupportInitialize)(pictureBox)).EndInit();
            return pictureBox;
        }
        private void pictureBox2_Click(object sender, EventArgs e) {
        }
        private void pictureBox3_Click(object sender, EventArgs e) {
        }
        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e) {
        }
        private void 最初ToolStripMenuItem_Click(object sender, EventArgs e) {
        }
        private void 重新开始游戏ToolStripMenuItem_Click(object sender, EventArgs e) {
            states = new MyList<State>();
            this.panel1.Controls.Clear();
            init();
            over = false;
            me = true;
            hui = 0;
            button1.Text = "悔棋(0)";
            if (!myFirst) {
                me = !me;
                winJudge(8, 8);
            }
        }
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("五子棋都不会！还有脸点帮助");
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Environment.Exit(0);
        }
        private void checkBox1_Click(object sender, EventArgs e) {
            checkBox2.Checked = false;
            checkBox1.Checked = true;
            myFirst = true;
        }
        private void checkBox2_Click(object sender, EventArgs e) {
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            myFirst = false;
        }
        int hui = 0;
        // 悔棋
        private void button1_Click_1(object sender, EventArgs e) {
            if (over) {
                MessageBox.Show("游戏结束");
                return;
            }
            if (!me) {
                MessageBox.Show("等待对方下棋");
            }
            if (states.count<=1) {
                MessageBox.Show("无法退棋");
                return;
            }
            reStep();
            reStep();
            hui--;
            button1.Text = "悔棋(" + hui + ")";
        }
        public void reStep() {
            State s = states.getAndRemoveFirst();
            if (s != null) {
                chessBorad[s.x, s.y] = 0;
                for (int i = 0; i < s.k_valueList.Count; i++) {
                    K_Value k = (K_Value)s.k_valueList[i];
                    myWin[k.k] = k.myWinValue;
                    computerWin[k.k] = k.computerWinValue;
                }
                s.pictureBox.Dispose();
            }
        }
        private void button2_Click(object sender, EventArgs e) {
        }
    }
}
