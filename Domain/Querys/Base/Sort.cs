namespace Domain.Querys.Base
{
    [Serializable]
    public struct Sort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }
}
