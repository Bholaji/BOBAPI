namespace Bob.Model.DTO
{
	public class BaseDto
    {
        public Guid ID { get; set; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; }
        public DateTime ModificaionDate { get; set; }
    }
}
