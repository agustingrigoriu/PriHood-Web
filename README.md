# Primera ejecución
## Con Visual Code
1. Si es primera vez, abrir proyecto en Visual Code
2. Sino esta intalado el plugin de C#, instalar y reiniciar Visual Code.
3. Visual code instalará un par de cosas, dejar, si salen carteles darle aceptar a todo.
4. Ir al `Debug` (el bichito tachado del costado) y darle play.

## SIN Visual Code
Para ejectuar el proyecto sin Visual Code:
1. En consola o terminal correr
```dotnet restore```
para instalar dependencias.
2. Y luego para correr:
```dotnet run```

# Ejecutar facilmente
Para ejecutar todo de una sola vez, se puede hacer

```sh
npm start
```
en donde estará ejecutandose el panel y el api a la vez.

# Ejecutar por separado
## Ejecutar solamente la web
Para esto solo hay que correr el dotnet:

```dotnet run```
## Modificación del panel de administración
Para modificar el panel, hay que tener ejecutando el servidor de C# y el compilador de Angular en modo escucha.
Esto permite recompilar el panel con cada cambio y no tener que reiniciar el servidor cada vez que se quiera ver el cambio.
Para esto, en una consola separada, hay que ejecutar:

```npm install``` 
(si es la primera vez)

```ng build --watch```

Previamente hay que tener instalado Angular. Para hacer esto ir al sitio de angular (https://angular.io/) e instalarlo si falla el comando `ng`.

```npm install -g @angular/cli```

en Linux/Mac puede ser que se necesite ejecutar el comando anterior como root (con sudo), ya que establece un nuevo comando en el sistema.

## Actualizar base de datos

Para actualizar la base de datos es necesario ejecutar el comando:

```npm run update-db```

El comando va a pedir la contraseña de la base de datos.

Si tira error de que no existe el comando `mysql` falta agregar la ruta del bin de mysql al PATH. En Linux y Mac se puede hacer de esta forma:
```export PATH=${PATH}:/usr/local/mysql/bin```