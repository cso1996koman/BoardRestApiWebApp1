using WebFramework.Api;
using Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace RestApiProject.Models
{
    public class TopicDto : BaseDto<TopicDto, Topic, int>
    {
        public int ParentTopicId { get; set; }
        [Required]
        [StringLength(Topic.ENTITY_TOPIC_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
    public class HavingUserBoardbyTopicDto : BaseDto<HavingUserBoardbyTopicDto, Topic, int>
    {
        public int ParentTopicId { get; set; }
        [Required]
        [StringLength(Topic.ENTITY_TOPIC_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public ICollection<UserBoardTopic> UserBoardTopics { get; set; }
    }
}
