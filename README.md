# 📘 README - Paga Diario: Sistema de Gestión de Microcréditos

[![GitHub](https://img.shields.io/badge/Repositorio-GitHub-blue?logo=github)](https://github.com/Giango2025/PagaDiario-Celendin)
[![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.7.2-purple)](https://dotnet.microsoft.com/)
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022-orange)](https://visualstudio.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---

## 🏦 Descripción del Proyecto

**Paga Diario** es un sistema de gestión financiera desarrollado en **C#** para la organización local sin fines de lucro "Paga Diario" en la provincia de **Celendín, Cajamarca, Perú**. 

El sistema automatiza la colocación de microcréditos y la captación de ahorros, reemplazando por completo el proceso manual (cuadernos, cartulinas y calculadora) por una solución digital robusta, segura y eficiente.

---

## 🎯 Características Principales

### ✅ Menú Principal con 12 Opciones:

| Opción | Funcionalidad |
|--------|---------------|
| **1**  | Registrar Nuevo Cliente |
| **2**  | Aperturar Cuenta de Ahorros (con planes 20/30/40/50 soles) |
| **3**  | Otorgar Préstamo (15% de interés anual, 30 cuotas diarias) |
| **4**  | Registrar Pago Diario (descuenta automáticamente del saldo pendiente) |
| **5**  | Ver Reporte General (Clientes, Préstamos, Ahorros, Pagos) |
| **6**  | Clientes con Ahorro |
| **7**  | Clientes con Crédito |
| **8**  | Clientes con Ambos |
| **9**  | Estadísticas Financieras |
| **10** | Buscar Cliente por Nombre |
| **11** | Simular 100 Registros de Prueba |
| **12** | Salir y Guardar (persistencia dual en .txt y .bin) |

### 🔐 Validaciones Robustas

- **DNI:** 8 dígitos, no todos iguales (ej. 11111111 no permitido).
- **Nombres y Apellidos:** Solo letras y espacios (sin números ni caracteres especiales).
- **Teléfono:** 9 dígitos exactos.
- **Montos:** Validación de números positivos, no se permiten valores negativos.
- **Planes de Ahorro:** Solo 20, 30, 40 o 50 soles.
- **Pagos:** No puede superar el saldo pendiente del préstamo.

### 💾 Persistencia Dual

- **Archivos de Texto (.txt):** Legibles por humanos, útiles para auditoría.
- **Archivos Binarios (.bin):** No legibles, mayor seguridad y eficiencia.
- **Carga Automática:** Al iniciar, prioriza archivos binarios; si no existen, carga desde texto.

### 🎨 Interfaz Visual

- Colores en consola para diferenciar:
  - **Cian** → Títulos e información.
  - **Verde** → Mensajes de éxito.
  - **Rojo** → Mensajes de error.
  - **Amarillo** → Advertencias e instrucciones.
- Instrucción **"REGRESAR"** para cancelar cualquier operación y volver al menú.

---

## 🛠️ Tecnologías Utilizadas

- **Lenguaje:** C# (.NET Framework 4.7.2)
- **IDE:** Visual Studio 2022
- **Persistencia:** `System.IO`, `BinaryFormatter`, `StreamReader/Writer`
- **Validaciones:** `Regex`, `TryParse`
- **Control de Versiones:** Git + GitHub

---

## 📦 Estructura del Proyecto

```
PagaDiario-Celendin/
├── PagaDiarioCelendin/
│   ├── Program.cs              # Menú principal y punto de entrada
│   ├── Cliente.cs              # Clase Cliente
│   ├── Prestamo.cs             # Clase Préstamo
│   ├── Ahorro.cs               # Clase Ahorro
│   ├── PagoDiario.cs           # Clase Pago Diario
│   ├── ArchivoService.cs       # Persistencia (lectura/escritura)
│   ├── NegocioService.cs       # Lógica de negocio (CRUD)
│   ├── ReporteService.cs       # Reportes y estadísticas
│   ├── Utils.cs                # Validaciones y mensajes con color
│   ├── Constantes.cs           # Constantes centralizadas
│   ├── App.config              # Configuración de .NET Framework
│   ├── Properties/
│   │   └── AssemblyInfo.cs     # Metadatos del ensamblado
│   └── PagaDiarioCelendin.csproj
├── .gitignore                  # Archivos ignorados por Git
├── README.md                   # Este archivo
└── LICENSE                     # Licencia MIT
```

---

## 🚀 Instalación y Ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/Giango2025/PagaDiario-Celendin.git
cd PagaDiario-Celendin
```

### 2. Abrir en Visual Studio

- Abre Visual Studio 2022 (o superior).
- Selecciona **"Abrir un proyecto o solución"**.
- Navega hasta la carpeta clonada y abre el archivo `PagaDiarioCelendin.csproj`.

### 3. Compilar y Ejecutar

- Presiona **Ctrl+Shift+B** para compilar.
- Presiona **F5** para ejecutar con depuración.

### 4. Uso Rápido

Al ejecutar, verás el menú principal con 12 opciones. Sigue las instrucciones en pantalla.

- Para cancelar cualquier operación, escribe **`REGRESAR`**.
- Al salir (opción 12), los datos se guardan automáticamente.

---

## 📸 Capturas de Pantalla

> Puedes ver capturas del menú, reportes y validaciones en el informe PDF y en la presentación PPT dentro del repositorio.

---

## 📄 Documentación Adicional

- **Informe Final:** [Descargar PDF](https://github.com/Giango2025/PagaDiario-Celendin/blob/feature/base/INFOME%20FINAL%202026%20FUNDAMENTOS%20DE%20PROGRAMACION.pdf)
- **Presentación:** [Descargar PPTX](https://github.com/Giango2025/PagaDiario-Celendin/blob/feature/base/PRESENTACION.pptx)
- **Enlace al Repositorio:** [GitHub](https://github.com/Giango2025/PagaDiario-Celendin)

---

## 🧪 Pruebas Realizadas

Se ejecutaron 10 casos de prueba documentados en el informe:
- 6 casos normales (registro, ahorro, préstamo, pago, reportes).
- 2 casos límite (capital 0 y 10000).
- 2 casos inválidos (DNI corto, monto fuera de rango).

Todos los casos **PASARON** exitosamente.

---

## 👥 Autores

| Nombre | Participación | Rol |
|--------|---------------|-----|
| **Arribasplata Rojas, Leonardo David** | 100% | Lógica de negocio y persistencia |
| **Centurión Tingal, Kebin Kenjhi** | 100% | Validaciones y reportes |
| **Silva Aguirre, Gianfranco Alejandro** | 100% | Menú, integración y documentación |

---

## 📚 Bibliografía

- Joyanes Aguilar, L. (2020). *Fundamentos de programación: Algoritmos, estructura de datos y objetos*. McGraw-Hill.
- Microsoft. (s.f.). *Programming guide concepts*. https://learn.microsoft.com/es-es/dotnet/csharp/programming-guide/concepts/
- GitHub. (s.f.). *Start your journey*. https://docs.github.com/es/get-started/start-your-journey

---

## 📞 Contacto

Para consultas o sugerencias, abre un **Issue** en el repositorio o contacta a los autores.

---

## 📄 Licencia

Este proyecto está bajo la licencia **MIT**. Consulta el archivo [LICENSE](LICENSE) para más detalles.

---

**¡Gracias por usar Paga Diario!** 💰🚀