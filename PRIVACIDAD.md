# ESTATUTOS DE PRIVACIDAD - aDVance ERP

**Última actualización:** Abril 2025  
**Versión:** 1.0

## 1. INTRODUCCIÓN

En **aDVance ERP**, nos tomamos muy en serio la privacidad y seguridad de sus datos. Estos Estatutos de Privacidad describen cómo se recopilan, usan, almacenan y protegen los datos cuando utiliza nuestro software.

### 1.1 Alcance

Estos estatutos aplican a:
- Todas las versiones de aDVance ERP
- Todos los módulos y funcionalidades
- Todos los usuarios finales y administradores
- Datos procesados a través del software

### 1.2 Modelo de Procesamiento de Datos

**Importante:** aDVance ERP es un software de instalación local (on-premise). Esto significa que:

- ✅ **Sus datos permanecen en su infraestructura**
- ✅ **Los desarrolladores NO tienen acceso a sus datos**
- ✅ **Usted tiene control total sobre su información**
- ✅ **No hay transmisión de datos a servidores externos**

---

## 2. RESPONSABLE DEL TRATAMIENTO DE DATOS

### 2.1 Usted es el Responsable

Como usuario/empresa que instala y opera aDVance ERP, **USTED es el responsable del tratamiento de datos** según leyes aplicables como:

- **GDPR** (Reglamento General de Protección de Datos) - Unión Europea
- **LGPD** (Lei Geral de Proteção de Dados) - Brasil
- **CCPA** (California Consumer Privacy Act) - Estados Unidos
- **LOPDGDD** - España
- Leyes locales de protección de datos de su jurisdicción

### 2.2 Rol de los Desarrolladores

Los desarrolladores de aDVance ERP actúan únicamente como:
- **Proveedores de software**
- **Herramientas de procesamiento**
- **Sin acceso ni control sobre sus datos**

---

## 3. TIPOS DE DATOS PROCESADOS

### 3.1 Datos que USTED puede procesar

aDVance ERP le permite gestionar diversos tipos de datos empresariales:

#### Datos de Contactos
- Nombres de clientes y proveedores
- Información de contacto (teléfonos, emails, direcciones)
- Historial de transacciones comerciales
- Preferencias y notas comerciales

#### Datos Financieros
- Movimientos de caja y bancos
- Ingresos y egresos
- Métodos de pago
- Estados de cuenta

#### Datos de Inventario
- Descripción de productos y servicios
- Cantidades y valores de stock
- Ubicaciones de almacén
- Movimientos de mercancías

#### Datos de Producción
- Órdenes de fabricación
- Materiales utilizados
- Mano de obra aplicada
- Costos de producción

#### Datos de Usuarios del Sistema
- Nombres de usuario
- Credenciales de acceso (encriptadas)
- Roles y permisos
- Registro de actividades (logs)

### 3.2 Datos Sensibles

**Advertencia:** El software puede técnicamente almacenar datos sensibles si usted decide ingresarlos. Es su responsabilidad:

- Evaluar la necesidad de procesar datos sensibles
- Implementar salvaguardas adicionales
- Obtener consentimientos explícitos cuando sea requerido
- Cumplir con requisitos legales específicos

---

## 4. BASE LEGAL PARA EL PROCESAMIENTO

### 4.1 Determinada por Usted

Como responsable del tratamiento, **USTED debe determinar la base legal** para cada tipo de procesamiento:

- **Consentimiento** del titular de datos
- **Ejecución de contrato**
- **Obligación legal**
- **Interés legítimo**
- **Protección de intereses vitales**

### 4.2 Recomendaciones

Documente siempre:
- Qué datos procesa y por qué
- Base legal para cada procesamiento
- Tiempo de retención establecido
- Medidas de seguridad implementadas

---

## 5. ALMACENAMIENTO Y SEGURIDAD

### 5.1 Ubicación de Datos

Los datos se almacenan en:

- **Base de datos local:** MariaDB/MySQL en su infraestructura
- **Archivos de configuración:** En el sistema de archivos del servidor
- **Logs del sistema:** En directorios locales designados

### 5.2 Medidas de Seguridad Recomendadas

Como responsable, usted debería implementar:

#### Seguridad Física
- ✅ Servidores en ubicaciones seguras
- ✅ Control de acceso físico
- ✅ Protección contra desastres naturales

#### Seguridad Técnica
- ✅ Firewalls y segmentación de red
- ✅ Antivirus y antimalware actualizados
- ✅ Copias de seguridad automáticas y regulares
- ✅ Encriptación de discos (BitLocker, LUKS, etc.)
- ✅ Contraseñas fuertes y políticas de renovación

#### Seguridad de Base de Datos
- ✅ Usuarios de BD con privilegios mínimos necesarios
- ✅ Contraseñas complejas para cuentas de BD
- ✅ Acceso restringido por IP/red
- ✅ Backups encriptados y fuera del sitio

#### Seguridad de Aplicación
- ✅ Gestión adecuada de credenciales de usuario
- ✅ Roles y permisos configurados correctamente
- ✅ Revisión periódica de accesos
- ✅ Monitoreo de actividades sospechosas

### 5.3 Encriptación

**Nota importante:** aDVance ERP actualmente:

- ✅ Encripta contraseñas de usuarios (hash)
- ⚠️ **NO encripta datos de la base de datos en reposo**
- ⚠️ **NO encripta comunicaciones de red automáticamente**

**Recomendación:** Implemente encriptación a nivel de:
- Disco completo del servidor
- Conexiones de base de datos (SSL/TLS)
- Red (VPN, TLS)

---

## 6. DERECHOS DE LOS TITULARES DE DATOS

### 6.1 Derechos que Usted Debe Respetar

Según GDPR y leyes similares, los titulares de datos tienen derecho a:

#### Derecho de Acceso
- Solicitar confirmación de si sus datos están siendo procesados
- Acceder a una copia de sus datos personales

#### Derecho de Rectificación
- Corregir datos inexactos o incompletos

#### Derecho de Supresión ("Derecho al Olvido")
- Solicitar eliminación de datos personales
- Cuando no haya obligación legal de conservarlos

#### Derecho a la Limitación del Procesamiento
- Solicitar restricción del procesamiento en ciertos casos

#### Derecho a la Portabilidad de Datos
- Recibir datos en formato estructurado y común
- Transferir datos a otro responsable

#### Derecho de Oposición
- Oponerse al procesamiento en ciertas circunstancias

### 6.2 Cómo Ejercer estos Derechos con aDVance ERP

El software proporciona herramientas para:

- **Acceso:** Exportar datos desde módulos correspondientes
- **Rectificación:** Editar registros directamente
- **Supresión:** Eliminar registros (con validaciones de integridad)
- **Portabilidad:** Exportar datos en formatos estándar (CSV, Excel)

**Su responsabilidad:** Establecer procesos internos para atender solicitudes de titulares de datos dentro de plazos legales (generalmente 30 días).

---

## 7. RETENCIÓN DE DATOS

### 7.1 Usted Define los Plazos

Como responsable, **USTED debe establecer políticas de retención** basadas en:

- Requisitos legales aplicables
- Necesidades comerciales legítimas
- Finalidad del procesamiento

### 7.2 Consideraciones Comunes

#### Datos Fiscales y Contables
- Generalmente 5-10 años según jurisdicción
- Requeridos por leyes tributarias

#### Datos de Clientes Activos
- Mientras dure la relación comercial
- Más período legal posterior

#### Datos de Ex-clientes
- Período limitado post-relación
- Sujetos a statutes of limitation

#### Logs del Sistema
- 6 meses a 2 años recomendado
- Para seguridad y auditoría

### 7.3 Eliminación Segura

Cuando elimine datos:

- Use funciones de eliminación del software
- Considere purgas de base de datos
- Asegure eliminación de backups antiguos
- Documente eliminaciones realizadas

---

## 8. TRANSFERENCIAS INTERNACIONALES

### 8.1 Escenario Típico

Dado que aDVance ERP es software local:

- ✅ **No hay transferencia automática de datos**
- ✅ **Los datos permanecen bajo su control**
- ✅ **Usted decide dónde alojar el servidor**

### 8.2 Si Usa Servicios en la Nube

Si decide alojar la base de datos en la nube:

- Verifique ubicación de los servidores
- Evalúe adecuación del país destino
- Considere cláusulas contractuales estándar
- Revise certificaciones del proveedor (ISO 27001, SOC 2, etc.)

---

## 9. COOKIES Y TECNOLOGÍAS SIMILARES

### 9.1 Uso Local

aDVance ERP es una aplicación de escritorio que:

- ❌ **NO usa cookies**
- ❌ **NO hace seguimiento web**
- ❌ **NO incluye publicidad**
- ❌ **NO conecta con servicios externos por defecto**

### 9.2 Almacenamiento Local

La aplicación puede usar:

- Archivos de configuración locales
- Caché temporal de la aplicación
- Logs de actividad locales

Todo esto permanece en su equipo/servidor.

---

## 10. MENORES DE EDAD

### 10.1 No Dirigido a Menores

aDVance ERP está diseñado para uso empresarial y profesional.

### 10.2 Su Responsabilidad

Si procesa datos de menores:

- Verifique edad mínima según ley local
- Obtenga consentimiento parental cuando sea requerido
- Implemente protecciones adicionales
- Justifique necesidad del procesamiento

---

## 11. CAMBIOS EN LA POLÍTICA DE PRIVACIDAD

### 11.1 Actualizaciones

Podemos actualizar estos Estatutos de Privacidad periodicamente. Los cambios se publicarán en:

- Repositorio oficial de GitHub
- Archivo PRIVACIDAD.md actualizado
- Notas de release de nuevas versiones

### 11.2 Su Responsabilidad

Como operador del software, usted debe:

- Revisar actualizaciones periódicamente
- Evaluar impacto en su cumplimiento
- Actualizar sus propias políticas si es necesario
- Informar a usuarios afectados cuando corresponda

---

## 12. VIOLACIONES DE SEGURIDAD

### 12.1 Notificación de Brechas

**Importante:** Como responsable del tratamiento, **USTED es responsable de:**

- Detectar violaciones de seguridad
- Investigar incidentes
- Notificar autoridades cuando sea requerido
- Informar a titulares de datos afectados
- Documentar incidentes y medidas tomadas

### 12.2 Plazos de Notificación

Según GDPR:
- **72 horas** para notificar autoridad supervisora
- **Sin demora indebida** para informar a titulares afectados

### 12.3 Reportar Bugs de Seguridad

Si descubre vulnerabilidades en aDVance ERP:

- **Email:** cdavisong@gmail.com
- **GitHub Issues:** (use opción de reporte de seguridad si disponible)
- Proporcione detalles suficientes para reproducir
- Permita tiempo razonable para corrección antes de divulgación pública

---

## 13. AUDITORÍA Y CUMPLIMIENTO

### 13.1 Herramientas Disponibles

aDVance ERP incluye características para ayudarle:

- **Logs de actividad:** Registro de acciones de usuarios
- **Gestión de roles:** Control de accesos granular
- **Exportación de datos:** Para revisiones y auditorías
- **Historial de cambios:** En registros clave

### 13.2 Auditorías Internas Recomendadas

Realice periódicamente:

- ✅ Revisión de usuarios activos y permisos
- ✅ Análisis de logs de seguridad
- ✅ Verificación de copias de seguridad
- ✅ Evaluación de medidas técnicas
- ✅ Revisión de bases legales de procesamiento

### 13.3 Documentación Requerida

Mantenga registro de:

- Actividades de procesamiento
- Bases legales aplicadas
- Medidas de seguridad implementadas
- Incidentes y respuestas
- Solicitudes de titulares y respuestas

---

## 14. CONTACTO Y CONSULTAS

### 14.1 Sobre el Software

Para preguntas sobre funcionalidades de privacidad en aDVance ERP:

- **Email:** cdavisong@gmail.com
- **GitHub:** https://github.com/cdavisong/aDVanceERP
- **Issues:** https://github.com/cdavisong/aDVanceERP/issues

### 14.2 Sobre Sus Datos

Para ejercer derechos sobre datos procesados CON aDVance ERP:

- **Contacte directamente a la empresa/usuario** que opera el sistema
- Ellos son los responsables del tratamiento
- Los desarrolladores del software no tienen acceso a sus datos

---

## 15. GLOSARIO

**Responsable del Tratamiento:** Persona física o jurídica que determina los fines y medios del procesamiento de datos (USTED como usuario de aDVance ERP).

**Encargado del Tratamiento:** Persona que procesa datos por cuenta del responsable (NO APLICA en este caso, los desarrolladores no son encargados).

**Titular de Datos:** Persona física cuyos datos personales son procesados (sus clientes, empleados, proveedores, etc.).

**Procesamiento:** Cualquier operación realizada con datos personales (recogida, almacenamiento, uso, etc.).

**Datos Personales:** Cualquier información relacionada con una persona física identificada o identificable.

**Datos Sensibles:** Categorías especiales de datos (origen racial, salud, creencias, etc.) que requieren protección adicional.

---

## 16. RESUMEN EJECUTIVO

### 🔑 Puntos Clave

1. **Sus datos son SUYOS** - Permanecen en su infraestructura
2. **USTED es el responsable** - Del cumplimiento de leyes de privacidad
3. **Nosotros somos proveedores** - Sin acceso a sus datos
4. **Implemente seguridad** - Copias de seguridad, encriptación, controles de acceso
5. **Respete derechos ARCO** - Acceso, rectificación, cancelación, oposición
6. **Documente todo** - Procesamientos, bases legales, medidas de seguridad

### ✅ Buenas Prácticas

- Realice copias de seguridad diarias
- Use contraseñas fuertes
- Actualice regularmente el software
- Capacite a usuarios en seguridad
- Revise permisos periódicamente
- Tenga plan de respuesta a incidentes

### ⚠️ Advertencias

- El software NO encripta datos en reposo por defecto
- Las contraseñas se guardan con hash (seguro)
- Usted debe cumplir leyes locales de privacidad
- Consulte profesionales legales para cumplimiento específico

---

## 17. MARCOS LEGALES DE REFERENCIA

Estos estatutos consideran:

- **Reglamento (UE) 2016/679 (GDPR)** - Unión Europea
- **California Consumer Privacy Act (CCPA)** - EE.UU.
- **Lei Geral de Proteção de Dados (LGPD)** - Brasil
- **Ley Orgánica 3/2018 (LOPDGDD)** - España
- Leyes locales de protección de datos

**Nota:** Estos estatutos son informativos y no constituyen asesoramiento legal. Consulte con profesionales legales para garantizar cumplimiento específico en su jurisdicción.

---

*Documento elaborado como guía de privacidad para usuarios de aDVance ERP. Última revisión: Abril 2025.*
