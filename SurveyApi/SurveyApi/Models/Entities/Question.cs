using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models.Entities
{
    public class Question
    {
        [Key] public Guid Id { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int QuestionType { get; set; }

        /// <summary>
        /// EF Core FK
        /// </summary>
        public virtual Survey Survey { get; set; }
        [ForeignKey("Surveys")] public Guid SurveyId { get; set; }
        
        /// <summary>
        /// The author of this survey.
        /// </summary>
        public Guid? UserId { get; set; }

        public string UserName { get; set; }
        
        public virtual List<Option> Options { get; set; }
    }
}