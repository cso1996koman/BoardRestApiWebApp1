using Entities;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace RestApiProject.Models
{
    public class UserBoardTopicDto : BaseDto<UserBoardTopicDto, UserBoardTopic, int>
    {
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int UserBoardId { get; set; }
        public UserBoard UserBoard { get; set; }
        public Topic Topic { get; set; }
    }
}
