using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserBoardComment : BaseEntity<int>
    {
        [StringLength(1000)]
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserBoardId { get; set; }
        [Required]
        public DateTimeOffset CreateDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User User { get; set; }
        public const int ENTITY_USERBOARDCOMMENT_CONTENT_MAX_LENGTH = 1000;
    }
}
