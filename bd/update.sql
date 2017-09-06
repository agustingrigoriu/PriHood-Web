-- Agustín Gregorieu 09/06/2017


SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema Prihood
-- -----------------------------------------------------
-- Base de datos del proyecto Prihood.
 DROP SCHEMA IF EXISTS `Prihood` ;

-- -----------------------------------------------------
-- Schema Prihood
--
-- Base de datos del proyecto Prihood.
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Prihood` DEFAULT CHARACTER SET utf8 ;
USE `Prihood` ;

-- -----------------------------------------------------
-- Table `Prihood`.`Perfil`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Perfil` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Usuario` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `email` VARCHAR(50) UNIQUE NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `avatar` VARCHAR(100) NULL,
  `id_perfil` INT NOT NULL,
  `id_barrio` INT NULL,
  `token` VARCHAR(255) NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_Usuario_1`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Usuario_2`
    FOREIGN KEY (`id_perfil`)
    REFERENCES `Prihood`.`Perfil` (`id`)
    ON DELETE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Residencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Residencia` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(50) NOT NULL,
  `ubicacion` VARCHAR(100) NOT NULL,
  `codigo` VARCHAR(6) NOT NULL UNIQUE,
  `id_barrio` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_Residencia_1`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `Prihood`.`Tipo_Documento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Documento` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Persona`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Persona` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `id_tipo_documento` INT NOT NULL,
  `nro_documento` VARCHAR(45) NOT NULL,
  `telefono_movil` VARCHAR(45) NULL,
  `fecha_nacimiento` DATETIME NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_Persona_1`
    FOREIGN KEY (`id_tipo_documento`)
    REFERENCES `Prihood`.`Tipo_Documento` (`id`)
    ON DELETE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Residente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Residente` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `id_persona` INT NOT NULL,
  `fecha_ingreso` DATETIME NULL,
  `id_residencia` INT NOT NULL,
  `id_usuario` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_Residente_1`
    FOREIGN KEY (`id_usuario`)
    REFERENCES `Prihood`.`Usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Residente_2`
    FOREIGN KEY (`id_residencia`)
    REFERENCES `Prihood`.`Residencia` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Residente_3`
    FOREIGN KEY (`id_persona`)
    REFERENCES `Prihood`.`Persona` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Empleado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Empleado` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `fecha_inicio_actividad` DATETIME NULL,
  `id_usuario` INT NOT NULL,
  `id_persona` INT NOT NULL,
  `id_barrio` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_Empleado_1`
    FOREIGN KEY (`id_usuario`)
    REFERENCES `Prihood`.`Usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Empleado_2`
    FOREIGN KEY (`id_persona`)
    REFERENCES `Prihood`.`Persona` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Empleado_3`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


-- Se agrega nueva tabla de barrios

CREATE TABLE IF NOT EXISTS `Prihood`.`Barrio` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(100) NOT NULL,
  `ubicacion` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB;


-- Se agrega nuevas tablas para las visitas

CREATE TABLE IF NOT EXISTS `Prihood`.`TipoVisita` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`));

CREATE TABLE IF NOT EXISTS `Prihood`.`Visitante` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `id_tipo_visita` INT NOT NULL,
  `id_residente` INT NOT NULL,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `id_tipo_documento` INT NULL,
  `numero_documento` VARCHAR(45) NULL,
  `patente` VARCHAR(45) NULL,
  `observaciones` VARCHAR(100) NULL,
  `fecha_visita` DATETIME NULL,
  `avatar` VARCHAR(100) NULL,
  `estado` VARCHAR(30) DEFAULT 'esperando',
  PRIMARY KEY (`id`),
  INDEX `id_tipo_visita_idx` (`id_tipo_visita` ASC),
  INDEX `id_tipo_documento_idx` (`id_tipo_documento` ASC),
  CONSTRAINT `id_tipo_visita`
    FOREIGN KEY (`id_tipo_visita`)
    REFERENCES `Prihood`.`TipoVisita` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_residente`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_tipo_documento`
    FOREIGN KEY (`id_tipo_documento`)
    REFERENCES `Prihood`.`Tipo_Documento` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE TABLE IF NOT EXISTS `Prihood`.`EventoVisita` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`));


CREATE TABLE IF NOT EXISTS `Prihood`.`Visita` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `id_evento` INT NOT NULL,
  `fecha` DATETIME NOT NULL,
  `id_visitante` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `id_evento_idx` (`id_evento` ASC),
  INDEX `id_visitante_idx` (`id_visitante` ASC),
  CONSTRAINT `id_evento`
    FOREIGN KEY (`id_evento`)
    REFERENCES `Prihood`.`EventoVisita` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_visitante`
    FOREIGN KEY (`id_visitante`)
    REFERENCES `Prihood`.`Visitante` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE IF NOT EXISTS `Prihood`.`VisitasXResidente` (
  `id_visita` INT NOT NULL,
  `id_residente` INT NOT NULL,
  PRIMARY KEY (`id_visita`, `id_residente`),
  INDEX `id_residente_idx` (`id_residente` ASC),
  CONSTRAINT `id_visita_2`
    FOREIGN KEY (`id_visita`)
    REFERENCES `Prihood`.`Visita` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_residente_3`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

  CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Servicio` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

  CREATE TABLE IF NOT EXISTS `Prihood`.`Proveedor` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(20) NOT NULL,
  `descripcion` text NULL,
  `telefono` VARCHAR(15) NULL,
  `id_tipo_servicio` INT NOT NULL,
  `id_residente_recomienda` INT NOT NULL,
  `avatar` VARCHAR(50) NOT NULL,
  `direccion` VARCHAR(40) NULL,
  `rating_total` INT NOT NULL,
  `cantidad_votos` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_id_tipo_servicio`
    FOREIGN KEY (`id_tipo_servicio`)
    REFERENCES `Prihood`.`Tipo_Servicio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_residente_recomienda`
    FOREIGN KEY (`id_residente_recomienda`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

   CREATE TABLE IF NOT EXISTS `Prihood`.`RegistroVotos` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `fecha` DATETIME NOT NULL,
  `id_proveedor` INT NOT NULL,
  `comentario` TEXT NULL,
  `id_residente` INT NOT NULL,
  `rating` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_id_proveedor`
    FOREIGN KEY (`id_proveedor`)
    REFERENCES `Prihood`.`Proveedor` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_residente`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- -----------------------------------------------------
-- Módulo Amenities
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Amenity` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Prihood`.`Estado_Reserva` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Prihood`.`Dia_Semana` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Prihood`.`Amenity` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(20) NOT NULL,
  `ubicacion` VARCHAR(20) NOT NULL,
  `descripcion` text NULL,
  `telefono` VARCHAR(15) NULL,
  `id_tipo_amenity` INT NOT NULL,
  `id_barrio` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_id_tipo_amenity`
    FOREIGN KEY (`id_tipo_amenity`)
    REFERENCES `Prihood`.`Tipo_Amenity` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_barrio_amenity`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE IF NOT EXISTS `Prihood`.`Turno` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(20) NOT NULL,
  `hora_desde` TIME NOT NULL,
  `duracion` INT NOT NULL,
  `costo` FLOAT NOT NULL,
  `id_amenity` INT NOT NULL,
  `id_dia_semana` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_id_amenity`
    FOREIGN KEY (`id_amenity`)
    REFERENCES `Prihood`.`Amenity` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_dia_semana`
    FOREIGN KEY (`id_dia_semana`)
    REFERENCES `Prihood`.`Dia_Semana` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION); 

CREATE TABLE IF NOT EXISTS `Prihood`.`Reserva` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `fecha` DATETIME NOT NULL,
  `observaciones` text NULL,
  `costo` FLOAT NOT NULL,
  `id_turno` INT NOT NULL,
  `id_residente` INT NOT NULL,
  `id_estado_reserva` INT NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_id_turno`
    FOREIGN KEY (`id_turno`)
    REFERENCES `Prihood`.`Turno` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_residente_1`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_estado_reserva`
    FOREIGN KEY (`id_estado_reserva`)
    REFERENCES `Prihood`.`Estado_Reserva` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
    );     

-- Inserción de valores a tabla PERFIL

INSERT INTO Perfil (id, descripcion) VALUES ("1", "Root"), ("2", "Administrador"), ("3", "Residente"), ("4", "Encargado de Seguridad");

-- Inserción de valores a tabla Tipo_Documento 

INSERT INTO  Tipo_Servicio(descripcion) VALUES ("Piscinas"), ("Jardinería"), ("Plomería"), ("Gasista"), ("Fumigación"), ("Otros");

-- Inserción de valores a tabla Tipo_Documento 

INSERT INTO  Tipo_Documento(descripcion) VALUES ("Documento Único"), ("Libreta de Enrolamiento"), ("Libreta Cívica"), ("Otro");

-- Inserción de usuarios por defecto de prueba

INSERT INTO Usuario(email, password, id_perfil) VALUES("admin@admin.com","8c6976e5b5410415bde908bd4dee15","1");

-- Inserción de tipos de visita

INSERT INTO  TipoVisita(id, nombre) VALUES ("1", "Frecuente"), ("2", "Actual");

-- Inserción de eventos visita

INSERT INTO  EventoVisita(id, nombre) VALUES ("1", "Ingreso"), ("2", "Egreso");

-- Inserción de tipoes de amenities

INSERT INTO  Tipo_Amenity(id, descripcion) VALUES ("1", "Cancha de tenis"), ("2", "Cancha de fútbol"), ("3", "Cancha de paddle"), ("4", "Salón"), ("5", "Cancha de futbol"), ("6", "Piscinas");