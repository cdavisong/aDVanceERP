-- ======================================================
-- Datos de Catálogo - aDVance ERP
-- Base de datos: advanceerp
-- ======================================================

USE `advanceerp`;

-- ======================================================
-- adv__tipo_movimiento
-- ======================================================
INSERT INTO `adv__tipo_movimiento` (`id_tipo_movimiento`, `nombre`, `efecto`) VALUES
(1, 'Compra', 'Carga'),
(2, 'Venta', 'Descarga'),
(3, 'Devolución de Venta', 'Carga'),
(4, 'Devolución a Proveedor', 'Descarga'),
(5, 'Ajuste de Inventario (+)', 'Carga'),
(6, 'Ajuste de Inventario (-)', 'Descarga'),
(7, 'Entrada de Producción', 'Carga'),
(8, 'Salida a Producción', 'Descarga'),
(9, 'Consumo Interno', 'Descarga'),
(10, 'Merma o Pérdida', 'Descarga'),
(11, 'Carga Inicial', 'Carga'),
(12, 'Traslado entre Almacenes', 'Transferencia');

-- ======================================================
-- adv__unidad_medida
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(1, 'Unidad', 'u', 'Unidad básica de conteo (Pieza, Artículo)'),
(2, 'Docena', 'doc', 'Conjunto de 12 unidades'),
(3, 'Paquete', 'pqt', 'Grupo de artículos empaquetados'),
(4, 'Caja', 'cj', 'Contenedor de artículos agrupados'),
(5, 'Bulto', 'blt', 'Empaque grande de productos sueltos'),
(6, 'Palet', 'plt', 'Plataforma para agrupar cajas o bultos'),
(7, 'Juego', 'jg', 'Conjunto de piezas que funcionan juntas'),
(8, 'Par', 'par', 'Conjunto de dos artículos iguales'),
(9, 'Kit', 'kt', 'Conjunto de productos complementarios'),
(10, 'Metro', 'm', 'Unidad básica de longitud para telas y materiales'),
(11, 'Centímetro', 'cm', 'Submúltiplo del metro, para medidas pequeñas'),
(12, 'Pulgada', 'in', 'Medida anglosajona frecuente en patrones y botones'),
(13, 'Yarda', 'yd', 'Medida anglosajona para telas (0.9144 metros)'),
(14, 'Kilogramo', 'kg', 'Unidad básica de masa para materias primas a granel'),
(15, 'Gramo', 'g', 'Submúltiplo del kilogramo, para pesos ligeros'),
(16, 'Onza', 'oz', 'Medida anglosajona para masa (28.35 gramos), usada en tejidos (oz/yd²)'),
(17, 'Litro', 'l', 'Unidad de volumen para líquidos'),
(18, 'Mililitro', 'ml', 'Submúltiplo del litro'),
(19, 'Onza líquida', 'fl oz', 'Medida anglosajona específica para volumen de líquidos (29.57 ml)'),
(20, 'Galón', 'gal', 'Para líquidos a granel (3.785 litros)'),
(21, 'Metro cuadrado', 'm²', 'Unidad de área para telas o superficies planas'),
(22, 'Pulgada cuadrada', 'in²', 'Unidad de área anglosajona'),
(23, 'Minuto', 'min', 'Unidad de tiempo'),
(24, 'Hora', 'h', 'Unidad de tiempo (60 minutos)'),
(25, 'Día', 'd', 'Unidad de tiempo (24 horas)');

-- ======================================================
-- adv__moneda
-- ======================================================
INSERT INTO `adv__moneda` (`id_moneda`, `codigo`, `nombre`, `simbolo`, `precision_decimal`, `es_base`, `activa`) VALUES
(1, 'CUP', 'Peso Cubano', '$', 2, 1, 1),
(2, 'USD', 'Dólar Estadounidense', 'US$', 2, 0, 1),
(3, 'EUR', 'Euro', '€', 2, 0, 1),
(4, 'CAD', 'Dólar Canadiense', 'C$', 2, 0, 1),
(5, 'RUB', 'Rublo Ruso', '₽', 2, 0, 1),
(6, 'CNY', 'Yuan Chino', '¥', 2, 0, 1);

-- ======================================================
-- adv__clasificacion_producto
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(1, 'General', 'Clasificación general para productos genéricos');

-- ======================================================
-- adv__rol
-- ======================================================
INSERT INTO `adv__rol` (`id_rol`, `nombre`, `descripcion`, `activo`) VALUES
(1, 'Administrador', 'Acceso total al sistema', 1),
(2, 'Vendedor', 'Solo punto de venta y consultas', 1),
(3, 'Almacenero', 'Gestión de inventario', 1),
(4, 'Cajero', 'Gestión de caja', 1),
(5, 'Supervisor', 'Reportes y supervisión', 1);

-- ======================================================
-- adv__permiso_rol
-- ======================================================
INSERT INTO `adv__permiso_rol` (`id_permiso_rol`, `id_rol`, `modulo`, `puede_ver`, `puede_crear`, `puede_editar`, `puede_eliminar`) VALUES
-- Administrador (id_rol=1) - Todos los permisos
(1, 1, 'MOD_SEGURIDAD', 1, 1, 1, 1),
(2, 1, 'MOD_EMPRESA', 1, 1, 1, 1),
(3, 1, 'MOD_INVENTARIO', 1, 1, 1, 1),
(4, 1, 'MOD_VENTA', 1, 1, 1, 1),
(5, 1, 'MOD_COMPRA', 1, 1, 1, 1),
(6, 1, 'MOD_FINANZAS', 1, 1, 1, 1),
(7, 1, 'MOD_RECURSOS_HUMANOS', 1, 1, 1, 1),
(8, 1, 'MOD_MOVIL', 1, 1, 1, 1),

-- Vendedor (id_rol=2)
(9, 2, 'MOD_SEGURIDAD', 1, 0, 0, 0),
(10, 2, 'MOD_EMPRESA', 1, 0, 1, 0),
(11, 2, 'MOD_INVENTARIO', 1, 1, 1, 0),
(12, 2, 'MOD_VENTA', 1, 1, 1, 1),
(13, 2, 'MOD_COMPRA', 1, 1, 1, 1),
(14, 2, 'MOD_FINANZAS', 1, 1, 1, 0),
(15, 2, 'MOD_RECURSOS_HUMANOS', 1, 1, 1, 0),
(16, 2, 'MOD_MOVIL', 1, 0, 0, 0),

-- Almacenero (id_rol=3)
(17, 3, 'MOD_SEGURIDAD', 1, 0, 0, 0),
(18, 3, 'MOD_EMPRESA', 1, 0, 0, 0),
(19, 3, 'MOD_INVENTARIO', 1, 0, 0, 0),
(20, 3, 'MOD_VENTA', 1, 0, 0, 0),
(21, 3, 'MOD_COMPRA', 1, 0, 0, 0),
(22, 3, 'MOD_FINANZAS', 1, 0, 0, 0),
(23, 3, 'MOD_RECURSOS_HUMANOS', 1, 0, 0, 0),
(24, 3, 'MOD_MOVIL', 1, 0, 0, 0),

-- Cajero (id_rol=4)
(25, 4, 'MOD_INVENTARIO', 1, 1, 1, 0),
(26, 4, 'MOD_COMPRA', 1, 1, 0, 0),
(27, 4, 'MOD_VENTA', 1, 0, 0, 0),
(28, 4, 'MOD_EMPRESA', 1, 0, 0, 0),

-- Supervisor (id_rol=5)
(29, 5, 'MOD_VENTA', 1, 1, 0, 0),
(30, 5, 'MOD_INVENTARIO', 1, 0, 0, 0),
(31, 5, 'MOD_EMPRESA', 1, 0, 0, 0);

-- ======================================================
-- adv__version_esquema
-- ======================================================
INSERT INTO `adv__version_esquema` (`id_version`, `version`, `descripcion`, `fecha_aplicada`) VALUES
(1, '1.0.0', 'Esquema inicial', NOW());