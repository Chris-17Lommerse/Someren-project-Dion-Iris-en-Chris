using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Drinks
{
    public interface IDrinksRepository
    {
        List<Drink> GetAll();
        Drink? GetById(int drankId);
        void Add(Drink drink);
        void Update(Drink drink);
        void Delete(Drink drink);
    }
}
