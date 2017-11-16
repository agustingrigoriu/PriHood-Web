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


INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 1','Manzana 1 Lote 1','4f9824',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 2','Manzana 1 Lote 2','6ccb53',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 3','Manzana 1 Lote 3','db9682',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 4','Manzana 2 Lote 1','8ad5b5',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 5','Manzana 2 Lote 2','1fdae1',1);
INSERT INTO Residencia (nombre, ubicacion, codigo, id_barrio) VALUES ('Residencia 6','Manzana 2 Lote 3','e43178',1);

-- Usuarios
INSERT INTO Usuario(email, password, id_perfil) VALUES("admin@admin.com","8c6976e5b5410415bde908bd4dee15", 1);
INSERT INTO Usuario(email, password, id_perfil, id_barrio) VALUES("lmgabrielac@gmail.com","8c6976e5b5410415bde908bd4dee15",2, 1);
INSERT INTO Usuario(email, password, id_perfil, id_barrio) VALUES("elianabelen.valdivia@gmail.com","8c6976e5b5410415bde908bd4dee15",4, 1);
INSERT INTO Usuario(email, password, id_perfil, id_barrio) VALUES("madrazolucas@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1);
INSERT INTO Usuario(email, password, id_perfil, id_barrio) VALUES("pato12p@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1);
INSERT INTO Usuario(email, password, id_perfil, id_barrio) VALUES("agustin.gregorieu@gmail.com","8c6976e5b5410415bde908bd4dee15",3, 1);

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

