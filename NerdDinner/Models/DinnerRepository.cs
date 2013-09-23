namespace NerdDinner.Models
{
    public interface IDinnerRepository : IGenericRepository<Dinner>
    {
    }

    public class DinnerRepository :
        GenericRepository<NerdDinnerContext, Dinner>, IDinnerRepository
    {
    }
}