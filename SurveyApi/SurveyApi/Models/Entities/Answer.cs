using System;
using System.Collections.Generic;

namespace SurveyApi.Models.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SurveyId { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        public virtual List<QuestionAnswer> QuestionAnsers { get; set; }
    }
}