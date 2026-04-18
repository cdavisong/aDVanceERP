-- ======================================================
-- Datos de Clasificaciones de Productos por Tipo de Negocio
-- Base de datos: advanceerp
-- ======================================================

USE `advanceerp`;

-- ======================================================
-- LIMPIAR clasificaciones existentes (opcional - comentar si no se desea)
-- ======================================================
-- TRUNCATE TABLE adv__clasificacion_producto;

-- ======================================================
-- 1. CLASIFICACIONES GENERALES (para cualquier negocio)
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(1, 'General', 'Clasificación general para productos genéricos'),
(2, 'Materia Prima', 'Insumos y materiales para producción'),
(3, 'Producto Terminado', 'Productos listos para la venta'),
(4, 'Producto en Proceso', 'Productos en etapa de producción'),
(5, 'Mercancía para Venta', 'Productos para reventa (comercio)'),
(6, 'Activo Fijo', 'Bienes duraderos de la empresa'),
(7, 'Insumo', 'Materiales de consumo operativo'),
(8, 'Muestra', 'Productos de muestra sin costo'),
(9, 'Obsequio Promocional', 'Productos para promociones'),
(10, 'Repuesto', 'Piezas y componentes de repuesto');

-- ======================================================
-- 2. COMERCIO MINORISTA (Tiendas, Supermercados, Retail)
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(11, 'Abarrotes', 'Productos básicos de despensa'),
(12, 'Bebidas', 'Bebidas alcohólicas y no alcohólicas'),
(13, 'Lácteos y Refrigerados', 'Productos que requieren refrigeración'),
(14, 'Carnes y Embutidos', 'Productos cárnicos'),
(15, 'Frutas y Verduras', 'Productos frescos'),
(16, 'Panadería y Pastelería', 'Productos de panadería'),
(17, 'Limpieza del Hogar', 'Productos de limpieza'),
(18, 'Cuidado Personal', 'Productos de higiene y belleza'),
(19, 'Ferretería', 'Herramientas y artículos de ferretería'),
(20, 'Electrodomésticos', 'Electrodomésticos y electrónicos'),
(21, 'Ropa y Calzado', 'Textil y vestimenta'),
(22, 'Juguetería', 'Juguetes y juegos'),
(23, 'Papelería', 'Útiles escolares y de oficina'),
(24, 'Mascotas', 'Alimentos y accesorios para mascotas'),
(25, 'Farmacia', 'Medicamentos y productos farmacéuticos');

-- ======================================================
-- 3. RESTAURANTES Y GASTRONOMÍA
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(26, 'Carnes', 'Res, cerdo, pollo, pescados, mariscos'),
(27, 'Verduras', 'Vegetales frescos y procesados'),
(28, 'Frutas', 'Frutas frescas y procesadas'),
(29, 'Granos y Cereales', 'Arroz, frijoles, lentejas, quinoa'),
(30, 'Especias y Condimentos', 'Sales, pimientas, hierbas, salsas'),
(31, 'Aceites y Grasas', 'Aceite vegetal, oliva, mantequilla'),
(32, 'Lácteos', 'Leche, queso, yogurt, crema'),
(33, 'Huevos', 'Huevos frescos y procesados'),
(34, 'Harinas y Panadería', 'Harinas, panes, masas'),
(35, 'Bebidas', 'Gaseosas, jugos, vinos, cervezas'),
(36, 'Utensilios de Cocina', 'Vajilla, cubiertos, equipamiento'),
(37, 'Empaques', 'Envases, bolsas, recipientes'),
(38, 'Productos de Limpieza', 'Detergentes, desinfectantes'),
(39, 'Menaje', 'Mantelería, servilletas, decoración');

-- ======================================================
-- 4. FARMACIA Y SALUD
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(40, 'Medicamentos Controlados', 'Requieren receta médica especial'),
(41, 'Medicamentos de Venta Libre', 'Medicamentos sin receta'),
(42, 'Genéricos', 'Medicamentos genéricos'),
(43, 'Suplementos Nutricionales', 'Vitaminas, minerales, suplementos'),
(44, 'Cuidado de la Piel', 'Cremas, protectores solares'),
(45, 'Cuidado Capilar', 'Shampoos, acondicionadores'),
(46, 'Higiene Bucal', 'Pastas dentales, enjuagues'),
(47, 'Primeros Auxilios', 'Vendas, gasas, antisépticos'),
(48, 'Productos para Bebés', 'Pañales, leche, toallitas'),
(49, 'Ortopedia', 'Férulas, muletas, soportes'),
(50, 'Equipo Médico', 'Tensiometros, glucómetros, termómetros');

-- ======================================================
-- 5. CONSTRUCCIÓN Y FERRETERÍA
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(51, 'Materiales Básicos', 'Cemento, arena, grava, ladrillos'),
(52, 'Acero y Hierro', 'Varillas, perfiles, láminas'),
(53, 'Madera', 'Tablas, listones, triplay'),
(54, 'Pinturas y Acabados', 'Pinturas, selladores, barnices'),
(55, 'Eléctricos', 'Cables, interruptores, lámparas'),
(56, 'Plomería', 'Tuberías, llaves, sanitarios'),
(57, 'Herramientas Manuales', 'Martillos, destornilladores, llaves'),
(58, 'Herramientas Eléctricas', 'Taladros, sierras, esmeriles'),
(59, 'Seguridad Industrial', 'Cascos, guantes, arneses'),
(60, 'Adhesivos y Selladores', 'Pegamentos, siliconas, cintas'),
(61, 'Ferretería Menor', 'Clavos, tornillos, bisagras');

-- ======================================================
-- 6. TEXTIL Y CONFECCIÓN
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(62, 'Telas', 'Telas por metro para confección'),
(63, 'Hilos', 'Hilos de coser de diferentes colores'),
(64, 'Accesorios de Moda', 'Botones, cierres, elásticos'),
(65, 'Ropa de Dama', 'Prendas femeninas'),
(66, 'Ropa de Caballero', 'Prendas masculinas'),
(67, 'Ropa Infantil', 'Prendas para niños'),
(68, 'Ropa Interior', 'Ropa íntima y calcetines'),
(69, 'Calzado', 'Zapatos, zapatillas, sandalias'),
(70, 'Accesorios', 'Cinturones, carteras, sombreros'),
(71, 'Lencería', 'Lencería fina'),
(72, 'Deportivo', 'Ropa y calzado deportivo');

-- ======================================================
-- 7. ELECTRÓNICA Y TECNOLOGÍA
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(73, 'Computadoras', 'Laptops, desktops, tablets'),
(74, 'Componentes PC', 'Procesadores, memorias, discos duros'),
(75, 'Periféricos', 'Teclados, mouse, monitores'),
(76, 'Telefonía', 'Celulares, teléfonos fijos'),
(77, 'Audio y Video', 'Parlantes, audífonos, TVs'),
(78, 'Electrodomésticos', 'Lavadoras, neveras, microondas'),
(79, 'Accesorios Móviles', 'Fundas, cargadores, protectores'),
(80, 'Redes', 'Routers, switches, cables'),
(81, 'Software', 'Programas y licencias'),
(82, 'Consolas y Videojuegos', 'Consolas, juegos, accesorios'),
(83, 'Fotografía', 'Cámaras, lentes, trípodes');

-- ======================================================
-- 8. AUTOMOTRIZ
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(84, 'Repuestos Motor', 'Motores, pistones, válvulas'),
(85, 'Sistema Eléctrico', 'Baterías, alternadores, fusibles'),
(86, 'Suspensión y Dirección', 'Amortiguadores, rótulas, cremalleras'),
(87, 'Frenos', 'Pastillas, discos, bombas'),
(88, 'Lubricantes', 'Aceites, grasas, aditivos'),
(89, 'Neumáticos', 'Llantas, cámaras, aros'),
(90, 'Accesorios', 'Luces, alfombras, adornos'),
(91, 'Herramientas Automotrices', 'Gatos, llaves, scanners'),
(92, 'Pintura Automotriz', 'Pinturas, masillas, lacas');

-- ======================================================
-- 9. AGRICULTURA Y GANADERÍA
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(93, 'Semillas', 'Semillas de cultivos y pastos'),
(94, 'Fertilizantes', 'Abonos orgánicos y químicos'),
(95, 'Plaguicidas', 'Insecticidas, fungicidas, herbicidas'),
(96, 'Alimento Animal', 'Concentrados, forrajes, suplementos'),
(97, 'Veterinaria', 'Medicamentos y vacunas para animales'),
(98, 'Equipo Agrícola', 'Tractores, arados, cosechadoras'),
(99, 'Riego', 'Mangueras, aspersores, sistemas'),
(100, 'Herramientas Agrícolas', 'Machetes, palas, rastrillos'),
(101, 'Corrales y Cercas', 'Alambre, postes, mallas');

-- ======================================================
-- 10. OFICINA Y PAPELERÍA
-- ======================================================
INSERT INTO `adv__clasificacion_producto` (`id_clasificacion_producto`, `nombre`, `descripcion`) VALUES
(102, 'Papelería Básica', 'Hojas, cuadernos, carpetas'),
(103, 'Escritura', 'Lápices, bolígrafos, marcadores'),
(104, 'Organización', 'Archivadores, folders, etiquetas'),
(105, 'Mobiliario', 'Escritorios, sillas, estanterías'),
(106, 'Equipo de Oficina', 'Impresoras, escáneres, faxes'),
(107, 'Insumos de Impresión', 'Tóners, tintas, papel especial'),
(108, 'Arte y Manualidades', 'Pinturas, pinceles, cartulinas'),
(109, 'Empaque y Envío', 'Sobres, cajas, cintas adhesivas');

-- ======================================================
-- ACTUALIZAR AUTO_INCREMENT
-- ======================================================
ALTER TABLE `adv__clasificacion_producto` AUTO_INCREMENT = 110;