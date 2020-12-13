using System;

namespace SurveyApi.Models.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid OptionId { get; set; }
        public Guid SurveyId { get; set; }

        public DateTime CreatedDateUtc { get; set; }
        
        /// <summary>
        /// In case there's question is asking for text input instead of multiple-choice.
        /// </summary>
        public string TextAnswer { get; set; }
    }
}