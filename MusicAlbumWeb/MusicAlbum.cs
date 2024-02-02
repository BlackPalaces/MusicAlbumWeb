namespace MusicAlbumWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MusicAlbum")]
    public partial class MusicAlbum
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Musicname { get; set; }

        [StringLength(250)]
        public string Artist { get; set; }

        [StringLength(250)]
        public string Musiclink { get; set; }

        [StringLength(250)]
        public string Album { get; set; }

        [StringLength(250)]
        public string Musicpic { get; set; }

        public int? Hit { get; set; }

        [StringLength(250)]
        public string Type { get; set; }
    }
}
