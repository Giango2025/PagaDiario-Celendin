using System;

namespace PagaDiarioCelendin
{
    /// <summary>
    /// Clase estática que contiene todas las constantes del sistema.
    /// Centraliza valores fijos para facilitar el mantenimiento.
    /// </summary>
    public static class Constantes
    {
        // ========== TASAS DE INTERÉS ==========
        /// <summary>Tasa de interés anual para préstamos (15%)</summary>
        public const double TASA_INTERES_PRESTAMO_ANUAL = 0.15;

        /// <summary>Tasa de interés mensual para ahorros (1.25% = 15% anual / 12)</summary>
        public const double TASA_INTERES_AHORRO_MENSUAL = 0.0125;

        /// <summary>Tasa de interés anual para ahorros (15%)</summary>
        public const double TASA_INTERES_AHORRO_ANUAL = 0.15;

        // ========== MONTOS ==========
        /// <summary>Saldo base de apertura de cuenta de ahorros (fijo)</summary>
        public const double SALDO_BASE_AHORRO = 20.00;

        /// <summary>Planes de ahorro disponibles</summary>
        public static readonly int[] PLANES_AHORRO = { 20, 30, 40, 50 };

        /// <summary>Número de cuotas para préstamos</summary>
        public const int CUOTAS_PRESTAMO = 30;

        /// <summary>Base para generar IDs de préstamo</summary>
        public const int BASE_ID_PRESTAMO = 1000;

        // ========== VALIDACIONES ==========
        /// <summary>Longitud exacta del DNI</summary>
        public const int LONGITUD_DNI = 8;

        /// <summary>Longitud exacta del teléfono</summary>
        public const int LONGITUD_TELEFONO = 9;

        // ========== MENSAJES ==========
        public const string MSJ_REGRESAR = "REGRESAR";
        public const string MSJ_ERROR_DNI = "DNI inválido (8 dígitos, no todos iguales).";
        public const string MSJ_ERROR_TELEFONO = "Teléfono debe tener 9 dígitos.";
        public const string MSJ_ERROR_NOMBRES = "Solo se permiten letras y espacios.";
        public const string MSJ_ERROR_MONTO_NEGATIVO = "El monto no puede ser negativo.";
    }
}