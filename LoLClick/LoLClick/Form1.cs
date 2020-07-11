using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using OpenCvSharp;

namespace LoLClick
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("User32.dll")]
        private static extern void Mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        private struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private const string imgPath = @"./img/";
        private const string ClientName = "LeagueClientUx";
        private Bitmap Click_Img = null;
        private Bitmap Original_Img = null;
        private IntPtr mainWindow;

        public Form1()
        {
            InitializeComponent();
            Click_Img = new Bitmap(imgPath + "StartBnt.png");
            if( Click_Img != null)
            {
                Console.WriteLine("이미지를 찾았습니다.");
            }
            else
            {
                Console.WriteLine("이미지를 찾지 못했습니다.");
            }
        }

/********************************* Form 클릭 함수*********************/
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
            timer1.Interval = 300;
            timer1.Stop();

            Process[] processes = Process.GetProcessesByName(ClientName);
            Process p = processes[0];
            mainWindow = p.MainWindowHandle;
            Rect LeagueRect = new Rect();
            GetWindowRect(mainWindow, ref LeagueRect);

            #region LeagueRect 속성 출력
            Console.WriteLine("****LeagueRect 속성****");
            Console.WriteLine("LeagueRect.Top : " + LeagueRect.Top);
            Console.WriteLine("LeagueRect.Bottom : " + LeagueRect.Bottom);
            Console.WriteLine("LeagueRect.Left : " + LeagueRect.Left);
            Console.WriteLine("LeagueRect.Right : " + LeagueRect.Right);
            Console.WriteLine("**********************");
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MousePosX.Text = Cursor.Position.X.ToString();
            MousePosY.Text = Cursor.Position.Y.ToString();

            if (Original_Img != null)
            {
                searchIMG(Original_Img, Click_Img);
            }
        }

        private void Click_Click(object sender, EventArgs e)
        {
            searchIMGUpdate();
            timer1.Start();
        }

        /*********************************** 메소드 **********************************/
        private void searchIMG(Bitmap screen_img, Bitmap find_img)
        {
            using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
            using(Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
            using(Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
            {
                double minval, maxval = 0;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(res, out minval, out maxval, out minLoc, out maxLoc);
                Console.WriteLine("찾은 이미지 유사도 : " + maxval);
                Console.WriteLine($"{minval}, {maxval}, {minLoc}, {maxLoc}");

                if(maxval >= 0.8)
                {
                    Console.WriteLine("유사도 0.8 이상 로직");
                    timer1.Stop();
                }
                else
                {
                    Console.WriteLine("이미지를 찾는 중 입니다....");
                }
            }
        }

        private void searchIMGUpdate()
        {
            Graphics Graphicsdata = Graphics.FromHwnd(mainWindow);
            Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                //찾은 플레이어의 크기만큼 화면을 캡쳐합니다.
                IntPtr hdc = g.GetHdc();
                PrintWindow(mainWindow, hdc, 0x2);
                g.ReleaseHdc(hdc);
            }

            Original_Img = bmp;
        }
    }
}
