using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScrumProject.Models.DataLayer;

public partial class WorkerImage
{
    [Key]
    [Column("ImageID")]
    public int ImageId { get; set; }

    [Unicode(false)]
    public string ImageSrc { get; set; } = null!;

    [Unicode(false)]
    public string ImageAlt { get; set; } = null!;

    [Unicode(false)]
    public string? Description { get; set; }
}
