using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PriHood.Models
{
    public partial class PrihoodContext : DbContext
    {
        public virtual DbSet<Barrio> Barrio { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Residencia> Residencia { get; set; }
        public virtual DbSet<Residente> Residente { get; set; }
        public virtual DbSet<ResidentesXresidencia> ResidentesXresidencia { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoEmpleado> TipoEmpleado { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioXbarrio> UsuarioXbarrio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost;database=Prihood;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barrio>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasColumnName("ubicacion")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasIndex(e => e.IdPersona)
                    .HasName("fk_Empleado_3");

                entity.HasIndex(e => e.IdTipoEmpleado)
                    .HasName("fk_Empleado_2");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_Empleado_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaInicioActividad)
                    .HasColumnName("fecha_inicio_actividad")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("id_persona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoEmpleado)
                    .HasColumnName("id_tipo_empleado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Empleado_3");

                entity.HasOne(d => d.IdTipoEmpleadoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdTipoEmpleado)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Empleado_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Empleado_1");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasIndex(e => e.IdTipoDocumento)
                    .HasName("fk_Persona_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fecha_nacimiento")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTipoDocumento)
                    .HasColumnName("id_tipo_documento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NroDocumento)
                    .IsRequired()
                    .HasColumnName("nro_documento")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TelefonoMovil)
                    .HasColumnName("telefono_movil")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdTipoDocumentoNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .HasConstraintName("fk_Persona_1");
            });

            modelBuilder.Entity<Residencia>(entity =>
            {
                entity.HasIndex(e => e.Codigo)
                    .HasName("codigo")
                    .IsUnique();

                entity.HasIndex(e => e.IdBarrio)
                    .HasName("fk_Residencia_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.IdBarrio)
                    .HasColumnName("id_barrio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasColumnName("ubicacion")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.Residencia)
                    .HasForeignKey(d => d.IdBarrio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Residencia_1");
            });

            modelBuilder.Entity<Residente>(entity =>
            {
                entity.HasIndex(e => e.IdPersona)
                    .HasName("fk_Residente_3");

                entity.HasIndex(e => e.IdResidencia)
                    .HasName("fk_Residente_2");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_Residente_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("fecha_ingreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("id_persona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidencia)
                    .HasColumnName("id_residencia")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Residente)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Residente_3");

                entity.HasOne(d => d.IdResidenciaNavigation)
                    .WithMany(p => p.Residente)
                    .HasForeignKey(d => d.IdResidencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Residente_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Residente)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Residente_1");
            });

            modelBuilder.Entity<ResidentesXresidencia>(entity =>
            {
                entity.HasKey(e => new { e.IdResidencia, e.IdResidente })
                    .HasName("PK_ResidentesXResidencia");

                entity.ToTable("ResidentesXResidencia");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_ResidentesXResidencia_2");

                entity.Property(e => e.IdResidencia)
                    .HasColumnName("id_residencia")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdResidenciaNavigation)
                    .WithMany(p => p.ResidentesXresidencia)
                    .HasForeignKey(d => d.IdResidencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ResidentesXResidencia_1");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.ResidentesXresidencia)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ResidentesXResidencia_2");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.ToTable("Tipo_Documento");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<TipoEmpleado>(entity =>
            {
                entity.ToTable("Tipo_Empleado");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("email")
                    .IsUnique();

                entity.HasIndex(e => e.IdPerfil)
                    .HasName("fk_Usuario_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("id_perfil")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NombreUsuario)
                    .HasColumnName("nombre_usuario")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("fk_Usuario_1");
            });

            modelBuilder.Entity<UsuarioXbarrio>(entity =>
            {
                entity.HasKey(e => new { e.IdBarrio, e.IdUsuario })
                    .HasName("PK_UsuarioXBarrio");

                entity.ToTable("UsuarioXBarrio");

                entity.HasIndex(e => e.IdBarrio)
                    .HasName("fk_UsuarioXBarrio_2_idx");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_UsuarioXBarrio_2");

                entity.Property(e => e.IdBarrio)
                    .HasColumnName("id_barrio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.UsuarioXbarrio)
                    .HasForeignKey(d => d.IdBarrio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_UsuarioXBarrio_1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioXbarrio)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_UsuarioXBarrio_2");
            });
        }
    }
}