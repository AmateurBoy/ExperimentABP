using ExperimentABP.Entitys;

namespace ExperimentABP.Services
{
    public interface IDeterminantService
    {
      
        //Класификатор - назначает опцию для дивайса
        Option Classification(string nameExperiment, List<Option> list);
        //Назначает необходимою функцию в зависимости от входных параметров.
        Option QueryExperiment(string nameExperiment, string token);
        //Отдает все результаты.
        public List<UserOptions> GetStatistic();
    }
}