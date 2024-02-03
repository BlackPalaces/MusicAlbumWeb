using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MusicAlbumWeb
{
    public partial class MyPhoneModel : DbContext
    {
        public MyPhoneModel()
            : base("name=MyPhoneModel")
        {
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<MusicAlbum> MusicAlbum { get; set; }
        public virtual DbSet<FavoriteMusic> FavoriteMusic { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.Songs)
                .IsUnicode(false);

            modelBuilder.Entity<Album>()
                .Property(e => e.AlbumPic)
                .IsUnicode(false);

            modelBuilder.Entity<Artist>()
                .Property(e => e.Songs)
                .IsUnicode(false);

            modelBuilder.Entity<Artist>()
                .Property(e => e.ArtistPic)
                .IsUnicode(false);
        }
    }
}
