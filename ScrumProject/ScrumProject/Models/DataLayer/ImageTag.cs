using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class ImageTag
{
    [Key]
    [Column("ImageTagID")]
    public int ImageTagId { get; set; }

    [Column("ImageTag")]
    [StringLength(15)]
    [Unicode(false)]
    public string ImageTag1 { get; set; } = null!;

    [InverseProperty("ImageTag")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
