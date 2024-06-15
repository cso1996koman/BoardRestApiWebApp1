using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserBoard : BaseEntity<int>
    {
        [Required]
        [StringLength(ENTITY_USERBOARD_TITLE_MAX_LENGTH)]
        public string Title { get; set; }
        [Required]
        [StringLength(ENTITY_USERBOARD_CONTENT_MAX_LENGTH)]
        public string Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
        public ICollection<UserBoardComment> UserBoardComments { get; set; }
        public ICollection<UserBoardImage> UserBoardImages { get; set; }
        public ICollection<UserBoardTopic> UserBoardTopics { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User User { get; set; }
        public const int ENTITY_USERBOARD_TITLE_MAX_LENGTH = 100;
        public const int ENTITY_USERBOARD_CONTENT_MAX_LENGTH = int.MaxValue;
    }
}
