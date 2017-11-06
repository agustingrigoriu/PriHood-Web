-- MySQL dump 10.13  Distrib 5.7.20, for Linux (x86_64)
--
-- Host: localhost    Database: Prihood
-- ------------------------------------------------------
-- Server version	5.7.20-0ubuntu0.16.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `Alertas`
--

LOCK TABLES `Alertas` WRITE;
/*!40000 ALTER TABLE `Alertas` DISABLE KEYS */;
/*!40000 ALTER TABLE `Alertas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Amenity`
--

LOCK TABLES `Amenity` WRITE;
/*!40000 ALTER TABLE `Amenity` DISABLE KEYS */;
INSERT INTO `Amenity` VALUES (1,'Salón ','Manzana 6 Lote 5','Salón preparado para fiestas, capacidad de 100 personas, posee asador.',NULL,4,1),(2,'Cancha A','Manzana 7','Cancha de tenis de polvo de ladrillo',NULL,1,1),(3,'Cancha B','Manzana 7','Cancha de tenis de cemento',NULL,1,1),(4,'Cancha A','Manzana 8','Cancha de fútbol 8',NULL,2,1),(5,'Pileta','Manzana 8','Pileta',NULL,5,1);
/*!40000 ALTER TABLE `Amenity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Asistencia_Evento`
--

LOCK TABLES `Asistencia_Evento` WRITE;
/*!40000 ALTER TABLE `Asistencia_Evento` DISABLE KEYS */;
/*!40000 ALTER TABLE `Asistencia_Evento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Barrio`
--

LOCK TABLES `Barrio` WRITE;
/*!40000 ALTER TABLE `Barrio` DISABLE KEYS */;
INSERT INTO `Barrio` VALUES (1,'La Lucinda','Ruta 8 km 3',-31.429075,-64.189044),(2,'Los Sauces','En algún lugar',-31.429075,-64.189044),(3,'Los Perales','NothingHill',-31.429075,-64.189044),(4,'El Tipal','Silent Hill',-31.429075,-64.189044),(5,'Las Delicias','Racoon City',-31.429075,-64.189044);
/*!40000 ALTER TABLE `Barrio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Comentario`
--

LOCK TABLES `Comentario` WRITE;
/*!40000 ALTER TABLE `Comentario` DISABLE KEYS */;
INSERT INTO `Comentario` VALUES (1,'Se extenderá hasta las 16 hs',2,1,'2017-11-06 17:17:25');
/*!40000 ALTER TABLE `Comentario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Dia_Semana`
--

LOCK TABLES `Dia_Semana` WRITE;
/*!40000 ALTER TABLE `Dia_Semana` DISABLE KEYS */;
INSERT INTO `Dia_Semana` VALUES (1,'Lunes'),(2,'Martes'),(3,'Miercoles'),(4,'Jueves'),(5,'Viernes'),(6,'Sabado'),(7,'Domingo');
/*!40000 ALTER TABLE `Dia_Semana` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Empleado`
--

LOCK TABLES `Empleado` WRITE;
/*!40000 ALTER TABLE `Empleado` DISABLE KEYS */;
INSERT INTO `Empleado` VALUES (1,'2017-05-02 00:00:00',2,5,1),(2,'2017-03-15 00:00:00',3,4,1);
/*!40000 ALTER TABLE `Empleado` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `EstadoSolicitud`
--

LOCK TABLES `EstadoSolicitud` WRITE;
/*!40000 ALTER TABLE `EstadoSolicitud` DISABLE KEYS */;
INSERT INTO `EstadoSolicitud` VALUES (1,'Pendiente'),(2,'Aceptada'),(3,'Rechazada');
/*!40000 ALTER TABLE `EstadoSolicitud` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Estado_Reserva`
--

LOCK TABLES `Estado_Reserva` WRITE;
/*!40000 ALTER TABLE `Estado_Reserva` DISABLE KEYS */;
INSERT INTO `Estado_Reserva` VALUES (1,'creada'),(2,'cancelada'),(3,'denegada');
/*!40000 ALTER TABLE `Estado_Reserva` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `EventoVisita`
--

LOCK TABLES `EventoVisita` WRITE;
/*!40000 ALTER TABLE `EventoVisita` DISABLE KEYS */;
INSERT INTO `EventoVisita` VALUES (1,'Ingreso'),(2,'Egreso');
/*!40000 ALTER TABLE `EventoVisita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Eventos`
--

LOCK TABLES `Eventos` WRITE;
/*!40000 ALTER TABLE `Eventos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Eventos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Expensas`
--

LOCK TABLES `Expensas` WRITE;
/*!40000 ALTER TABLE `Expensas` DISABLE KEYS */;
/*!40000 ALTER TABLE `Expensas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Perfil`
--

LOCK TABLES `Perfil` WRITE;
/*!40000 ALTER TABLE `Perfil` DISABLE KEYS */;
INSERT INTO `Perfil` VALUES (1,'Root'),(2,'Administrador'),(3,'Residente'),(4,'Encargado de Seguridad');
/*!40000 ALTER TABLE `Perfil` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Persona`
--

LOCK TABLES `Persona` WRITE;
/*!40000 ALTER TABLE `Persona` DISABLE KEYS */;
INSERT INTO `Persona` VALUES (1,'Agustín ','Gregorieu',1,'38500850','3515298313','1994-10-12 00:00:00'),(2,'Lucas ','Madrazo',1,'38650207','3512548258','1994-08-31 00:00:00'),(3,'Patricio ','Pérez',1,'38653035','3875910634','1994-12-07 00:00:00'),(4,'Belén ','Valdivia',1,'38988071','3513316229','1995-04-12 00:00:00'),(5,'Gabriela ','Caballero',1,'37729061','388154778083','1995-06-06 00:00:00');
/*!40000 ALTER TABLE `Persona` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Proveedor`
--

LOCK TABLES `Proveedor` WRITE;
/*!40000 ALTER TABLE `Proveedor` DISABLE KEYS */;
/*!40000 ALTER TABLE `Proveedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Publicacion`
--

LOCK TABLES `Publicacion` WRITE;
/*!40000 ALTER TABLE `Publicacion` DISABLE KEYS */;
INSERT INTO `Publicacion` VALUES (1,'Por problemas ajenos a la instalación eléctrica del barrio, no habrá luz desde las 8:00 am del día de mañana. Se reestablecería al mediodía.',2,NULL,'2017-11-06 17:16:59','Corte de Luz Temporal'),(2,'Por problemas personales he decidido enviar las expensas a partir del día 5, sepan disculpar.',2,NULL,'2017-11-06 17:19:13','Retraso en envío de expensas');
/*!40000 ALTER TABLE `Publicacion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `RegistroVotos`
--

LOCK TABLES `RegistroVotos` WRITE;
/*!40000 ALTER TABLE `RegistroVotos` DISABLE KEYS */;
/*!40000 ALTER TABLE `RegistroVotos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Reserva`
--

LOCK TABLES `Reserva` WRITE;
/*!40000 ALTER TABLE `Reserva` DISABLE KEYS */;
/*!40000 ALTER TABLE `Reserva` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Residencia`
--

LOCK TABLES `Residencia` WRITE;
/*!40000 ALTER TABLE `Residencia` DISABLE KEYS */;
INSERT INTO `Residencia` VALUES (1,'Residencia 1','Manzana 1 Lote 1','4f9824',1),(3,'Residencia 2','Manzana 1 Lote 2','6ccb53',1),(4,'Residencia 3','Manzana 1 Lote 3','db9682',1),(5,'Residencia 4','Manzana 2 Lote 1','8ad5b5',1),(6,'Residencia 5','Manzana 2 Lote 2','1fdae1',1),(7,'Residencia 6','Manzana 2 Lote 3','e43178',1);
/*!40000 ALTER TABLE `Residencia` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Residente`
--

LOCK TABLES `Residente` WRITE;
/*!40000 ALTER TABLE `Residente` DISABLE KEYS */;
/*!40000 ALTER TABLE `Residente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `SolicitudViaje`
--

LOCK TABLES `SolicitudViaje` WRITE;
/*!40000 ALTER TABLE `SolicitudViaje` DISABLE KEYS */;
/*!40000 ALTER TABLE `SolicitudViaje` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `TipoVisita`
--

LOCK TABLES `TipoVisita` WRITE;
/*!40000 ALTER TABLE `TipoVisita` DISABLE KEYS */;
INSERT INTO `TipoVisita` VALUES (1,'Frecuente'),(2,'Actual');
/*!40000 ALTER TABLE `TipoVisita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Tipo_Alerta`
--

LOCK TABLES `Tipo_Alerta` WRITE;
/*!40000 ALTER TABLE `Tipo_Alerta` DISABLE KEYS */;
INSERT INTO `Tipo_Alerta` VALUES (1,'Actividad sospechosa','assets/img/pruebas/alertas/sospechoso.png'),(2,'Atención Médica','assets/img/pruebas/alertas/doctor.png');
/*!40000 ALTER TABLE `Tipo_Alerta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Tipo_Amenity`
--

LOCK TABLES `Tipo_Amenity` WRITE;
/*!40000 ALTER TABLE `Tipo_Amenity` DISABLE KEYS */;
INSERT INTO `Tipo_Amenity` VALUES (1,'Cancha de tenis','assets/img/pruebas/amenities/tennis.png'),(2,'Cancha de fútbol','assets/img/pruebas/amenities/futbol.png'),(3,'Cancha de paddle','assets/img/pruebas/amenities/paddle.png'),(4,'Salón','assets/img/pruebas/amenities/salon.png'),(5,'Piscinas','assets/img/pruebas/amenities/piscina.png');
/*!40000 ALTER TABLE `Tipo_Amenity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Tipo_Documento`
--

LOCK TABLES `Tipo_Documento` WRITE;
/*!40000 ALTER TABLE `Tipo_Documento` DISABLE KEYS */;
INSERT INTO `Tipo_Documento` VALUES (1,'Documento Único'),(2,'Libreta de Enrolamiento'),(3,'Libreta Cívica'),(4,'Otro');
/*!40000 ALTER TABLE `Tipo_Documento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Tipo_Evento`
--

LOCK TABLES `Tipo_Evento` WRITE;
/*!40000 ALTER TABLE `Tipo_Evento` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tipo_Evento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Tipo_Servicio`
--

LOCK TABLES `Tipo_Servicio` WRITE;
/*!40000 ALTER TABLE `Tipo_Servicio` DISABLE KEYS */;
INSERT INTO `Tipo_Servicio` VALUES (1,'Piscinas'),(2,'Jardinería'),(3,'Plomería'),(4,'Gasista'),(5,'Fumigación'),(6,'Otros');
/*!40000 ALTER TABLE `Tipo_Servicio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Trayecto`
--

LOCK TABLES `Trayecto` WRITE;
/*!40000 ALTER TABLE `Trayecto` DISABLE KEYS */;
/*!40000 ALTER TABLE `Trayecto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Turno`
--

LOCK TABLES `Turno` WRITE;
/*!40000 ALTER TABLE `Turno` DISABLE KEYS */;
/*!40000 ALTER TABLE `Turno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Usuario`
--

LOCK TABLES `Usuario` WRITE;
/*!40000 ALTER TABLE `Usuario` DISABLE KEYS */;
INSERT INTO `Usuario` VALUES (1,'admin@admin.com','8c6976e5b5410415bde908bd4dee15',NULL,1,NULL,NULL),(2,'lmgabrielac@gmail.com','8c6976e5b5410415bde908bd4dee15',NULL,2,1,NULL),(3,'elianabelen.valdivia@gmail.com','8c6976e5b5410415bde908bd4dee15',NULL,4,1,NULL),(4,'madrazolucas@gmail.com','8c6976e5b5410415bde908bd4dee15',NULL,3,1,NULL),(5,'pato12p@gmail.com','8c6976e5b5410415bde908bd4dee15',NULL,3,1,NULL),(6,'agustin.gregorieu@gmail.com','8c6976e5b5410415bde908bd4dee15',NULL,3,1,NULL);
/*!40000 ALTER TABLE `Usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Viaje`
--

LOCK TABLES `Viaje` WRITE;
/*!40000 ALTER TABLE `Viaje` DISABLE KEYS */;
/*!40000 ALTER TABLE `Viaje` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Visita`
--

LOCK TABLES `Visita` WRITE;
/*!40000 ALTER TABLE `Visita` DISABLE KEYS */;
/*!40000 ALTER TABLE `Visita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Visitante`
--

LOCK TABLES `Visitante` WRITE;
/*!40000 ALTER TABLE `Visitante` DISABLE KEYS */;
/*!40000 ALTER TABLE `Visitante` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `VisitasXResidente`
--

LOCK TABLES `VisitasXResidente` WRITE;
/*!40000 ALTER TABLE `VisitasXResidente` DISABLE KEYS */;
/*!40000 ALTER TABLE `VisitasXResidente` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-06 17:30:17
