using INTERVIEW.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using INTERVIEW.Data;

namespace INTERVIEW.Repository
{
    public class DataRepository
    {
        private readonly MainDbContext _context;
        public DataRepository(MainDbContext context)
        {
            _context = context;
        }

        public List<QuestionModel> GetQuestion(string course, int number)
        {
            var data = _context.questions.Where(w => w.Course == course).OrderByDescending(x => x.Rank).Take(number + number/2).ToList();
            var shuffledList = data.OrderBy(_ => Guid.NewGuid()).Take(number).ToList();
            return shuffledList;
        }
        public QuestionModel GetQuestionById(int Id)
        {
            return _context.questions.Find(Id);
        }
        public void UpdateSingleQuestion(QuestionModel question)
        {
            var data = _context.questions.Find(question.Id);
            data.Question = question.Question;
            data.Rank = question.Rank;
            data.Course = question.Course;
            _context.SaveChanges();
        }

        public void updateQuestion(List<QuestionModel> questionList)
        {
           
            var curData = _context.questions.Where(d=>d.Course == questionList[0].Course).ToList();
            Dictionary<string, int> Map = new Dictionary<string, int>();
            foreach(var q in questionList)
            {
                //Map[q.Question] = q.Rank;
                var exist = _context.questions.FirstOrDefault(f=>f.Question == q.Question);
                if(exist != null)
                {
                    exist.Rank = q.Rank;
                    _context.SaveChanges();
                }

            }
            //foreach (var item in curData)
            //{

            //    if (Map[item.Question] != 0)
            //    {
            //        item.Rank = Map[item.Question];
            //        _context.SaveChanges();
            //    }
            //}
        }

        public void AddNewQuestions(List<QuestionModel> newQuestionList)
        {
            foreach(var Q in newQuestionList)
            {
                var exists = _context.questions.FirstOrDefault(f => f.Question == Q.Question);
                if(exists == null)
                {
                    _context.questions.Add(Q);
                }
            }
            _context.SaveChanges();
        }

        public List<QuestionModel> GetAllQuestion(string Course)
        {
            if(Course == null)
            {
                return _context.questions.ToList();
            }
            return _context.questions.Where(w => w.Course == Course).OrderByDescending(o => o.Rank).ToList();
        }

        public void DeleteQuestion(string Question)
        {
            var exist = _context.questions.FirstOrDefault(q => q.Question == Question);
            if(exist != null)
            {
                _context.questions.Remove(exist);
                _context.SaveChanges();
            }
        }

        public void ResetAllQuestionRank(string Course) 
        {
            var data = _context.questions.Where(q => q.Course == Course).ToList();
            foreach(var item in data)
            {
                item.Rank = 1;
            }
            _context.SaveChanges();
        }
        
        public void IncrementTestCount(string Course)
        {
            var exist = _context.testCounts.FirstOrDefault(f => f.Course == Course);
            if(exist != null)
            {
                exist.Frequency += 1;
                _context.SaveChanges();
            }
        }

        public List<TestCount> GetTestCount()
        {
            return _context.testCounts.ToList();
        }

    }
}
