using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HM_SnakeGame
{
    /// <summary>
    /// 게임 UI 요소들과의 상호작용을 처리합니다.
    /// </summary>

    internal class GameUI
    {
        private PictureBox picCanvas;   // 그림을 그릴 PictureBox
        private Label txtScore;         // 점수를 표시할 Label
        private Label txtHighScore;     // 최고 점수를 표시할 Label
        private Button startButton;     // 게임 시작 버튼
        private Button snapButton;      // 스냅샷 찍기 버튼

        private SnakeGame game;
        private Timer gameTimer;        // 게임 타이머

        public GameUI(PictureBox picCanvas , Label txtScore , Label txtHighScore , Button startButton , Button snapButton , Timer gameTimer , SnakeGame game)
        {
            this.picCanvas = picCanvas;
            this.txtScore = txtScore;
            this.txtHighScore = txtHighScore;
            this.startButton = startButton;
            this.snapButton = snapButton;
            this.gameTimer = gameTimer;
            this.game = game;

            game.OnGameOver += GameOverEvent;
        }

        // 게임 시작시 게임 초기화 및 버튼 비활성화
        public void StartGame()
        {
            game.ResetGame();
            startButton.Enabled = false;
            snapButton.Enabled = false;
            gameTimer.Start();
        }

        // 스크린 샷
        public void TakeSnapshot()
        {
            // 라벨 생성 및 설정
            Label caption = new Label
            {
                Text = "점수 : " + game.Score + " 최고점수 : " + game.HighScore,
                Font = new Font("Ariel", 12, FontStyle.Bold),
                ForeColor = Color.Purple,
                AutoSize = false,
                Width = picCanvas.Width,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // 그림 영역에 캡션 추가
            picCanvas.Controls.Add(caption);

            // 파일 저장 대화 상자 생성 및 설정
            SaveFileDialog dialog = new SaveFileDialog
            {
                FileName = "HM SnakeGame snapshot", // 파일 이름 기본값 설정
                DefaultExt = "jpg",                 // 기본 파일 확장자 설정
                Filter = "JPG Image File | *.jpg",  // 파일 형식 필터 설정
                ValidateNames = true                // 파일 이름 유효성 검사 활성화
            };

            // 파일 저장 대화 상자 표시 및 확인 버튼이 눌렸는지 확인
            if ( dialog.ShowDialog() == DialogResult.OK )
            {
                int width = Convert.ToInt32(picCanvas.Width);
                int height = Convert.ToInt32(picCanvas.Height);
                Bitmap bmp = new Bitmap(width, height);                                 // 지정된 너비와 높이로 비트맵 객체 생성
                picCanvas.DrawToBitmap(bmp , new Rectangle(0 , 0 , width , height));    // 비트맵 객체에 그림 영역의 내용을 그리기
                bmp.Save(dialog.FileName , ImageFormat.Jpeg);                           // 비트맵 이미지를 지정된 파일 이름으로 JPEG 형식으로 저장
                picCanvas.Controls.Remove(caption);                                     // 그림 영역에서 캡션 제거
            }
        }

        // 점수 및 최고점수를 갱신하는 UI 업데이트
        public void UpdateUI()
        {
            txtScore.Text = "Score : " + game.Score;
        }

        // 뱀과 음식 그래픽 처리
        public void Draw(Graphics canvas)
        {
            Brush snakeColor;

            for ( int i = 0; i < game.Snake.Count; i++ )
            {
                // 뱀의 머리는 검정색으로, 나머지 몸통은 짙은 녹색으로 설정
                snakeColor = i == 0 ? Brushes.Black : Brushes.DarkGreen;

                // 뱀의 각 부분을 타원으로 그림
                canvas.FillEllipse(snakeColor ,
                    new Rectangle(game.Snake[ i ].X * Settings.Width , game.Snake[ i ].Y * Settings.Height , Settings.Width , Settings.Height));
            }

            canvas.FillEllipse(Brushes.DarkRed ,
                new Rectangle(game.Food.X * Settings.Width , game.Food.Y * Settings.Height , Settings.Width , Settings.Height));
        }

        public void GameOverEvent(object sender , EventArgs e)
        {
            if ( game.Score >= game.HighScore )
            {
                txtHighScore.Text = "High Score : " + Environment.NewLine + game.HighScore;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
    }
}
