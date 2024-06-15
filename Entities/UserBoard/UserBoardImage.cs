using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class UserBoardImage : BaseEntity<int>
    {
        [StringLength(ENTITY_USERBOARDIMAGE_PATH_MAX_LENGTH)]
        [Required]
        public string Path { get; set; }
        [Required]
        public int UserBoardId { get; set; }
        public const int ENTITY_USERBOARDIMAGE_PATH_MAX_LENGTH = 1000;
    }
}
