namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Image")]
    public partial class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }

        public int Id { get; set; }

        [DisplayName("Upload Image")]
        public string Image1 { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}
