using AbideChallenge.Models;
using System.Web.Mvc;
using AbideChallenge.Questions.Abstract;
using AbideChallenge.Repositories.Abstract;
using System.Collections.Generic;
using AbideChallenge.Domain;

namespace AbideChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IQuestionService _questionService;

        public HomeController()
        { }

        public HomeController(IPrescriptionRepository prescriptionRepository, IPracticeRepository practiceRepository, IQuestionService questionService)
        {
            _prescriptionRepository = prescriptionRepository;
            _practiceRepository = practiceRepository;
            _questionService = questionService;
        }

        public ActionResult Index()
        {
            // Read the files in here. They get stored in application cache so shouldn't need to 
            // be read again during the session.
            _practiceRepository.LoadPractices();
            _prescriptionRepository.LoadPrescriptions();

            return View("Question1");
        }

        [HttpGet]
        public ActionResult Question1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question1Answer()
        {
            Question1ViewModel model = new Question1ViewModel()
            {
                PracticeCount = _questionService.Question1()
            };

            return View("Question1", model);
        }

        [HttpGet]
        public ActionResult Question2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question2Answer()
        {
            Question2ViewModel model = new Question2ViewModel()
            {
                AverageCost = _questionService.Question2()
            };

            return View("Question2", model);
        }


        [HttpGet]
        public ActionResult Question3()
        {
            Question3ViewModel model = new Question3ViewModel()
            {
                Rows = new List<Question3Row>()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Question3Answer()
        {
            Question3ViewModel model = new Question3ViewModel()
            {
                Rows = _questionService.Question3()
            };

            return View("Question3", model);
        }


        [HttpGet]
        public ActionResult Question4()
        {
            Question4ViewModel model = new Question4ViewModel()
            {
                Rows = new List<Question4Row>()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Question4Answer()
        {
            Question4ViewModel model = new Question4ViewModel()
            {
                Rows = _questionService.Question4()
            };

            return View("Question4", model);
        }


        [HttpGet]
        public ActionResult Question5()
        {
            Question5ViewModel model = new Question5ViewModel()
            {
                Rows = new List<Question5Row>()
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Question5Answer()
        {
            Question5ViewModel model = new Question5ViewModel()
            {
                Rows = _questionService.Question5()
            };

            return View("Question5", model);
        }

    }
}