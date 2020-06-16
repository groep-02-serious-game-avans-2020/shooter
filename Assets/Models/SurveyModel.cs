using System;
using System.Collections.Generic;

namespace Assets.Models
{
    [Serializable]
    public class SurveyModel
    {
        public string _id;
        public string title;
        public List<QuestionModel> questions;
    }
}
