using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class Appointment
{
    [Key]
    [Column("appt_id")]
    public int ApptId { get; set; }

    [Column("req_date", TypeName = "datetime")]
    public DateTime ReqDate { get; set; }

    [Column("type")]
    [StringLength(255)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("contact_email")]
    [StringLength(80)]
    [Unicode(false)]
    public string ContactEmail { get; set; } = null!;

    [Column("contact_phone")]
    [StringLength(10)]
    [Unicode(false)]
    public string ContactPhone { get; set; } = null!;

    [Column("appt_stat")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ApptStat { get; set; }
}
