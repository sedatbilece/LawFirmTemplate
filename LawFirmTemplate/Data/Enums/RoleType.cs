using System.ComponentModel.DataAnnotations;

namespace LawFirmTemplate.Data.Enums
{
    public enum RoleType
    {
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "Görüntüleyici")]
        Viewer,
        [Display(Name = "Admin")]
        Admin,
    }
}
