using AB_testsABP.Entitys;

namespace AB_testsABP.Services
{
    public interface IDatabaseManager
    {
        int CreateOption(Option option);
        int CreateUser(User user);
        string DelateUser(int id);
        Experiment GetExperiment(int id);
        Experiment GetExperiment(string name);
        Option GetOption(int id);
        Option GetOption(string name);
        User GetUser(int id);
        User GetUser(string name);
        void RecreateDefoltsValueTables();
    }
}