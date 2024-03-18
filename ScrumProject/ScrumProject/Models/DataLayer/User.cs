using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class User
{
    [Key]
    [Column("u_id")]
    public int UId { get; set; }

    [Column("user_type")]
    [StringLength(1)]
    [Unicode(false)]
    public string UserType { get; set; } = null!;

    [Column("f_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string FName { get; set; } = null!;

    [Column("l_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string LName { get; set; } = null!;

    [Column("email")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("pass")]
    [StringLength(50)]
    [Unicode(false)]
    public string Pass { get; set; } = null!;

    [InverseProperty("UIdNavigation")]
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
