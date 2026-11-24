<#
  Demostración de todos los endpoints de la API de Hotel Crown (útil para grabar el video).

  Uso (con la aplicación ya iniciada: cd HotelApp.W; dotnet run --launch-profile http):
      powershell -ExecutionPolicy Bypass -File docs\demo-api.ps1            # corre todo seguido
      powershell -ExecutionPolicy Bypass -File docs\demo-api.ps1 -Pausar    # espera Enter entre pasos

  Recomendado: borrar HotelApp.W\Crown.db antes de iniciar la app para empezar con la base vacía
  (así los ids coinciden con los de este script).
#>
param(
    [string]$Base = "http://localhost:5280/api",
    [switch]$Pausar
)

$ErrorActionPreference = "Stop"
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

function Invoke-Paso {
    param([string]$Titulo, [string]$Metodo, [string]$Ruta, [string]$Cuerpo)

    Write-Host ""
    Write-Host "== $Titulo" -ForegroundColor Cyan
    Write-Host "$Metodo /api$Ruta" -ForegroundColor Yellow
    if ($Cuerpo) { Write-Host "Cuerpo: $Cuerpo" -ForegroundColor DarkGray }

    $parametros = @{ Uri = "$Base$Ruta"; Method = $Metodo; UseBasicParsing = $true; ContentType = "application/json; charset=utf-8" }
    if ($Cuerpo) { $parametros.Body = [System.Text.Encoding]::UTF8.GetBytes($Cuerpo) }

    try {
        $respuesta = Invoke-WebRequest @parametros
        $codigo = [int]$respuesta.StatusCode
        $texto = $respuesta.Content
    }
    catch [System.Net.WebException] {
        $codigo = [int]$_.Exception.Response.StatusCode
        $lector = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
        $texto = $lector.ReadToEnd()
    }

    $color = if ($codigo -lt 300) { "Green" } elseif ($codigo -lt 500) { "Magenta" } else { "Red" }
    Write-Host "HTTP $codigo" -ForegroundColor $color
    if ($texto) { Write-Host $texto }
    if ($Pausar) { [void](Read-Host "Enter para continuar") }
}

Write-Host "Demostración de la API de Hotel Crown -> $Base" -ForegroundColor White

# ---------- BUILD ----------
Invoke-Paso "Build: crear"                    POST   "/BuildApi"   '{"nombre":"Torre A","descripcion":"Edificio principal","cantidadPisos":5}'
Invoke-Paso "Build: listar"                   GET    "/BuildApi"
Invoke-Paso "Build: obtener por id"           GET    "/BuildApi/1"
Invoke-Paso "Build: actualizar"               PUT    "/BuildApi/1" '{"idBuild":1,"nombre":"Torre A","descripcion":"Remodelado","cantidadPisos":6}'
Invoke-Paso "Build: id inexistente -> 404"    GET    "/BuildApi/99"
Invoke-Paso "Build: id distinto -> 400"       PUT    "/BuildApi/1" '{"idBuild":2,"nombre":"X","descripcion":"X","cantidadPisos":1}'
Invoke-Paso "Build: crear temporal"           POST   "/BuildApi"   '{"nombre":"Temporal","descripcion":"Para borrar","cantidadPisos":1}'
Invoke-Paso "Build: eliminar -> 204"          DELETE "/BuildApi/2"

# ---------- PISO ----------
Invoke-Paso "Piso: crear"                     POST   "/PisoApi"    '{"numeroPiso":1,"idBuild":1,"nombre":"Primer piso"}'
Invoke-Paso "Piso: listar"                    GET    "/PisoApi"
Invoke-Paso "Piso: obtener por id"            GET    "/PisoApi/1"
Invoke-Paso "Piso: actualizar"                PUT    "/PisoApi/1"  '{"idPiso":1,"numeroPiso":1,"idBuild":1,"nombre":"Planta baja"}'
Invoke-Paso "Piso: eliminar inexistente -> 404" DELETE "/PisoApi/99"
Invoke-Paso "Piso: crear temporal"            POST   "/PisoApi"    '{"numeroPiso":2,"idBuild":1,"nombre":"Temporal"}'
Invoke-Paso "Piso: eliminar -> 204"           DELETE "/PisoApi/2"

# ---------- CLIENTE ----------
Invoke-Paso "Cliente: crear"                  POST   "/ClienteApi" '{"nombre":"Luis","apellido":"Paz","cedula":"001-0000001-1","telefono":"809-555-0101","correo":"luis@example.com"}'
Invoke-Paso "Cliente: listar"                 GET    "/ClienteApi"
Invoke-Paso "Cliente: obtener por id"         GET    "/ClienteApi/1"
Invoke-Paso "Cliente: actualizar"             PUT    "/ClienteApi/1" '{"idCliente":1,"nombre":"Luis","apellido":"Paz","cedula":"001-0000001-1","telefono":"809-555-0199","correo":"luis@example.com"}'
Invoke-Paso "Cliente: eliminar inexistente -> 404" DELETE "/ClienteApi/99"
Invoke-Paso "Cliente: datos obligatorios faltantes -> 400" POST "/ClienteApi" '{}'
Invoke-Paso "Cliente: crear temporal"         POST   "/ClienteApi" '{"nombre":"Temporal","apellido":"X","cedula":"9","telefono":"9","correo":"temporal@example.com"}'
Invoke-Paso "Cliente: eliminar -> 204"        DELETE "/ClienteApi/2"

# ---------- EMPLEADO ----------
Invoke-Paso "Empleado: crear"                 POST   "/EmpleadoApi" '{"nombre":"Eva","apellido":"Ruiz","cedula":"002","telefono":"809-555-0202","cargo":"Recepcionista","salario":35000,"correo":"eva@example.com"}'
Invoke-Paso "Empleado: listar"                GET    "/EmpleadoApi"
Invoke-Paso "Empleado: obtener por id"        GET    "/EmpleadoApi/1"
Invoke-Paso "Empleado: actualizar"            PUT    "/EmpleadoApi/1" '{"idEmpleado":1,"nombre":"Eva","apellido":"Ruiz","cedula":"002","telefono":"809-555-0202","cargo":"Gerente","salario":55000,"correo":"eva@example.com"}'
Invoke-Paso "Empleado: crear temporal"        POST   "/EmpleadoApi" '{"nombre":"Temporal","apellido":"X","cedula":"9","telefono":"9","cargo":"X","salario":1,"correo":"x@example.com"}'
Invoke-Paso "Empleado: eliminar -> 204"       DELETE "/EmpleadoApi/2"

# ---------- HABITACION ----------
Invoke-Paso "Habitación: crear 101 (disponible)"    POST "/HabitacionApi" '{"numero":"101","capacidad":2,"precioNoche":80,"disponible":true,"idPiso":1,"precio":80,"tipo":"Doble","estado":"Libre"}'
Invoke-Paso "Habitación: crear 102 (no disponible)" POST "/HabitacionApi" '{"numero":"102","capacidad":4,"precioNoche":120,"disponible":false,"idPiso":1,"precio":120,"tipo":"Suite","estado":"Mantenimiento"}'
Invoke-Paso "Habitación: listar"              GET    "/HabitacionApi"
Invoke-Paso "Habitación: obtener por id"      GET    "/HabitacionApi/1"
Invoke-Paso "Habitación: actualizar"          PUT    "/HabitacionApi/1" '{"idHabitacion":1,"numero":"101","capacidad":3,"precioNoche":80,"disponible":true,"idPiso":1,"precio":80,"tipo":"Triple","estado":"Libre"}'
Invoke-Paso "Habitación: eliminar inexistente -> 404" DELETE "/HabitacionApi/99"
Invoke-Paso "Habitación: precio negativo -> 400" POST "/HabitacionApi" '{"numero":"999","capacidad":2,"precioNoche":-80,"disponible":true,"idPiso":1}'
Invoke-Paso "Habitación: crear temporal"      POST   "/HabitacionApi" '{"numero":"103","capacidad":1,"precioNoche":50,"disponible":true,"idPiso":1,"precio":50,"tipo":"Sencilla","estado":"Libre"}'
Invoke-Paso "Habitación: eliminar -> 204"     DELETE "/HabitacionApi/3"

# ---------- USUARIO ----------
Invoke-Paso "Usuario: crear"                  POST   "/UsuarioApi" '{"nombreUsuario":"admin","clave":"S3creta!","rol":"Administrador"}'
Invoke-Paso "Usuario: listar (sin clave)"     GET    "/UsuarioApi"
Invoke-Paso "Usuario: obtener por id"         GET    "/UsuarioApi/1"
Invoke-Paso "Usuario: actualizar"             PUT    "/UsuarioApi/1" '{"idUsuario":1,"nombreUsuario":"admin","clave":"Nueva!","rol":"Supervisor"}'
Invoke-Paso "Usuario: eliminar inexistente -> 404" DELETE "/UsuarioApi/99"
Invoke-Paso "Usuario: crear temporal"         POST   "/UsuarioApi" '{"nombreUsuario":"temporal","clave":"x","rol":"Invitado"}'
Invoke-Paso "Usuario: eliminar -> 204"        DELETE "/UsuarioApi/2"

# ---------- RESERVA ----------
Invoke-Paso "Reserva EXITOSA (3 noches x 80 = 240)"         POST "/ReservaApi" '{"idCliente":1,"idHabitacion":1,"fechaEntrada":"2026-12-01","fechaSalida":"2026-12-04"}'
Invoke-Paso "Reserva RECHAZADA: fechas se solapan -> 422"   POST "/ReservaApi" '{"idCliente":1,"idHabitacion":1,"fechaEntrada":"2026-12-03","fechaSalida":"2026-12-06"}'
Invoke-Paso "Reserva RECHAZADA: salida antes de entrada"    POST "/ReservaApi" '{"idCliente":1,"idHabitacion":1,"fechaEntrada":"2026-12-10","fechaSalida":"2026-12-08"}'
Invoke-Paso "Reserva RECHAZADA: habitación no disponible"   POST "/ReservaApi" '{"idCliente":1,"idHabitacion":2,"fechaEntrada":"2026-12-10","fechaSalida":"2026-12-12"}'
Invoke-Paso "Reserva RECHAZADA: cliente inexistente"        POST "/ReservaApi" '{"idCliente":99,"idHabitacion":1,"fechaEntrada":"2026-12-10","fechaSalida":"2026-12-12"}'
Invoke-Paso "Cliente con reservas: no se puede eliminar -> 409"    DELETE "/ClienteApi/1"
Invoke-Paso "Habitación con reservas: no se puede eliminar -> 409" DELETE "/HabitacionApi/1"
Invoke-Paso "Reserva: listar"                 GET    "/ReservaApi"
Invoke-Paso "Reserva: obtener por id"         GET    "/ReservaApi/1"
Invoke-Paso "Reserva: actualizar (total -> 400)" PUT "/ReservaApi/1" '{"idReserva":1,"idCliente":1,"idHabitacion":1,"fechaEntrada":"2026-12-01","fechaSalida":"2026-12-06"}'
Invoke-Paso "Reserva: crear otra para borrar" POST   "/ReservaApi" '{"idCliente":1,"idHabitacion":1,"fechaEntrada":"2026-12-20","fechaSalida":"2026-12-22"}'
Invoke-Paso "Reserva: eliminar -> 204"        DELETE "/ReservaApi/2"

Write-Host ""
Write-Host "Demostración terminada. Los datos quedan guardados (puede verlos en la web)." -ForegroundColor White
