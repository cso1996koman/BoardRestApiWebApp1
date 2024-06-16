using WebFramework.Api;
using Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace RestApiProject.Models
{
    public class UserBoardCommentDto : BaseDto<UserBoardCommentDto, UserBoardComment, int>
    {
        [Required]
        [StringLength(UserBoardComment.ENTITY_USERBOARDCOMMENT_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }
        [Required]
        public int UserBoardId { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public UserBoard UserBoard { get; set; }
    }
    public class UserBoardCommentCreateDto : BaseDto<UserBoardCommentCreateDto, UserBoardComment, int>
    {
        [Required]
        [StringLength(UserBoardComment.ENTITY_USERBOARDCOMMENT_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }
        [Required]
        public int UserBoardId { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
