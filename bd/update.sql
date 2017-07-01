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
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `descripcion_UNIQUE` (`descripcion` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Usuario` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre_usuario` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `id_perfil` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `nombre_usuario_UNIQUE` (`nombre_usuario` ASC),
  INDEX `fk_Usuario_1_idx` (`id_perfil` ASC),
  CONSTRAINT `fk_Usuario_1`
    FOREIGN KEY (`id_perfil`)
    REFERENCES `Prihood`.`Perfil` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Residencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Residencia` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `ubicacion` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `ubicacion_UNIQUE` (`ubicacion` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Tipo_Documento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Documento` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `descripcion_UNIQUE` (`descripcion` ASC))
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
  `fecha_nacimiento` DATE NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  INDEX `fk_Persona_1_idx` (`id_tipo_documento` ASC),
  CONSTRAINT `fk_Persona_1`
    FOREIGN KEY (`id_tipo_documento`)
    REFERENCES `Prihood`.`Tipo_Documento` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Residente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Residente` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `id_persona` INT NOT NULL,
  `fecha_ingreso` DATE NULL,
  `id_residencia` INT NOT NULL,
  `id_usuario` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `id_persona_UNIQUE` (`id_persona` ASC),
  UNIQUE INDEX `id_usuario_UNIQUE` (`id_usuario` ASC),
  INDEX `fk_Residente_2_idx` (`id_residencia` ASC),
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
-- Table `Prihood`.`Tipo_Empleado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Tipo_Empleado` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descripcion` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `descripcion_UNIQUE` (`descripcion` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`Empleado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`Empleado` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `fecha_inicio_actividad` DATE NULL,
  `id_tipo_empleado` INT NOT NULL,
  `id_usuario` INT NOT NULL,
  `id_persona` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `id_usuario_UNIQUE` (`id_usuario` ASC),
  INDEX `fk_Empleado_2_idx` (`id_tipo_empleado` ASC),
  UNIQUE INDEX `id_persona_UNIQUE` (`id_persona` ASC),
  CONSTRAINT `fk_Empleado_1`
    FOREIGN KEY (`id_usuario`)
    REFERENCES `Prihood`.`Usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Empleado_2`
    FOREIGN KEY (`id_tipo_empleado`)
    REFERENCES `Prihood`.`Tipo_Empleado` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Empleado_3`
    FOREIGN KEY (`id_persona`)
    REFERENCES `Prihood`.`Persona` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Prihood`.`ResidentesXResidencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Prihood`.`ResidentesXResidencia` (
  `id_residencia` INT NOT NULL,
  `id_residente` INT NOT NULL,
  PRIMARY KEY (`id_residencia`, `id_residente`),
  INDEX `fk_ResidentesXResidencia_2_idx` (`id_residente` ASC),
  CONSTRAINT `fk_ResidentesXResidencia_1`
    FOREIGN KEY (`id_residencia`)
    REFERENCES `Prihood`.`Residencia` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ResidentesXResidencia_2`
    FOREIGN KEY (`id_residente`)
    REFERENCES `Prihood`.`Residente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- Agustín Gregorieu  30/06/2017 

-- Se agrega nueva columna mail a tabla Usuario

ALTER TABLE Usuario ADD email VARCHAR(150) ;

-- Se agrega nueva tabla de barrios

CREATE TABLE IF NOT EXISTS `Prihood`.`Barrio` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(100) NOT NULL,
  `ubicacion` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB;

-- Se agrega nueva tabla de barrios por usuario
CREATE TABLE IF NOT EXISTS `Prihood`.`UsuarioXBarrio` (
  `id_barrio` INT NOT NULL,
  `id_usuario` INT NOT NULL,
  PRIMARY KEY (`id_barrio`, `id_usuario`),
  INDEX `fk_UsuarioXBarrio_2_idx` (`id_barrio` ASC),
  CONSTRAINT `fk_UsuarioXBarrio_1`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `Prihood`.`Barrio` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_UsuarioXBarrio_2`
    FOREIGN KEY (`id_usuario`)
    REFERENCES `Prihood`.`Usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;
