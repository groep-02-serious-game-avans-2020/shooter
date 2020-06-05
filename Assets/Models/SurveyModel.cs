using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public class SurveyModel
    {
        public String _ID { get; set; }
        public String title { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public String resultUrl { get; set; }
    }
}
