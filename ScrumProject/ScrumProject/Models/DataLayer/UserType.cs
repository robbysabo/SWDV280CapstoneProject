using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class UserType
{
    [Key]
    [Column("UserTypeID")]
    public int UserTypeId { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; } = null!;

    [InverseProperty("UserType")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
