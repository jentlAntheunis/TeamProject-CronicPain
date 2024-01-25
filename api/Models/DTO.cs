namespace Pebbles.Models;


public class QuestionnaireDTO
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public List<QuestionDTO> Questions { get; set; }
}

public class QuestionDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public ScaleDTO Scale { get; set; }
    
}

public class ScaleDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<OptionDTO> Options { get; set; }
}

public class OptionDTO
{
    public Guid Id { get; set; }
    //public ScaleDTO Scale { get; set; }
    //public string Content { get; set; }
    public string Position { get; set; } 
    public string Content { get; set; }
}

public class AnswerInputDTO
{
    public List<AnswerDTO> Answers { get; set; }
    public Guid QuestionnaireId { get; set; }
    public int QuestionnaireIndex { get; set; }
}


public class AnswerDTO
{
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public string OptionContent { get; set; } 
    public int QuestionnaireIndex { get; set; }
}

public class IntOverDaysDTO
{
    public List<DayTDO> Days { get; set; }
}

public class DayTDO
{
    public DateTime Date { get; set; }
    public int Int { get; set; }
}

public class QuestionnaireDetailDTO
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime? Date { get; set; }
    public string CategoryName { get; set; }
    public List<QuestionDetailDTO> Questions { get; set; }
}

public class QuestionDetailDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public List<AnswerDTO> Answers { get; set; }
}