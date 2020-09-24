-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-09-2020 a las 16:28:16
-- Versión del servidor: 10.4.14-MariaDB
-- Versión de PHP: 7.4.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `placemybet`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `APUESTAS`
--

CREATE TABLE `APUESTAS` (
  `id_apuesta` int(11) NOT NULL,
  `tipo_apuesta` varchar(10) NOT NULL,
  `dinero_apostado` int(11) NOT NULL,
  `cuota` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `ref_email_usuario` varchar(50) NOT NULL,
  `id_mercado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `APUESTAS`
--

INSERT INTO `APUESTAS` (`id_apuesta`, `tipo_apuesta`, `dinero_apostado`, `cuota`, `fecha`, `ref_email_usuario`, `id_mercado`) VALUES
(3, 'OVER', 150, 3, '2020-09-15', 'carlos@gmail.com', 3),
(4, 'UNDER', 200, 5, '2020-09-18', 'carlos@gmail.com', 5);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `CUENTAS`
--

CREATE TABLE `CUENTAS` (
  `numero_tarjeta_credito` varchar(16) NOT NULL,
  `saldo_actual` int(11) NOT NULL,
  `banco` int(11) NOT NULL,
  `ref_email_usuario` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `EVENTOS`
--

CREATE TABLE `EVENTOS` (
  `id_partido` int(11) NOT NULL,
  `equipo_visitante` varchar(50) NOT NULL,
  `equipo_local` varchar(50) NOT NULL,
  `fecha` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `EVENTOS`
--

INSERT INTO `EVENTOS` (`id_partido`, `equipo_visitante`, `equipo_local`, `fecha`) VALUES
(3, 'Madrid', 'Valencia', '2020-09-15'),
(4, 'Valencia', 'Barcelona', '2020-09-15');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `MERCADOS`
--

CREATE TABLE `MERCADOS` (
  `id_mercado` int(11) NOT NULL,
  `cuota_over` int(11) NOT NULL,
  `cuota_under` int(11) NOT NULL,
  `dinero_over` int(11) NOT NULL,
  `dinero_under` int(11) NOT NULL,
  `id_partido` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `MERCADOS`
--

INSERT INTO `MERCADOS` (`id_mercado`, `cuota_over`, `cuota_under`, `dinero_over`, `dinero_under`, `id_partido`) VALUES
(3, 4, 5, 100, 200, 3),
(4, 5, 7, 200, 500, 3),
(5, 6, 3, 200, 150, 4),
(6, 2, 5, 100, 150, 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `USUARIOS`
--

CREATE TABLE `USUARIOS` (
  `email` varchar(50) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellidos` varchar(50) NOT NULL,
  `edad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `USUARIOS`
--

INSERT INTO `USUARIOS` (`email`, `nombre`, `apellidos`, `edad`) VALUES
('carlos@gmail.com', 'Carlos', 'Micó', 27),
('kiko@gmail.com', 'Kiko', 'Martínez', 29);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `APUESTAS`
--
ALTER TABLE `APUESTAS`
  ADD PRIMARY KEY (`id_apuesta`),
  ADD KEY `email_usuario` (`ref_email_usuario`),
  ADD KEY `id_mercado` (`id_mercado`);

--
-- Indices de la tabla `CUENTAS`
--
ALTER TABLE `CUENTAS`
  ADD PRIMARY KEY (`numero_tarjeta_credito`),
  ADD KEY `email` (`ref_email_usuario`);

--
-- Indices de la tabla `EVENTOS`
--
ALTER TABLE `EVENTOS`
  ADD PRIMARY KEY (`id_partido`);

--
-- Indices de la tabla `MERCADOS`
--
ALTER TABLE `MERCADOS`
  ADD PRIMARY KEY (`id_mercado`),
  ADD KEY `id_partido` (`id_partido`);

--
-- Indices de la tabla `USUARIOS`
--
ALTER TABLE `USUARIOS`
  ADD PRIMARY KEY (`email`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `APUESTAS`
--
ALTER TABLE `APUESTAS`
  MODIFY `id_apuesta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `EVENTOS`
--
ALTER TABLE `EVENTOS`
  MODIFY `id_partido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `MERCADOS`
--
ALTER TABLE `MERCADOS`
  MODIFY `id_mercado` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `APUESTAS`
--
ALTER TABLE `APUESTAS`
  ADD CONSTRAINT `apuestas_ibfk_1` FOREIGN KEY (`ref_email_usuario`) REFERENCES `usuarios` (`email`) ON UPDATE CASCADE,
  ADD CONSTRAINT `apuestas_ibfk_2` FOREIGN KEY (`id_mercado`) REFERENCES `mercados` (`id_mercado`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `CUENTAS`
--
ALTER TABLE `CUENTAS`
  ADD CONSTRAINT `cuentas_ibfk_1` FOREIGN KEY (`ref_email_usuario`) REFERENCES `usuarios` (`email`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `MERCADOS`
--
ALTER TABLE `MERCADOS`
  ADD CONSTRAINT `mercados_ibfk_1` FOREIGN KEY (`id_partido`) REFERENCES `eventos` (`id_partido`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
