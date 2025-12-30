using SchoolFees.EN.models;

namespace SchoolFees.BL.Codes
{
    /// <summary>
    /// Generador de códigos cortos y únicos para alumnos.
    /// Formato: [InicialesApellido][Año][SufijoBase36]
    /// Ejemplo: JS25A9
    /// </summary>
    public static class AlumnoCodeGenerator
    {
        // Conjunto de caracteres para conversión a Base36 (0-9, A-Z)
        private const string Base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Genera un código único para un alumno basado en sus apellidos,
        /// el año actual y un consecutivo único.
        /// </summary>
        /// <param name="alumno">Entidad alumno</param>
        /// <param name="consecutivo">Número incremental único (ej. Id de BD)</param>
        /// <returns>Código de alumno corto y legible</returns>
        public static string Generar(Alumno alumno, int consecutivo)
        {
            // Iniciales obtenidas de los apellidos
            var iniciales = ObtenerIniciales(alumno.Apellidos);

            // Año actual en formato corto (ej. 2025 -> 25)
            var year = DateTime.UtcNow.Year % 100;

            // Conversión del consecutivo a Base36 para reducir longitud
            var sufijo = ToBase36(consecutivo, 2);

            return $"{iniciales}{year}{sufijo}";
        }

        /// <summary>
        /// Obtiene las iniciales de los apellidos del alumno.
        /// Ejemplo: "Jimenez Santos" -> "JS"
        /// </summary>
        private static string ObtenerIniciales(string apellidos)
        {
            var partes = apellidos
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return string.Concat(partes.Select(p => char.ToUpper(p[0])));
        }

        /// <summary>
        /// Convierte un número decimal a Base36 y ajusta su longitud.
        /// </summary>
        /// <param name="value">Número a convertir</param>
        /// <param name="length">Longitud fija del resultado</param>
        /// <returns>Cadena en Base36</returns>
        private static string ToBase36(int value, int length)
        {
            var result = "";

            while (value > 0)
            {
                result = Base36[value % 36] + result;
                value /= 36;
            }

            return result.PadLeft(length, '0');
        }
    }
}
