namespace SigcatminProAddinUI.Models
{
    public class ComboBoxItemGeneric<TId>
    {
        public TId Id { get; set; }
        public string DisplayName { get; set; }
        public override string ToString()
        {
            return DisplayName; 
        }
    }
}
