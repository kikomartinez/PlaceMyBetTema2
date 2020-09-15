-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 15-09-2020 a las 13:33:18
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
-- Estructura de tabla para la tabla `apuesta`
--

CREATE TABLE `apuesta` (
  `id_apuesta` int(11) NOT NULL,
  `tipo_apuesta` varchar(10) NOT NULL,
  `dinero_apostado` int(11) NOT NULL,
  `cuota` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `email_usuario` varchar(50) NOT NULL,
  `id_mercado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuenta`
--

CREATE TABLE `cuenta` (
  `numero_tarjeta_credito` varchar(16) NOT NULL,
  `saldo_actual` int(11) NOT NULL,
  `banco` int(11) NOT NULL,
  `email_usuario` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `evento`
--

CREATE TABLE `evento` (
  `id_partido` int(11) NOT NULL,
  `equipo_visitante` varchar(50) NOT NULL,
  `equipo_local` varchar(50) NOT NULL,
  `fecha` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mercado`
--

CREATE TABLE `mercado` (
  `id_mercado` int(11) NOT NULL,
  `cuota_over` int(11) NOT NULL,
  `cuota_under` int(11) NOT NULL,
  `dinero_over` int(11) NOT NULL,
  `dinero_under` int(11) NOT NULL,
  `id_partido` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `email` varchar(50) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellidos` varchar(50) NOT NULL,
  `edad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `apuesta`
--
ALTER TABLE `apuesta`
  ADD PRIMARY KEY (`id_apuesta`),
  ADD KEY `email_usuario` (`email_usuario`),
  ADD KEY `id_mercado` (`id_mercado`);

--
-- Indices de la tabla `cuenta`
--
ALTER TABLE `cuenta`
  ADD PRIMARY KEY (`numero_tarjeta_credito`),
  ADD KEY `email` (`email_usuario`);

--
-- Indices de la tabla `evento`
--
ALTER TABLE `evento`
  ADD PRIMARY KEY (`id_partido`);

--
-- Indices de la tabla `mercado`
--
ALTER TABLE `mercado`
  ADD PRIMARY KEY (`id_mercado`),
  ADD KEY `id_partido` (`id_partido`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`email`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `apuesta`
--
ALTER TABLE `apuesta`
  MODIFY `id_apuesta` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `evento`
--
ALTER TABLE `evento`
  MODIFY `id_partido` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `mercado`
--
ALTER TABLE `mercado`
  MODIFY `id_mercado` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `apuesta`
--
ALTER TABLE `apuesta`
  ADD CONSTRAINT `apuesta_ibfk_1` FOREIGN KEY (`email_usuario`) REFERENCES `usuario` (`email`) ON UPDATE CASCADE,
  ADD CONSTRAINT `apuesta_ibfk_2` FOREIGN KEY (`id_mercado`) REFERENCES `mercado` (`id_mercado`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `cuenta`
--
ALTER TABLE `cuenta`
  ADD CONSTRAINT `cuenta_ibfk_1` FOREIGN KEY (`email_usuario`) REFERENCES `usuario` (`email`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `mercado`
--
ALTER TABLE `mercado`
  ADD CONSTRAINT `mercado_ibfk_1` FOREIGN KEY (`id_partido`) REFERENCES `evento` (`id_partido`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
