namespace ExperimentABP.Entitys
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Experiment Experiment {get;set;}
    }
}
