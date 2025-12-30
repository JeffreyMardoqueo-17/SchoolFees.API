#### Para la configuración de la cadena de conexion a la BD

En este caso uso llos secretos de usaurios que se inicializan.

 ```bash
    dotnet user-secrets init
 ```

para agregar la cadena de conexion, despues que ya haya inicializado los secretos de usuarios uso el siguiente comando:

```bash
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(el servidor);Database=(Nombre de la BD);Trusted_Connection=True;TrustServerCertificate=True;"
```

Esto puede variar dependiendo si el SQL lo tengo con usuario o contraeña, en este caso lo tengo con Autenticacion de windows

#### ultimo paso para dejarlo listo

Para dejarlo listo debo de agregar una ultima cosa en el program:

```csharp
// DbContext
builder.Services.AddDbContext< SchoolFeesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```
