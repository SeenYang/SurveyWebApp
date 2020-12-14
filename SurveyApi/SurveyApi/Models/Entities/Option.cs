using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApi.Models.Entities
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        
        /// <summary>
        /// This Parent Option could be enabled for hierarchy options.
        /// i.e. when select A, for the upcoming options could provide different options set.
        /// </summary>
        public Guid? ParentOptionId { get; set; }
        
        
        /// <summary>
        /// This is the enum id.
        /// Single choice, multiple choice, etc.
        /// For now, all choice could only be single choice.
        /// </summary>
        public int Type { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int OptionType { get; set; }
        
        [ForeignKey("Questions")]
        public Guid QuestionId { get; set; }
        /// <summary>
        /// FK for EF Core
        /// </summary>
        public virtual Question Question { get; set; }
    }
}