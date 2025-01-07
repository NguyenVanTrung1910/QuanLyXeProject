namespace Shared
{
    public class DataTableJson
    {
        public int? draw { get; set; }
        public int? recordsTotal { get; set; }
        public int? recordsFiltered { get; set; }
        public object data { get; set; }
        public string ex_message { get; set; }
        public string querytext { get; set; }


        public DataTableJson Message(string ex_message)
        {
            this.ex_message = ex_message;

            return this;
        }
        public DataTableJson(string ex_message = null)
        {
            this.ex_message = ex_message;
        }
        public DataTableJson(string ex_message, object data)
        {
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
        }
        public DataTableJson() { }
        public DataTableJson(object data, int draw, int recordsTotal, int recordsFiltered)
        {
            this.data = data;
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsTotal;
        }
    }
}
