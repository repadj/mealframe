using System.ComponentModel.DataAnnotations;

namespace App.DTO.Identity;

public class LoginInfo
{
    [MaxLength(64)]
    public string Email { get; set; } = default!;
    [MaxLength(64)]
    public string Password { get; set; } = default!;
}