using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PriHood.Models
{
    public partial class PrihoodContext : DbContext
    {
        public PrihoodContext(DbContextOptions<PrihoodContext> options) : base(options)
        {
        }

        public PrihoodContext() : base()
        {
        }
        public virtual DbSet<Barrio> Barrio { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EventoVisita> EventoVisita { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Residencia> Residencia { get; set; }
        public virtual DbSet<Residente> Residente { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoVisita> TipoVisita { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }
        public virtual DbSet<Visitante> Visitante { get; set; }
        public virtual DbSet<VisitasXresidente> VisitasXresidente { get; set; }

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
                entity.HasIndex(e => e.IdBarrio)
                    .HasName("fk_Empleado_3");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("fk_Empleado_2");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_Empleado_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaInicioActividad)
                    .HasColumnName("fecha_inicio_actividad")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdBarrio)
                    .HasColumnName("id_barrio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("id_persona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdBarrio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Empleado_3");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Empleado_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Empleado_1");
            });

            modelBuilder.Entity<EventoVisita>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)");
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

            modelBuilder.Entity<TipoVisita>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("email")
                    .IsUnique();

                entity.HasIndex(e => e.IdBarrio)
                    .HasName("fk_Usuario_1");

                entity.HasIndex(e => e.IdPerfil)
                    .HasName("fk_Usuario_2");

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

                entity.Property(e => e.IdBarrio)
                    .HasColumnName("id_barrio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("id_perfil")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdBarrio)
                    .HasConstraintName("fk_Usuario_1");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("fk_Usuario_2");
            });

            modelBuilder.Entity<Visita>(entity =>
            {
                entity.HasIndex(e => e.IdEvento)
                    .HasName("id_evento_idx");

                entity.HasIndex(e => e.IdVisitante)
                    .HasName("id_visitante_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdEvento)
                    .HasColumnName("id_evento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdVisitante)
                    .HasColumnName("id_visitante")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Visita)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_evento");

                entity.HasOne(d => d.IdVisitanteNavigation)
                    .WithMany(p => p.Visita)
                    .HasForeignKey(d => d.IdVisitante)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_visitante");
            });

            modelBuilder.Entity<Visitante>(entity =>
            {
                entity.HasIndex(e => e.IdTipoDocumento)
                    .HasName("id_tipo_documento_idx");

                entity.HasIndex(e => e.IdTipoVisita)
                    .HasName("id_tipo_visita_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FechaVisita)
                    .HasColumnName("fecha_visita")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTipoDocumento)
                    .HasColumnName("id_tipo_documento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoVisita)
                    .HasColumnName("id_tipo_visita")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NumeroDocumento)
                    .HasColumnName("numero_documento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Patente)
                    .HasColumnName("patente")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdTipoDocumentoNavigation)
                    .WithMany(p => p.Visitante)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .HasConstraintName("id_tipo_documento");

                entity.HasOne(d => d.IdTipoVisitaNavigation)
                    .WithMany(p => p.Visitante)
                    .HasForeignKey(d => d.IdTipoVisita)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_tipo_visita");
            });

            modelBuilder.Entity<VisitasXresidente>(entity =>
            {
                entity.HasKey(e => new { e.IdVisita, e.IdResidente })
                    .HasName("PK_VisitasXResidente");

                entity.ToTable("VisitasXResidente");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("id_residente_idx");

                entity.Property(e => e.IdVisita)
                    .HasColumnName("id_visita")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.VisitasXresidente)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_residente");

                entity.HasOne(d => d.IdVisitaNavigation)
                    .WithMany(p => p.VisitasXresidente)
                    .HasForeignKey(d => d.IdVisita)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_visita");
            });
        }
    }
}