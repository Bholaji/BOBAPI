namespace Bob.Model.Entities
{
	public class BaseLeaveEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate { get; set; }
	}
}
