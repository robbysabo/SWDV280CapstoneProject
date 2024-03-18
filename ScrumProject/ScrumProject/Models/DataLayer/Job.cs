using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class Job
{
    [Key]
    [Column("job_id")]
    public int JobId { get; set; }

    [Column("description")]
    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("img_id")]
    public int ImgId { get; set; }

    [Column("u_id")]
    public int UId { get; set; }

    [ForeignKey("ImgId")]
    [InverseProperty("Jobs")]
    public virtual Image Img { get; set; } = null!;

    [ForeignKey("UId")]
    [InverseProperty("Jobs")]
    public virtual User UIdNavigation { get; set; } = null!;
}
