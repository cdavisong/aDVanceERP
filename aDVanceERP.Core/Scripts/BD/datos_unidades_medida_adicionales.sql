-- ======================================================
-- Datos de Unidades de Medida Adicionales por Tipo de Negocio
-- Base de datos: advanceerp
-- ======================================================

USE `advanceerp`;

-- ======================================================
-- LIMPIAR unidades existentes (opcional - comentar si no se desea)
-- ======================================================
-- TRUNCATE TABLE adv__unidad_medida;

-- ======================================================
-- UNIDADES EXISTENTES (se mantienen, solo se agregan nuevas)
-- Nota: Si se truncó la tabla, ejecutar primero 02_datos_catalogo.sql
-- ======================================================

-- ======================================================
-- 1. UNIDADES PARA COMERCIO MINORISTA
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(26, 'Tira', 'tira', 'Conjunto de productos en tira (ej: 6 unidades)'),
(27, 'Blíster', 'blis', 'Empaque blíster de medicamentos o pequeños'),
(28, 'Frasco', 'fco', 'Recipiente para líquidos o sólidos'),
(29, 'Tubo', 'tub', 'Formato tubular (ej: pasta dental)'),
(30, 'Atomizador', 'atm', 'Envase con atomizador'),
(31, 'Sobre', 'sobre', 'Empaque pequeño de papel o plástico'),
(32, 'Lata', 'lata', 'Envase metálico'),
(33, 'Botella', 'bot', 'Envase de vidrio o plástico'),
(34, 'Tarjeta', 'tarj', 'Presentación tipo tarjeta (ej: tarjeta SIM)'),
(35, 'Display', 'disp', 'Exhibidor de productos');

-- ======================================================
-- 2. UNIDADES PARA RESTAURANTES Y COCINA
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(36, 'Ración', 'rac', 'Porción individual para servir'),
(37, 'Porción', 'por', 'Cantidad estándar para un comensal'),
(38, 'Cucharada', 'cda', 'Medida culinaria (~15ml)'),
(39, 'Cucharadita', 'cdta', 'Medida culinaria (~5ml)'),
(40, 'Pizca', 'piz', 'Cantidad muy pequeña'),
(41, 'Atado', 'atado', 'Verduras o hierbas atadas'),
(42, 'Manojo', 'man', 'Cantidad que cabe en la mano'),
(43, 'Pieza', 'pza', 'Unidad individual (frutas, verduras)'),
(44, 'Cabeza', 'cab', 'Para lechugas, ajos, coliflores'),
(45, 'Diente', 'dte', 'Para ajos'),
(46, 'Rama', 'rama', 'Para hierbas y vegetales'),
(47, 'Hoja', 'hoja', 'Para hierbas y vegetales de hoja'),
(48, 'Barra', 'bar', 'Formato de barra (mantequilla, pan)'),
(49, 'Rebanada', 'reb', 'Porción cortada (pan, queso, jamón)'),
(50, 'Litro', 'L', 'Unidad de volumen para líquidos'),
(51, 'Centilitro', 'cL', 'Submúltiplo del litro');

-- ======================================================
-- 3. UNIDADES PARA FARMACIA
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(52, 'Comprimido', 'comp', 'Tableta o pastilla'),
(53, 'Cápsula', 'cáps', 'Cápsula gelatinosa'),
(54, 'Ampolla', 'amp', 'Envase inyectable'),
(55, 'Vial', 'vial', 'Frasco pequeño para inyectables'),
(56, 'Miligramo', 'mg', 'Submúltiplo del gramo'),
(57, 'Microgramo', 'mcg', 'Unidad muy pequeña de masa'),
(58, 'Unidad Internacional', 'UI', 'Medida de actividad biológica'),
(59, 'Gotero', 'got', 'Aplicación por gotas'),
(60, 'Inhalación', 'inh', 'Dosis para inhalador'),
(61, 'Parche', 'patch', 'Parche transdérmico'),
(62, 'Jeringa', 'jeringa', 'Dosis en jeringa prellenada');

-- ======================================================
-- 4. UNIDADES PARA CONSTRUCCIÓN
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(63, 'Metro cúbico', 'm³', 'Unidad de volumen para materiales a granel'),
(64, 'Kilogramo', 'kg', 'Unidad de masa'),
(65, 'Tonelada', 't', '1000 kilogramos'),
(66, 'Ladrillo', 'lad', 'Unidad para ladrillos'),
(67, 'Bloque', 'blq', 'Unidad para bloques de concreto'),
(68, 'Plancha', 'pl', 'Para láminas de triplay o drywall'),
(69, 'Rollo', 'rollo', 'Para alambre, mangueras, cintas'),
(70, 'Galón', 'gal', 'Para pinturas y líquidos'),
(71, 'Saco', 'saco', 'Bulto de cemento, cal, arena'),
(72, 'Varilla', 'var', 'Para acero de construcción'),
(73, 'Metro lineal', 'm l', 'Medida de longitud para perfiles'),
(74, 'Metro cuadrado', 'm²', 'Unidad de área para acabados'),
(75, 'Pulgada', 'in', 'Medida anglosajona'),
(76, 'Pie', 'ft', 'Medida anglosajona de longitud'),
(77, 'Yarda', 'yd', 'Medida anglosajona de longitud');

-- ======================================================
-- 5. UNIDADES PARA TEXTIL
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(78, 'Metro', 'm', 'Unidad de longitud para telas'),
(79, 'Centímetro', 'cm', 'Medidas pequeñas de longitud'),
(80, 'Talla única', 'TU', 'Talla estándar única'),
(81, 'Talla XS', 'XS', 'Extra Small'),
(82, 'Talla S', 'S', 'Small'),
(83, 'Talla M', 'M', 'Medium'),
(84, 'Talla L', 'L', 'Large'),
(85, 'Talla XL', 'XL', 'Extra Large'),
(86, 'Talla XXL', 'XXL', 'Double Extra Large'),
(87, 'Talla 0-3M', '0-3M', 'Talla para bebé 0-3 meses'),
(88, 'Talla 3-6M', '3-6M', 'Talla para bebé 3-6 meses'),
(89, 'Talla 6-9M', '6-9M', 'Talla para bebé 6-9 meses'),
(90, 'Talla 9-12M', '9-12M', 'Talla para bebé 9-12 meses'),
(91, 'Talla 12-18M', '12-18M', 'Talla para bebé 12-18 meses'),
(92, 'Talla 18-24M', '18-24M', 'Talla para bebé 18-24 meses'),
(93, 'Talla 2T', '2T', 'Toddler 2 años'),
(94, 'Talla 3T', '3T', 'Toddler 3 años'),
(95, 'Talla 4T', '4T', 'Toddler 4 años'),
(96, 'Talla 5T', '5T', 'Toddler 5 años'),
(97, 'Número calzado', '#', 'Número de zapato'),
(98, 'Docena de pares', 'doc par', '12 pares de calcetines/medias');

-- ======================================================
-- 6. UNIDADES PARA ELECTRÓNICA
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(99, 'Gigabyte', 'GB', 'Unidad de almacenamiento'),
(100, 'Terabyte', 'TB', 'Unidad de almacenamiento'),
(101, 'Megahertz', 'MHz', 'Frecuencia de procesador'),
(102, 'Gigahertz', 'GHz', 'Frecuencia de procesador'),
(103, 'Pulgadas', '"', 'Tamaño de pantalla'),
(104, 'Megapixel', 'MP', 'Resolución de cámara'),
(105, 'Lumen', 'lm', 'Brillo de proyectores'),
(106, 'Watts', 'W', 'Potencia eléctrica'),
(107, 'Kilowatt/hora', 'kWh', 'Consumo energético'),
(108, 'Milímetro', 'mm', 'Medidas pequeñas de grosor');

-- ======================================================
-- 7. UNIDADES PARA AUTOMOTRIZ
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(109, 'Juego', 'jgo', 'Set completo de piezas'),
(110, 'Kit', 'kit', 'Conjunto de componentes'),
(111, 'Litro', 'L', 'Para aceites y fluidos'),
(112, 'Cuarto', 'qt', 'Cuarto de galón (0.946 L)'),
(113, 'Psi', 'psi', 'Presión de neumáticos'),
(114, 'Bar', 'bar', 'Unidad de presión'),
(115, 'Grado API', 'API', 'Viscosidad de lubricantes'),
(116, 'Milímetro', 'mm', 'Dimensión de neumáticos'),
(117, 'Pulgada', '"', 'Dimensión de llantas');

-- ======================================================
-- 8. UNIDADES PARA AGRICULTURA
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(118, 'Hectárea', 'ha', '10,000 metros cuadrados'),
(119, 'Kilogramo', 'kg', 'Unidad de masa para cosechas'),
(120, 'Quintal', 'qq', '100 kilogramos'),
(121, 'Tonelada métrica', 't', '1000 kilogramos'),
(122, 'Saco', 'saco', 'Saco de grano o fertilizante'),
(123, 'Litro', 'L', 'Para líquidos (agua, químicos)'),
(124, 'Mililitro', 'mL', 'Para dosis pequeñas'),
(125, 'Gramo', 'g', 'Para semillas pequeñas'),
(126, 'Unidad', 'u', 'Para animales o árboles'),
(127, 'Cabeza', 'cab', 'Para ganado'),
(128, 'Galón', 'gal', 'Para agroquímicos líquidos');

-- ======================================================
-- 9. UNIDADES PARA OFICINA
-- ======================================================
INSERT INTO `adv__unidad_medida` (`id_unidad_medida`, `nombre`, `abreviatura`, `descripcion`) VALUES
(129, 'Resma', 'resma', '500 hojas de papel'),
(130, 'Caja', 'caja', 'Empaque de múltiples unidades'),
(131, 'Cartucho', 'cart', 'Cartucho de tinta o tóner'),
(132, 'Carpeta', 'carp', 'Unidad de organización'),
(133, 'Taco', 'taco', 'Bloque de hojas adhesivas'),
(134, 'Clips', 'clips', 'Sujetapapeles'),
(135, 'Grapas', 'grap', 'Caja de grapas'),
(136, 'Cinta', 'cinta', 'Rollo de cinta adhesiva');

-- ======================================================
-- ACTUALIZAR AUTO_INCREMENT
-- ======================================================
ALTER TABLE `adv__unidad_medida` AUTO_INCREMENT = 137;