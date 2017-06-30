# Estilos de Programación/Código

En este documento se explicarán los estilos y patrones **básicos** a usar a la hora de escribir del código de nuestra aplicación. Todo código debería adaptarse a estos estándares con el objetivo de lograr un fácil mantenimiento. Existen guías mucho más amplias en para este tema pero no es el objetivo aprender de memoria cada una de ellas, si no ir desarrollando nuevas a medida que desarrollemos como equipo, pero partiendo de algunas ideas esenciales.

## Convenciones generales de nombre y formato del código

### Espacios en blanco

No usar tabs ni espacios en blancos al final de una línea.

### Longitud de línea

80 caracteres o menos. Si se supera esa cantidad se debería continuar en una nueva línea.

### Indentación

Dos espacios por nivel lógico. Esto es fácilmente configurable en el entorno Visual Studio Code.

### Espacios alrededor de los operadores
Siempre poner espacios alrededor de los operadores ( = + - * / ) , y después de comas:

Ejemplo
```
var x = y + z;
var values = ["Volvo", "Saab", "Fiat"];
```

### Estructuras de Control

Usar el estilo K&R, es decir, la llave “{” se coloca al final de la primera línea, si surge un caso como el del “else” este estará rodeado de ambas llaves.

Ejemplo:

```
if (...) {
} else if (...) {
} else {
}

while (...) {
}

do {
} while (...);

for (...; ...; ...) {
}

switch (...) {
  case 1: {
    // When you need to declare a variable in a switch, put the block in braces
    int var;
    break;
  }
  case 2:
    ...
    break;
  default:
    break;
}

```

### Evitar variables globales
Minimizar el uso de variables globales. Esto incluye todos los tipos de datos, objetos y funciones. Variables y funciones globales pueden ser sobrescritos por otros scripts.

* Utilizar variables locales en su lugar, y aprender cómo utilizar los cierres .
* Siempre declarar variables locales
* Todas las variables utilizadas en una función deben ser declarados como variables locales.

### Declaraciones sobre el Top
Es una buena práctica de codificación para poner todas las declaraciones en la parte superior de cada script o función.

Esto permite:

* Código más limpio.
* Proporcionar un único lugar para buscar las variables.
* Que sea más fácil evitar las variables globales no deseados (implícitas)
* Reducir la posibilidad de re-declaraciones no deseados

Ejemplo:

### Inicializar variables

Es una buena práctica de codificación para inicializar las variables cuando se declaran.

```
var firstName = "",
    lastName = "",
    price = 0,
    discount = 0,
    fullPrice = 0,
    myArray = [],
    myObject = {};
```

## Convenciones de C#

### Convenciones de diseño

Un buen diseño utiliza un formato que destaque la estructura del código y haga que el código sea más fácil de leer. Las muestras y ejemplos de Microsoft cumplen las convenciones siguientes:

* Utilice la configuración del Editor de código predeterminada (sangría automática, sangrías de 4 caracteres, tabulaciones guardadas como espacios). Para obtener más información, vea Opciones, editor de texto, C#, formato.
* Escriba solo una instrucción por línea.
* Escriba solo una declaración por línea.
* Si a las líneas de continuación no se les aplica sangría automáticamente, hágalo con una tabulación (cuatro espacios).
* Agregue al menos una línea en blanco entre las definiciones de método y las de propiedad.
* Utilice paréntesis para que las cláusulas de una expresión sean evidentes, como se muestra en el código siguiente.

### Convenciones de los comentarios
* Coloque el comentario en una línea independiente, no al final de una línea de código.
* Comience el texto del comentario con una letra mayúscula.
* Finalice el texto del comentario con un punto.
* Inserte un espacio entre el delimitador de comentario (//) y el texto del comentario, como se muestra en el ejemplo siguiente.

```
// The following declaration creates a query. It does not run
// the query.
```
* No cree bloques con formato de asteriscos alrededor de comentarios.

### Convención en Strings (Tipo de datos)

* Utilice el operador + para concatenar cadenas cortas, como se muestra en el código siguiente.

```
string displayName = nameList[n].LastName + ", " + nameList[n].FirstName;
```
* Para anexar cadenas en bucles, especialmente cuando se trabaja con grandes cantidades de texto, use un objeto StringBuilder.

```
var phrase = "lalalalalalalalalalalalalalalalalalalalalalalalalalalalalala";
var manyPhrases = new StringBuilder();
for (var i = 0; i < 10000; i++)
{
    manyPhrases.Append(phrase);
}
//Console.WriteLine("tra" + manyPhrases);
```

### Variables locales con asignación implícita de tipos

* Use tipos implícitos para las variables locales cuando el tipo de la variable sea obvio desde el lado derecho de la asignación, o cuando el tipo exacto no sea importante.

```
var var1 = "This is clearly a string.";
var var2 = 27;
var var3 = Convert.ToInt32(Console.ReadLine());
```

* No use var cuando el tipo no sea evidente desde el lado derecho de la asignación.

```
int var4 = ExampleClass.ResultSoFar();
```

### Estructura general de un programa de C#

Los programas de C# pueden constar de uno o más archivos. Cada archivo puede contener cero o más espacios de nombres. Un espacio de nombres puede contener tipos como clases, structs, interfaces, enumeraciones y delegados, además de otros espacios de nombres. El siguiente es el esqueleto de un programa de C# que contiene todos estos elementos.

```
// A skeleton of a C# program 
using System;
namespace YourNamespace
{
    class YourClass
    {
    }

    struct YourStruct
    {
    }

    interface IYourInterface 
    {
    }

    delegate int YourDelegate();

    enum YourEnum 
    {
    }

    namespace YourNestedNamespace
    {
        struct YourStruct 
        {
        }
    }

    class YourMainClass
    {
        static void Main(string[] args) 
        {
            //Your program starts here...
        }
    }
}
```

## Convenciones de Javascript

### Nombre de variables

* Todos los nombres comienzan con una letra.
```
firstName = "John";
lastName = "Doe";

price = 19.90;
tax = 0.20;

fullPrice = price + (price * tax);
```

### Regla de los objetos

* Colocar el soporte de apertura en la misma línea que el nombre del objeto.
* Utilizar dos puntos más un espacio entre cada propiedad y su valor.
* Utilizar comillas alrededor de los valores de cadena, no en torno a valores numéricos.
* No añadir una coma después de la última pareja propiedad-valor.
* Colocar el soporte de cierre en una nueva línea, sin espacios iniciales.
* Siempre terminar una definición de objeto con un punto y coma.

Ejemplo:
```
var person = {
    firstName: "John",
    lastName: "Doe",
    age: 50,
    eyeColor: "blue"
}; 
```
* Objetos cortos se pueden escribir comprimidos, en una línea, usando solamente los espacios entre las propiedades, como este:

```
var person = {firstName:"John", lastName:"Doe", age:50, eyeColor:"blue"};
```

