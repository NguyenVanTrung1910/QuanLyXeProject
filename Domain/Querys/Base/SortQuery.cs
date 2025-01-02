using System;

namespace Domain.Querys.Base
{
    [Serializable]
    public struct SortQuery
    {
        public string field { get; set; }
        public string dir { get; set; }
    }
}
