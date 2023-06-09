using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdBoardsWeb.ViewModels.Auth;

public class RecoveryPasswordViewModel
{
    [DisplayName("Логин")]
    [Required(ErrorMessage = "Поле Логин обязательно")]
    public string Login { get; set; } = null!;
}