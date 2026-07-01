# Sistema de Gestión de Torneos de Fútbol

## Descripción

Aplicación web desarrollada en ASP.NET Core MVC utilizando C# y Supabase como sistema de almacenamiento de datos. El sistema permite administrar torneos de fútbol, gestionar equipos participantes y registrar partidos con sus respectivos resultados.

La aplicación implementa una arquitectura MVC (Model-View-Controller) y utiliza Supabase como única fuente de datos mediante el paquete oficial Supabase 1.1.1.

## Tecnologías Utilizadas

* ASP.NET Core MVC
* C#
* .NET 8
* Supabase
* Bootstrap 5
* Visual Studio
* PostgreSQL (gestionado por Supabase)

## Requisitos Previos

* Visual Studio 2022 o superior
* .NET 8 SDK
* Cuenta en Supabase
* Conexión a Internet

## Configuración de Supabase

### Crear las tablas

Ejecutar los siguientes scripts SQL en el editor SQL de Supabase.

#### Tabla Torneos

```sql
create table torneos
(
    id bigint generated always as identity primary key,
    nombre varchar(100) not null,
    edicion integer not null,
    activo boolean default true,
    creado_en timestamp default now()
);
```

#### Tabla Equipos

```sql
create table equipos
(
    id bigint generated always as identity primary key,
    torneo_id bigint not null,
    nombre varchar(100) not null,
    ciudad varchar(100) not null,

    constraint fk_equipo_torneo
        foreign key (torneo_id)
        references torneos(id)
);
```

#### Tabla Partidos

```sql
create table partidos
(
    id bigint generated always as identity primary key,

    torneo_id bigint not null,

    equipo_local_id bigint not null,

    equipo_visitante_id bigint not null,

    goles_local integer default 0,

    goles_visitante integer default 0,

    fecha_partido timestamp not null,

    jugado boolean default false,

    constraint fk_partido_torneo
        foreign key (torneo_id)
        references torneos(id),

    constraint fk_local
        foreign key (equipo_local_id)
        references equipos(id),

    constraint fk_visitante
        foreign key (equipo_visitante_id)
        references equipos(id)
);
```

### Configurar credenciales

Abrir el archivo:

```text
Services/SupabClient.cs
```

y reemplazar:

```csharp
private static string url = "TU_URL_SUPABASE";
private static string key = "TU_ANON_KEY";
```

por los datos correspondientes del proyecto Supabase.

## Dependencias

Instalar los siguientes paquetes NuGet:

```powershell
Install-Package Supabase -Version 1.1.1
Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```

## Ejecución

1. Clonar el repositorio.
2. Abrir la solución en Visual Studio.
3. Restaurar paquetes NuGet.
4. Configurar las credenciales de Supabase.
5. Ejecutar la aplicación.
6. Acceder a la página principal del sistema.

## Funcionalidades

### Torneos

* Crear torneo.
* Editar torneo.
* Consultar torneos activos.
* Desactivar torneos.
* Validación para impedir la desactivación si existen equipos inscritos.

### Equipos

* Registrar equipos.
* Modificar equipos.
* Eliminar equipos.
* Validación para impedir la eliminación si el equipo participa en partidos.

### Partidos

* Registrar partidos.
* Generar enfrentamientos aleatorios.
* Registrar resultados.
* Eliminar partidos pendientes.
* Restricción para impedir modificaciones en partidos ya jugados.

## Arquitectura

El sistema sigue el patrón MVC:

* Models: Entidades y ViewModels.
* Views: Interfaz de usuario desarrollada con Razor y Bootstrap.
* Controllers: Gestión de solicitudes HTTP.
* Services: Comunicación con Supabase y aplicación de reglas de negocio.

## Autor

Proyecto académico desarrollado para la gestión de torneos de fútbol utilizando ASP.NET Core MVC y Supabase.
