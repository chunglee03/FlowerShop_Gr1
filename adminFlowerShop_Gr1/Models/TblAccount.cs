using System;
using System.Collections.Generic;

namespace adminFlowerShop_Gr1.Models
{
    public partial class TblAccount
    {
        public TblAccount()
        {
            TblPosts = new HashSet<TblPost>();
        }

        public int AccountId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool Active { get; set; }
        public string? FullName { get; set; }
        public int? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual TblRole? Role { get; set; }
        public virtual ICollection<TblPost> TblPosts { get; set; }
    }
}
