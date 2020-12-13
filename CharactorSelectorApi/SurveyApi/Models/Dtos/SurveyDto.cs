using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApi.Models.Dtos
{
    public class SurveyDto
    {
        [Required] public Guid Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "{0} length can not loger than {1}.")]
        public string Description { get; set; }

        public SurveyType Type { get; set; }

        /// <summary>
        /// This is the Author ID
        /// </summary>
        public Guid? UserId { get; set; }

        public string UserName { get; set; }
        
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}