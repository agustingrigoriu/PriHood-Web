-- Personas
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Agustín ', 'Gregorieu', '1', '38500850', '3515298313', '1994-10-12');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Lucas ', 'Madrazo', '1', '38650207', '3512548258', '1994-08-31');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Patricio ', 'Pérez', '1', '38653035', '3875910634', '1994-12-07');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Belén ', 'Valdivia', '1', '38988071', '3513316229', '1995-04-12');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Gabriela ', 'Caballero', '1', '37729061', '388154778083', '1995-06-06');

-- Barrios
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("La Lucinda","Ruta 8 km 3", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("Los Sauces","En algún lugar", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("Los Perales","NothingHill", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("El Tipal","Silent Hill", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("Las Delicias","Racoon City", -31.429075, -64.189044);

-- Residencias
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 1','Manzana 1 Lote 1','4f9824',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 2','Manzana 1 Lote 2','6ccb53',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 3','Manzana 1 Lote 3','db9682',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 4','Manzana 2 Lote 1','8ad5b5',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 5','Manzana 2 Lote 2','1fdae1',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 6','Manzana 2 Lote 3','e43178',1);

-- Usuarios
INSERT INTO Usuario(email, password, id_perfil, avatar) VALUES("admin@admin.com","8c6976e5b5410415bde908bd4dee15", 1, "https://s3-us-west-2.amazonaws.com/prihood/root.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("lmgabrielac@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("elianabelen.valdivia@gmail.com","8c6976e5b5410415bde908bd4dee15",4, 1, "https://s3-us-west-2.amazonaws.com/prihood/03e7ff21-f121-4c67-92fc-e6028032422a.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("madrazolucas@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1, "https://s3.us-west-2.amazonaws.com/prihood/67afbb1f-3e9f-42c7-b8b7-f24b94f487cf.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("pato12p@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1, "https://s3.us-west-2.amazonaws.com/prihood/55cc7bef-a9fc-490b-ae63-3c349039c2dc.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("agustin.gregorieu@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1, "https://s3-us-west-2.amazonaws.com/prihood/064770fb-1068-42c2-8453-207f84672540.png");

-- Empleados (Administrador y Encargado de Seguridad)
INSERT INTO Empleado(fecha_inicio_actividad, id_usuario, id_persona, id_barrio) VALUES ('2017-05-2', 2, 5, 1);
INSERT INTO Empleado(fecha_inicio_actividad, id_usuario, id_persona, id_barrio) VALUES ('2017-03-15', 3, 4, 1);

-- Residentes
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2014-02-2', 4, 2, 1);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2013-08-15', 5, 3, 3);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 6, 1, 4);

-- Publicaciones
INSERT INTO `Publicacion` VALUES (1,'Por problemas ajenos a la instalación eléctrica del barrio, no habrá luz desde las 8:00 am del día de mañana. Se reestablecería al mediodía.',2,NULL,'2017-11-06 17:16:59','Corte de Luz Temporal'),(2,'Por problemas personales he decidido enviar las expensas a partir del día 5, sepan disculpar.',2,NULL,'2017-11-06 17:19:13','Retraso en envío de expensas');

-- Comentarios
INSERT INTO `Comentario` VALUES (1,'Se extenderá hasta las 16 hs',2,1,'2017-11-06 17:17:25');

-- Amenities
INSERT INTO `Amenity` VALUES (1,'Salón ','Manzana 6 Lote 5','Salón preparado para fiestas, capacidad de 100 personas, posee asador.',NULL,4,1),(2,'Cancha A','Manzana 7','Cancha de tenis de polvo de ladrillo',NULL,1,1),(3,'Cancha B','Manzana 7','Cancha de tenis de cemento',NULL,1,1),(4,'Cancha A','Manzana 8','Cancha de fútbol 8',NULL,2,1),(5,'Pileta','Manzana 8','Pileta',NULL,5,1);


-- Turnos
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (1,'Turno Mañana 1','09:00:00',60,150,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (2,'Turno Mañana 2','10:00:00',60,150,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (3,'Turno Mañana 3','11:00:00',60,150,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (4,'Turno Tarde 1','15:00:00',60,160,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (5,'Turno Tarde 2','16:00:00',60,160,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (6,'Turno Tarde 3','17:00:00',60,160,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (7,'Turno Tarde 4','18:00:00',60,160,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (8,'Turno Tarde 5','19:00:00',60,160,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (9,'Turno Noche 1','20:00:00',60,180,2,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (11,'Fiestas Noche','20:30:00',180,500,1,5);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (12,'Fiestas tarde','14:00:00',270,800,1,6);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (13,'Fiestas Noche 2','20:00:00',210,900,1,6);

-- Reservas
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (1,'2017-11-20 16:57:55','',150,1,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (2,'2017-10-20 16:57:55','',150,1,1,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (3,'2017-10-19 16:57:55','',150,1,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (4,'2017-09-20 16:57:55','',150,1,3,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (5,'2017-11-24 17:07:49','',500,11,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (6,'2017-08-05 16:57:55','',150,2,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (7,'2017-08-06 16:57:55','',150,3,1,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (8,'2017-08-12 16:57:55','',150,4,3,1);

-- Visitantes Frecuentes
INSERT INTO `Visitante` (`id`,`id_tipo_visita`,`id_residente`,`nombre`,`apellido`,`id_tipo_documento`,`numero_documento`,`patente`,`observaciones`,`fecha_visita`,`avatar`,`estado`) VALUES (3,1,2,'Leia ','Skywalker',1,'20874493','DST345','Auto color negro, BMW',NULL,'https://s3-us-west-2.amazonaws.com/prihood/37c16a28-593f-448f-80f2-04e98470ef01.jpg','esperando');
INSERT INTO `Visitante` (`id`,`id_tipo_visita`,`id_residente`,`nombre`,`apellido`,`id_tipo_documento`,`numero_documento`,`patente`,`observaciones`,`fecha_visita`,`avatar`,`estado`) VALUES (5,1,2,'Han','Solo',1,'17363661','HSD241','Gol Trend color marrón',NULL,'https://s3-us-west-2.amazonaws.com/prihood/7d379d2a-0e7d-408b-a7c1-c4acba785c4c.jpg','esperando');
INSERT INTO `Visitante` (`id`,`id_tipo_visita`,`id_residente`,`nombre`,`apellido`,`id_tipo_documento`,`numero_documento`,`patente`,`observaciones`,`fecha_visita`,`avatar`,`estado`) VALUES (6,1,2,'Luke','Skywalker',1,'38500850','LKS357','Fiat 600 color verde ',NULL,'https://s3-us-west-2.amazonaws.com/prihood/492c912f-6ffb-4e64-af48-f1c32d8ac288.jpg','esperando');

-- Visitas
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (1,1,'2017-11-17 17:43:25',3);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (2,1,'2017-11-17 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (3,1,'2017-11-18 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (4,1,'2017-11-18 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (5,1,'2017-11-18 17:43:25',3);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (6,1,'2017-11-19 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (7,1,'2017-11-19 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (8,1,'2017-11-20 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (9,1,'2017-11-20 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (10,1,'2017-11-20 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (11,1,'2017-11-20 17:43:25',6);

