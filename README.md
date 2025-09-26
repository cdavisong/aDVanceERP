# aDVance ERP 🚀

**Sistema de Gestión Empresarial para Windows - Versión 0.4.26.135-beta**

[![Estado](https://img.shields.io/badge/Estado-Beta-orange)](https://github.com/tuusuario/advanceerp)
[![Licencia](https://img.shields.io/badge/Licencia-GPLv3-blue)](LICENSE)
[![Plataforma](https://img.shields.io/badge/Plataforma-Windows-0078D6)](https://www.microsoft.com/windows)
[![Tecnología](https://img.shields.io/badge/Tecnología-C%23%20WinForms-239120)](https://dotnet.microsoft.com)

## 📋 Descripción General

**aDVance ERP** es un sistema de planificación de recursos empresariales desarrollado en **C# con Windows Forms**, diseñado específicamente para pequeñas y medianas empresas. Ofrece 6 módulos principales integrados para una gestión empresarial completa.

<img width="1365" height="767" alt="image" src="https://github.com/user-attachments/assets/10d34dfa-1a25-4844-8e8c-c0ed101cac4d" />

## 🛠️ Stack Tecnológico

- **Lenguaje**: C# (.NET 8)
- **Interfaz**: Windows Forms
- **Base de Datos**: MariaDB/MySQL
- **Plataforma**: Windows 10/11
- **Licencia**: GPL v3

## ✨ Características Principales

### 👥 **Contactos** - Gestión Centralizada
- Base de datos unificada de clientes, proveedores y contactos
- Información de contacto completa (teléfonos, emails, direcciones)
- Historial de interacciones y relaciones comerciales
- Categorización y segmentación de contactos

### 💰 **Finanzas** - Control Económico
- Gestión de caja y movimientos monetarios
- Control de ingresos y egresos
- Múltiples métodos de pago (efectivo, transferencia)
- Conciliación básica y reportes financieros
- Movimientos bancarios y cuentas

### 📊 **Inventario** - Gestión de Stock
<img width="1365" height="767" alt="image" src="https://github.com/user-attachments/assets/a8baabbc-959d-4de4-b51c-14f12f33b3a2" />

- **Categorización inteligente**: Mercancías, Productos Terminados, Materias Primas
- Múltiples almacenes con control individual
- Movimientos de inventario con valorización automática
- Costo promedio y control de existencias en tiempo real
- Ajustes de inventario y rectificaciones

### 🏭 **Taller** - Producción y Manufactura ✅ **FUNCIONAL**
- Órdenes de producción con seguimiento completo
- Control de materiales y consumo de materias primas
- Gestión de actividades de producción y mano de obra
- Cálculo automático de costos de manufactura
- Estados de producción (Abierta, En Proceso, Cerrada, Cancelada)
- Control de gastos indirectos y dinámicos

> **⚠️ Nota sobre el Módulo de Taller**: Completamente funcional pero con interfaz cargada. En desarrollo: rediseño para simplificar la experiencia de usuario.

### 🛒 **Compraventa** - Operaciones Comerciales
- **Ventas**: Proceso completo de venta, desde cotización a entrega
- **Compras**: Gestión de compras a proveedores
- Múltiples tipos de entrega (Presencial, Mensajería)
- Seguimiento de estado de entregas
- Gestión de precios y descuentos
- Historial completo de transacciones

### 🔐 **Seguridad** - Control de Accesos
- Sistema de roles y permisos granular
- Autenticación segura de usuarios
- Control por módulo y funcionalidad
- Administración de cuentas de usuario
- Registro de sesiones y actividades

## 📥 Descarga e Instalación

### Requisitos del Sistema
- **Sistema Operativo**: Windows 7, 10 o 11
- **.NET**: 8 o superior
- **Base de Datos**: MariaDB 10.4+ o MySQL 5.7+
- **Memoria RAM**: 4 GB mínimo (8 GB recomendado)

### Instalación en 3 Pasos

1. **Descargar el instalador**
   - Ve a [Releases](https://github.com/tuusuario/aDVanceERP/releases)
   - Descarga el archivo `advanceerp-v0.4.26.135-beta.zip`

2. **Ejecutar el instalador**
   - Descompima el contenido del archivo descargado
   - Seguir el asistente de instalación
   
   NOTA: Actualmente el instalador no crea la base de datos automátiamente, solicite al equipo de desarrollo el script de base de datos actualizado para cargar en su servidor MariaDB

3. **Iniciar la aplicación**
   - Ejecutar desde el escritorio
   - La primera cuenta creada será la cuenta de administración

## 📊 Estado de los Módulos

| Módulo | Estado | Estabilidad | Notas |
|--------|---------|-------------|-------|
| **Contactos** | ✅ Activo | Alta | Base centralizada operativa |
| **Finanzas** | ✅ Activo | Alta | Control financiero completo |
| **Inventario** | ✅ Activo | Alta | Gestión de stock estable |
| **Taller** | ✅ Funcional | Media | UI necesita optimización |
| **Compraventa** | ✅ Activo | Alta | Procesos comerciales estables |
| **Seguridad** | ✅ Activo | Alta | Sistema de permisos robusto |

## 🎯 Casos de Uso Principales

### Para Empresas Manufactureras
- Control completo del ciclo de producción
- Gestión de materias primas y productos terminados
- Costeo preciso de manufactura
- Integración venta-producción-inventario

### Para Comercios
- Gestión de inventario multi-almacén
- Control de ventas y compras
- Administración de clientes y proveedores
- Reportes financieros básicos

## 🚀 Para Desarrolladores

### Requisitos de Desarrollo
- **Visual Studio**: 2019 o superior
- **.NET**: 8+
- **Base de Datos**: MariaDB/MySQL
- **Control de Versiones**: Git

### Compilar desde Código Fuente

1. **Clonar repositorio**
   ```bash
   git clone https://github.com/tuusuario/advanceerp.git
   cd advanceerp

### 🤝 Contribuir al Proyecto

## Áreas Prioritarias de Mejora
- 🎨 Rediseño UI Módulo Taller - Simplificar interfaz
- 🐛 Testing y Depuración - Reportar issues
- 📊 Reportes Avanzados - Mejorar análisis de datos
- 🔌 Integraciones - APIs y conectores

## Cómo Contribuir
1. Fork del proyecto
2. Crear rama para feature (git checkout -b feature/mejora-ui)
3. Commit de cambios (git commit -m 'feat: mejorar interfaz')
4. Push y Pull Request

### 📄 Licencia GPL v3

Este software se distribuye bajo GNU General Public License v3. Eres libre de:

- Usar, estudiar y modificar el código
- Distribuir copias
- Contribuir mejoras a la comunidad



<div align="center">
¿Listo para probar aDVance ERP? Descargar última versión

*Desarrollado en C# WinForms - 6 módulos integrados para gestión completa*

⭐ Dános una estrella en GitHub

¡Contribuciones bienvenidas! Especialmente en diseño de UI y testing
</div>
