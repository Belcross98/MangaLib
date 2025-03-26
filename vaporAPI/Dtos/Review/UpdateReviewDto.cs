using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Review
{
    public class UpdateReviewDto
    {
        public required int Rating {get;set;}
        public string Comment { get; set; } = string.Empty;
    }
}