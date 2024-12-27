using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CrudWithMovieScaffcolding.Models;

public partial class FilmStudioContext : DbContext
{
    public FilmStudioContext()
    {
    }

    public FilmStudioContext(DbContextOptions<FilmStudioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCast> MovieCasts { get; set; }

    public virtual DbSet<MovieDirection> MovieDirections { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Reviewer> Reviewers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LP027;Database=Film_Studio;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActId).HasName("PK_Actor_act_id");

            entity.ToTable("actor");

            entity.Property(e => e.ActId).HasColumnName("act_id");
            entity.Property(e => e.ActFname)
                .HasMaxLength(20)
                .HasColumnName("act_fname");
            entity.Property(e => e.ActGender)
                .HasMaxLength(1)
                .HasColumnName("act_gender");
            entity.Property(e => e.ActLname)
                .HasMaxLength(20)
                .HasColumnName("act_lname");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirId).HasName("PK_Director_dir_id");

            entity.ToTable("director");

            entity.Property(e => e.DirId).HasColumnName("dir_id");
            entity.Property(e => e.DirFname)
                .HasMaxLength(20)
                .HasColumnName("dir_fname");
            entity.Property(e => e.DirLname)
                .HasMaxLength(20)
                .HasColumnName("dir_lname");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenId).HasName("PK_Genres_gen_id");

            entity.ToTable("genres");

            entity.Property(e => e.GenId).HasColumnName("gen_id");
            entity.Property(e => e.GenTitle)
                .HasMaxLength(20)
                .HasColumnName("gen_title");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovId).HasName("PK_Movie_mov_id");

            entity.ToTable("movie");

            entity.Property(e => e.MovId).HasColumnName("mov_id");
            entity.Property(e => e.MovLang)
                .HasMaxLength(20)
                .HasColumnName("mov_lang");
            entity.Property(e => e.MovRelCountry)
                .HasMaxLength(20)
                .HasColumnName("mov_rel_country");
            entity.Property(e => e.MovRlDt).HasColumnName("mov_rl_dt");
            entity.Property(e => e.MovTime).HasColumnName("mov_time");
            entity.Property(e => e.MovTitle)
                .HasMaxLength(50)
                .HasColumnName("mov_title");
            entity.Property(e => e.MovYear).HasColumnName("mov_year");
        });

        modelBuilder.Entity<MovieCast>(entity =>
        {
            entity.HasKey(e => e.CastId).HasName("PK_Movie_cast_Cast_id");

            entity.ToTable("Movie_cast");

            entity.Property(e => e.CastId).HasColumnName("Cast_id");
            entity.Property(e => e.ActId).HasColumnName("act_id");
            entity.Property(e => e.MovId).HasColumnName("mov_id");
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .HasColumnName("role");

            entity.HasOne(d => d.Act).WithMany(p => p.MovieCasts)
                .HasForeignKey(d => d.ActId)
                .HasConstraintName("FK_Movie_cast_mov_id_movie_mov_id");

            entity.HasOne(d => d.Mov).WithMany(p => p.MovieCasts)
                .HasForeignKey(d => d.MovId)
                .HasConstraintName("FK_Movie_cast_act_id_actor_act_id");
        });

        modelBuilder.Entity<MovieDirection>(entity =>
        {
            entity.HasKey(e => e.DirectionId).HasName("PK_Movie_Direction_Direction_id");

            entity.ToTable("Movie_Direction");

            entity.Property(e => e.DirectionId).HasColumnName("Direction_id");
            entity.Property(e => e.DirId).HasColumnName("dir_id");
            entity.Property(e => e.MovId).HasColumnName("mov_id");

            entity.HasOne(d => d.Dir).WithMany(p => p.MovieDirections)
                .HasForeignKey(d => d.DirId)
                .HasConstraintName("FK_Movie_Direction_dir_id_director_dir_id");

            entity.HasOne(d => d.Mov).WithMany(p => p.MovieDirections)
                .HasForeignKey(d => d.MovId)
                .HasConstraintName("FK_Movie_Direction_mov_id_movie_mov_id");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => e.GenresId).HasName("PK_Movie_Genres_Genres_id");

            entity.ToTable("Movie_Genres");

            entity.Property(e => e.GenresId).HasColumnName("Genres_id");
            entity.Property(e => e.GenId).HasColumnName("gen_id");
            entity.Property(e => e.MovId).HasColumnName("mov_id");

            entity.HasOne(d => d.Gen).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.GenId)
                .HasConstraintName("FK_Movie_Genres_gen_id_genres_gen_id");

            entity.HasOne(d => d.Mov).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.MovId)
                .HasConstraintName("FK_Movie_Genres_mov_id_movie_mov_id");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK_Rating_Rating_id");

            entity.ToTable("Rating");

            entity.Property(e => e.RatingId).HasColumnName("Rating_id");
            entity.Property(e => e.MovId).HasColumnName("mov_id");
            entity.Property(e => e.NumORatings).HasColumnName("num_o_ratings");
            entity.Property(e => e.RevId).HasColumnName("rev_id");
            entity.Property(e => e.RevStars)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("rev_stars");

            entity.HasOne(d => d.Mov).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.MovId)
                .HasConstraintName("FK_Rating_mov_id_movie_mov_id");

            entity.HasOne(d => d.Rev).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RevId)
                .HasConstraintName("FK_Rating_rev_id_Reviewer_rev_id");
        });

        modelBuilder.Entity<Reviewer>(entity =>
        {
            entity.HasKey(e => e.RevId).HasName("PK_Reviewer_rev_id");

            entity.ToTable("Reviewer");

            entity.Property(e => e.RevId).HasColumnName("rev_id");
            entity.Property(e => e.RevName)
                .HasMaxLength(30)
                .HasColumnName("rev_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
