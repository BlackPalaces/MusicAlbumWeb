namespace MusicAlbumWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        public int Id { get; set; }

        public string Albumname { get; set; }

        public string Songs { get; set; }

        [StringLength(250)]
        public string AlbumPic { get; set; }
    }
}
