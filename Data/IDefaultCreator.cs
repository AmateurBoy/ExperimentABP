namespace ExperimentABP.Data
{
    /// <summary>
    /// Опціональний інтерфейс для виконнання створення та видалення таблиць БД
    /// </summary>
    public interface IDefaultCreator
    {
        /// <summary>
        /// Метод для створення дефолтных таблиц.
        /// </summary>
        void DefaultCreateTables();
        /// <summary>
        /// Метод для наповнення таблиці дефолтными значеннями.
        /// </summary>
        void СreateDefoltsValueTables();
        /// <summary>
        /// Видалення дефолтных таблиц.
        /// </summary>
        void DeleteTables();
        


    }
}
