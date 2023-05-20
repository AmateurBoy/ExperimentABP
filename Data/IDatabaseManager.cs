using ExperimentABP.Entitys;

namespace ExperimentABP.Data
{
    /// <summary>
    /// Необходимые методы для решения задачи.<br></br> 
    /// Создание новых девайсов, Create<br></br> 
    /// Создание назначение опций для девайсов, Create<br></br> 
    /// Отдача дивайсов, Read<br></br> 
    /// Отдача одной назначеной опции для девайса, Read<br></br> 
    /// Отдача Результатирующей таблици Read<br></br> 
    /// Для удобстав разработки, метод  для пересоздания таблиц.<br></br> 
    /// </summary>
    public interface IDatabaseManager
    {
        /// <summary>
        /// Для удобстав разработки, метод  для пересоздания таблиц.
        /// </summary>
        void RecreateDefoltsValueTables();
        /// <summary>
        /// Create<br></br>
        /// Создать новое устройство если такого еще нет.<br></br>
        /// Если устройсто уже есть, вернет существующею сущность.
        /// </summary>
        /// <param name="diveceToken">diveceToken для создания сущности User</param>
        /// <returns>Возвращает сущность User</returns>
        User CreateDevice(string diveceToken);
        /// <summary>
        /// Create<br></br>
        /// Присвоение устройству назначеной опции<br></br>
        /// </summary>
        /// <param name="user">DTO клас для описания сущности.</param>
        /// <param name="option">DTO клас для описания сущности.</param>
        /// <returns>Возвращает id device</returns>
        UserOptions CreateUserOption(User user, Option option);
        /// <summary>
        /// Получить из БД сущность Experiment
        /// </summary>
        /// <param name="id">id сущности</param>
        /// <returns>Возваращает сущность UserOption</returns>
        Experiment GetExperiment(int id);
        /// <summary>
        /// Получить из БД сущность Experiment
        /// </summary>
        /// <param name="name">Имя сущности</param>
        /// <returns>Возваращает сущность Experiment</returns>
        Experiment GetExperiment(string name);
        /// <summary>
        /// Получить из БД сущность Option<br></br>
        /// Хранит в себе сущность Experiment
        /// </summary>
        /// <param name="id">id сущности</param>
        /// <returns>Возваращает сущность Option</returns>
        Option GetOption(int id);
        /// <summary>
        /// Получить из БД сущность Option<br></br>
        /// Хранит в себе сущность Experiment
        /// </summary>
        /// <param name="name">Имя сущности</param>
        /// <returns>Возваращает сущность Option</returns>
        Option GetOption(string name);
        /// <summary>
        /// Получть сущность UserOptions<br></br>
        /// Хранит в себе сущность User,лист Option
        /// </summary>
        /// <param name="UserId">id сущности User</param>
        /// <returns></returns>
        UserOptions GetUserOption(int UserId);
        /// <summary>
        /// Получть сущность Options<br></br>
        /// </summary>
        /// <param name="experiment">Сущность Experiment</param>
        /// <returns>Список сущностей Option которые зависят от сущности Experiment</returns>
        List<Option> GetOption(Experiment experiment);
        /// <summary>
        /// Получить сущность User
        /// </summary>
        /// <param name="id">id User</param>
        /// <returns>сущность User</returns>
        User GetUser(int id);
        /// <summary>
        /// Получить сущность User
        /// </summary>
        /// <param name="name">имя User</param>
        /// <returns></returns>
        User GetUser(string name);
        /// <summary>
        /// Получить всю колекцию данных про Експереметны.
        /// </summary>        
        /// <returns>Полную всю колекцию зависимостей UserOptions </returns>
        public List<UserOptions> GetAllUserOptions();
       
    }
}