namespace Web.Models
{
    public class DropDownViewModel
    {
        public DropDownViewModel(int id, string value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get; set; }
        public string Value { get; set; }
    }
}