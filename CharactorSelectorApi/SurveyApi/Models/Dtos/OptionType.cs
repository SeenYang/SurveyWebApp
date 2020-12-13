using System.ComponentModel;

namespace SurveyApi.Models.Dtos
{
    public enum OptionType
    {
        Default,
        [Description("General")] General,
        [Description("SingleChoice")] SingleChoice,
        [Description("MultipleChoice")] MultipleChoice,
        [Description("FreeText")] FreeText,
        
    }
}