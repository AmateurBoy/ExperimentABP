using ExperimentABP.Entitys;

namespace ExperimentABP.Data
{
    /// <summary>
    /// Інтерфейс для моніпуляції з БД в контексті проекту.
    /// </summary>
    public interface IDatabaseManager
    {

        /// <summary>
        /// Create<br></br>
        /// Створити новий пристрій<br></br>
        /// </summary>
        /// <param name="deviceToken">deviceToken для створення Device</param>
        /// <returns>Повертає сутність Device</returns>
        Device CreateDevice(string deviceToken);
        /// <summary>
        /// Create<br></br>
        /// Присвоєння пристрою назначину опцію<br></br>
        /// </summary>
        /// <param name="device">клас для опису сутності.</param>
        /// <param name="option">клас для опису сутності.</param>
        /// <returns>Повертає id device</returns>
        DeviceOption CreateUserOption(Device device, Option option);
        /// <summary>
        /// Отримати із БД сутність Experiment
        /// </summary>
        /// <param name="id">id сутності</param>
        /// <returns>сутності DeviceOption</returns>
        Experiment GetExperiment(int id);
        /// <summary>
        /// Отримати из БД сутність Experiment
        /// </summary>
        /// <param name="name">Імя сутності</param>
        /// <returns>сутність Experiment</returns>
        Experiment GetExperiment(string name);
        /// <summary>
        /// Отримати из БД сутність Option<br></br>
        /// Зберігае у собі сутність Experiment
        /// </summary>
        /// <param name="id">id сутності</param>
        /// <returns>сутність Option</returns>
        Option GetOption(int id);
        /// <summary>
        /// Отримати з БД суть Option<br></br>
        /// Зберігає сутність Experiment
        /// </summary>
        /// <param name="name">Ім'я сутності</param>
        /// <returns>Звертає сутність Option</returns>
        Option GetOption(string name);
        /// <summary>
        /// Отримати сутність UserOptions<br></br>
        /// Зберігає сутність User,Option
        /// </summary>
        /// <param name="DiveceId">id сутності User</param>
        /// <returns></returns>
        List<DeviceOption> GetDeviceOptions(int DiveceId);
        /// <summary>
        /// Отримати сутність Options<br></br>
        /// </summary>
        /// <param name="experiment">Сутність Experiment</param>
        /// <returns>Список сутностей Option які залежать від сутності Experiment</returns>
        List<Option> GetOptions(int ExperimentId);
        /// <summary>
        /// Отримати сутність Device
        /// </summary>
        /// <param name="id">id Device</param>
        /// <returns>сутність Device</returns>
        Device GetDevice(int id);
        /// <summary>
        /// Отримати сутність Device
        /// </summary>
        /// <param name="name">ім'я User</param>
        /// <returns>сутність Device</returns>
        Device GetDevice(string name);
        /// <summary>
        /// Отримати усю колекцію даних про Експеременти.
        /// </summary>
        /// <returns>Усієї колекції залежностей DeviceOptions</returns>
        public List<DeviceOption> GetAllDeviceOptions();

       
    }
}