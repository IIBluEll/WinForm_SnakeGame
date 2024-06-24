# WinForm_SnakeGame

## __1. 플레이 영상__
<center>


##### <하단 이미지를 클릭해주세요>
[![Video Label](https://github.com/IIBluEll/WinForm_SnakeGame/assets/19919570/27af0312-7167-430a-9fff-f0f9ce699786)](https://youtube.com/shorts/MCntPHtO3I0?si=2pU85UvVEjFwC4n0)</center>

## __2. 주요 기능 코드__

#### __1. 뱀 관련 기능__ [<SnakeGame.cs_Link>](https://github.com/IIBluEll/WinForm_SnakeGame/blob/main/HM_SnakeGame/Class%20Folder/SnakeGame.cs)
> * __뱀 음식 먹음 및 몸통 증가__   
> ```C#
>// Class Folder/SnakeGame.cs - 66 ~ 77
>
>        // 게임 업데이트 / 뱀의 이동과 충돌 처리
>        public void Update()
>        {
>            MoveSnake();    // 뱀 이동
>            // 음식 먹기 체크
>            if ( Snake[ 0 ].X == Food.X && Snake[ 0 ].Y == Food.Y )
>            {
>                EatFood();
>            }
>
>            CheckCollision();   // 충돌 체크
>        }
>```
>```C#
>// Class Folder/SnakeGame.cs - 141 ~ 155
>
>        // 음식을 먹었을 때
>        private void EatFood()
>        {
>            Score++;
>
>            // 몸체 추가
>            Circle body = new Circle
>            {
>                X = Snake[Snake.Count - 1].X,
>                Y = Snake[Snake.Count - 1].Y
>            };
>            Snake.Add(body);    
>
>            GenerateFood();     // 새로운 음식 생성
>        }
>```
>
> * __뱀 자신의 몸통 충돌 체크__
> ```C#
>// Class Folder/SnakeGame.cs - 162 ~ 173
>
>        // 뱀의 충돌을 체크
>        private void CheckCollision()
>        {
>            for ( int i = 1; i < Snake.Count; i++ )
>            {
>                if ( Snake[ 0 ].X == Snake[ i ].X && Snake[ 0 ].Y == Snake[ i ].Y )
>                {
>                    GameOver();
>                    break;
>                }
>            }
>        }
>```
---
#### __2. 게임 UI__  [<GameUI.cs_Link>](https://github.com/IIBluEll/WinForm_SnakeGame/blob/main/HM_SnakeGame/Class%20Folder/GameUI.cs)
> * __인게임 스크린캡쳐 및 저장 기능__
> ```C#
>// GameUI.cs 49 ~ 86
>
>        // 스크린 샷
>        public void TakeSnapshot()
>        {
>            // 라벨 생성 및 설정
>            Label caption = new Label
>            {
>                Text = "점수 : " + game.Score + " 최고점수 : " + game.HighScore,
>                Font = new Font("Ariel", 12, FontStyle.Bold),
>                ForeColor = Color.Purple,
>                AutoSize = false,
>                Width = picCanvas.Width,
>                Height = 30,
>                TextAlign = ContentAlignment.MiddleCenter
>            };
>
>            // 그림 영역에 캡션 추가
>            picCanvas.Controls.Add(caption);
>
>            // 파일 저장 대화 상자 생성 및 설정
>            SaveFileDialog dialog = new SaveFileDialog
>            {
>                FileName = "HM SnakeGame snapshot", // 파일 이름 기본값 설정
>                DefaultExt = "jpg",                 // 기본 파일 확장자 설정
>                Filter = "JPG Image File | *.jpg",  // 파일 형식 필터 설정
>                ValidateNames = true                // 파일 이름 유효성 검사 활성화
>            };
>
>            // 파일 저장 대화 상자 표시 및 확인 버튼이 눌렸는지 확인
>            if ( dialog.ShowDialog() == DialogResult.OK )
>            {
>                int width = Convert.ToInt32(picCanvas.Width);
>                int height = Convert.ToInt32(picCanvas.Height);
>
>                // 지정된 너비와 높이로 비트맵 객체 생성
>                Bitmap bmp = new Bitmap(width, height);                  
>
>                // 비트맵 객체에 그림 영역의 내용을 그리기
>                picCanvas.DrawToBitmap(bmp , new Rectangle(0 , 0 , width , height));   
>
>                // 비트맵 이미지를 지정된 파일 이름으로 JPEG 형식으로 저장
>                bmp.Save(dialog.FileName , ImageFormat.Jpeg);    
>
>                // 그림 영역에서 캡션 제거       
>                picCanvas.Controls.Remove(caption);                                     
>            }
>        }
>```
> * __뱀과 음식 그래픽 처리__
> ```C#
>// Class Folder/GameUI.cs - 95 ~ 111
>
>        // 뱀과 음식 그래픽 처리
>        public void Draw(Graphics canvas)
>        {
>            Brush snakeColor;
>
>            for ( int i = 0; i < game.Snake.Count; i++ )
>            {
>                // 뱀의 머리는 검정색으로, 나머지 몸통은 짙은 녹색으로 설정
>                snakeColor = i == 0 ? Brushes.Black : Brushes.DarkGreen;
>
>                // 뱀의 각 부분을 타원으로 그림
>                canvas.FillEllipse(snakeColor ,
>                    new Rectangle(game.Snake[ i ].X * Settings.Width , game.Snake[ i ].Y * Settings.Height , Settings.Width , Settings.Height));
>            }
>
>            canvas.FillEllipse(Brushes.DarkRed ,
>                new Rectangle(game.Food.X * Settings.Width , game.Food.Y * Settings.Height , Settings.Width , Settings.Height));
>        }
>
#### __3. 메인 Form 코드__  [<Form1.cs_Link>](https://github.com/IIBluEll/WinForm_SnakeGame/blob/main/HM_SnakeGame/Class%20Folder/Form1.cs)
>````c#
>namespace HM_SnakeGame
>{
>    public partial class Form1 : Form
>    {
>
>        private SnakeGame game;
>        private GameUI gameUI;
>
>        public Form1()
>        {
>            new Settings();
>
>            InitializeComponent();
>            game = new SnakeGame(picCanvas.Width / Settings.Width , picCanvas.Height / Settings.Height);
>            gameUI = new GameUI(picCanvas , txtScore , txtHighScore , startButton , snapButton , gameTimer , game);
>
>            game.OnGameOver += Game_OnGameOver;
>        }
>
>        // 키가 눌렸을 때 호출되는 이벤트 핸들러
>        private void KeyisDown(object sender , KeyEventArgs e)
>        {
>            switch ( e.KeyCode )
>            {
>                case Keys.Left:
>                    game.ChangeDirection("left");
>                    break;
>                case Keys.Right:
>                    game.ChangeDirection("right");
>                    break;
>                case Keys.Up:
>                    game.ChangeDirection("up");
>                    break;
>                case Keys.Down:
>                    game.ChangeDirection("down");
>                    break;
>            }
>        }
>
>        // 키가 떼졌을 때 호출되는 이벤트 핸들러
>        private void KeyisUp(object sender , KeyEventArgs e)
>        {
>            switch ( e.KeyCode )
>            {
>                case Keys.Left:
>                case Keys.Right:
>                case Keys.Up:
>                case Keys.Down:
>                    game.UpdateDirection(false , false , false , false);
>                    break;
>            }
>        }
>
>        // 게임 시작 버튼 클릭 이벤트 핸들러
>        private void StartGame(object sender , EventArgs e)
>        {
>            gameUI.StartGame();
>        }
>
>        // 스냅샷 버튼 클릭 이벤트 핸들러
>        private void TakeSnapSHot(object sender , EventArgs e)
>        {
>            gameUI.TakeSnapshot();
>        }
>
>        // 게임 타이머 이벤트 핸들러
>        private void GameTimerEvent(object sender , EventArgs e)
>        {
>            game.Update();
>            gameUI.UpdateUI();
>            picCanvas.Invalidate(); // 그림 영역 초기화
>        }
>
>        // PictureBox의 페인트 이벤트 핸들러
>        private void UpdatePictureBox(object sender , PaintEventArgs e)
>        {
>            gameUI.Draw(e.Graphics);
>        }
>
>        // 게임 오버 이벤트 핸들러
>        private void Game_OnGameOver(object sender , EventArgs e)
>        {
>            gameTimer.Stop();
>            startButton.Enabled = true;
>            snapButton.Enabled = true;
>
>            
>            MessageBox.Show("게임 오버! 점수 : " + game.Score);
>        }
>    }
>}
>````