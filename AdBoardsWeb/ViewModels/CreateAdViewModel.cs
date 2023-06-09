using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdBoardsWeb.ViewModels;

public class CreateAdViewModel
{
    public int Id { get; set; }
    
    [DisplayName("Цена")]
    [Required(ErrorMessage = "Поле Логин обязательно")]
    [Range(0, int.MaxValue, ErrorMessage = "Цена должна быть больше или равна нулю")]
    public int? Price { get; set; }

    [DisplayName("Название")]
    [Required(ErrorMessage = "Поле Название обязательно")]
    [MinLength(2, ErrorMessage = "Минимальная длина названия должна быть 2 символа")]
    [MaxLength(32, ErrorMessage = "Максимальная длина названия может быть 64 символа")]
    public string Name { get; set; } = null!;

    [DisplayName("Описание")]
    [Required(ErrorMessage = "Поле Описание обязательно")]
    public string Description { get; set; } = null!;
    
    [DisplayName("Город")]
    [Required(ErrorMessage = "Поле Город обязательно")]
    [MinLength(2, ErrorMessage = "Минимальная длина названия Города должна быть 2 символа")]
    [MaxLength(32, ErrorMessage = "Максимальная длина названия Города может быть 32 символа")]
    public string City { get; set; } = null!;

    public int CategoryId { get; set; }
    
    public int AdTypeId { get; set; }
    
    public string? PhotoName { get; set; }

    [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Выбран неподдерживаемый тип файла")]
    [ValidateNever]
    public IFormFile? Photo { get; set; }
}