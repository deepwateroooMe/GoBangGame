using System.Collections;
using System.Windows.Forms;

namespace GoBangMe {

    class Ready {
        public int x;
        public int y;
        public int count;
        public int getCount() {
            return this.count;
        }
        /**
         * 计算所有赢法种类
         * @return
         */
        public Ready(int x, int y) {
            this.x = x;
            this.y = y;
            count = initCount();
        }
        private int initCount() {
            int count = 0;
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y - 4; j++) {
                    count++;
                }
            }
            // 横线上
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y - 4; j++) {
                    count++;
                }
            }
            // 斜线上
            for (int i = 0; i < x - 4; i++) {
                for (int j = 0; j < y - 4; j++) {
                    count++;
                }
            }
            // 反斜线上
            for (int i = 0; i < x - 4; i++) {
                for (int j = y - 1; j > 3; j--) {
                    count++;
                }
            }
            return count;
        }
        /**
         * 初始化所有赢法
         * @param c
         * @return
         */
        public bool[,,] initChess() {
            bool[,,] wins =new bool[x,y,count];
            int counts = 0;
            for (int i = 0; i<x; i++) {
                for (int j = 0; j<y-4; j++) {
                    for (int k = 0; k< 5; k++) {
                        wins[i,j + k,counts] = true;
                    }
                    counts++;
                }
            }
            for (int i = 0; i<x; i++) {
                for (int j = 0; j<y-4; j++) {
                    for (int k = 0; k< 5; k++) {
                        wins[j + k,i, counts] = true;
                    }
                    counts++;
                }
            }
            for (int i = 0; i<x-4; i++) {
                for (int j = 0; j<y-4; j++) {
                    for (int k = 0; k< 5; k++) {
                        wins[i + k,j + k, counts] = true;
                    }
                    counts++;
                }
            }
            for (int i = 0; i<x-4; i++) {
                for (int j = y - 1; j > 3; j--) {
                    for (int k = 0; k< 5; k++) {
                        wins[i + k,j - k, counts] = true;// 记录赢得可能性
                    }
                    counts++;
                }
            }
            return wins;
        }
        public int[] getMyWin() {
            int[] myWin = new int[count];
            return myWin;
        }
        public int[] getConputerWin() {
            int[] conputer = new int[count];
            return conputer;
        }
        /**
         * 初始化棋盘
         * @return
         */
        public int[,] getChessBorad() {
            int[,] chessBorad = new int[x,y];
            return chessBorad;
        }
    }
    class State {
        public int x;
        public int y;
        public PictureBox pictureBox;
        public ArrayList k_valueList = new ArrayList();
    }
    class K_Value {
        public K_Value(int k,int myWinValue,int computerWinValue) {
            this.k = k;
            this.myWinValue = myWinValue;
            this.computerWinValue = computerWinValue;
        }
        public int k;
        public int myWinValue;
        public int computerWinValue;
    }
// c#的集合太傻逼了，在此自制链表
    class MyList<T> {
        public int count;// 总数
        public Node<T> first;// 头节点
        public Node<T> last;// 尾节点
        // 添加函数
        public void add(T t) {
            if (first == null) {
                first = new Node<T>(t);
                last = first;
            } else {
                Node<T> node = new Node<T>(t);
                node.next = first;
                first.prev = node;
                first = node;
            }
            count++;
        }
        /**
         * 获取并删除最后一个元素
         * **/
        public T getAndRemoveLast() {
            Node<T> las = last;
            if (last != null) {
                count--;
                if (first == last) {
                    last = null;
                    first = last;
                    return las.t;
                }
                else {
                    last = last.prev;
                    las.prev = null;
                    last.next = null;
                    return las.t;
                }
            } else {
                return default(T);
            }
        }
        /**
         * 获取并删除第一个元素
         * **/
        public T getAndRemoveFirst() {
            Node<T> fir = first;
            if (first != null) {
                count--;
                if (first == last) {
                    last = null;
                    first = last;
                    return fir.t;
                }
                else {
                    first = first.next;
                    fir.next = null;
                    first.prev = null;
                    return fir.t;
                }
            } else {
                return default(T);
            }
        }
    }
    class Node<T> {
        public Node<T> next;
        public Node<T> prev;
        public T t;
        public Node(T t) {
            this.t = t;
            next = null;
            prev = null;
        }
    }
}
