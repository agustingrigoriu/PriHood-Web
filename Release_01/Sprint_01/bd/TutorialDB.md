#Tutorial BD

##Instalación

La base de datos se llamará `Prihood`, recordar que es case sensitive, hay que respetar mayúsculas y minúsculas. Para tener la BD instalada hay que seguir los siguientes pasos.

1. Abrir terminal
2. Ejecutar `mysql -h {host} -P {port} -u {username} -p` . Para mi caso sería: `mysql -h localhost -P 3306 -u root -p`. De esta forma nos cambiará el prompt a `mysql>`.
3. Una vez dentro de mysql, deberemos crear la base de datos, para ello haremos uso del comando `CREATE DATABASE Prihood;`, se creará y podremos acceder a ella con `USE Prihood;`. Pero por ahora no lo haremos así y saldremos de mysql.
4. Salir con comando `\q`.
5. Por último, haremos un "recovery" de la estructura de la BD a partir del script *update.sql*. El mismo se encuentra en el repositorio. El recovery se hace con el comando `mysql -u root -p Prihood < update.sql` 
6. La terminal nos indicará con un mensaje que la BD ha sido creada.

**Nota: ** [Comandos para mysql](https://stackoverflow.com/questions/17666249/how-to-import-an-sql-file-using-the-command-line-in-mysql)

#Estándar para trabajar con el script


