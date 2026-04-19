# aDVance ERP 🚀

> Sistema de Planificación de Recursos Empresariales (ERP) modular y extensible para pequeñas y medianas empresas.

[![Estado](https://img.shields.io/badge/Estado-En%20Desarrollo-orange)](https://github.com/cdavisong/aDVanceERP)
[![Licencia](https://img.shields.io/badge/Licencia-GPLv3-blue)](LICENSE.txt)
[![Plataforma](https://img.shields.io/badge/Plataforma-Windows-0078D6)](https://www.microsoft.com/windows)
[![.NET](https://img.shields.io/badge/.NET-C%23-purple)](https://dotnet.microsoft.com/)
[![Issues](https://img.shields.io/github/issues/cdavisong/aDVanceERP)](https://github.com/cdavisong/aDVanceERP/issues)

---

## 📑 Tabla de Contenidos

- [Descripción General](#-descripción-general)
- [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
- [Stack Tecnológico](#-stack-tecnológico)
- [Módulos del Sistema](#-módulos-del-sistema)
- [Aplicaciones Móviles](#-aplicaciones-móviles)
- [Instalación y Configuración](#-instalación-y-configuración)
- [Estado de Desarrollo](#-estado-de-desarrollo)
- [Para Desarrolladores](#-para-desarrolladores)
- [Contribuir](#-contribuir)
- [Licencia](#-licencia)
- [Contacto](#-contacto)

---

## 📋 Descripción General

**aDVance ERP** es un sistema de gestión empresarial desarrollado en **.NET/C#** con arquitectura basada en módulos extensibles. Proporciona herramientas integrales para la administración de operaciones comerciales, inventarios, ventas, compras, recursos humanos y más.

### Características Principales

- **Arquitectura modular**: Cada funcionalidad está encapsulada en módulos independientes
- **Extensible**: Diseño basado en interfaces y patrón MVP para facilitar ampliaciones
- **Multiplataforma móvil**: Aplicaciones complementarias para Android (POS y Stock)
- **Código abierto**: Licenciado bajo GPL v3
- **Base de datos**: MySQL/MariaDB

---

## 🏗️ Arquitectura del Proyecto

### Proyectos Principales

| Proyecto | Descripción |
|----------|-------------|
| `aDVanceERP.Desktop` | Aplicación de escritorio principal (.NET WinForms) |
| `aDVanceERP.Core` | Núcleo del sistema con modelos, servicios, repositorios y controladores |
| `aDVanceERP.Core.Extension` | Interfaces y clases base para extensiones modulares |

### Estructura del Core

```
aDVanceERP.Core/
├── Controladores/      # Controladores de archivos y exportación
├── Documentos/         # Definición de documentos del sistema
├── Eventos/            # Sistema de eventos (AgregadorEventos)
├── Excepciones/        # Manejo de excepciones personalizadas
├── Infraestructura/    # Extensiones, globales y helpers
├── Modelos/            # Modelos de datos (BD, Comun, Modulos)
├── Presentadores/      # Lógica de presentación (patrón MVP)
├── Repositorios/       # Acceso a datos
├── Scripts/            # Scripts SQL de base de datos
├── Servicios/          # Servicios del sistema
└── Vistas/             # Interfaces de usuario
```

---

## 🛠️ Stack Tecnológico

| Tecnología | Versión | Uso |
|------------|---------|-----|
| **.NET** | 8+ | Framework principal |
| **C#** | Latest | Lenguaje de programación |
| **WinForms** | - | Interfaz de escritorio |
| **Guna.UI2** | - | Componentes UI modernos |
| **MySQL/MariaDB** | 5.7+/10.4+ | Base de datos |
| **Xamarin/Android** | - | Aplicaciones móviles |
| **Visual Studio** | 2022+ | IDE de desarrollo |
| **Git** | - | Control de versiones |

---

## 📦 Módulos del Sistema

### Módulos Activos ✅

| Módulo | Versión | Funcionalidades Principales |
|--------|---------|----------------------------|
| **Seguridad** | 1.0.0.0 | Autenticación de usuarios, gestión de cuentas, aprobación de usuarios, roles y permisos |
| **Empresa** | 1.0.0.0 | Registro y configuración de datos empresariales, parámetros del sistema |
| **Inventario** | 1.0.0.0 | Productos, almacenes, movimientos, clasificaciones, unidades de medida, presentaciones, estadísticas |
| **Venta** | 1.0.0.0 | Pedidos, ventas, pagos, envíos, clientes, mensajeros, estadísticas de venta |
| **Compra** | 1.0.0.0 | Solicitudes de compra, órdenes de compra, pagos a proveedores, gestión de proveedores, estadísticas |
| **Recursos Humanos** | 1.0.0.0 | Gestión de empleados, administración de personas |
| **Caja Registradora** | 1.0.0.0 | Apertura/cierre de turno, movimientos de caja, detalle de turnos |
| **Móvil** | 1.0.0.0 | Integración con dispositivos aDVance POS y aDVance STOCK |

### Módulos en Desarrollo 🚧

| Módulo | Versión | Estado |
|--------|---------|--------|
| **Servicios** | 1.0.0.0 | Funcionalidad básica disponible, en desarrollo activo |

---

## 📱 Aplicaciones Móviles

### aDVancePOS.Mobile

Aplicación Android para punto de venta móvil.

**Funcionalidades:**
- Procesamiento de ventas
- Cobro a clientes
- Escaneo de productos
- Gestión de ventas en espera
- Consulta de licencias
- Configuración de dispositivo

**Archivos principales:**
- `MainActivity.cs` - Actividad principal
- `CobroActivity.cs` - Proceso de cobro
- `EscanerActivity.cs` - Escaneo de códigos
- `ConfiguracionActivity.cs` - Configuración

### aDVanceSTOCK.Mobile

Aplicación Android para gestión móvil de inventario.

**Funcionalidades:**
- Registro de productos
- Toma de fotografías de productos
- Escaneo de códigos de barras
- Actualización de stock
- Gestión de licencias
- Configuración de dispositivo

**Archivos principales:**
- `MainActivity.cs` - Actividad principal
- `RegistroProductoActivity.cs` - Registro de productos
- `FotoActivity.cs` - Captura de imágenes
- `EscanerActivity.cs` - Escaneo

---

## 🚀 Instalación y Configuración

### Requisitos del Sistema

**Mínimos:**
- Windows 10 o superior
- .NET 8 o superior
- MySQL 5.7+ / MariaDB 10.4+
- 4 GB RAM
- 500 MB espacio disponible

**Recomendados:**
- Windows 11
- .NET 8+
- MySQL 8.0+ / MariaDB 10.6+
- 8 GB RAM
- SSD con 1 GB disponible

### Pasos de Instalación

1. **Clonar el repositorio**
```bash
git clone https://github.com/cdavisong/aDVanceERP.git
cd aDVanceERP
```

2. **Restaurar paquetes NuGet**
```bash
dotnet restore aDVanceERP.sln
```

3. **Configurar base de datos**
- Crear base de datos en MySQL/MariaDB
- Ejecutar scripts desde `aDVanceERP.Core/Scripts/BD/`
- Configurar cadena de conexión en `app.config`

4. **Compilar y ejecutar**
```bash
dotnet build aDVanceERP.sln
dotnet run --project aDVanceERP.Desktop/aDVanceERP.Desktop.csproj
```

### Primer Inicio

1. Ejecutar la aplicación
2. Registrar usuario administrador (Módulo Seguridad)
3. Configurar datos de empresa (Módulo Empresa)
4. Habilitar módulos requeridos

---

## 📊 Estado de Desarrollo

| Componente | Versión | Estado | Estabilidad |
|------------|---------|--------|-------------|
| aDVanceERP.Core | 1.0.0.0 | ✅ Completado | Alta |
| Módulo Seguridad | 1.0.0.0 | ✅ Activo | Alta |
| Módulo Empresa | 1.0.0.0 | ✅ Activo | Alta |
| Módulo Inventario | 1.0.0.0 | ✅ Activo | Alta |
| Módulo Venta | 1.0.0.0 | ✅ Activo | Alta |
| Módulo Compra | 1.0.0.0 | ✅ Activo | Alta |
| Módulo RRHH | 1.0.0.0 | ✅ Activo | Alta |
| Módulo Caja | 1.0.0.0 | ✅ Activo | Alta |
| Módulo Móvil | 1.0.0.0 | ✅ Activo | Media |
| Módulo Servicios | 1.0.0.0 | 🚧 Desarrollo | Baja |
| aDVancePOS.Mobile | - | ✅ Activo | Media |
| aDVanceSTOCK.Mobile | - | ✅ Activo | Media |

---

## 👨‍💻 Para Desarrolladores

### Arquitectura

El proyecto sigue el patrón **MVP (Model-View-Presenter)** con inyección de dependencias mediante eventos.

**Componentes clave:**
- `AgregadorEventos`: Sistema de publicación/suscripción de eventos
- `ModuloExtensionBase`: Clase base para todos los módulos
- Interfaces en `Core.Extension.Interfaces`

### Compilar desde Código Fuente

1. **Requisitos de desarrollo**
   - Visual Studio 2022+
   - .NET SDK 8+
   - MySQL/MariaDB
   - Git

2. **Configurar entorno**
```bash
git clone https://github.com/cdavisong/aDVanceERP.git
cd aDVanceERP
```

3. **Configurar base de datos**
   - Editar archivo de configuración con credenciales MySQL
   - Ejecutar scripts de inicialización

4. **Abrir en Visual Studio**
   - Abrir `aDVanceERP.sln`
   - Restaurar paquetes NuGet
   - Compilar solución

### Crear un Nuevo Módulo

1. Crear nuevo proyecto Class Library (.NET)
2. Heredar de `ModuloExtensionBase`
3. Implementar métodos: `Inicializar()`, `InicializarVistas()`, `Apagar()`
4. Registrar en solución principal

---

## 🤝 Contribuir

### Áreas Prioritarias

- 🎨 Mejoras de UI/UX en módulos existentes
- 🐛 Testing y reporte de bugs
- 📊 Reportes y análisis de datos avanzados
- 🔌 Integraciones con APIs externas
- 📱 Mejoras en aplicaciones móviles
- 📝 Documentación adicional

### Cómo Contribuir

1. Fork el proyecto
2. Crea una rama (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -m 'feat: nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request

### Estándares de Código

- Seguir convenciones de nomenclatura de C#
- Documentar clases y métodos públicos
- Mantener coherencia con arquitectura MVP existente
- Escribir tests para nuevas funcionalidades

> **¿Encontraste un bug?** Abre un [Issue](https://github.com/cdavisong/aDVanceERP/issues)

---

## 📄 Licencia

Este proyecto está licenciado bajo la **GNU General Public License v3.0**.

Eres libre de:
- ✅ Usar el software para cualquier propósito
- ✅ Estudiar cómo funciona
- ✅ Modificar el código
- ✅ Distribuir copias
- ✅ Contribuir mejoras

Consulta el archivo [LICENSE.txt](LICENSE.txt) para más detalles.

---

## 📬 Contacto

¿Dudas, sugerencias o necesitas soporte?

- **Email**: cdavisong@gmail.com
- **GitHub Issues**: [Abrir Issue](https://github.com/cdavisong/aDVanceERP/issues)
- **Repositorio**: [github.com/cdavisong/aDVanceERP](https://github.com/cdavisong/aDVanceERP)

---

<div align="center">

### ¿Listo para probar aDVance ERP?

[Descargar última versión](https://github.com/cdavisong/aDVanceERP/releases)

**Desarrollado en C# WinForms · .NET 8+ · MySQL**

⭐ ¡Dános una estrella en GitHub!  
🤝 ¡Las contribuciones son bienvenidas!

</div>
