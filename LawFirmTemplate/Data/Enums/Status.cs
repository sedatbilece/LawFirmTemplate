using System.ComponentModel.DataAnnotations;

namespace LawFirmTemplate.Data.Enums
{
    public enum Status
    {
        [Display(Name = "Aktif")]
        Active,
        [Display(Name = "Pasif")]
        Passive,
        [Display(Name = "Donduruldu")]
        Frozen
    }
}
