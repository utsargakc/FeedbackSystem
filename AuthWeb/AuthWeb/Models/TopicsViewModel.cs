using AuthWeb.Data;

namespace AuthWeb.Models
{
    public class TopicsViewModel
    {
        public IEnumerable<Topics> Topics { get; set; }
        public IEnumerable<Responses> Responses { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<AdditionalFeedback> AdditionalFeedbacks { get; set; }
    }
}
