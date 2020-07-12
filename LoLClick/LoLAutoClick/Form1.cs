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
using OpenCvSharp;
using System.Diagnostics;

namespace LoLAutoClick
{
    public partial class Form1 : Form
    {
        // 윈도우 프로세스
        [DllImport("User32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        // 마우스 이벤트
        [DllImport("User32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        // 이미지 스크린 저장 값
        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        // 윈도우 최상위로 만들기
        [DllImport("User32")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        // 속성값 구조체
        private struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
        Rect LeagueRect = new Rect(); // 생성

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002; // 마우스 왼쪽 플레그
        private const uint MOUSEEVENTF_LEFTUP = 0x0004; // 마우스 왼쪽 플레그

        private const string imgPath = @"./img/"; // 이미지 절대 경로
        private const string processeName = "LeagueClientUx"; // 클라이언트명
        private Bitmap Search_Img = null; // 스샷 이미지
        private Bitmap Click_Img = null; // 비교할 이미지
        private IntPtr mainProcesse; // 메인 프로세스

        OpenCvSharp.Point minLoc, maxLoc; // 찾은 이미지 위치 값

        public Form1()
        {
            InitializeComponent();
            Click_Img = new Bitmap(imgPath + "AcceptanceBnt.png");
            SearchImgUpdate();
            if (Click_Img != null)
            {
                Console.WriteLine("이미지를 찾았습니다.");
            }
            else
            {
                Console.WriteLine("이미지를 찾지 못했습니다.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
            timer1.Interval = 300;
            timer1.Stop();

            timer2_Tick(sender, e);
            timer2.Interval = 300;
            timer2.Start();

            Process[] processes = Process.GetProcessesByName(processeName);
            Process p = processes[0];
            mainProcesse = p.MainWindowHandle;
            GetWindowRect(mainProcesse, ref LeagueRect);

            #region LeagueRect 속성 출력
            Console.WriteLine("****LeagueRect 속성****");
            Console.WriteLine("LeagueRect.Top : " + LeagueRect.Top);
            Console.WriteLine("LeagueRect.Bottom : " + LeagueRect.Bottom);
            Console.WriteLine("LeagueRect.Left : " + LeagueRect.Left);
            Console.WriteLine("LeagueRect.Right : " + LeagueRect.Right);
            Console.WriteLine("**********************");
            #endregion
        }

        // 이미지 서칭 타이머
        private void timer1_Tick(object sender, EventArgs e)
        {
            SearchImgUpdate();
            SearchImg(Search_Img, Click_Img);
        }

        // 커서 위치 타이머
        private void timer2_Tick(object sender, EventArgs e)
        {
            label_posX.Text = Cursor.Position.X.ToString();
            label_posY.Text = Cursor.Position.Y.ToString();
        }

        // 이미지 찾기 버튼
        private void Bnt_Search_Click(object sender, EventArgs e)
        {
            GetWindowRect(mainProcesse, ref LeagueRect);
            timer1.Start();
        }

        // 이미지 비교
        private void SearchImg(Bitmap screen_img, Bitmap find_img)
        {
            using (Mat ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(screen_img))
            using (Mat FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(find_img))
            using (Mat res = ScreenMat.MatchTemplate(FindMat, TemplateMatchModes.CCoeffNormed))
            {
                double minval, maxval = 0;

                Cv2.MinMaxLoc(res, out minval, out maxval, out minLoc, out maxLoc);
                Console.WriteLine("찾은 이미지 유사도 : " + maxval);
                Console.WriteLine($"{minval}, {maxval}, {minLoc}, {maxLoc}");

                if (maxval >= 0.8)
                {
                    Console.WriteLine("유사도 0.8 이상 로직");
                    SetForegroundWindow(mainProcesse); // 윈도우를 최상위로 만들기
                    ScreenRatioCalculator(); // 스크린 좌표 함수
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); // 마우스 이벤트
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); // 마우스 이벤트
                    timer1.Stop(); // 찾기 종료
                }
                else
                {
                    Console.WriteLine("이미지를 찾는 중 입니다....");
                }
            }
        }

        // 비교 이미지 업데이트
        private void SearchImgUpdate()
        {
            Graphics Graphicsdata = Graphics.FromHwnd(mainProcesse);
            Rectangle rect = Rectangle.Round(Graphicsdata.VisibleClipBounds);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                //찾은 플레이어의 크기만큼 화면을 캡쳐합니다.
                IntPtr hdc = g.GetHdc();
                PrintWindow(mainProcesse, hdc, 0x2);
                g.ReleaseHdc(hdc);
            }
            Search_Img = bmp;
        }

        // 마우스 클릭 이벤트
        private void Bnt_PosChange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(text_posX.Text) && !string.IsNullOrEmpty(text_posY.Text))
            {
                Cursor.Position = new System.Drawing.Point(
                                Convert.ToInt32(text_posX.Text.ToString()),
                                Convert.ToInt32(text_posY.Text.ToString())
                                                          );
            }
            else
            {
                Console.WriteLine("입력 문자가 공백 입니다.");
            }
        }

        // 화면 비율 커서 위치 로직
        private void ScreenRatioCalculator()
        {
            int x = LeagueRect.Left;
            int y = LeagueRect.Top;

            Cursor.Position = new System.Drawing.Point(
                x + maxLoc.X,
                y + maxLoc.Y
                );
        }

        // 어플 종료
        private void Bnt_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}