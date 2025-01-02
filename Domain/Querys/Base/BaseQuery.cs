
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Domain.Querys.Base
{
    public abstract class BaseQuery
    {
        #region Tham số phục vụ hiện thị trên grid
        // Thuộc tính liên quan đến grid
        public string t_gridRequest { get; set; }
        public int draw { get; set; }
        public List<SortQuery> sort { get; set; }

        // Thuộc tính tìm kiếm chung
        public string Keyword { get; set; }
        public List<string> SearchIn { get; set; }


        ////Thuộc tính duyệt đã duyệt
        //private int _trangThaiDuyet;
        //private SPModerationStatusType _moderationStatus;
        //public int TrangThaiDuyet
        //{
        //    get => _trangThaiDuyet;
        //    set
        //    {
        //        _trangThaiDuyet = value;
        //        _moderationStatus = (SPModerationStatusType)value;
        //    }
        //}
        //public SPModerationStatusType ModerationStatus
        //{
        //    get => _moderationStatus;
        //    set
        //    {
        //        _moderationStatus = value;
        //        _trangThaiDuyet = (int)value;
        //    }
        //}

        // Thuộc tính liên quan đến ngày tháng
        public string dateField { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }

        // Thuộc tính liên quan đến tìm kiếm theo ID
        /// <summary>
        /// Tìm theo listid
        /// </summary>
        public string lstID { get; set; }
        public bool isgetBylisID { get; set; }
        public List<int> ItemIDs { get; set; }
        //public List<int> lstIDGet
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(lstID))
        //        {
        //            isgetBylisID = true;
        //            return SIConvert.GetIDsFormString(lstID);
        //        }
        //        return new List<int>();
        //    }
        //    set { }
        //}


        public string FolderQuery { get; set; }
        public bool Paging { get; set; }

        public BaseQuery()
        {
            TuNgay = string.Empty;
            DenNgay = string.Empty;
            sort = new List<SortQuery>();
            SearchIn = new List<string>();
            ItemIDs = new List<int>();
            isgetBylisID = false;
            draw = 0;
            Paging = true;
        }

        public GridRequest oGridRequest
        {
            get
            {
                var request = string.IsNullOrEmpty(t_gridRequest)
                    ? new GridRequest()
                    : JsonConvert.DeserializeObject<GridRequest>(t_gridRequest);

                var specialClassFilter = request.filter?.filters?.ToList() ?? new List<FilterQuery>();

                if (!string.IsNullOrEmpty(Keyword) && SearchIn.Any())
                {
                    var searchFilter = CreateSearchFilter();
                    request.filter = CombineFilters(searchFilter, specialClassFilter);
                }

                AddDateFilters(request);
                EnsureDefaultSort(request);

                return request;
            }
        }
        public DateTime? SearchTuNgay
        {

            get
            {

                DateTimeFormatInfo dtfiParser;
                dtfiParser = new DateTimeFormatInfo();
                dtfiParser.ShortDatePattern = "dd/MM/yyyy";
                dtfiParser.DateSeparator = "/";
                DateTime? temp = null;
                if (!string.IsNullOrEmpty(TuNgay))
                {
                    temp = Convert.ToDateTime(TuNgay, dtfiParser).Date.AddTicks(-1);
                }
                return temp;
            }
            //set { this.SearchTuNgay = value; }

        }
        public DateTime? SearchDenNgay
        {

            get
            {
                DateTimeFormatInfo dtfiParser;
                dtfiParser = new DateTimeFormatInfo();
                dtfiParser.ShortDatePattern = "dd/MM/yyyy";
                dtfiParser.DateSeparator = "/";
                DateTime? temp = null;
                if (!string.IsNullOrEmpty(DenNgay))
                {
                    temp = Convert.ToDateTime(DenNgay, dtfiParser);
                    temp = temp.Value.Date.AddTicks(-1);
                }
                return temp;
            }

        }

        #endregion

        #region Function
        private FilterQuery CreateSearchFilter()
        {
            return new FilterQuery
            {
                logic = "or",
                filters = SearchIn.Select(x => new FilterQuery
                {
                    phuongthuc = "contains",
                    field = x,
                    value = Keyword
                }).ToList()
            };
        }

        private FilterQuery CombineFilters(FilterQuery searchFilter, List<FilterQuery> specialFilters)
        {
            var combinedFilter = new FilterQuery
            {
                logic = "and",
                filters = new List<FilterQuery> { searchFilter, new FilterQuery
        {
            phuongthuc = "gt",
            field = "ID",
            value = "0"
        }}
            };

            if (specialFilters.Any())
            {
                combinedFilter.filters.AddRange(specialFilters);
            }

            return combinedFilter;
        }

        private void AddDateFilters(GridRequest request)
        {
            if (string.IsNullOrEmpty(dateField)) return;

            if (SearchTuNgay.HasValue)
            {
                request.filter.filters.Add(new FilterQuery
                {
                    phuongthuc = "gt",
                    field = dateField,
                    value = SearchTuNgay.Value.ToString("yyyy/MM/dd HH:mm:ss")
                });
            }

            if (SearchDenNgay.HasValue)
            {
                request.filter.filters.Add(new FilterQuery
                {
                    phuongthuc = "lt",
                    field = dateField,
                    value = SearchDenNgay.Value.ToString("yyyy/MM/dd HH:mm:ss")
                });
            }
        }

        private void EnsureDefaultSort(GridRequest request)
        {
            if (request.sort == null || !request.sort.Any())
            {
                request.sort = new List<SortQuery>
        {
            new SortQuery
            {
                field = "ID",
                dir = "desc"
            }
        };
            }
        }
        #endregion
    }
}
