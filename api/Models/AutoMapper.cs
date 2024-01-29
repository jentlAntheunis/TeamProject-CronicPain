using AutoMapper;
using Pebbles.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Existing mappings
        CreateMap<Question, QuestionDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.Scale))
            // Include mapping for Answers
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

        CreateMap<Scale, ScaleDTO>();
        CreateMap<Option, OptionDTO>();

        // Mapping for Answer to AnswerDTO
        CreateMap<Answer, AnswerDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
            .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.OptionId))
            .ForMember(dest => dest.OptionContent, opt => opt.MapFrom(src => src.Option.Content)); // Assuming Option.Content exists

        CreateMap<Questionnaire, QuestionnaireDTO>();
        CreateMap<Questionnaire, QuestionnaireDTO2>();
    }
}
