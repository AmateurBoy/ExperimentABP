namespace ExperimentABP.Entitys
{
    public class DeviceOption
    {
        public int Id { get; set; }
        public Device Device { get; set; }
        public Option Option { get; set; }
    }
}
