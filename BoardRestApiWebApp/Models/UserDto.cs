using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace RestApiProject.Models
{
    public class UserDto : BaseDto<UserDto, User>, IValidatableObject
    {
        [Required]
        [StringLength(User.ENTITY_USER_USERNAME_MAX_LENGTH)]
        public string UserName { get; set; }
        [Required]
        [StringLength(User.ENTITY_USER_EMAIL_MAX_LENGTH)]
        public string Email { get; set; }
        [Required]
        [StringLength(User.ENTITY_USER_PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }
        [Required]
        [StringLength(User.ENTITY_USER_FULLNAME_MAX_LENGTH)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("사용자이름은 테스트 될수 없습니다.", new[] { nameof(UserName) });
            if (Password.Equals("123456"))
                yield return new ValidationResult("비밀번호는 123456 일수 없습니다.", new[] { nameof(Password) });
            if (Password.Length < 6)
                yield return new ValidationResult("비밀번호는 6자이상이어야 합니다..", new[] { nameof(Password) });
        }
    }
}
