using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYJ_WatchDog.Models
{
    public class Settings
    {
        public int CheckTick { get; set; }   // 체크 주기(초)
        public bool IsAuto { get; set; }    // 자동시작 여부
        public bool KillNotRespond { get; set; } // 응답없음 킬 여부
        public List<TaskInfo> TaskList { get; set; }    // 감시대상 프로그램 목록
    }
}
