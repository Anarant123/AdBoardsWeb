using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdBoardsWeb.ViewModels.Auth;

public class RegisterViewModel
{
    [DisplayName("Логин")]
    [Required(ErrorMessage = "Поле Логин обязательно")]
    [MinLength(2, ErrorMessage = "Минимальная длина логина должна быть 2 символа")]
    [MaxLength(32, ErrorMessage = "Максимальная длина логина может быть 32 символа")]
    public string Login { get; set; } = null!;

    [DisplayName("Дата рождения")]
    [Required(ErrorMessage = "Поле Дата рождения обязательно")]
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }

    [DisplayName("Номер телефона")]
    [Required(ErrorMessage = "Поле Номер телефона обязательно")]
    [RegularExpression(@"^(\+)[1-9][0-9\-().]{9,15}$", ErrorMessage = "Номер телефона имеет неправильный формат")]
    public string Phone { get; set; } = null!;

    [DisplayName("Email")]
    [Required(ErrorMessage = "Поле Email обязательно")]
    [EmailAddress(ErrorMessage = "Email имеет неправильный формат")]
    [MaxLength(32, ErrorMessage = "Максимальная длина email может быть 32 символа")]
    public string Email { get; set; } = null!;

    [DisplayName("Пароль")]
    [Required(ErrorMessage = "Поле Пароль обязательно")]
    [MinLength(8, ErrorMessage = "Минимальная длина пароля должна быть 8 символов")]
    [MaxLength(32, ErrorMessage = "Максимальная длина пароля может быть 32 символа")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DisplayName("Подтверждение пароля")]
    [Required(ErrorMessage = "Поле Подтверждение пароля обязательно")]
    [Compare(nameof(Password), ErrorMessage = "Подтверждение пароля должно совпадать с паролем")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}