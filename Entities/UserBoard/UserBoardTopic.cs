using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class UserBoardTopic : BaseEntity<int>
    {
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int UserBoardId { get; set; }
        [Required]
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        [Required]
        [ForeignKey(nameof(UserBoardId))]
        public UserBoard UserBoard { get; set; }
    }
}
