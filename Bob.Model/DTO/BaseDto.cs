namespace Bob.Model.DTO
{
	public class BaseDto
    {
        public Guid ID { get; set; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public DateTime ModificaionDate { get; set; }
    }
}
