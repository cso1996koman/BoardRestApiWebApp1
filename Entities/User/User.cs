
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public User()
        {
            IsActive = true;
        }
        [Required]
        [StringLength(ENTITY_USER_FULLNAME_MAX_LENGTH)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public ICollection<UserBoard> UserBoards { get; set; }
        public ICollection<UserBoardComment> UserBoardComments { get; set; }
        public const int ENTITY_USER_FULLNAME_MAX_LENGTH = 100;
        public const int ENTITY_USER_USERNAME_MAX_LENGTH = 100;
        public const int ENTITY_USER_EMAIL_MAX_LENGTH = 256;
        public const int ENTITY_USER_PASSWORD_MAX_LENGTH = 500;
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
        }
    }
    public enum GenderType
    {
        [Display(Name = "남성")]
        Male = 1,
        [Display(Name = "여성")]
        Female = 2
    }
}
