using WebFramework.Api;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace RestApiProject.Models
{
    public class UserBoardImageDto : BaseDto<UserBoardImageDto, UserBoardImage, int>
    {
        [Required]
        [StringLength(UserBoardImage.ENTITY_USERBOARDIMAGE_PATH_MAX_LENGTH)]
        public string Path { get; set; }
        [Required]
        public int UserBoardId { get; set; }
    }
}
