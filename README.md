# WinForm_SnakeGame

### 주요 기능 코드 설명

> 뱀 관련 기능
>   > * 뱀 음식 먹음 및 몸통 증가   
>   > ```C#
>   >// Class Folder/SnakeGame.cs - 66 ~ 77
>   >
>   >        // 게임 업데이트 / 뱀의 이동과 충돌 처리
>   >        public void Update()
>   >        {
>   >            MoveSnake();    // 뱀 이동
>   >            // 음식 먹기 체크
>   >            if ( Snake[ 0 ].X == Food.X && Snake[ 0 ].Y == Food.Y )
>   >            {
>   >                EatFood();
>   >            }
>   >
>   >            CheckCollision();   // 충돌 체크
>   >        }
>   >```
>   >```C#
>   >// Class Folder/SnakeGame.cs - 141 ~ 155
>   >
>   >        // 음식을 먹었을 때
>   >        private void EatFood()
>   >        {
>   >            Score++;
>   >
>   >            // 몸체 추가
>   >            Circle body = new Circle
>   >            {
>   >                X = Snake[Snake.Count - 1].X,
>   >                Y = Snake[Snake.Count - 1].Y
>   >            };
>   >            Snake.Add(body);    
>   >
>   >            GenerateFood();     // 새로운 음식 생성
>   >        }
>   >```
>   >
>   > * 뱀 자신의 몸통 충돌 체크
>   > ```C#
>   >// Class Folder/SnakeGame.cs - 162 ~ 173
>   >
>   >        // 뱀의 충돌을 체크
>   >        private void CheckCollision()
>   >        {
>   >            for ( int i = 1; i < Snake.Count; i++ )
>   >            {
>   >                if ( Snake[ 0 ].X == Snake[ i ].X && Snake[ 0 ].Y == Snake[ i ].Y )
>   >                {
>   >                    GameOver();
>   >                    break;
>   >                }
>   >            }
>   >        }
>   >```