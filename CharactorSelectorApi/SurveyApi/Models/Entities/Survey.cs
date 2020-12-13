using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApi.Models.Entities
{
    /// <summary>
    /// This is the entity for Survey.
    /// Survey to Questions are 1-to-many.
    /// </summary>
    public class Survey
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }

        /// <summary>
        /// The author of this survey.
        /// </summary>
        public Guid? UserId { get; set; }

        public string UserName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}