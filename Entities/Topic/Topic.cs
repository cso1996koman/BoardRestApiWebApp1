using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class Topic : BaseEntity<int>
    {
        public int? ParentTopicId { get; set; }
        [Required]
        [StringLength(ENTITY_TOPIC_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey(nameof(ParentTopicId))]
        public Topic ParentTopic { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User User { get; set; }
        public ICollection<Topic> ChildTopics { get; set; }
        public ICollection<UserBoardTopic> UserBoardTopics { get; set; }
        public const int ENTITY_TOPIC_TITLE_MAX_LENGTH = 100;
    }
}
