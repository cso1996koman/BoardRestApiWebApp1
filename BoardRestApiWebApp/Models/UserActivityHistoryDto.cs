using AutoMapper;
using AutoMapper.Configuration;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace RestApiProject.Models
{
    public class UserActivityHistoryDto : BaseDto<UserActivityHistoryDto, User, int>
    {
        [Required]
        [StringLength(User.ENTITY_USER_FULLNAME_MAX_LENGTH)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }
        public ICollection<UserBoard> UserBoardsCreateHistory { get; set; }
        public ICollection<UserBoardComment> UserBoardCommentsCreateHistory { get; set; }
        public override void CustomMappings(IMappingExpression<User, UserActivityHistoryDto> mappingExpression)
        {
            mappingExpression.ForMember(
                dest => dest.UserBoardsCreateHistory,
                config => config.MapFrom(src => src.UserBoards));
            mappingExpression.ForMember(
                dest => dest.UserBoardCommentsCreateHistory,
                config => config.MapFrom(src => src.UserBoardComments));
        }
    }
}
