using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdBoardsWeb.ViewModels;

public class ProfileViewModel
{
    [DisplayName("Логин")] public string Login { get; set; } = null!;

    [DisplayName("Никнейм")] public string? Name { get; set; }

    [DisplayName("Город")] public string? City { get; set; }

    [DisplayName("Дата рождения")] public DateTime Birthday { get; set; }

    [DisplayName("Номер телефона")] public string Phone { get; set; } = null!;

    [DisplayName("Email")] public string Email { get; set; } = null!;

    public string PhotoName { get; set; } = null!;
}