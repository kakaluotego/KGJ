using System;
using System.Collections.Generic;
using System.Text;

namespace KGJ.Users.Dto
{
    public class TestModel
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string Remark1 { get; set; }

        public string Remark2 { get; set; }

        public string Remark3 { get; set; }

        public string Remark4 { get; set; }

        public long Id { get; set; }
    }
}
