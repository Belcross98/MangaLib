using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Helpers
{
    public class QueryObject
    {
        public string? MangaName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}