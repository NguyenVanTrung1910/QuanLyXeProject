using System.Collections.Generic;

namespace Domain.Querys.Base
{
    public class GridRequest
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

        public int ItemID { get; set; }
        public List<SortQuery> sort { get; set; }
        public string Field { get; set; }
        public bool FieldOption { get; set; }
        public FilterQuery filter { get; set; }
        public GridRequest()
        {
            sort = new List<SortQuery>();
            filter = new FilterQuery();
        }

    }
}
