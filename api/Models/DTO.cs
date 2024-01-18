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
    public ScaleDTO Scale { get; set; }
    public string Content { get; set; }
    public string Position { get; set; } 
}
