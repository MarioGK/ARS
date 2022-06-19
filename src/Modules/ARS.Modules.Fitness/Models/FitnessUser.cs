using ARS.Common.Attributes;
using ARS.Common.Models;
using ARS.Store.Common.Data;

namespace ARS.Module.Fitness.Models;

[AutoOptions("Aluno")]
public class FitnessUser : User
{
    [AutoOptions(true, false,"Treino")]
    public ExerciseBundle ExerciseBundle { get; set; }

    public List<FitnessMessage>? Messages { get; set; }

    [AutoOptions(true, false, "Pagamento")]
    public Subscription Subscription { get; set; } = new();

    [AutoOptions(true, true,"Status")]
    public FitnessUserStatus Status { get; set; }
}