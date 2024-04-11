using AuthWeb.Data;
using AuthWeb.Models;
using AuthWeb.Services;
using Azure;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthWeb.Controllers
{
    [Authorize(Roles ="Admin, User")]
    public class FeedbackController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<FeedbackController> _logger;
        public FeedbackController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger<FeedbackController> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        //Admin COntroller methods

        [Authorize(Roles = "Admin")]
        public IActionResult Topics()
        {
            var topics = _db.topics.ToList();
            var responses = _db.responses.ToList();
            var users = _userManager.Users.ToList();
            var model = new TopicsViewModel { Topics = topics, Responses = responses, Users = users };
            return View(model);
        }

        public IActionResult TopicsDetailedView()
        {
            var topics = _db.topics.ToList();
            var responses = _db.responses.ToList();
            var users = _userManager.Users.ToList();
            var model = new TopicsViewModel { Topics = topics, Responses = responses, Users = users };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            var topics = _db.topics.ToList();
            var responses = _db.responses.ToList();
            var users = _userManager.Users.ToList();
            var feedbacks = _db.additionalfeedbacks.ToList();
            var model = new TopicsViewModel { Topics = topics, Responses = responses, Users = users, AdditionalFeedbacks = feedbacks };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult FeedbackInbox()
        {
            var topics = _db.topics.ToList();
            var responses = _db.responses.ToList();
            var users = _userManager.Users.ToList();
            var model = new TopicsViewModel { Topics = topics, Responses = responses, Users = users };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SuggestionInbox()
        {
            var topics = _db.topics.ToList();
            var feedbacks = _db.additionalfeedbacks.ToList();
            var users = _userManager.Users.ToList();
            var model = new TopicsViewModel { Topics = topics, AdditionalFeedbacks = feedbacks, Users = users };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult TopicResponseView(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            IEnumerable<Topics> topicFromDb = _db.topics.Where(t => t.Id == id);

            IEnumerable<Responses> responsesForTopic = _db.responses.Where(r => r.TopicId == id);
            var users = _userManager.Users.ToList();

            TopicsViewModel viewModel = new TopicsViewModel
            {
                Topics = topicFromDb,
                Responses = responsesForTopic,
                Users = users
            };

            return View(viewModel);
        }

        public IActionResult UserResponseView(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IEnumerable<ApplicationUser> userFromDb = _db.Users.Where(t => t.Id == id);
            IEnumerable<Responses> responsesForTopic = _db.responses.Where(r => r.UserId == id);
            IEnumerable<AdditionalFeedback> feedbacks = _db.additionalfeedbacks.Where(f => f.UserId == id);
            var users = _userManager.Users.ToList();

            TopicsViewModel viewModel = new TopicsViewModel
            {
                Users = userFromDb,
                Responses = responsesForTopic,
                AdditionalFeedbacks = feedbacks
            };

            return View(viewModel);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UsersView()
        {
            var topics = _db.topics.ToList();
            var responses = _db.responses.ToList();
            var usersInrole = await _userManager.GetUsersInRoleAsync("User");
            var users = usersInrole.ToList();
            var feedbacks = _db.additionalfeedbacks.ToList();
            var model = new TopicsViewModel { Topics = topics, Responses = responses, Users = users, AdditionalFeedbacks = feedbacks };
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult AddTopics()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTopics(Topics obj)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;

            var data = new Topics()
            {
                Topic = obj.Topic,
                Status = obj.Status,
                EnableRating = obj.EnableRating,
                Created_by = currentUser.Id
            };
            _db.topics.Add(data);
            _db.SaveChanges();
            TempData["success"] = "Topic added sucessfully!!";
            return RedirectToAction("Topics");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditTopics(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Topics TopicfromDb = _db.topics.Find(id);
            if (TopicfromDb == null)
            {
                return NotFound();
            }

            return View(TopicfromDb);
        }

        [HttpPost]
        public IActionResult EditTopics(Topics obj)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var data = new Topics()
            {
                Id = obj.Id,
                Topic = obj.Topic,
                Status = obj.Status,
                EnableRating =obj.EnableRating,
                Created_by = currentUser.Id
            };
            _db.topics.Update(data);
            _db.SaveChanges();
            TempData["success"] = "Topic edited sucessfully!!";
            return RedirectToAction("Topics");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTopics(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Topics TopicfromDb = _db.topics.Find(id);
            if (TopicfromDb == null)
            {
                return NotFound();
            }

            return View(TopicfromDb);
        }


        [HttpPost]
        public IActionResult DeleteTopics(int? id)
        {
            Topics topic = _db.topics.Find(id);
            if (topic == null)
            {
                return NotFound(id);
            }

            _db.topics.Remove(topic);
            _db.SaveChanges();
            TempData["success"] = "Topic deleted sucessfully!!";
            return RedirectToAction("Topics");
        }

        //Scheduling Topics
        public IActionResult ChangeSchedule(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Topics TopicfromDb = _db.topics.Find(id);
            if (TopicfromDb == null)
            {
                return NotFound();
            }

            return View(TopicfromDb);
        }

        [HttpPost]
        public IActionResult ChangeSchedule(Topics obj, int repeatingNumber, List<string> selectedDays)
        {
            try
            {
                _logger.LogInformation("ChangeSchedule method invoked.");
                //string recurrence = Request.Form["recurrence"];
                int repeating_number = repeatingNumber;
                var selected_days = selectedDays;
                string days = string.Join(",", selected_days);
                Console.WriteLine(days);
                switch (obj.recurrenceType)
                {
                    case "daily":
                        Console.WriteLine("daily");
                        string cronExpressionDaily = $"0 0 */{repeating_number} * *";
                        RecurringJob.AddOrUpdate($"{obj.Id}", () => MakeActive(obj, days), cronExpressionDaily);
                        TempData["success"] = "Schedule updated sucessfully!!";
                        break;
                    case "weekly":
                        string cronExpressionWeekly = string.Empty;
                        if (selected_days.Any())
                        {
                            int selectedDayIndex = Convert.ToInt32(selected_days.First());
                            int offset = (selectedDayIndex + repeatingNumber - 2) % 7;
                            cronExpressionWeekly = $"0 0 * * {days}#{offset}";
                        }
                        //else
                        //{
                        //    cronExpressionWeekly = $"0 0 * * 0";
                        //}
                        RecurringJob.AddOrUpdate($"{obj.Id}", () => MakeActive(obj, days), cronExpressionWeekly);
                        Console.WriteLine("weekly");
                        TempData["success"] = "Schedule updated successfully!!";
                        break;
                    case "monthly":
                        string cronExpressionMonthly = $"0 0 {obj.dayOfMonth} */{repeating_number} *";
                        RecurringJob.AddOrUpdate($"{obj.Id}", () => MakeActive(obj, days), cronExpressionMonthly);
                        Console.WriteLine("monthly");
                        TempData["success"] = "Schedule updated sucessfully!!";
                        break;
                    case "yearly":
                        string cronExpressionYearly = $"0 0 1 1 1/{repeating_number}";
                        RecurringJob.AddOrUpdate($"{obj.Id}", () => MakeActive(obj, days), cronExpressionYearly);
                        Console.WriteLine("yearly");
                        TempData["success"] = "Schedule updated sucessfully!!";
                        break;
                }

                //if (recurrence == "Minutely")
                //{
                //    RecurringJob.AddOrUpdate($"{obj.Id}", () => MakeActive(obj, days), Cron.Minutely());
                //}
                //else if (recurrence == "Every2minutes")
                //{
                //    RecurringJob.AddOrUpdate($"{obj.Id}", () => MakeActive(obj, days), Cron.MinuteInterval(2));

                //}

                if (obj.recurrenceType != null)
                {
                    var data = new Topics()
                    {
                        Id = obj.Id,
                        Topic = obj.Topic,
                        Status = obj.Status,
                        EnableRating = obj.EnableRating,
                        startDate = DateOnly.MinValue,
                        endDate = DateOnly.MinValue,
                        repeatingNumber = obj.repeatingNumber,
                        recurrenceType = obj.recurrenceType,
                        dayOfMonth = obj.dayOfMonth,
                        selectedDays = days,
                        Created_by = obj.Created_by
                    };
                    _db.topics.Update(data);
                    _db.SaveChanges();
                }
                else
                {
                    var data = new Topics()
                    {
                        Id = obj.Id,
                        Topic = obj.Topic,
                        Status = obj.Status,
                        EnableRating = obj.EnableRating,
                        startDate = obj.startDate,
                        endDate = obj.endDate,
                        repeatingNumber = obj.repeatingNumber,
                        recurrenceType = obj.recurrenceType,
                        dayOfMonth = obj.dayOfMonth,
                        selectedDays = days,
                        Created_by = obj.Created_by
                    };
                    TempData["success"] = "Custom Period updated sucessfully!!";
                    _db.topics.Update(data);
                    _db.SaveChanges();
                    RecurringJob.RemoveIfExists($"{obj.Id}");
                }
                _logger.LogInformation("ChangeSchedule method execution completed successfully.");
                return RedirectToAction("Topics");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in ChangeSchedule method.");
                return View();
            }
        }

        //private DateTime CalculateNextExecution(DateTime currentTime, int selectedDayOfWeek, int repeatingNumber)
        //{
        //    // Calculate days until next selected day
        //    int daysUntilNextSelectedDay = (selectedDayOfWeek - (int)currentTime.DayOfWeek + 7) % 7;

        //    // Calculate days to add based on the repeating number
        //    int daysToAdd = daysUntilNextSelectedDay + (7 * (repeatingNumber - 1));

        //    // Calculate next execution time
        //    DateTime nextExecutionTime = currentTime.Date.AddDays(daysToAdd);

        //    return nextExecutionTime;
        //}

        public void MakeActive( Topics obj, string days)
        {

            var data = new Topics()
            {
                Id = obj.Id,
                Topic = obj.Topic,
                Status = "Active",
                EnableRating = obj.EnableRating,
                startDate = DateOnly.MinValue,
                endDate = DateOnly.MinValue,
                repeatingNumber = obj.repeatingNumber,
                recurrenceType = obj.recurrenceType,
                dayOfMonth = obj.dayOfMonth,
                selectedDays = days,
                Created_by = obj.Created_by
            };
            _db.topics.Update(data);
            _db.SaveChanges();

            BackgroundJob.Schedule(() => MakeInactive(data, days), TimeSpan.FromHours(19));
        }
        
        public void MakeInactive(Topics obj, string days)
        {
            var data = new Topics()
            {
                Id = obj.Id,
                Topic = obj.Topic,
                Status = "Inactive",
                EnableRating = obj.EnableRating,
                repeatingNumber = obj.repeatingNumber,
                recurrenceType = obj.recurrenceType,
                dayOfMonth = obj.dayOfMonth,
                selectedDays = days,
                Created_by = obj.Created_by
            };
            _db.topics.Update(data);
            _db.SaveChanges();
        }



        //Users Controller methods
        //Feedback
        public IActionResult GiveFeedback()
        {
            var topics = _db.topics.ToList();
            var model = new TopicsViewModel { Topics = topics };
            return View(model);
        }

        [HttpPost]
        public IActionResult GiveFeedback(Dictionary<int, string> responses,Dictionary<int, int> ratings,AdditionalFeedback obj)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (responses == null || responses.Count == 0)
            {
                ModelState.AddModelError("", "No responses were submitted.");
            }

            bool addedResponse = false;
            foreach (var topicId in responses.Keys)
            {
                var value = responses.Values.ToList();
                Console.WriteLine(value);
                if (responses[topicId] != null)
                {
                    addedResponse = true;
                    var feedbackResponse = new Responses
                    {
                        TopicId = topicId,
                        UserId = currentUser.Id,
                        CreatedOn = DateTime.Now,
                        Response = responses[topicId],
                    };
                    _db.responses.Add(feedbackResponse);
                    if (ratings != null && ratings.TryGetValue(topicId, out int rating))
                    {
                        feedbackResponse.Rating = rating;
                    }
                }
            }
            if (addedResponse == true)
            { 
                _db.SaveChanges();
                TempData["success"] = "Thank you for your feedback!";
            }
            
            var topics = _db.topics.ToList();
            var model = new GiveFeedbackViewModel { Topics = topics };
            return RedirectToAction("GiveFeedback");
            
        }
        public IActionResult AdditionalFeedback()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdditionalFeedback(AdditionalFeedback obj)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var data = new AdditionalFeedback()
            {
                Feedback = obj.Feedback,
                SubmittedOn = DateTime.Now,
                UserId = currentUser.Id,
            };
            if (data.Feedback != null)
            {
                _db.additionalfeedbacks.Add(data);
                _db.SaveChanges();
                TempData["success"] = "Thank you for your feedback!";
            }
            return View();
        }

        public IActionResult MyResponses()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var topics = _db.topics.ToList();
            var responses = _db.responses.Where(r => r.UserId == currentUser.Id).ToList();
            var users = _userManager.Users.ToList();
            var model = new TopicsViewModel { Topics = topics, Responses = responses, Users = users };
            return View(model);
        }
        [HttpPost]
        public IActionResult MyResponses(string a)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            int responseID = int.Parse(Request.Form["responseID"]);
            string response = Request.Form["response"];

            var existingResponse = _db.responses.FirstOrDefault(r => r.Id == responseID);
            if (existingResponse != null)
            {
                existingResponse.Response = response;
                existingResponse.isEdited = "True";
                _db.responses.Update(existingResponse);
                _db.SaveChanges();
                TempData["success"] = "Response edited successfully!";
            }
            else
            {
                TempData["faliure"] = "Response not found!";
            }
            return RedirectToAction("MyResponses");
        }

        public IActionResult MySuggestions()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var topics = _db.topics.ToList();
            var feedbacks = _db.additionalfeedbacks.Where(f => f.UserId == currentUser.Id).ToList();
            var users = _userManager.Users.ToList();
            var model = new TopicsViewModel { Topics = topics, AdditionalFeedbacks = feedbacks, Users = users };
            return View(model);
        }

        [HttpPost]
        public IActionResult MySuggestions(string a)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            int feedbackID = int.Parse(Request.Form["feedbackID"]);
            string feedback = Request.Form["feedback"];

            var existingFeedback = _db.additionalfeedbacks.FirstOrDefault(f => f.Id == feedbackID);
            if (existingFeedback != null)
            {
                existingFeedback.Feedback = feedback;
                existingFeedback.isEdited = "True";
                _db.additionalfeedbacks.Update(existingFeedback);
                _db.SaveChanges();
                TempData["success"] = "Suggestion edited successfully!";
            }
            else
            {
                TempData["faliure"] = "Suggestion not found!";
            }
            return RedirectToAction("MySuggestions");
        }
    }
}
