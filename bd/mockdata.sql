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

