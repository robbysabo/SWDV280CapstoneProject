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

    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("ContactFName")]
    [StringLength(255)]
    [Unicode(false)]
    public string ContactFname { get; set; } = null!;

    [Column("ContactLName")]
    [StringLength(255)]
    [Unicode(false)]
    public string ContactLname { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string ContactEmail { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string ContactPhone { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string? ApptStat { get; set; }
}
