using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;


namespace RestApiProject.Models
{
    public class UserBoardDto : BaseDto<UserBoardDto, UserBoard, int>
    {
        [Required]
        [StringLength(UserBoard.ENTITY_USERBOARD_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [Required]
        [StringLength(UserBoard.ENTITY_USERBOARD_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
    }
    public class UserBoardSelectDto : BaseDto<UserBoardSelectDto, UserBoard, int>
    {
        [Required]
        [StringLength(UserBoard.ENTITY_USERBOARD_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [Required]
        [StringLength(UserBoard.ENTITY_USERBOARD_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }
        [Required]
        [StringLength(User.ENTITY_USER_FULLNAME_MAX_LENGTH)]
        public string AuthorFullName { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
        public ICollection<UserBoardComment> UserBoardComments { get; set; }
        public ICollection<UserBoardImage> UserBoardImages { get; set; }
        public ICollection<UserBoardTopic> UserBoardTopics { get; set; }
        public override void CustomMappings(IMappingExpression<UserBoard, UserBoardSelectDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.AuthorFullName,
                config => config.MapFrom(src => src.User.FullName));
        }
    }
}
