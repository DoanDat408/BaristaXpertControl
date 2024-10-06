namespace BaristaXpertControl.Domain.Entities
{
    public class Store
    {
        public int Id { get; set; }            
        public string Location { get; set; }

        public ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
    }
}
