using ExperimentABP.Data;
using ExperimentABP.Entitys;

namespace ExperimentABP.Services
{
    public class DeterminantService : IDeterminantService
    {
      
        readonly IDatabaseManager _databaseManager;
        private Random random = new Random();
        public DeterminantService(IDatabaseManager databaseManager)
        {
            this._databaseManager = databaseManager;
        }
        private int GetPercentQuery(int correct)
        {
            return random.Next(correct);
        }
        public Option QueryExperiment(string nameExperiment, string token)
        {
            try
            {
                var user = _databaseManager.GetUser(token);
                var option = _databaseManager.GetUserOption(user.Id).Options.FirstOrDefault(x => x.Experiment.Name == nameExperiment);

                return option;
            }
            catch
            {
                var user = new User();
                user.Name = token;
                _databaseManager.CreateUser(user);
                user = _databaseManager.GetUser(token);
                var exper = _databaseManager.GetExperiment(nameExperiment);
                var options = _databaseManager.GetOption(exper);
                var option = Classification(nameExperiment, options);
                _databaseManager.CreateUserOption(user, option);
                return option;
            }
        }
        public Option Classification(string nameExperiment, List<Option> list)
        {
            Option result = null;
            if (nameExperiment == "button_color")
            {
                var resultProc = GetPercentQuery(99);
                if (resultProc <= 33)
                {
                    result = list[0];
                }
                if (resultProc <= 66 && resultProc > 33)
                {
                    result = list[1];
                }
                if (resultProc <= 99 && resultProc > 66)
                {
                    result = list[2];
                }

            }
            if (nameExperiment == "price")
            {
                var resultProc = GetPercentQuery(100);
                //5%
                if (resultProc <= 5)
                {
                    result = list.FirstOrDefault(x => x.Name == "50");
                }
                //10%
                if (resultProc <= 15 && resultProc > 5)
                {
                    result = list.FirstOrDefault(x => x.Name == "5");
                }
                //10%
                if (resultProc <= 25 && resultProc > 15)
                {
                    result = list.FirstOrDefault(x => x.Name == "20");
                }
                //75%
                if (resultProc <= 75 && resultProc > 25)
                {
                    result = list.FirstOrDefault(x => x.Name == "10");
                }
            }
            return result;
        }
        public List<UserOptions> GetStatistic()
        {
            //ResutDTO 
            _databaseManager.GetUserOption(1004);
            

            
            return null;
        }
        
        
    }
}
