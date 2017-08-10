# Web y api rest

Utilizamos los servicios de Amazon Web Services para poder ajolar nuestros servidor C# de backend y la base de datos MySQL.

Separamos lo que sería el modulo encargado de la comunicación con la aplicación que sería el Api Rest, de lo que sería nuestro panel de administración.

Ambos reciben servicios de la capa de negocio que posee todos los controladores de las reglas de negocio de cada módulo y se conecta con la base de datos. 

# Ionic App

Utilizamos el framework Ionic que es un conjunto de herramientas que nos permite crear aplicaciones basadas en web.

Utiliza un Webview para mostrar los componentes, que se conectan al backend (el api rest) a través de un módulo. Tambíen nos permite acceder a características nativas como sacar fotos, geolocalización y más cosas de forma simple sin importar la plataforma que está corriendo el celular (iOS o Android).