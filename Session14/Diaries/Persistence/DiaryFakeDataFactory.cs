using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;

namespace Diaries.Persistence
{
    public static class DiaryFakeDataFactory
    {
        public static List<Diary> CreateDiariesForUser(long userId, int count)
        {
            return Builder<Diary>.CreateListOfSize(count)
                .All()
                .With(a=> a.Id = Faker.RandomNumber.Next(1, 999999))
                .With(a => a.UserId = userId)
                .With(a=> a.DiaryDate = DateTime.Now.AddDays(Faker.RandomNumber.Next(-356, -1)))
                .With(a=> a.Text = Faker.Lorem.Paragraph())
                .Build()
                .ToList();
        }
    }
}
