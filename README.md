# aDVance ERP 🚀

> Un software ERP para pequeñas y medianas empresas, centrado principalmente en sistemas de inventario, compraventa y producción.

[![Estado](https://img.shields.io/badge/Estado-Beta-orange)](https://github.com/cdavisong/aDVanceERP)
[![Licencia](https://img.shields.io/badge/Licencia-GPLv3-blue)](LICENSE)
[![Plataforma](https://img.shields.io/badge/Plataforma-Windows-0078D6)](https://www.microsoft.com/windows)
[![Release](https://img.shields.io/github/v/release/cdavisong/aDVanceERP)](https://github.com/cdavisong/aDVanceERP/releases)
[![Issues](https://img.shields.io/github/issues/cdavisong/aDVanceERP)](https://github.com/cdavisong/aDVanceERP/issues)

---

## 📑 Tabla de Contenidos

- [Descripción General](#-descripción-general)
- [Stack Tecnológico](#-stack-tecnológico)
- [Características Principales](#-características-principales)
- [Capturas de Pantalla](#-capturas-de-pantalla)
- [Descarga e Instalación](#-descarga-e-instalación)
- [Estado de los Módulos](#-estado-de-los-módulos)
- [Casos de Uso Principales](#-casos-de-uso-principales)
- [Para Desarrolladores](#-para-desarrolladores)
- [Contribuir al Proyecto](#-contribuir-al-proyecto)
- [Licencia](#-licencia-gpl-v3)
- [Contacto](#contacto)

---

## 📋 Descripción General

**aDVance ERP** es un sistema de planificación de recursos empresariales desarrollado en **C# con Windows Forms**, diseñado para pequeñas y medianas empresas. Su enfoque principal es el control de inventario, compraventa y manufactura, integrando procesos clave en una sola plataforma robusta y flexible.

---

## 🛠️ Stack Tecnológico

- **Lenguaje:** C# (.NET 8)
- **Interfaz:** Windows Forms
- **Base de Datos:** MariaDB/MySQL
- **Plataforma:** Windows 10/11
- **Licencia:** GPL v3

---

## ✨ Características Principales

### 👥 Contactos — Gestión Centralizada
- Base de datos unificada de clientes, proveedores y contactos
- Historial de interacciones y relaciones comerciales
- Categorización y segmentación

### 💰 Finanzas — Control Económico
- Gestión de caja, ingresos, egresos y movimientos bancarios
- Múltiples métodos de pago y conciliación básica
- Reportes financieros

### 📊 Inventario — Gestión de Stock
- Categorización inteligente: mercancías, productos terminados, materias primas
- Multi-almacén con control individual
- Movimientos valorizados y existencias en tiempo real
- Ajustes y rectificaciones de inventario

### 🏭 Taller — Producción y Manufactura
- Órdenes de producción con seguimiento
- Control de materiales, mano de obra y gastos indirectos
- Estados de producción (Abierta, En Proceso, Cerrada, Cancelada)
- Costeo automático de manufactura

> **Nota:** El módulo de Taller es completamente funcional pero requiere rediseño UI para simplificar la experiencia.

### 🛒 Compraventa — Operaciones Comerciales
- Ventas: cotización, entrega y seguimiento
- Compras: gestión de proveedores y compras
- Tipos de entrega y seguimiento de estados
- Gestión de precios, descuentos e historial de transacciones

### 🔐 Seguridad — Control de Accesos
- Sistema de roles y permisos granular por módulo
- Autenticación segura y administración de usuarios
- Registro de sesiones y actividades

---

## 🖼️ Capturas de Pantalla

<img width="1365" height="767" alt="Inventario" src="https://github.com/user-attachments/assets/a8baabbc-959d-4de4-b51c-14f12f33b3a2" />
<img width="1365" height="767" alt="Módulo Principal" src="https://github.com/user-attachments/assets/10d34dfa-1a25-4844-8e8c-c0ed101cac4d" />

*¿Quieres más capturas? ¡Sugiere nuevas vistas en Issues!*

---

## 📥 Descarga e Instalación

### Requisitos del Sistema

- **Sistema Operativo:** Windows 7, 10 o 11
- **.NET:** 8 o superior
- **Base de Datos:** MariaDB 10.4+ o MySQL 5.7+
- **Memoria RAM:** 4 GB mínimo (8 GB recomendado)

### Instalación en 3 Pasos

1. **Descargar el instalador**
   - Ve a [Releases](https://github.com/cdavisong/aDVanceERP/releases)
   - Descarga el archivo más reciente (ej: `advanceerp-v0.4.26.135-beta.zip`)

2. **Ejecutar el instalador**
   - Descomprime el archivo descargado
   - Sigue el asistente de instalación

   > **Importante:** El instalador **no crea la base de datos automáticamente**. Solicita el script actualizado al equipo de desarrollo para cargarlo en MariaDB/MySQL.

3. **Iniciar la aplicación**
   - Ejecuta desde el escritorio
   - La primera cuenta creada será la de administración

---

## 📊 Estado de los Módulos

| Módulo        | Estado       | Estabilidad | Notas                          |
|---------------|--------------|-------------|--------------------------------|
| Contactos     | ✅ Activo    | Alta        | Base centralizada operativa    |
| Finanzas      | ✅ Activo    | Alta        | Control financiero completo    |
| Inventario    | ✅ Activo    | Alta        | Gestión de stock estable       |
| Taller        | ✅ Funcional | Media       | UI necesita optimización       |
| Compraventa   | ✅ Activo    | Alta        | Procesos comerciales estables  |
| Seguridad     | ✅ Activo    | Alta        | Sistema de permisos robusto    |

---

## 🎯 Casos de Uso Principales

### Empresas Manufactureras
- Control de ciclo de producción
- Gestión de materias primas y productos terminados
- Costeo preciso de manufactura
- Integración venta–producción–inventario

### Comercios
- Inventario multi-almacén
- Control de ventas y compras
- Administración de clientes y proveedores
- Reportes financieros básicos

---

## 🚀 Para Desarrolladores

### Requisitos de Desarrollo

- **Visual Studio**: 2019 o superior
- **.NET:** 8+
- **Base de Datos:** MariaDB/MySQL
- **Control de Versiones:** Git

### Compilar desde Código Fuente

1. **Clonar repositorio**
   ```bash
   git clone https://github.com/cdavisong/aDVanceERP.git
   cd aDVanceERP
   ```
2. **Configurar conexión de base de datos**
   - Edita el archivo de configuración (ej: `app.config`) con tus credenciales MariaDB/MySQL

3. **Compilar y ejecutar**
   - Abre el proyecto en Visual Studio y compila

---

## 🤝 Contribuir al Proyecto

### Áreas Prioritarias de Mejora

- 🎨 Rediseño UI Módulo Taller — Simplificar interfaz
- 🐛 Testing y Depuración — Reportar issues
- 📊 Reportes Avanzados — Mejorar análisis de datos
- 🔌 Integraciones — APIs y conectores

### ¿Cómo Contribuir?
1. Haz fork del proyecto
2. Crea tu rama (`git checkout -b feature/nueva-funcionalidad`)
3. Haz commit de tus cambios (`git commit -m 'feat: nueva funcionalidad'`)
4. Haz push y abre un Pull Request

> **¿Encontraste un bug o tienes una sugerencia?**  
> Abre un [Issue](https://github.com/cdavisong/aDVanceERP/issues)

---

## 📄 Licencia GPL v3

Este software se distribuye bajo GNU General Public License v3.  
Eres libre de:
- Usar, estudiar y modificar el código
- Distribuir copias
- Contribuir mejoras a la comunidad

Consulta el archivo [LICENSE](LICENSE) para más detalles.

---

## 📬 Contacto

¿Dudas, sugerencias o necesitas el script de base de datos?
- Email: cdavisong@gmail.com
- [Abrir Issue](https://github.com/cdavisong/aDVanceERP/issues)
- GitHub Discussions *(próximamente)*

---

<div align="center">

¿Listo para probar **aDVance ERP**?  
Descarga la [última versión aquí](https://github.com/cdavisong/aDVanceERP/releases)

*Desarrollado en C# WinForms — 6 módulos integrados para gestión empresarial*

⭐ ¡Dános una estrella en GitHub!  
¡Contribuciones bienvenidas, especialmente en UI y testing!

</div>
