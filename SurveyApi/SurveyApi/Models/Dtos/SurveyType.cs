using System.ComponentModel;

namespace SurveyApi.Models.Dtos
{
    public enum SurveyType
    {
        Default,
        [Description("General")] General
    }
}