using ExperimentABP.Entitys;

namespace ExperimentABP.Services
{
    /// <summary>
    /// Сервіс для виконнання бізнес логіки проекту.
    /// </summary>
    public interface IDeterminantService
    {

        /// <summary>
        /// Класифікатор призначає одну зі списку опцій, які перейшли в аркуші.
        /// </summary>
        /// <param name="nameExperiment">Ім'я експеременту</param>
        /// <param name="list">лист з опціями</param>
        /// <returns></returns>
        Option Classification(string nameExperiment, List<Option> list);
        /// <summary>
        /// Визначає повернути результат із БД або створити новий запис
        /// </summary>
        /// <param name="nameExperiment">Ім'я експеременту</param>
        /// <param name="token">Токен девайсу</param>
        /// <returns></returns>
        Option QueryExperiment(string nameExperiment, string token);
        /// <summary>
        /// Lazy Loading<br></br>
        /// Віддача ДТО з необхідним результатом для статистики.
        /// </summary>
        /// <returns>Колекція з результатами</returns>
        public List<ResultAllDTO> GetStatistic();
    }
}