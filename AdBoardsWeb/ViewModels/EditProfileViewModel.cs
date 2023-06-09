using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdBoardsWeb.ViewModels;

public class EditProfileViewModel
{
    [DisplayName("Никнейм")]
    [MinLength(2, ErrorMessage = "Минимальная длина никнейма должна быть 2 символа")]
    [MaxLength(32, ErrorMessage = "Максимальная длина никнейма может быть 32 символа")]
    public string? Name { get; set; }

    [DisplayName("Номер телефона")]
    [Required(ErrorMessage = "Поле Номер телефона обязательно")]
    [RegularExpression(@"^(\+)[1-9][0-9\-().]{9,15}$", ErrorMessage = "Номер телефона имеет неправильный формат")]
    public string Phone { get; set; } = null!;

    [DisplayName("Email")]
    [Required(ErrorMessage = "Поле Email обязательно")]
    [EmailAddress(ErrorMessage = "Email имеет неправильный формат")]
    [MaxLength(32, ErrorMessage = "Максимальная длина email может быть 32 символа")]
    public string Email { get; set; } = null!;

    [DisplayName("Город")]
    [MinLength(2, ErrorMessage = "Минимальная длина названия города должна быть 2 символа")]
    [MaxLength(32, ErrorMessage = "Максимальная длина названия города может быть 32 символа")]
    public string? City { get; set; }

    [DisplayName("Дата рождения")]
    [Required(ErrorMessage = "Поле Дата рождения обязательно")]
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }

    public string PhotoName { get; set; } = null!;
    
    [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Выбран неподдерживаемый тип файла")]
    [ValidateNever]
    public IFormFile? Photo { get; set; }
}