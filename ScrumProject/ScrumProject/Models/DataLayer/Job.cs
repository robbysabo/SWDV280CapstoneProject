using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class Job
{
    [Key]
    [Column("JobID")]
    public int JobId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string TypeDescription { get; set; } = null!;

    [Column("ImageID")]
    public int ImageId { get; set; }
}
