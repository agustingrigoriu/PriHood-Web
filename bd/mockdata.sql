-- Personas
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Agustín ', 'Gregorieu', '1', '38500850', '3515298313', '1994-10-12');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Lucas ', 'Madrazo', '1', '38650207', '3512548258', '1994-08-31');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Patricio ', 'Pérez', '1', '38653035', '3875910634', '1994-12-07');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Belén ', 'Valdivia', '1', '38988071', '3513316229', '1995-04-12');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Gabriela ', 'Caballero', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Juan Mas ', 'Sanz', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Roberto ', 'Torres', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Ruben ', 'Vega', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Santiago ', 'Parra', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Joan ', 'Mendez', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('Luis ', 'Pascual', '1', '37729061', '388154778083', '1995-06-06');
INSERT INTO Persona(nombre, apellido, id_tipo_documento, nro_documento, telefono_movil, fecha_nacimiento) VALUES ('David ', 'Rovira', '1', '37729061', '388154778083', '1995-06-06');

-- Barrios
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("Los Sauces","Ruta 8 km 3", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("La Lucinda","En algún lugar", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("Los Perales","NothingHill", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("El Tipal","Silent Hill", -31.429075, -64.189044);
INSERT INTO Barrio(nombre, ubicacion, latitud, longitud) VALUES("Las Delicias","Racoon City", -31.429075, -64.189044);

-- Residencias
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Perez','Manzana 1 Lote 1','4f9824',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Rodriguez','Manzana 1 Lote 2','6ccb53',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Madrazo','Manzana 1 Lote 3','db9682',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Caballero','Manzana 2 Lote 1','8ad5b5',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Valdivia','Manzana 2 Lote 2','1fdae1',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Peralta','Manzana 2 Lote 3','e43178',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Cuello','Manzana 3 Lote 1','a43178',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Castro','Manzana 3 Lote 1','a4a178',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Sanz','Manzana 3 Lote 1','a43118',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Serrano','Manzana 3 Lote 1','a23178',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Ferrer','Manzana 3 Lote 1','b43178',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Ortega','Manzana 3 Lote 1','a4s178',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Moreno','Manzana 3 Lote 1','a43t78',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Familia Duran','Manzana 3 Lote 1','a43148',1);

-- Usuarios
INSERT INTO Usuario(email, password, id_perfil, avatar) VALUES("admin@admin.com","8c6976e5b5410415bde908bd4dee15", 1, "https://s3-us-west-2.amazonaws.com/prihood/root.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("lmgabrielac@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("elianabelen.valdivia@gmail.com","8c6976e5b5410415bde908bd4dee15",4, 1, "https://s3-us-west-2.amazonaws.com/prihood/03e7ff21-f121-4c67-92fc-e6028032422a.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("madrazolucas@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1, "https://s3.us-west-2.amazonaws.com/prihood/67afbb1f-3e9f-42c7-b8b7-f24b94f487cf.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("pato12p@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1, "https://s3.us-west-2.amazonaws.com/prihood/55cc7bef-a9fc-490b-ae63-3c349039c2dc.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("agustin.gregorieu@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1, "https://s3-us-west-2.amazonaws.com/prihood/064770fb-1068-42c2-8453-207f84672540.png");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("juan.sanz@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("roberto.torres@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("ruben.vega@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("santiago.parra@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("joan.mendez@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("luis.pascual@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");
INSERT INTO Usuario(email, password, id_perfil, id_barrio, avatar) VALUES("david.rovira@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1, "https://s3.us-west-2.amazonaws.com/prihood/471391_4798362610780_1250457058_o.jpg");

-- Empleados (Administrador y Encargado de Seguridad)
INSERT INTO Empleado(fecha_inicio_actividad, id_usuario, id_persona, id_barrio) VALUES ('2017-05-2', 2, 5, 1);
INSERT INTO Empleado(fecha_inicio_actividad, id_usuario, id_persona, id_barrio) VALUES ('2017-03-15', 3, 4, 1);

-- Residentes
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2014-02-2', 4, 2, 1);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2013-08-15', 5, 3, 3);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 6, 1, 4);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 7, 6, 5);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 8, 7, 6);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 9, 8, 7);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 10, 9, 8);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 11, 10, 9);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 12, 11, 10);
INSERT INTO Residente(fecha_ingreso, id_usuario, id_persona, id_residencia) VALUES ('2012-06-15', 13, 12, 11);

-- Publicaciones
INSERT INTO `Publicacion` VALUES 
(1,'Por problemas ajenos a la instalación eléctrica del barrio, no habrá luz desde las 8:00 am del día de mañana. Se reestablecería al mediodía.',2,NULL,'2017-11-06 17:16:59','Corte de Luz Temporal'),
(2,'Por problemas personales he decidido enviar las expensas a partir del día 5, sepan disculpar.',2,NULL,'2017-11-06 17:19:13','Retraso en envío de expensas'),
(3,'Se les informa a los vecinos que el próximo viernes 1 de diciembre a las 18 horas se lleverá a cabo una reunión para definir la creación de un nuevo salón.',2,NULL,'2017-11-28 18:00:13','Reunión para el proyecto de nuevo salón');

-- Comentarios
INSERT INTO `Comentario` VALUES (1,'Se extenderá hasta las 16 hs',2,1,'2017-11-06 17:17:25');
INSERT INTO `Comentario` VALUES (2,'Perfecto allí estaré.',7,3,'2017-11-29 17:17:25');
INSERT INTO `Comentario` VALUES (3,'¿Cuando se va a discutir sobre el asfalto de la calle principal?',8,3,'2017-11-29 18:17:25');
INSERT INTO `Comentario` VALUES (4,'El próximo més se tratará el tema del asfalto.',2,3,'2017-11-29 18:21:25');

-- Amenities
INSERT INTO `Amenity` VALUES 
(1,'Quincho familiar','Manzana 6 Lote 5','Quincho para asados para capacidad de 40 personas, posee asador.',NULL,4,1),
(2,'Cancha A','Manzana 7','Cancha de tenis de polvo de ladrillo',NULL,1,1),
(3,'Cancha B','Manzana 7','Cancha de tenis de cemento',NULL,1,1),
(4,'Cancha A','Manzana 8','Cancha de fútbol 8',NULL,2,1),
(5,'Pileta','Manzana 8','Pileta',NULL,5,1),
(6,'Salón para fiestas','Manzana 4 Lote 1','Salón preparado para fiestas, capacidad de 100 personas, posee asador.',NULL,4,1),
(7,'Cancha A','Manzana 4 Lote 1','Cancha de paddle con iluminación.',NULL,3,1);

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

INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (14,'Turno Mañana 1','09:00:00',60,150,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (15,'Turno Mañana 2','10:00:00',60,150,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (16,'Turno Mañana 3','11:00:00',60,150,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (17,'Turno Tarde 1','15:00:00',60,160,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (18,'Turno Tarde 2','16:00:00',60,160,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (19,'Turno Tarde 3','17:00:00',60,160,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (20,'Turno Tarde 4','18:00:00',60,160,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (21,'Turno Tarde 5','19:00:00',60,160,7,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (22,'Turno Noche 1','20:00:00',60,180,7,1);

INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (23,'Fiestas tarde','14:00:00',270,800,6,1);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (24,'Fiestas tarde','14:00:00',270,800,6,2);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (25,'Fiestas tarde','14:00:00',270,800,6,3);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (26,'Fiestas tarde','14:00:00',270,800,6,4);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (27,'Fiestas tarde','14:00:00',270,800,6,5);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (28,'Fiestas noche','20:00:00',240,1400,6,5);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (29,'Fiestas noche','20:00:00',240,1400,6,6);
INSERT INTO `Turno` (`id`,`nombre`,`hora_desde`,`duracion`,`costo`,`id_amenity`,`id_dia_semana`) VALUES (30,'Fiestas noche','20:00:00',240,1400,6,7);


-- Reservas
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (1,'2017-11-20 16:57:55','',150,1,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (2,'2017-10-20 16:57:55','',150,1,1,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (3,'2017-10-19 16:57:55','',150,1,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (4,'2017-09-20 16:57:55','',150,1,3,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (5,'2017-11-24 17:07:49','',500,11,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (6,'2017-08-05 16:57:55','',150,2,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (7,'2017-08-06 16:57:55','',150,3,1,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (8,'2017-08-12 16:57:55','',150,4,3,1);

INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (9,'2017-11-05 16:57:55','',150,14,2,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (10,'2017-11-06 16:57:55','',150,15,1,1);
INSERT INTO `Reserva` (`id`,`fecha`,`observaciones`,`costo`,`id_turno`,`id_residente`,`id_estado_reserva`) VALUES (11,'2017-11-12 16:57:55','',150,16,3,1);

-- Visitantes Frecuentes
INSERT INTO `Visitante` (`id`,`id_tipo_visita`,`id_residente`,`nombre`,`apellido`,`id_tipo_documento`,`numero_documento`,`patente`,`observaciones`,`fecha_visita`,`avatar`,`estado`) VALUES (3,1,2,'Leia ','Skywalker',1,'20874493','DST345','Auto color negro, BMW',NULL,'https://s3-us-west-2.amazonaws.com/prihood/37c16a28-593f-448f-80f2-04e98470ef01.jpg','esperando');
INSERT INTO `Visitante` (`id`,`id_tipo_visita`,`id_residente`,`nombre`,`apellido`,`id_tipo_documento`,`numero_documento`,`patente`,`observaciones`,`fecha_visita`,`avatar`,`estado`) VALUES (5,1,2,'Han','Solo',1,'17363661','HSD241','Gol Trend color marrón',NULL,'https://s3-us-west-2.amazonaws.com/prihood/7d379d2a-0e7d-408b-a7c1-c4acba785c4c.jpg','esperando');
INSERT INTO `Visitante` (`id`,`id_tipo_visita`,`id_residente`,`nombre`,`apellido`,`id_tipo_documento`,`numero_documento`,`patente`,`observaciones`,`fecha_visita`,`avatar`,`estado`) VALUES (6,1,2,'Luke','Skywalker',1,'38500850','LKS357','Fiat 600 color verde ',NULL,'https://s3-us-west-2.amazonaws.com/prihood/492c912f-6ffb-4e64-af48-f1c32d8ac288.jpg','esperando');

-- Visitantes actuales
INSERT INTO `Visitante` (`id`, `id_tipo_visita`, `id_residente`, `nombre`, `apellido`, `id_tipo_documento`, `numero_documento`, `patente`, `observaciones`, `fecha_visita`, `avatar`, `estado`)
VALUES
	(7, 2, 2, 'Lucas', 'Madrazo', 1, '3865124', '', '', '2017-12-01 21:12:14', NULL, 'esperando');

-- Visitas
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (1,1,'2017-11-25 17:43:25',3);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (2,1,'2017-11-25 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (3,1,'2017-11-26 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (4,1,'2017-11-26 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (5,1,'2017-11-26 17:43:25',3);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (6,1,'2017-11-26 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (7,1,'2017-11-26 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (8,1,'2017-11-27 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (9,1,'2017-11-27 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (10,1,'2017-11-27 17:43:25',5);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (11,1,'2017-11-28 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (12,1,'2017-11-28 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (13,1,'2017-11-28 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (14,1,'2017-11-28 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (15,1,'2017-11-29 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (16,1,'2017-11-29 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (17,1,'2017-11-29 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (18,1,'2017-11-30 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (19,1,'2017-11-30 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (20,1,'2017-11-30 17:43:25',6);
INSERT INTO `Visita` (`id`,`id_evento`,`fecha`,`id_visitante`) VALUES (21,1,'2017-11-30 17:43:25',6);

-- Proveedores

INSERT INTO `Proveedor` (`id`, `nombre`, `descripcion`, `telefono`, `id_tipo_servicio`, `id_residente_recomienda`, `avatar`, `direccion`, `rating_total`, `cantidad_votos`)
VALUES
	(1,'Aqua Pool','Limpieza de piletas, desagotes e hidrolavados. Pintura y reparación de decks.','4225334',1,1,'assets/img/pruebas/proveedores/piscinas.png','Caseros 1234',0,0),
	(2,'MH jardines','Mantenimiento de jardines, podado de arbustos.','4671231',2,1,'assets/img/pruebas/proveedores/jardineria.png','San Lorenzo 221',0,0),
	(3,'Raul Suarez','Mantenimiento de jardines internos y externos.','15532117',2,1,'assets/img/pruebas/proveedores/jardineria.png','Independencia 750',5,1),
	(4,'Plomehouse SRL','Reparacion de caños rotos, mantenimiento y limpieza de tuberias.','4567781',3,1,'assets/img/pruebas/proveedores/plomero.png','San Luis 345',0,0),
	(5,'Matatodo','Fumigacion de todo tipo de plagas con productos no toxicos para mascotas.','5671234',5,1,'assets/img/pruebas/proveedores/fumigacion.png','Laprida 1124',0,0);

-- Eventos

INSERT INTO `Eventos` (`id`, `id_residente`, `id_tipo_evento`, `titulo`, `descripcion`, `fecha`, `hora_desde`, `hora_hasta`, `imagen`)
VALUES
	(1,2,3,'Juguetes para navidad','Se recibirán juguetes para donar al hospital de niños con motivo de festejar la navidad. Esperamos tu colaboración.','2017-12-22 20:45:57','13:00:00','17:00:00','https://s3-us-west-2.amazonaws.com/prihood/2282fac8-6daf-4422-a056-cbcb069e6d28.jpg'),
	(2,2,5,'Torneo de futbol 5','Inscripción en el lugar, costo por equipo de $1000 pesos. Incluye agua y comida.','2017-12-15 20:49:40','18:00:00','22:00:00','https://s3-us-west-2.amazonaws.com/prihood/102b2c0f-880a-48f8-b475-28d7ec994679.jpg');

-- Cargar expensas

INSERT INTO `Expensas` (`id_residencia`, `fecha_transaccion`, `fecha_expensa`, `fecha_vencimiento`, `pagado`, `url_expensa`, `monto`, `observaciones`)
VALUES
	(1, '2017-11-28 17:56:36', '2017-08-01 00:00:00', '2017-09-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(1, '2017-11-28 17:56:36', '2017-09-01 00:00:00', '2017-10-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(1, '2017-11-28 17:56:36', '2017-10-01 00:00:00', '2017-11-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(1, '2017-11-28 17:56:36', '2017-11-01 00:00:00', '2017-12-10 00:00:00', 0, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),

	(2, '2017-11-28 17:56:36', '2017-08-01 00:00:00', '2017-09-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(2, '2017-11-28 17:56:36', '2017-09-01 00:00:00', '2017-10-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(2, '2017-11-28 17:56:36', '2017-10-01 00:00:00', '2017-11-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(2, '2017-11-28 17:56:36', '2017-11-01 00:00:00', '2017-12-10 00:00:00', 0, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),

	(3, '2017-11-28 17:56:36', '2017-08-01 00:00:00', '2017-09-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(3, '2017-11-28 17:56:36', '2017-09-01 00:00:00', '2017-10-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(3, '2017-11-28 17:56:36', '2017-10-01 00:00:00', '2017-11-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(3, '2017-11-28 17:56:36', '2017-11-01 00:00:00', '2017-12-10 00:00:00', 0, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),

	(4, '2017-11-28 17:56:36', '2017-08-01 00:00:00', '2017-09-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(4, '2017-11-28 17:56:36', '2017-09-01 00:00:00', '2017-10-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(4, '2017-11-28 17:56:36', '2017-10-01 00:00:00', '2017-11-10 00:00:00', 1, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL),
	(4, '2017-11-28 17:56:36', '2017-11-01 00:00:00', '2017-12-10 00:00:00', 0, 'https://s3-us-west-2.amazonaws.com/prihood/a88f8810-b225-460b-967c-56b0bace6f15.png', 3561, NULL);

	-- Cargar valoraciones

INSERT INTO `RegistroVotos` (`id`, `fecha`, `id_proveedor`, `comentario`, `id_residente`, `rating`)
VALUES
	(1, '2017-11-28 00:00:00', 3, 'Muy bueno, de confianza.', 1, 5);


-- Cargamos viajes
INSERT INTO `Viaje` (`id`, `id_residente`, `auto_modelo`, `auto_patente`, `auto_color`, `destino`, `auto_asientos`, `fecha`, `hora`, `sale_barrio`, `observaciones`, `id_dia_semana`)
VALUES
	(1,1,'chevrolet','12 ASD 23','rojo','Ciudad Empresaria',4,'2017-12-01 00:00:00',NULL,b'1','nos encontramos en la entrada norte',NULL);

-- Cargamos trayectos

INSERT INTO `Trayecto` (`id`, `id_viaje`, `longitud`, `latitud`, `orden`)
VALUES
	(1,1,-64.18908,-31.429150000000003,1),
	(2,1,-64.2063,-31.373950000000004,92),
	(3,1,-64.20609,-31.373010000000004,93),
	(4,1,-64.20603,-31.372600000000002,94),
	(5,1,-64.20597000000001,-31.37252,95),
	(6,1,-64.20592,-31.37233,96),
	(7,1,-64.20587,-31.372030000000002,97),
	(8,1,-64.20642000000001,-31.374510000000004,91),
	(9,1,-64.20588000000001,-31.37187,98),
	(10,1,-64.20553000000001,-31.36943,100),
	(11,1,-64.20523,-31.367850000000004,101),
	(12,1,-64.20514,-31.367620000000002,102),
	(13,1,-64.20503000000001,-31.367070000000002,103),
	(14,1,-64.20459000000001,-31.364940000000004,104),
	(15,1,-64.20448,-31.36426,105),
	(16,1,-64.20578,-31.370880000000003,99),
	(17,1,-64.20401000000001,-31.361810000000002,106),
	(18,1,-64.20644,-31.374660000000002,90),
	(19,1,-64.20677,-31.376670000000004,88),
	(20,1,-64.20771,-31.38313,74),
	(21,1,-64.2077,-31.382990000000003,75),
	(22,1,-64.20765,-31.38286,76),
	(23,1,-64.20749,-31.382600000000004,77),
	(24,1,-64.20726,-31.38229,78),
	(25,1,-64.20702,-31.38192,79),
	(26,1,-64.20667,-31.37591,89),
	(27,1,-64.20689,-31.381670000000003,80),
	(28,1,-64.2069,-31.38145,82),
	(29,1,-64.20704,-31.38106,83),
	(30,1,-64.20731,-31.38049,84),
	(31,1,-64.20741000000001,-31.380170000000003,85),
	(32,1,-64.20736000000001,-31.379710000000003,86),
	(33,1,-64.20714000000001,-31.37845,87),
	(34,1,-64.20688,-31.381570000000004,81),
	(35,1,-64.20772000000001,-31.38342,73),
	(36,1,-64.20383000000001,-31.36079,107),
	(37,1,-64.20312,-31.357090000000003,109),
	(38,1,-64.20604,-31.346100000000003,128),
	(39,1,-64.20675,-31.345100000000002,129),
	(40,1,-64.20732000000001,-31.344220000000004,130),
	(41,1,-64.20747,-31.343970000000002,131),
	(42,1,-64.20757,-31.343750000000004,132),
	(43,1,-64.2077,-31.343380000000003,133),
	(44,1,-64.20441000000001,-31.348560000000003,127),
	(45,1,-64.20779,-31.342890000000004,134),
	(46,1,-64.20776000000001,-31.340790000000002,136),
	(47,1,-64.20773000000001,-31.340190000000003,137),
	(48,1,-64.20775,-31.338150000000002,138),
	(49,1,-64.20775,-31.337270000000004,139),
	(50,1,-64.20771,-31.337100000000003,140),
	(51,1,-64.20773000000001,-31.33668,141),
	(52,1,-64.20782000000001,-31.342530000000004,135),
	(53,1,-64.20323,-31.3576,108),
	(54,1,-64.20276000000001,-31.351080000000003,126),
	(55,1,-64.2025,-31.35168,124),
	(56,1,-64.20301,-31.356690000000004,110),
	(57,1,-64.20285000000001,-31.356510000000004,111),
	(58,1,-64.20271000000001,-31.356230000000004,112),
	(59,1,-64.20266000000001,-31.356030000000004,113),
	(60,1,-64.20266000000001,-31.355900000000002,114),
	(61,1,-64.20276000000001,-31.355430000000002,115),
	(62,1,-64.20266000000001,-31.351290000000002,125),
	(63,1,-64.20279000000001,-31.355190000000004,116),
	(64,1,-64.2026,-31.354190000000003,118),
	(65,1,-64.20249000000001,-31.35375,119),
	(66,1,-64.20241,-31.353140000000003,120),
	(67,1,-64.20234,-31.35255,121),
	(68,1,-64.20237,-31.352200000000003,122),
	(69,1,-64.20241,-31.35197,123),
	(70,1,-64.20276000000001,-31.354900000000004,117),
	(71,1,-64.2078,-31.336250000000003,142),
	(72,1,-64.20767000000001,-31.38389,72),
	(73,1,-64.20751,-31.384780000000003,70),
	(74,1,-64.17858000000001,-31.413750000000004,20),
	(75,1,-64.17774,-31.41175,21),
	(76,1,-64.17761,-31.411410000000004,22),
	(77,1,-64.17817000000001,-31.411220000000004,23),
	(78,1,-64.18022,-31.410600000000002,24),
	(79,1,-64.18156,-31.410200000000003,25),
	(80,1,-64.18051,-31.41832,19),
	(81,1,-64.18428,-31.4094,26),
	(82,1,-64.18705,-31.408580000000004,28),
	(83,1,-64.18942,-31.407880000000002,29),
	(84,1,-64.19001,-31.407690000000002,30),
	(85,1,-64.19137,-31.407300000000003,31),
	(86,1,-64.19262,-31.406950000000002,32),
	(87,1,-64.19331000000001,-31.406730000000003,33),
	(88,1,-64.18567,-31.409010000000002,27),
	(89,1,-64.19379,-31.406570000000002,34),
	(90,1,-64.18100000000001,-31.419480000000004,18),
	(91,1,-64.18186,-31.4217,16),
	(92,1,-64.18816000000001,-31.429430000000004,2),
	(93,1,-64.18798000000001,-31.42901,3),
	(94,1,-64.18719,-31.427110000000003,4),
	(95,1,-64.18525000000001,-31.427720000000004,5),
	(96,1,-64.18455,-31.427950000000003,6),
	(97,1,-64.18454,-31.427850000000003,7),
	(98,1,-64.18188,-31.421580000000002,17),
	(99,1,-64.18451,-31.42771,8),
	(100,1,-64.18412000000001,-31.42676,10),
	(101,1,-64.18365,-31.425600000000003,11),
	(102,1,-64.18351,-31.42565,12),
	(103,1,-64.18264,-31.423630000000003,13),
	(104,1,-64.18205,-31.422210000000003,14),
	(105,1,-64.18191,-31.421870000000002,15),
	(106,1,-64.18442,-31.427480000000003,9),
	(107,1,-64.20756,-31.38454,71),
	(108,1,-64.19391,-31.406520000000004,35),
	(109,1,-64.19399,-31.406360000000003,37),
	(110,1,-64.20305,-31.389490000000002,56),
	(111,1,-64.20309,-31.38932,57),
	(112,1,-64.20326,-31.388970000000004,58),
	(113,1,-64.20370000000001,-31.388230000000004,59),
	(114,1,-64.20385,-31.38791,60),
	(115,1,-64.20398,-31.387650000000004,61),
	(116,1,-64.20309,-31.389630000000004,55),
	(117,1,-64.20427000000001,-31.387150000000002,62),
	(118,1,-64.20477000000001,-31.386720000000004,64),
	(119,1,-64.20593000000001,-31.386010000000002,65),
	(120,1,-64.20655000000001,-31.385600000000004,66),
	(121,1,-64.20698,-31.385360000000002,67),
	(122,1,-64.2072,-31.385230000000004,68),
	(123,1,-64.20745000000001,-31.384950000000003,69),
	(124,1,-64.20452,-31.386860000000002,63),
	(125,1,-64.19403000000001,-31.406460000000003,36),
	(126,1,-64.20281,-31.389580000000002,54),
	(127,1,-64.20214,-31.39035,52),
	(128,1,-64.19393000000001,-31.406230000000004,38),
	(129,1,-64.19372,-31.40574,39),
	(130,1,-64.19365,-31.405540000000002,40),
	(131,1,-64.19353000000001,-31.405330000000003,41),
	(132,1,-64.19330000000001,-31.40387,42),
	(133,1,-64.19346,-31.403560000000002,43),
	(134,1,-64.20272,-31.389540000000004,53),
	(135,1,-64.19404,-31.402680000000004,44),
	(136,1,-64.19637,-31.399140000000003,46),
	(137,1,-64.1975,-31.397410000000004,47),
	(138,1,-64.19953000000001,-31.394370000000002,48),
	(139,1,-64.19993000000001,-31.393760000000004,49),
	(140,1,-64.20069000000001,-31.39261,50),
	(141,1,-64.20155000000001,-31.391240000000003,51),
	(142,1,-64.1952,-31.400910000000003,45),
	(143,1,-64.20791,-31.33593,143);

	-- Cargamos solicitudes de viajes

INSERT INTO `SolicitudViaje` (`id`, `id_residente`, `id_viaje`, `id_estado_solicitud`, `fecha`)
VALUES
	(1,2,1,3,'2017-11-28 18:26:17');