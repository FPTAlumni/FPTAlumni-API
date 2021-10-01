using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class University
    {
        public University()
        {
            Alumni = new HashSet<Alumnus>();
            Groups = new HashSet<Group>();
            Majors = new HashSet<Major>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Alumnus> Alumni { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Major> Majors { get; set; }
    }
}
