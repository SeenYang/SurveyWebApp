using System;

namespace SurveyApi.Models.Entities
{
    public class QuestionAnswer
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? OptionId { get; set; }

        /// <summary>
        /// In case there's question is asking for text input instead of multiple-choice.
        /// </summary>
        public string TextAnswer { get; set; }

        public virtual Answer Answer { get; set; }
    }
}