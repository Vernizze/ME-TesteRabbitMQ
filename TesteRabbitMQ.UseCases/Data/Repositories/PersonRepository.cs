using TesteRabbitMQ.UseCases.Data.Entities;
using TesteRabbitMQ.UseCases.Data.Repositories.Base;

namespace TesteRabbitMQ.UseCases.Data.Repositories
{
    public class PersonRepository
        : BaseRepository<Person>
    {
        #region Variables

        private static PersonRepository _instance;

        #endregion

        #region Constructors

        private PersonRepository() { }

        #endregion

        #region Attributes

        public static PersonRepository Instance
        {
            get
            {
                _instance = _instance ?? new PersonRepository();


                return _instance;
            }
        }

        #endregion
    }
}
