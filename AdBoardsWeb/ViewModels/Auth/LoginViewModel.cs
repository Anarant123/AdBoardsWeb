using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdBoardsWeb.ViewModels.Auth;

public class LoginViewModel
{
    [DisplayName("Логин")]
    [Required(ErrorMessage = "Поле Логин обязательно")]
    public string Login { get; set; } = null!;

    [DisplayName("Пароль")]
    [Required(ErrorMessage = "Поле Пароль обязательно")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}