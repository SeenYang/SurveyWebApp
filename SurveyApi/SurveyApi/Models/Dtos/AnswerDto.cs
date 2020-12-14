using System;
using System.Collections.Generic;

namespace SurveyApi.Models.Dtos
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SurveyId { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public List<QuestionAnswerDto> QuestionAnsers { get; set; }
    }

    public class QuestionAnswerDto
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? OptionId { get; set; }

        /// <summary>
        /// In case there's question is asking for text input instead of multiple-choice.
        /// </summary>
        public string TextAnswer { get; set; }
    }
}