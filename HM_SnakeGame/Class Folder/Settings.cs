using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_SnakeGame
{
    /// <summary>
    /// 셀의 크기와 초기 방향을 정의하는 기본 설정 클래스입니다.
    /// </summary>

    internal class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static string directions;

        public Settings() 
        {
            Width = 16;
            Height = 16;

            directions = "left";
        }
    }
}
