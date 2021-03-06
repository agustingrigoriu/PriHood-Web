﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PriHood.Models
{
    public partial class PrihoodContext : DbContext
    {
        public virtual DbSet<Alertas> Alertas { get; set; }
        public virtual DbSet<Amenity> Amenity { get; set; }
        public virtual DbSet<AsistenciaEvento> AsistenciaEvento { get; set; }
        public virtual DbSet<Barrio> Barrio { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<ComentariosEvento> ComentariosEvento { get; set; }
        public virtual DbSet<DiaSemana> DiaSemana { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EstadoReserva> EstadoReserva { get; set; }
        public virtual DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
        public virtual DbSet<EventoVisita> EventoVisita { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
        public virtual DbSet<Expensas> Expensas { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Publicacion> Publicacion { get; set; }
        public virtual DbSet<RegistroVotos> RegistroVotos { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Residencia> Residencia { get; set; }
        public virtual DbSet<Residente> Residente { get; set; }
        public virtual DbSet<SolicitudViaje> SolicitudViaje { get; set; }
        public virtual DbSet<TipoAlerta> TipoAlerta { get; set; }
        public virtual DbSet<TipoAmenity> TipoAmenity { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoEvento> TipoEvento { get; set; }
        public virtual DbSet<TipoServicio> TipoServicio { get; set; }
        public virtual DbSet<TipoVisita> TipoVisita { get; set; }
        public virtual DbSet<Trayecto> Trayecto { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Viaje> Viaje { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }
        public virtual DbSet<Visitante> Visitante { get; set; }
        public virtual DbSet<VisitasXresidente> VisitasXresidente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost;database=Prihood;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alertas>(entity =>
            {
                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_Alertas_1_idx");

                entity.HasIndex(e => e.IdTipoAlerta)
                    .HasName("fk_Alertas_2_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("text");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoAlerta)
                    .HasColumnName("id_tipo_alerta")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Visto)
                    .HasColumnName("visto")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.Alertas)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Alertas_1");

                entity.HasOne(d => d.IdTipoAlertaNavigation)
                    .WithMany(p => p.Alertas)
                    .HasForeignKey(d => d.IdTipoAlerta)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Alertas_2");
            });

            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.HasIndex(e => e.IdBarrio)
                    .HasName("fk_id_barrio_amenity");

                entity.HasIndex(e => e.IdTipoAmenity)
                    .HasName("fk_id_tipo_amenity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("text");

                entity.Property(e => e.IdBarrio)
                    .HasColumnName("id_barrio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoAmenity)
                    .HasColumnName("id_tipo_amenity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Ubicacion)
                    .HasColumnName("ubicacion")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.Amenity)
                    .HasForeignKey(d => d.IdBarrio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_barrio_amenity");

                entity.HasOne(d => d.IdTipoAmenityNavigation)
                    .WithMany(p => p.Amenity)
                    .HasForeignKey(d => d.IdTipoAmenity)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_tipo_amenity");
            });

            modelBuilder.Entity<AsistenciaEvento>(entity =>
            {
                entity.ToTable("Asistencia_Evento");

                entity.HasIndex(e => e.IdEvento)
                    .HasName("fk_Asistencia_Evento_2_idx");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_Asistencia_Evento_1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdEvento)
                    .HasColumnName("id_evento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.AsistenciaEvento)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Asistencia_Evento_2");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.AsistenciaEvento)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Asistencia_Evento_1");
            });

            modelBuilder.Entity<Barrio>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasColumnName("ubicacion")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasIndex(e => e.IdPublicacion)
                    .HasName("fk_Comentario_1_idx");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_Comentario_2_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPublicacion)
                    .HasColumnName("id_publicacion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.IdPublicacion)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Comentario_1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Comentario_2");
            });

            modelBuilder.Entity<ComentariosEvento>(entity =>
            {
                entity.ToTable("Comentarios_Evento");

                entity.HasIndex(e => e.IdEvento)
                    .HasName("fk_Comentarios_Evento_1_idx");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_Comentarios_Evento_2_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdEvento)
                    .HasColumnName("id_evento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.ComentariosEvento)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Comentarios_Evento_1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ComentariosEvento)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Comentarios_Evento_2");
            });

            modelBuilder.Entity<DiaSemana>(entity =>
            {
                entity.ToTable("Dia_Semana");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");
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

            modelBuilder.Entity<EstadoReserva>(entity =>
            {
                entity.ToTable("Estado_Reserva");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<EstadoSolicitud>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(255)");
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

            modelBuilder.Entity<Eventos>(entity =>
            {
                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_Eventos_1_idx");

                entity.HasIndex(e => e.IdTipoEvento)
                    .HasName("fk_Eventos_2_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("text");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.HoraDesde)
                    .HasColumnName("hora_desde")
                    .HasColumnType("time");

                entity.Property(e => e.HoraHasta)
                    .HasColumnName("hora_hasta")
                    .HasColumnType("time");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoEvento)
                    .HasColumnName("id_tipo_evento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Eventos_1");

                entity.HasOne(d => d.IdTipoEventoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdTipoEvento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Eventos_2");
            });

            modelBuilder.Entity<Expensas>(entity =>
            {
                entity.HasIndex(e => e.IdResidencia)
                    .HasName("fk_Expensas_1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaExpensa)
                    .HasColumnName("fecha_expensa")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaTransaccion)
                    .HasColumnName("fecha_transaccion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnName("fecha_vencimiento")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdResidencia)
                    .HasColumnName("id_residencia")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .HasColumnType("text");

                entity.Property(e => e.Pagado)
                    .HasColumnName("pagado")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UrlExpensa)
                    .IsRequired()
                    .HasColumnName("url_expensa")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdResidenciaNavigation)
                    .WithMany(p => p.Expensas)
                    .HasForeignKey(d => d.IdResidencia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Expensas_1");
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

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasIndex(e => e.IdResidenteRecomienda)
                    .HasName("fk_id_residente_recomienda");

                entity.HasIndex(e => e.IdTipoServicio)
                    .HasName("fk_id_tipo_servicio");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CantidadVotos)
                    .HasColumnName("cantidad_votos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("text");

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.IdResidenteRecomienda)
                    .HasColumnName("id_residente_recomienda")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoServicio)
                    .HasColumnName("id_tipo_servicio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RatingTotal)
                    .HasColumnName("rating_total")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasColumnType("varchar(15)");

                entity.HasOne(d => d.IdResidenteRecomiendaNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.IdResidenteRecomienda)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_residente_recomienda");

                entity.HasOne(d => d.IdTipoServicioNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.IdTipoServicio)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_tipo_servicio");
            });

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.HasIndex(e => e.IdPersonal)
                    .HasName("fk_Publicacion_2_idx");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_Publicacion_1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPersonal)
                    .HasColumnName("id_personal")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdPersonalNavigation)
                    .WithMany(p => p.PublicacionIdPersonalNavigation)
                    .HasForeignKey(d => d.IdPersonal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_Publicacion_2");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.PublicacionIdResidenteNavigation)
                    .HasForeignKey(d => d.IdResidente)
                    .HasConstraintName("fk_Publicacion_1");
            });

            modelBuilder.Entity<RegistroVotos>(entity =>
            {
                entity.HasIndex(e => e.IdProveedor)
                    .HasName("fk_id_proveedor");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_id_residente");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comentario)
                    .HasColumnName("comentario")
                    .HasColumnType("text");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdProveedor)
                    .HasColumnName("id_proveedor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.RegistroVotos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_proveedor");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.RegistroVotos)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_residente");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasIndex(e => e.IdEstadoReserva)
                    .HasName("fk_id_estado_reserva");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_id_residente_1");

                entity.HasIndex(e => e.IdTurno)
                    .HasName("fk_id_turno");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdEstadoReserva)
                    .HasColumnName("id_estado_reserva")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTurno)
                    .HasColumnName("id_turno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdEstadoReservaNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdEstadoReserva)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_estado_reserva");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_residente_1");

                entity.HasOne(d => d.IdTurnoNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdTurno)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_turno");
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

            modelBuilder.Entity<SolicitudViaje>(entity =>
            {
                entity.HasIndex(e => e.IdEstadoSolicitud)
                    .HasName("fk_estado_solicitudViaje_idx");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("fk_residente_solicitudViaje_idx");

                entity.HasIndex(e => e.IdViaje)
                    .HasName("fk_viaje_solicitudViaje_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdEstadoSolicitud)
                    .HasColumnName("id_estado_solicitud")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("id_viaje")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdEstadoSolicitudNavigation)
                    .WithMany(p => p.SolicitudViaje)
                    .HasForeignKey(d => d.IdEstadoSolicitud)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_estado_solicitudViaje");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.SolicitudViaje)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_residente_solicitudViaje");

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.SolicitudViaje)
                    .HasForeignKey(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_viaje_solicitudViaje");
            });

            modelBuilder.Entity<TipoAlerta>(entity =>
            {
                entity.ToTable("Tipo_Alerta");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnName("imagen")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<TipoAmenity>(entity =>
            {
                entity.ToTable("Tipo_Amenity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasColumnName("imagen")
                    .HasColumnType("varchar(100)");
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

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.ToTable("Tipo_Evento");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<TipoServicio>(entity =>
            {
                entity.ToTable("Tipo_Servicio");

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

            modelBuilder.Entity<Trayecto>(entity =>
            {
                entity.HasIndex(e => e.IdViaje)
                    .HasName("fk_viaje_trayecto_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("id_viaje")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.Orden)
                    .HasColumnName("orden")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.Trayecto)
                    .HasForeignKey(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_viaje_trayecto");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasIndex(e => e.IdAmenity)
                    .HasName("fk_id_amenity");

                entity.HasIndex(e => e.IdDiaSemana)
                    .HasName("fk_id_dia_semana");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Duracion)
                    .HasColumnName("duracion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HoraDesde)
                    .HasColumnName("hora_desde")
                    .HasColumnType("time");

                entity.Property(e => e.IdAmenity)
                    .HasColumnName("id_amenity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdDiaSemana)
                    .HasColumnName("id_dia_semana")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdAmenityNavigation)
                    .WithMany(p => p.Turno)
                    .HasForeignKey(d => d.IdAmenity)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_amenity");

                entity.HasOne(d => d.IdDiaSemanaNavigation)
                    .WithMany(p => p.Turno)
                    .HasForeignKey(d => d.IdDiaSemana)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_dia_semana");
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

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdBarrioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdBarrio)
                    .HasConstraintName("fk_Usuario_1");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("fk_Usuario_2");
            });

            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.HasIndex(e => e.IdDiaSemana)
                    .HasName("id_dia_semana_idx");

                entity.HasIndex(e => e.IdResidente)
                    .HasName("id_residente_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AutoAsientos)
                    .HasColumnName("auto_asientos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AutoColor)
                    .HasColumnName("auto_color")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AutoModelo)
                    .HasColumnName("auto_modelo")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AutoPatente)
                    .IsRequired()
                    .HasColumnName("auto_patente")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Destino)
                    .HasColumnName("destino")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasColumnType("time");

                entity.Property(e => e.IdDiaSemana)
                    .HasColumnName("id_dia_semana")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SaleBarrio)
                    .HasColumnName("sale_barrio")
                    .HasColumnType("bit(1)");

                entity.HasOne(d => d.IdDiaSemanaNavigation)
                    .WithMany(p => p.Viaje)
                    .HasForeignKey(d => d.IdDiaSemana)
                    .HasConstraintName("fk_dia_viaje");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.Viaje)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_residente_viaje");
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
                entity.HasIndex(e => e.IdResidente)
                    .HasName("id_residente");

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

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("esperando");

                entity.Property(e => e.FechaVisita)
                    .HasColumnName("fecha_visita")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdResidente)
                    .HasColumnName("id_residente")
                    .HasColumnType("int(11)");

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
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Observaciones)
                    .HasColumnName("observaciones")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Patente)
                    .HasColumnName("patente")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdResidenteNavigation)
                    .WithMany(p => p.Visitante)
                    .HasForeignKey(d => d.IdResidente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_residente");

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
                    .HasConstraintName("id_residente_3");

                entity.HasOne(d => d.IdVisitaNavigation)
                    .WithMany(p => p.VisitasXresidente)
                    .HasForeignKey(d => d.IdVisita)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("id_visita_2");
            });
        }
    }
}