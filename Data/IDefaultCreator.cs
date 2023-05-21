namespace ExperimentABP.Data
{
    public interface IDefaultCreator
    {
        /// <summary>
        /// Метод для создания дефолтных таблиц.
        /// </summary>
        void DefaultCreateTables();
        /// <summary>
        /// Метод для наполнения таблицы дефолтными значениями.
        /// </summary>
        void СreateDefoltsValueTables();
        /// <summary>
        /// Очистка дефолтных таблиц.
        /// </summary>
        void RemoveTablets();
        


    }
}
