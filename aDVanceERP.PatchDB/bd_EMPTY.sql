-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 18, 2025 at 02:58 AM
-- Server version: 10.4.6-MariaDB
-- PHP Version: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `advanceerp`
--

-- --------------------------------------------------------

--
-- Table structure for table `adv__almacen`
--

CREATE TABLE `adv__almacen` (
  `id_almacen` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE latin1_general_ci NOT NULL,
  `direccion` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `autorizo_venta` tinyint(1) NOT NULL DEFAULT 0,
  `notas` text COLLATE latin1_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__caja`
--

CREATE TABLE `adv__caja` (
  `id_caja` int(11) NOT NULL,
  `estado` enum('Inactiva','Abierta','Cerrada') COLLATE latin1_general_ci NOT NULL DEFAULT 'Abierta',
  `fecha_apertura` datetime DEFAULT NULL,
  `saldo_inicial` decimal(10,2) NOT NULL DEFAULT 0.00,
  `saldo_actual` decimal(10,2) NOT NULL DEFAULT 0.00,
  `fecha_cierre` datetime DEFAULT NULL,
  `id_cuenta_usuario` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__cliente`
--

CREATE TABLE `adv__cliente` (
  `id_cliente` int(11) NOT NULL,
  `numero` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `razon_social` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `id_contacto` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__compra`
--

CREATE TABLE `adv__compra` (
  `id_compra` int(11) NOT NULL,
  `fecha` datetime NOT NULL,
  `id_almacen` int(11) NOT NULL,
  `id_proveedor` int(11) NOT NULL,
  `total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__contacto`
--

CREATE TABLE `adv__contacto` (
  `id_contacto` int(11) NOT NULL,
  `nombre` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `direccion_correo_electronico` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci DEFAULT NULL,
  `direccion` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci DEFAULT NULL,
  `notas` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__cuenta_bancaria`
--

CREATE TABLE `adv__cuenta_bancaria` (
  `id_cuenta_bancaria` int(11) NOT NULL,
  `alias` varchar(100) COLLATE latin1_general_ci NOT NULL,
  `numero_tarjeta` varchar(20) COLLATE latin1_general_ci NOT NULL,
  `moneda` int(11) NOT NULL,
  `id_contacto` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__cuenta_usuario`
--

CREATE TABLE `adv__cuenta_usuario` (
  `id_cuenta_usuario` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE latin1_general_ci NOT NULL,
  `password_hash` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `password_salt` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `id_rol_usuario` int(11) NOT NULL,
  `administrador` tinyint(1) NOT NULL DEFAULT 0,
  `aprobado` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__db_version`
--

CREATE TABLE `adv__db_version` (
  `version` varchar(20) COLLATE latin1_general_ci NOT NULL,
  `applied_date` datetime NOT NULL,
  `patch_name` varchar(100) COLLATE latin1_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__detalle_compra_producto`
--

CREATE TABLE `adv__detalle_compra_producto` (
  `id_detalle_compra_producto` int(11) NOT NULL,
  `id_compra` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad` decimal(10,2) NOT NULL,
  `precio_compra` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__detalle_pago_transferencia`
--

CREATE TABLE `adv__detalle_pago_transferencia` (
  `id_detalle_pago_transferencia` int(11) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `id_tarjeta` int(11) NOT NULL,
  `numero_confirmacion` varchar(100) COLLATE latin1_general_ci NOT NULL,
  `numero_transaccion` varchar(100) COLLATE latin1_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__detalle_producto`
--

CREATE TABLE `adv__detalle_producto` (
  `id_detalle_producto` int(11) NOT NULL,
  `id_unidad_medida` int(11) DEFAULT 0,
  `descripcion` text COLLATE latin1_general_ci DEFAULT NULL,
  `activo` tinyint(4) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__detalle_venta_producto`
--

CREATE TABLE `adv__detalle_venta_producto` (
  `id_detalle_venta_producto` bigint(20) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `precio_compra_vigente` decimal(10,2) NOT NULL,
  `precio_venta_final` decimal(10,2) NOT NULL,
  `cantidad` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__empresa`
--

CREATE TABLE `adv__empresa` (
  `id_empresa` int(11) NOT NULL,
  `nombre` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `logotipo` longblob DEFAULT NULL,
  `id_contacto` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__inventario`
--

CREATE TABLE `adv__inventario` (
  `id_inventario` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `id_almacen` int(11) NOT NULL,
  `cantidad` decimal(10,2) NOT NULL,
  `costo_promedio` decimal(12,4) NOT NULL DEFAULT 0.0000,
  `valor_total` decimal(12,4) NOT NULL DEFAULT 0.0000,
  `ultima_actualizacion` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__mensajero`
--

CREATE TABLE `adv__mensajero` (
  `id_mensajero` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE latin1_general_ci NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `id_contacto` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__modulo`
--

CREATE TABLE `adv__modulo` (
  `id_modulo` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE latin1_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__movimiento`
--

CREATE TABLE `adv__movimiento` (
  `id_movimiento` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `costo_unitario` decimal(10,2) NOT NULL COMMENT 'Costo unitario del producto, para valorización de inventario',
  `costo_total` decimal(12,4) GENERATED ALWAYS AS (`cantidad_movida` * `costo_unitario`) STORED,
  `id_almacen_origen` int(11) NOT NULL,
  `id_almacen_destino` int(11) NOT NULL,
  `fecha_creacion` datetime NOT NULL DEFAULT current_timestamp(),
  `estado` enum('Pendiente','Completado','Cancelado','') COLLATE latin1_general_ci NOT NULL COMMENT 'Estado del movimiento',
  `saldo_inicial` decimal(10,2) NOT NULL COMMENT 'Saldo inicial antes de realizar el movimiento',
  `fecha` datetime NOT NULL,
  `cantidad_movida` decimal(10,2) NOT NULL,
  `saldo_final` decimal(10,2) NOT NULL COMMENT 'Saldo final luego de realizado el movimiento',
  `id_tipo_movimiento` int(11) NOT NULL DEFAULT 0,
  `id_cuenta_usuario` int(11) NOT NULL COMMENT 'Cuenta de usuario que realiza el movimiento'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__movimiento_caja`
--

CREATE TABLE `adv__movimiento_caja` (
  `id_movimiento_caja` int(11) NOT NULL,
  `id_caja` int(11) NOT NULL,
  `fecha` datetime DEFAULT NULL,
  `monto` decimal(10,2) NOT NULL DEFAULT 0.00,
  `tipo` enum('Ingreso','Egreso') COLLATE latin1_general_ci NOT NULL DEFAULT 'Ingreso',
  `concepto` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `id_pago` int(11) DEFAULT 0,
  `id_usuario` int(11) DEFAULT 0,
  `observaciones` text COLLATE latin1_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__orden_actividad`
--

CREATE TABLE `adv__orden_actividad` (
  `id_orden_actividad` int(11) NOT NULL,
  `id_orden_produccion` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `cantidad` decimal(10,2) NOT NULL,
  `costo` decimal(10,2) NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `adv__orden_gasto_dinamico`
--

CREATE TABLE `adv__orden_gasto_dinamico` (
  `id_orden_gasto_dinamico` int(11) NOT NULL,
  `id_orden_gasto_indirecto` int(11) NOT NULL,
  `formula` varchar(500) COLLATE latin1_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__orden_gasto_indirecto`
--

CREATE TABLE `adv__orden_gasto_indirecto` (
  `id_orden_gasto_indirecto` int(11) NOT NULL,
  `id_orden_produccion` int(11) NOT NULL,
  `concepto` varchar(100) NOT NULL,
  `cantidad` decimal(10,2) NOT NULL,
  `monto` decimal(10,2) NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `adv__orden_material`
--

CREATE TABLE `adv__orden_material` (
  `id_orden_material` int(11) NOT NULL,
  `id_orden_produccion` int(11) NOT NULL,
  `id_almacen` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL COMMENT 'Materia prima consumida',
  `cantidad` decimal(10,2) NOT NULL,
  `costo_unitario` decimal(10,2) NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `fecha_registro` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `adv__orden_produccion`
--

CREATE TABLE `adv__orden_produccion` (
  `id_orden_produccion` int(11) NOT NULL,
  `numero_orden` varchar(20) NOT NULL,
  `fecha_apertura` datetime NOT NULL DEFAULT current_timestamp(),
  `fecha_cierre` datetime DEFAULT NULL,
  `id_almacen` int(11) NOT NULL,
  `nombre_producto` varchar(150) NOT NULL COMMENT 'Producto terminado',
  `cantidad` decimal(10,2) NOT NULL,
  `estado` enum('Abierta','En Proceso','Cerrada','Cancelada') NOT NULL DEFAULT 'Abierta',
  `observaciones` text DEFAULT NULL,
  `costo_total` decimal(10,2) NOT NULL DEFAULT 0.00,
  `precio_unitario` decimal(10,2) NOT NULL DEFAULT 0.00,
  `margen_ganancia` decimal(5,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `adv__pago`
--

CREATE TABLE `adv__pago` (
  `id_pago` int(11) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `metodo_pago` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `monto` decimal(10,2) NOT NULL,
  `fecha_confirmacion` datetime DEFAULT NULL,
  `estado` varchar(20) COLLATE latin1_general_ci NOT NULL DEFAULT 'Completado'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__permiso`
--

CREATE TABLE `adv__permiso` (
  `id_permiso` int(11) NOT NULL,
  `id_modulo` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE latin1_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__producto`
--

CREATE TABLE `adv__producto` (
  `id_producto` int(11) NOT NULL,
  `categoria` enum('Mercancia','ProductoTerminado','MateriaPrima') COLLATE latin1_general_ci NOT NULL DEFAULT 'Mercancia',
  `codigo` varchar(100) COLLATE latin1_general_ci NOT NULL,
  `nombre` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `id_detalle_producto` int(11) DEFAULT NULL,
  `id_proveedor` int(11) DEFAULT NULL,
  `id_tipo_materia_prima` int(11) NOT NULL DEFAULT 1,
  `es_vendible` tinyint(1) NOT NULL DEFAULT 1,
  `precio_compra` decimal(10,2) NOT NULL,
  `costo_produccion_unitario` decimal(10,2) NOT NULL,
  `precio_venta_base` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__producto_mano_obra`
--

CREATE TABLE `adv__producto_mano_obra` (
  `id_producto_mano_obra` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `id_actividad_produccion` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__producto_materia_prima`
--

CREATE TABLE `adv__producto_materia_prima` (
  `id_producto_materia_prima` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `id_materia_prima` int(11) NOT NULL,
  `cantidad` decimal(10,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__proveedor`
--

CREATE TABLE `adv__proveedor` (
  `id_proveedor` int(11) NOT NULL,
  `razon_social` varchar(200) NOT NULL,
  `nit` varchar(50) NOT NULL,
  `id_contacto` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `adv__rol_permiso`
--

CREATE TABLE `adv__rol_permiso` (
  `id_rol_permiso` int(11) NOT NULL,
  `id_rol_usuario` int(11) NOT NULL,
  `id_permiso` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__rol_usuario`
--

CREATE TABLE `adv__rol_usuario` (
  `id_rol_usuario` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE latin1_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__seguimiento_entrega`
--

CREATE TABLE `adv__seguimiento_entrega` (
  `id_seguimiento_entrega` int(11) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `id_mensajero` int(11) DEFAULT NULL,
  `fecha_asignacion` datetime DEFAULT NULL,
  `fecha_entrega` datetime DEFAULT NULL,
  `fecha_pago` datetime DEFAULT NULL,
  `observaciones` text COLLATE latin1_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__sesion_usuario`
--

CREATE TABLE `adv__sesion_usuario` (
  `id_sesion_usuario` int(11) NOT NULL,
  `id_cuenta_usuario` int(11) NOT NULL,
  `token` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__telefono_contacto`
--

CREATE TABLE `adv__telefono_contacto` (
  `id_telefono_contacto` int(11) NOT NULL,
  `prefijo` varchar(8) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `numero` varchar(16) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `categoria` enum('Movil','Fijo','Otro','') CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `id_contacto` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__tipo_entrega`
--

CREATE TABLE `adv__tipo_entrega` (
  `id_tipo_entrega` int(11) NOT NULL,
  `nombre` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `descripcion` text COLLATE latin1_general_ci DEFAULT NULL,
  `requiere_pago_previo` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__tipo_materia_prima`
--

CREATE TABLE `adv__tipo_materia_prima` (
  `id_tipo_materia_prima` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` text COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__tipo_movimiento`
--

CREATE TABLE `adv__tipo_movimiento` (
  `id_tipo_movimiento` int(11) NOT NULL,
  `nombre` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `efecto` enum('Carga','Descarga','Transferencia') COLLATE latin1_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__unidad_medida`
--

CREATE TABLE `adv__unidad_medida` (
  `id_unidad_medida` int(11) NOT NULL,
  `nombre` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `abreviatura` varchar(10) COLLATE latin1_general_ci NOT NULL,
  `descripcion` text COLLATE latin1_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `adv__venta`
--

CREATE TABLE `adv__venta` (
  `id_venta` int(11) NOT NULL,
  `fecha` datetime NOT NULL,
  `id_almacen` int(11) NOT NULL,
  `id_cliente` int(11) DEFAULT NULL,
  `id_tipo_entrega` int(11) NOT NULL DEFAULT 1,
  `direccion_entrega` varchar(255) COLLATE latin1_general_ci DEFAULT NULL,
  `estado_entrega` varchar(50) COLLATE latin1_general_ci DEFAULT 'Pendiente',
  `total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `adv__almacen`
--
ALTER TABLE `adv__almacen`
  ADD PRIMARY KEY (`id_almacen`);

--
-- Indexes for table `adv__caja`
--
ALTER TABLE `adv__caja`
  ADD PRIMARY KEY (`id_caja`);

--
-- Indexes for table `adv__cliente`
--
ALTER TABLE `adv__cliente`
  ADD PRIMARY KEY (`id_cliente`);

--
-- Indexes for table `adv__compra`
--
ALTER TABLE `adv__compra`
  ADD PRIMARY KEY (`id_compra`);

--
-- Indexes for table `adv__contacto`
--
ALTER TABLE `adv__contacto`
  ADD PRIMARY KEY (`id_contacto`);

--
-- Indexes for table `adv__cuenta_bancaria`
--
ALTER TABLE `adv__cuenta_bancaria`
  ADD PRIMARY KEY (`id_cuenta_bancaria`);

--
-- Indexes for table `adv__cuenta_usuario`
--
ALTER TABLE `adv__cuenta_usuario`
  ADD PRIMARY KEY (`id_cuenta_usuario`);

--
-- Indexes for table `adv__db_version`
--
ALTER TABLE `adv__db_version`
  ADD PRIMARY KEY (`version`);

--
-- Indexes for table `adv__detalle_compra_producto`
--
ALTER TABLE `adv__detalle_compra_producto`
  ADD PRIMARY KEY (`id_detalle_compra_producto`);

--
-- Indexes for table `adv__detalle_pago_transferencia`
--
ALTER TABLE `adv__detalle_pago_transferencia`
  ADD PRIMARY KEY (`id_detalle_pago_transferencia`);

--
-- Indexes for table `adv__detalle_producto`
--
ALTER TABLE `adv__detalle_producto`
  ADD PRIMARY KEY (`id_detalle_producto`);

--
-- Indexes for table `adv__detalle_venta_producto`
--
ALTER TABLE `adv__detalle_venta_producto`
  ADD PRIMARY KEY (`id_detalle_venta_producto`);

--
-- Indexes for table `adv__empresa`
--
ALTER TABLE `adv__empresa`
  ADD PRIMARY KEY (`id_empresa`);

--
-- Indexes for table `adv__inventario`
--
ALTER TABLE `adv__inventario`
  ADD PRIMARY KEY (`id_inventario`);

--
-- Indexes for table `adv__mensajero`
--
ALTER TABLE `adv__mensajero`
  ADD PRIMARY KEY (`id_mensajero`);

--
-- Indexes for table `adv__modulo`
--
ALTER TABLE `adv__modulo`
  ADD PRIMARY KEY (`id_modulo`);

--
-- Indexes for table `adv__movimiento`
--
ALTER TABLE `adv__movimiento`
  ADD PRIMARY KEY (`id_movimiento`);

--
-- Indexes for table `adv__movimiento_caja`
--
ALTER TABLE `adv__movimiento_caja`
  ADD PRIMARY KEY (`id_movimiento_caja`);

--
-- Indexes for table `adv__orden_actividad`
--
ALTER TABLE `adv__orden_actividad`
  ADD PRIMARY KEY (`id_orden_actividad`);

--
-- Indexes for table `adv__orden_gasto_dinamico`
--
ALTER TABLE `adv__orden_gasto_dinamico`
  ADD PRIMARY KEY (`id_orden_gasto_dinamico`);

--
-- Indexes for table `adv__orden_gasto_indirecto`
--
ALTER TABLE `adv__orden_gasto_indirecto`
  ADD PRIMARY KEY (`id_orden_gasto_indirecto`);

--
-- Indexes for table `adv__orden_material`
--
ALTER TABLE `adv__orden_material`
  ADD PRIMARY KEY (`id_orden_material`);

--
-- Indexes for table `adv__orden_produccion`
--
ALTER TABLE `adv__orden_produccion`
  ADD PRIMARY KEY (`id_orden_produccion`),
  ADD UNIQUE KEY `numero_orden` (`numero_orden`);

--
-- Indexes for table `adv__pago`
--
ALTER TABLE `adv__pago`
  ADD PRIMARY KEY (`id_pago`);

--
-- Indexes for table `adv__permiso`
--
ALTER TABLE `adv__permiso`
  ADD PRIMARY KEY (`id_permiso`);

--
-- Indexes for table `adv__producto`
--
ALTER TABLE `adv__producto`
  ADD PRIMARY KEY (`id_producto`),
  ADD UNIQUE KEY `nombre_unico` (`nombre`),
  ADD KEY `idx_categoria` (`categoria`),
  ADD KEY `idx_codigo` (`codigo`);

--
-- Indexes for table `adv__producto_mano_obra`
--
ALTER TABLE `adv__producto_mano_obra`
  ADD PRIMARY KEY (`id_producto_mano_obra`);

--
-- Indexes for table `adv__producto_materia_prima`
--
ALTER TABLE `adv__producto_materia_prima`
  ADD PRIMARY KEY (`id_producto_materia_prima`);

--
-- Indexes for table `adv__proveedor`
--
ALTER TABLE `adv__proveedor`
  ADD PRIMARY KEY (`id_proveedor`);

--
-- Indexes for table `adv__rol_permiso`
--
ALTER TABLE `adv__rol_permiso`
  ADD PRIMARY KEY (`id_rol_permiso`);

--
-- Indexes for table `adv__rol_usuario`
--
ALTER TABLE `adv__rol_usuario`
  ADD PRIMARY KEY (`id_rol_usuario`);

--
-- Indexes for table `adv__seguimiento_entrega`
--
ALTER TABLE `adv__seguimiento_entrega`
  ADD PRIMARY KEY (`id_seguimiento_entrega`);

--
-- Indexes for table `adv__sesion_usuario`
--
ALTER TABLE `adv__sesion_usuario`
  ADD PRIMARY KEY (`id_sesion_usuario`),
  ADD UNIQUE KEY `id_cuenta_usuario` (`id_cuenta_usuario`);

--
-- Indexes for table `adv__telefono_contacto`
--
ALTER TABLE `adv__telefono_contacto`
  ADD PRIMARY KEY (`id_telefono_contacto`);

--
-- Indexes for table `adv__tipo_entrega`
--
ALTER TABLE `adv__tipo_entrega`
  ADD PRIMARY KEY (`id_tipo_entrega`);

--
-- Indexes for table `adv__tipo_materia_prima`
--
ALTER TABLE `adv__tipo_materia_prima`
  ADD PRIMARY KEY (`id_tipo_materia_prima`);

--
-- Indexes for table `adv__tipo_movimiento`
--
ALTER TABLE `adv__tipo_movimiento`
  ADD PRIMARY KEY (`id_tipo_movimiento`);

--
-- Indexes for table `adv__unidad_medida`
--
ALTER TABLE `adv__unidad_medida`
  ADD PRIMARY KEY (`id_unidad_medida`);

--
-- Indexes for table `adv__venta`
--
ALTER TABLE `adv__venta`
  ADD PRIMARY KEY (`id_venta`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `adv__caja`
--
ALTER TABLE `adv__caja`
  MODIFY `id_caja` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__cliente`
--
ALTER TABLE `adv__cliente`
  MODIFY `id_cliente` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__compra`
--
ALTER TABLE `adv__compra`
  MODIFY `id_compra` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__contacto`
--
ALTER TABLE `adv__contacto`
  MODIFY `id_contacto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__cuenta_bancaria`
--
ALTER TABLE `adv__cuenta_bancaria`
  MODIFY `id_cuenta_bancaria` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__cuenta_usuario`
--
ALTER TABLE `adv__cuenta_usuario`
  MODIFY `id_cuenta_usuario` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__detalle_compra_producto`
--
ALTER TABLE `adv__detalle_compra_producto`
  MODIFY `id_detalle_compra_producto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__detalle_pago_transferencia`
--
ALTER TABLE `adv__detalle_pago_transferencia`
  MODIFY `id_detalle_pago_transferencia` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__detalle_producto`
--
ALTER TABLE `adv__detalle_producto`
  MODIFY `id_detalle_producto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__detalle_venta_producto`
--
ALTER TABLE `adv__detalle_venta_producto`
  MODIFY `id_detalle_venta_producto` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__empresa`
--
ALTER TABLE `adv__empresa`
  MODIFY `id_empresa` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__inventario`
--
ALTER TABLE `adv__inventario`
  MODIFY `id_inventario` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__mensajero`
--
ALTER TABLE `adv__mensajero`
  MODIFY `id_mensajero` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__modulo`
--
ALTER TABLE `adv__modulo`
  MODIFY `id_modulo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__movimiento`
--
ALTER TABLE `adv__movimiento`
  MODIFY `id_movimiento` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__movimiento_caja`
--
ALTER TABLE `adv__movimiento_caja`
  MODIFY `id_movimiento_caja` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__orden_actividad`
--
ALTER TABLE `adv__orden_actividad`
  MODIFY `id_orden_actividad` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__orden_gasto_dinamico`
--
ALTER TABLE `adv__orden_gasto_dinamico`
  MODIFY `id_orden_gasto_dinamico` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__orden_gasto_indirecto`
--
ALTER TABLE `adv__orden_gasto_indirecto`
  MODIFY `id_orden_gasto_indirecto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__orden_material`
--
ALTER TABLE `adv__orden_material`
  MODIFY `id_orden_material` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__orden_produccion`
--
ALTER TABLE `adv__orden_produccion`
  MODIFY `id_orden_produccion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__pago`
--
ALTER TABLE `adv__pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__permiso`
--
ALTER TABLE `adv__permiso`
  MODIFY `id_permiso` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__producto`
--
ALTER TABLE `adv__producto`
  MODIFY `id_producto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__producto_mano_obra`
--
ALTER TABLE `adv__producto_mano_obra`
  MODIFY `id_producto_mano_obra` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__producto_materia_prima`
--
ALTER TABLE `adv__producto_materia_prima`
  MODIFY `id_producto_materia_prima` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__proveedor`
--
ALTER TABLE `adv__proveedor`
  MODIFY `id_proveedor` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__rol_permiso`
--
ALTER TABLE `adv__rol_permiso`
  MODIFY `id_rol_permiso` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__rol_usuario`
--
ALTER TABLE `adv__rol_usuario`
  MODIFY `id_rol_usuario` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__seguimiento_entrega`
--
ALTER TABLE `adv__seguimiento_entrega`
  MODIFY `id_seguimiento_entrega` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__sesion_usuario`
--
ALTER TABLE `adv__sesion_usuario`
  MODIFY `id_sesion_usuario` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__telefono_contacto`
--
ALTER TABLE `adv__telefono_contacto`
  MODIFY `id_telefono_contacto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__tipo_entrega`
--
ALTER TABLE `adv__tipo_entrega`
  MODIFY `id_tipo_entrega` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__tipo_materia_prima`
--
ALTER TABLE `adv__tipo_materia_prima`
  MODIFY `id_tipo_materia_prima` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__tipo_movimiento`
--
ALTER TABLE `adv__tipo_movimiento`
  MODIFY `id_tipo_movimiento` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__unidad_medida`
--
ALTER TABLE `adv__unidad_medida`
  MODIFY `id_unidad_medida` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `adv__venta`
--
ALTER TABLE `adv__venta`
  MODIFY `id_venta` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
