using System.Collections.Generic;

namespace Domain.Querys.Base
{

    public class FilterQuery
    {
        public string field { get; set; }
        public string phuongthuc { get; set; }
        public string value { get; set; }
        public List<FilterQuery> filters { get; set; }
        public string logic { get; set; }
        public FilterQuery()
        {
            filters = new List<FilterQuery>();
        }
    }
}
