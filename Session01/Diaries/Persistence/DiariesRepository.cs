using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diaries.Persistence
{
    public static class DiariesRepository
    {
        private static readonly List<Diary> AllDiaries = new List<Diary>();
        static DiariesRepository()
        {
            AllDiaries.AddRange(DiaryFakeDataFactory.CreateDiariesForUser(88421113, 5));    
            AllDiaries.AddRange(DiaryFakeDataFactory.CreateDiariesForUser(818727, 5));    
        }

        public static List<Diary> GetAllDiariesOfUser(long userId)
        {
            return AllDiaries.Where(a => a.UserId == userId).OrderByDescending(a=> a.DiaryDate).ToList();
        }

        public static Diary GetDiaryEntry(long userId, int entryId)
        {
            return AllDiaries.First(a => a.UserId == userId && a.Id == entryId);

        }
    }
}
