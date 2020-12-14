using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SurveyApi.Models.Entities;

namespace SurveyApi.Models.Dtos
{
    public class QuestionDto
    {
        [Required]
        public Guid Id { get; set; }
        
        // This is the display order. will be sorted before return.
        public int Order { get; set; }
        
        public DateTime CreatedDateUtc { get; set; }
        
        [Required]
        [StringLength(255, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Title { get; set; }
        
        public string SubTitle { get; set; }
        
        public int QuestionType { get; set; }
        
        [Required]
        public Guid SurveyId { get; set; }

        public List<OptionDto> Options { get; set; }
    }
}