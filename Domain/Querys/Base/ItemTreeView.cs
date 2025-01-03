namespace Domain.Querys.Base
{
    public class ItemTreeView
    {
        public string? id { get; set; }
        public string? text { get; set; }
        public int ModerationStatus { get; set; }
        public List<ItemTreeView>? children { get; set; }

    }
}
