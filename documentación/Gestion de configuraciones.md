# Gestion de configuraciones
## Pautas
- Una branch por cada *User Story* (US) en cada repositorio.
- Varios commits son mejor que uno. Permite encontrar mejor los errores.
- Commitear en lo posible cuando está funcionando. De no estar funcionando correctamente avisar en el comentario del commit.

## Sobre las branch
 El nombre de la misma es el código de la US que está en jira, por ejemplo `PRIH-1`. Esto para darle trazabilidad a las historias.

## Sobre los commits
Cada commit debe llevar un hashtag con el código de la tarea asociada a ese commit. Por ejemplo, `Crear pantalla de visitas #PRIH-123`. Dandole la trazabilidad a jira.

## Al finalizar una US
Se creará un pull request, en donde se asigna a alguien para revisar. No puede ser el mismo que creo el PR.

El responsable asignado tendrá que bajar la branch y corroborar que cumple con los criterios definidos para la US.

De cumplirse correctamente, se acepta el PR. Se borra la branch y se pasa a *Done* la US en jira.