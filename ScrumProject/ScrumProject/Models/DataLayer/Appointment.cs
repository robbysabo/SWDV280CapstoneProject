using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class Appointment
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    [Column("AppointmentTypeID")]
    public int AppointmentTypeId { get; set; }

    [ForeignKey("AppointmentTypeId")]
    [InverseProperty("Appointments")]
    public virtual AppointmentType AppointmentType { get; set; } = null!;
}
