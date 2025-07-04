using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace cp_journey_backend.Entities;

public class Problem {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(10)]
    public string Label { get; set; }
    
    public int Order { get; set; }

    public byte[]? StatementPdf { get; set; }
    
    public int ContestId { get; set; }
    public Contest Contest { get; set; }
     
    public int? SetterId { get; set; }
    public Person? Setter { get; set; }
    
    public List<Submission> Submissions { get; set; }
    
}