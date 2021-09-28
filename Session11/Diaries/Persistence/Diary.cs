using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diaries.Persistence
{
    public class Diary
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime DiaryDate { get; set; }
        public string Text { get; set; }
    }
}
