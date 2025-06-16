using System.ComponentModel.DataAnnotations;

namespace App.DTO.Identity;

public class RegisterInfo
{
    [MaxLength(64)]
    public string Email { get; set; } = default!;
    
    [MaxLength(64)]
    public string Password { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(32)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(32)]
    public string LastName { get; set; } = default!;
}