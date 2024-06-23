using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_SnakeGame
{
    /// <summary>
    /// 뱀의 머리 또는 각 몸통과 음식의 좌표를 위한 클래스입니다.
    /// </summary>

    internal class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Circle()
        {
            X = 0;
            Y = 0;
        }
    }
}
