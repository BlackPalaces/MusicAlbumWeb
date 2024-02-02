namespace MusicAlbumWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Artist")]
    public partial class Artist
    {
        public int Id { get; set; }

        public string Artistname { get; set; }

        public string Songs { get; set; }

        [StringLength(250)]
        public string ArtistPic { get; set; }
    }
}
