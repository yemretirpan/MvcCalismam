namespace MvcCalismam.ViewModels.Filters
{
    public class OgrenciListFilter
    {
        public string? Q { get; set; }           // search by name/surname/email
        public string? Sort { get; set; }        // name_asc, name_desc, tc_asc, tc_desc
        public int Page { get; set; } = 1;       // sayfa numarası
        public int PageSize { get; set; } = 10;  // sayfadaki kayıt sayısı
    }
}
