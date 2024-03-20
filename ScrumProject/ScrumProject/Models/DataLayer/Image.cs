using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class Image
{
    [Key]
    [Column("ImageID")]
    public int ImageId { get; set; }

    public byte[] ImageSrc { get; set; } = null!;

    [Unicode(false)]
    public string ImageAlt { get; set; } = null!;

    [Unicode(false)]
    public string? ImageCaption { get; set; }

    [Column("ImageTagID")]
    public int ImageTagId { get; set; }

    [ForeignKey("ImageTagId")]
    [InverseProperty("Images")]
    public virtual ImageTag ImageTag { get; set; } = null!;
}
