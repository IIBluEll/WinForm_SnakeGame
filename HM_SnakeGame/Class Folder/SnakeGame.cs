using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_SnakeGame
{
    /// <summary>
    /// 게임의 상태와 로직을 관리합니다.
    /// 뱀의 위치, 음식의 위치, 점수, 게임 오버 상태 등을 포함합니다.
    /// </summary>
    
    internal class SnakeGame
    {
        public List<Circle> Snake { get; private set; } // 뱀을 구성하는 Circle 객체 리스트
        public Circle Food { get; private set; }        // 음식 객체

        public int Score { get; private set; }
        public int HighScore { get; private set; }

        public string Direction { get; private set; }

        private int maxWidth;
        private int maxHeight;

        private Random rand = new Random();

        private bool goLeft, goRight, goUp, goDown;

        public SnakeGame(int width, int height)
        {
            Snake = new List<Circle>();
            Food = new Circle();
            rand = new Random();

            maxHeight = height;
            maxWidth = width;
            Direction = Settings.directions;

            ResetGame();
        }

        // 게임을 초기화
        public void ResetGame()
        {
            Snake.Clear();

            Score = 0;
            Direction = Settings.directions;

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);    //뱀 리스트에 머리 추가

            // 뱀의 몸체 초기화
            for ( int i = 0; i < 10; i++ )
            {
                Circle body = new Circle();
                Snake.Add(body); 
            }

            GenerateFood(); // 음식 생성
        }

        // 게임 업데이트 / 뱀의 이동과 충돌 처리
        public void Update()
        {
            MoveSnake();    // 뱀 이동

            // 음식 먹기 체크
            if ( Snake[ 0 ].X == Food.X && Snake[ 0 ].Y == Food.Y )
            {
                EatFood();
            }

            CheckCollision();   // 충돌 체크
        }

        // 뱀의 이동 방향을 변경
        public void ChangeDirection(string direction)
        {
            // 현재 이동 방향의 반대 방향으로 이동하지 않도록 함
            if ( ( direction == "left" && Direction != "right" ) ||
                ( direction == "right" && Direction != "left" ) ||
                ( direction == "up" && Direction != "down" ) ||
                ( direction == "down" && Direction != "up" ) )
            {
                Direction = direction;
            }
        }

        // 키 입력에 따라 방향 업데이트
        public void UpdateDirection(bool left , bool right , bool up , bool down)
        {
            goLeft = left;
            goRight = right;
            goUp = up;
            goDown = down;

            if ( goLeft ) Direction = "left";
            if ( goRight ) Direction = "right";
            if ( goUp ) Direction = "up";
            if ( goDown ) Direction = "down";
        }

        private void MoveSnake()
        {
            for ( int i = Snake.Count - 1; i >= 0; i-- )
            {
                if ( i == 0 )
                {
                    switch ( Direction )
                    {
                        case "left":
                            Snake[ i ].X--;
                            break;
                        case "right":
                            Snake[ i ].X++;
                            break;
                        case "up":
                            Snake[ i ].Y--;
                            break;
                        case "down":
                            Snake[ i ].Y++;
                            break;
                    }

                    if ( Snake[ i ].X < 0 ) Snake[ i ].X = maxWidth;
                    if ( Snake[ i ].X > maxWidth ) Snake[ i ].X = 0;
                    if ( Snake[ i ].Y < 0 ) Snake[ i ].Y = maxHeight;
                    if ( Snake[ i ].Y > maxHeight ) Snake[ i ].Y = 0;
                }
                else
                {
                    Snake[ i ].X = Snake[ i - 1 ].X;
                    Snake[ i ].Y = Snake[ i - 1 ].Y;
                }
            }
        }

        // 음식을 먹었을 때
        private void EatFood()
        {
            Score++;

            // 몸체 추가
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);    

            GenerateFood();     // 새로운 음식 생성
        }

        private void GenerateFood()
        {
            Food = new Circle { X = rand.Next(2 , maxWidth) , Y = rand.Next(2 , maxHeight) };
        }

        // 뱀의 충돌을 체크
        private void CheckCollision()
        {
            for ( int i = 1; i < Snake.Count; i++ )
            {
                if ( Snake[ 0 ].X == Snake[ i ].X && Snake[ 0 ].Y == Snake[ i ].Y )
                {
                    GameOver();
                    break;
                }
            }
        }

        private void GameOver()
        {
            if ( Score > HighScore )
            {
                HighScore = Score;
            }

            OnGameOver?.Invoke(this , EventArgs.Empty);
        }

        // 게임 오버 이벤트
        public event EventHandler OnGameOver;
    }
}
