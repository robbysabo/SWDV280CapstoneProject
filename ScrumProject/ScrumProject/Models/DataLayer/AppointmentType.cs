using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

[Table("AppointmentType")]
public partial class AppointmentType
{
    [Key]
    [Column("AppointmentTypeID")]
    public int AppointmentTypeId { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; } = null!;

    [InverseProperty("AppointmentType")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
