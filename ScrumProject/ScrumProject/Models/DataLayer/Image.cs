using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class Image
{
    [Key]
    [Column("img_id")]
    public int ImgId { get; set; }

    [Column("img_file")]
    public string? ImgFile { get; set; }

    [InverseProperty("Img")]
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
