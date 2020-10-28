using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYJ_WatchDog.Models
{
    public class TaskInfo
    {
        public string Name { get; set; }        // 프로그램명
        public string Desc { get; set; }        // 설명
        public string FilePath { get; set; }    // 실행파일경로
        public string MemorySize { get; set; }    // 메모리 크기
        public string StartTime { get; set; } // 시작시각
        public string PID { get; set; }    // PID
        public bool IsResponse { get; set; }    // 응답여부
        public bool IsCheck { get; set; }       // 선택여부
        public bool IsRun { get; set; }         // 실행여부
        public string Node { get; set; }        // 핸들을 찾기 위한 노드

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Handle { get; set; }

    }
}
