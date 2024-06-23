using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging; 

namespace HM_SnakeGame
{
    public partial class Form1 : Form
    {

        private SnakeGame game;
        private GameUI gameUI;

        public Form1()
        {
            new Settings();

            InitializeComponent();
            game = new SnakeGame(picCanvas.Width / Settings.Width , picCanvas.Height / Settings.Height);
            gameUI = new GameUI(picCanvas , txtScore , txtHighScore , startButton , snapButton , gameTimer , game);

            game.OnGameOver += Game_OnGameOver;
        }

        // 키가 눌렸을 때 호출되는 이벤트 핸들러
        private void KeyisDown(object sender , KeyEventArgs e)
        {
            switch ( e.KeyCode )
            {
                case Keys.Left:
                    game.ChangeDirection("left");
                    break;
                case Keys.Right:
                    game.ChangeDirection("right");
                    break;
                case Keys.Up:
                    game.ChangeDirection("up");
                    break;
                case Keys.Down:
                    game.ChangeDirection("down");
                    break;
            }
        }

        // 키가 떼졌을 때 호출되는 이벤트 핸들러
        private void KeyisUp(object sender , KeyEventArgs e)
        {
            switch ( e.KeyCode )
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    game.UpdateDirection(false , false , false , false);
                    break;
            }
        }

        // 게임 시작 버튼 클릭 이벤트 핸들러
        private void StartGame(object sender , EventArgs e)
        {
            gameUI.StartGame();
        }

        // 스냅샷 버튼 클릭 이벤트 핸들러
        private void TakeSnapSHot(object sender , EventArgs e)
        {
            gameUI.TakeSnapshot();
        }

        // 게임 타이머 이벤트 핸들러
        private void GameTimerEvent(object sender , EventArgs e)
        {
            game.Update();
            gameUI.UpdateUI();
            picCanvas.Invalidate(); // 그림 영역 초기화
        }

        // PictureBox의 페인트 이벤트 핸들러
        private void UpdatePictureBox(object sender , PaintEventArgs e)
        {
            gameUI.Draw(e.Graphics);
        }

        // 게임 오버 이벤트 핸들러
        private void Game_OnGameOver(object sender , EventArgs e)
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            snapButton.Enabled = true;

            
            MessageBox.Show("게임 오버! 점수 : " + game.Score);
        }
    }
}
