using AutoMapper;
using Pebbles.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Question, QuestionDTO>();
        CreateMap<Scale, ScaleDTO>();
        CreateMap<Option, OptionDTO>();
        CreateMap<Questionnaire, QuestionnaireDTO>();
    }
}
