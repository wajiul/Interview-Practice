using INTERVIEW.Data;
using INTERVIEW.Models;
using INTERVIEW.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTERVIEW.Controllers
{
    public class TestController : Controller
    {
        private readonly DataRepository _dataRepository; 
        public TestController(DataRepository dataRepository) 
        {
            _dataRepository = dataRepository; 
        }
        public IActionResult Index(string course)
        {
            var data = _dataRepository.GetQuestion(course, 10);
            
            return View(data); 
        }
        public IActionResult TestList()
        {
            var testCount = _dataRepository.GetTestCount();
            foreach (var test in testCount)
            {
                ViewData[test.Course] = test.Frequency;
            }
            return View();
        }
        [HttpPost]
        public void PostQuestion(string questions)
        {
            var data = JsonConvert.DeserializeObject<List<QuestionModel>>(questions); 
            if (data.Count > 0)
            {
                _dataRepository.updateQuestion(data); 
            }

        }

        public void CountTest(string currentCourse)
        {
            _dataRepository.IncrementTestCount(currentCourse);
        }

        [HttpGet]
        public IActionResult AddQuestions()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddQuestions(string course, string QList)
        {
            string[] Q = QList.Split('\n');
            string[] lines = QList.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var newQuestions = new List<QuestionModel>();
            for(int i = 0;i < lines.Length;i++)
            {
                if(!string.IsNullOrWhiteSpace(lines[i]))
                {
                    newQuestions.Add(new QuestionModel()
                    {
                        Course = course,
                        Question = lines[i],
                        Rank = 1
                    }) ;

                }
            }

            _dataRepository.AddNewQuestions(newQuestions);
            return View();
        }

        [HttpGet]
        public IActionResult ShowQuestion(string tmp = "tmp", string Course = null)
        {

            var data = _dataRepository.GetAllQuestion(Course);
            return View(data);
        }

        [HttpPost]
        public IActionResult ShowQuestion(string Course)
        {
            return RedirectToAction(nameof(ShowQuestion), new { tmp = "no", Course = Course });
           
        }
        public IActionResult Edith(int Id)
        {
            var data = _dataRepository.GetQuestionById(Id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edith(QuestionModel questionModel)
        {
            _dataRepository.UpdateSingleQuestion(questionModel);
            return RedirectToAction(nameof(ShowQuestion));
        }
        public IActionResult Delete(string Question, string Course)
        {
            _dataRepository.DeleteQuestion(Question);
            return RedirectToAction(nameof(ShowQuestion), new { tmp="any", Course = Course});
        }

        public IActionResult Reset(string course)
        {
            if(course != null)
            {
                _dataRepository.ResetAllQuestionRank(course);
            }
            return View();
        } 
    }
}
