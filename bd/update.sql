-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema Prihood
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `Prihood` ;

-- -----------------------------------------------------
-- Schema Prihood
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Prihood` DEFAULT CHARACTER SET utf8 ;
USE `Prihood` ;

-- -----------------------------------------------------
-- Table `Prihood`.`Barrio`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Barrio` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Barrio` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(100) NOT NULL,
  `ubicacion` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Perfil`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Perfil` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Perfil` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Usuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Usuario` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Usuario` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `email` VARCHAR(50) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `avatar` VARCHAR(100) NULL DEFAULT NULL,
  `id_perfil` INT(11) NOT NULL,
  `id_barrio` INT(11) NULL DEFAULT NULL,
  `token` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `email` (`email` ASC),
  INDEX `fk_Usuario_1` (`id_barrio` ASC),
  INDEX `fk_Usuario_2` (`id_perfil` ASC),
  CONSTRAINT `fk_Usuario_1`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Usuario_2`
    FOREIGN KEY (`id_perfil`)
    REFERENCES `Prihood`.`Perfil` (`id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Tipo_Documento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Tipo_Documento` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Documento` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Persona`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Persona` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Persona` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `id_tipo_documento` INT(11) NOT NULL,
  `nro_documento` VARCHAR(45) NOT NULL,
  `telefono_movil` VARCHAR(45) NULL DEFAULT NULL,
  `fecha_nacimiento` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Persona_1` (`id_tipo_documento` ASC),
  CONSTRAINT `fk_Persona_1`
    FOREIGN KEY (`id_tipo_documento`)
    REFERENCES `Prihood`.`Tipo_Documento` (`id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Empleado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Empleado` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Empleado` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `fecha_inicio_actividad` DATETIME NULL DEFAULT NULL,
  `id_usuario` INT(11) NOT NULL,
  `id_persona` INT(11) NOT NULL,
  `id_barrio` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Empleado_1` (`id_usuario` ASC),
  INDEX `fk_Empleado_2` (`id_persona` ASC),
  INDEX `fk_Empleado_3` (`id_barrio` ASC),
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
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`EventoVisita`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`EventoVisita` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`EventoVisita` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Residencia`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Residencia` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Residencia` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(50) NOT NULL,
  `ubicacion` VARCHAR(100) NOT NULL,
  `codigo` VARCHAR(6) NOT NULL,
  `id_barrio` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `codigo` (`codigo` ASC),
  INDEX `fk_Residencia_1` (`id_barrio` ASC),
  CONSTRAINT `fk_Residencia_1`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Residente`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Residente` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Residente` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `id_persona` INT(11) NOT NULL,
  `fecha_ingreso` DATETIME NULL DEFAULT NULL,
  `id_residencia` INT(11) NOT NULL,
  `id_usuario` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Residente_1` (`id_usuario` ASC),
  INDEX `fk_Residente_2` (`id_residencia` ASC),
  INDEX `fk_Residente_3` (`id_persona` ASC),
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
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Tipo_Servicio`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Tipo_Servicio` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Servicio` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Proveedor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Proveedor` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Proveedor` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(20) NOT NULL,
  `descripcion` TEXT NULL DEFAULT NULL,
  `telefono` VARCHAR(15) NULL DEFAULT NULL,
  `id_tipo_servicio` INT(11) NOT NULL,
  `id_residente_recomienda` INT(11) NOT NULL,
  `avatar` VARCHAR(20) NOT NULL,
  `direccion` VARCHAR(40) NULL DEFAULT NULL,
  `rating_total` INT(11) NOT NULL,
  `cantidad_votos` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_id_tipo_servicio` (`id_tipo_servicio` ASC),
  INDEX `fk_id_residente_recomienda` (`id_residente_recomienda` ASC),
  CONSTRAINT `fk_id_residente_recomienda`
    FOREIGN KEY (`id_residente_recomienda`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_tipo_servicio`
    FOREIGN KEY (`id_tipo_servicio`)
    REFERENCES `Prihood`.`Tipo_Servicio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`RegistroVotos`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`RegistroVotos` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`RegistroVotos` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `fecha` DATETIME NOT NULL,
  `id_proveedor` INT(11) NOT NULL,
  `comentario` TEXT NULL DEFAULT NULL,
  `id_residente` INT(11) NOT NULL,
  `rating` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_id_proveedor` (`id_proveedor` ASC),
  INDEX `fk_id_residente` (`id_residente` ASC),
  CONSTRAINT `fk_id_proveedor`
    FOREIGN KEY (`id_proveedor`)
    REFERENCES `Prihood`.`Proveedor` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_residente`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`TipoVisita`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`TipoVisita` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`TipoVisita` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Visitante`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Visitante` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Visitante` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `id_tipo_visita` INT(11) NOT NULL,
  `id_residente` INT(11) NOT NULL,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `id_tipo_documento` INT(11) NULL DEFAULT NULL,
  `numero_documento` VARCHAR(45) NULL DEFAULT NULL,
  `patente` VARCHAR(45) NULL DEFAULT NULL,
  `observaciones` VARCHAR(100) NULL DEFAULT NULL,
  `fecha_visita` DATETIME NULL DEFAULT NULL,
  `avatar` VARCHAR(100) NULL DEFAULT NULL,
  `estado` VARCHAR(30) NULL DEFAULT 'esperando',
  PRIMARY KEY (`id`),
  INDEX `id_tipo_visita_idx` (`id_tipo_visita` ASC),
  INDEX `id_tipo_documento_idx` (`id_tipo_documento` ASC),
  INDEX `id_residente` (`id_residente` ASC),
  CONSTRAINT `id_residente`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_tipo_documento`
    FOREIGN KEY (`id_tipo_documento`)
    REFERENCES `Prihood`.`Tipo_Documento` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_tipo_visita`
    FOREIGN KEY (`id_tipo_visita`)
    REFERENCES `Prihood`.`TipoVisita` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Visita`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Visita` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Visita` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `id_evento` INT(11) NOT NULL,
  `fecha` DATETIME NOT NULL,
  `id_visitante` INT(11) NOT NULL,
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
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`VisitasXResidente`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`VisitasXResidente` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`VisitasXResidente` (
  `id_visita` INT(11) NOT NULL,
  `id_residente` INT(11) NOT NULL,
  PRIMARY KEY (`id_visita`, `id_residente`),
  INDEX `id_residente_idx` (`id_residente` ASC),
  CONSTRAINT `id_residente_3`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_visita_2`
    FOREIGN KEY (`id_visita`)
    REFERENCES `Prihood`.`Visita` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Tipo_Amenitie`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Tipo_Amenitie` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Amenitie` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Amenitie`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Amenitie` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Amenitie` (
  `id` INT NOT NULL AUTO_INCREMENT,
<<<<<<< HEAD
  `nombre` VARCHAR(45) NOT NULL,
  `id_barrio` INT NOT NULL,
  `id_tipo_amenitie` INT NOT NULL,
=======
  `nombre` VARCHAR(20) NOT NULL,
  `descripcion` text NULL,
  `telefono` VARCHAR(15) NULL,
  `id_tipo_servicio` INT NOT NULL,
  `id_residente_recomienda` INT NOT NULL,
  `avatar` VARCHAR(50) NOT NULL,
  `direccion` VARCHAR(40) NULL,
  `rating_total` INT NOT NULL,
  `cantidad_votos` INT NOT NULL,
>>>>>>> 761104642c5718c50db929b3882d2b1d8b019789
  PRIMARY KEY (`id`),
  INDEX `fk_Amenitie_1_idx` (`id_tipo_amenitie` ASC),
  INDEX `fk_Amenitie_2_idx` (`id_barrio` ASC),
  CONSTRAINT `fk_Amenitie_1`
    FOREIGN KEY (`id_tipo_amenitie`)
    REFERENCES `Prihood`.`Tipo_Amenitie` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Amenitie_2`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `Prihood`.`Reserva`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Prihood`.`Reserva` ;

CREATE TABLE IF NOT EXISTS `Prihood`.`Reserva` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `fecha` DATE NOT NULL,
  `hora_desde` TIME NOT NULL,
  `hora_hasta` TIME NOT NULL,
  `id_amenitie` INT NOT NULL,
  `id_residente` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Reserva_1_idx` (`id_residente` ASC),
  INDEX `fk_Reserva_2_idx` (`id_amenitie` ASC),
  CONSTRAINT `fk_Reserva_1`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reserva_2`
    FOREIGN KEY (`id_amenitie`)
    REFERENCES `Prihood`.`Amenitie` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;



-- Inserción de valores a tabla PERFIL

INSERT INTO Perfil (id, descripcion) VALUES ("1", "Root"), ("2", "Administrador"), ("3", "Residente"), ("4", "Encargado de Seguridad");

-- Inserción de valores a tabla Tipo_Documento 

INSERT INTO  Tipo_Servicio(descripcion) VALUES ("Piscinas"), ("Jardinería"), ("Plomería"), ("Gasista"), ("Fumigación"), ("Otros");

-- Inserción de valores a tabla Tipo_Documento 

INSERT INTO  Tipo_Documento(descripcion) VALUES ("Documento Único"), ("Libreta de Enrolamiento"), ("Libreta Cívica"), ("Otro");

-- Inserción de valores a tabla Tipo_Documento 

INSERT INTO  Tipo_Amenitie(descripcion) VALUES ("Salón"), ("Canchas Tenis"), ("Canchas Fútbol"), ("Canchas Paddle"), ("Terraza");

-- Inserción de usuarios por defecto de prueba

INSERT INTO Usuario(email, password, id_perfil) VALUES("admin@admin.com","8c6976e5b5410415bde908bd4dee15","1");

-- Inserción de tipos de visita

INSERT INTO  TipoVisita(id, nombre) VALUES ("1", "Frecuente"), ("2", "Actual");

-- Inserción de eventos visita

INSERT INTO  EventoVisita(id, nombre) VALUES ("1", "Ingreso"), ("2", "Egreso");