CREATE DATABASE  IF NOT EXISTS `kiosco` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `kiosco`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: kiosco
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cliente` (
  `idcliente` int NOT NULL AUTO_INCREMENT,
  `cif` varchar(20) NOT NULL,
  `persona_id` int NOT NULL,
  `oferta_id` int NOT NULL,
  PRIMARY KEY (`idcliente`),
  UNIQUE KEY `idcliente_UNIQUE` (`idcliente`),
  UNIQUE KEY `cif_UNIQUE` (`cif`),
  KEY `fk_cliente_oferta1_idx` (`oferta_id`),
  KEY `fk_cliente_persona1_idx` (`persona_id`),
  CONSTRAINT `fk_cliente_oferta1` FOREIGN KEY (`oferta_id`) REFERENCES `oferta` (`idoferta`),
  CONSTRAINT `fk_cliente_persona1` FOREIGN KEY (`persona_id`) REFERENCES `persona` (`idpersona`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (1,'ABC123456',6,3),(2,'DEF654321',7,1),(3,'GHI789012',8,5),(4,'JKL345678',9,2),(5,'MNO901234',10,4),(6,'-',11,6);
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detalleventa`
--

DROP TABLE IF EXISTS `detalleventa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detalleventa` (
  `cantidad` int DEFAULT NULL,
  `precio_unitario` double DEFAULT NULL,
  `venta_id` int NOT NULL,
  `producto_id` int NOT NULL,
  PRIMARY KEY (`venta_id`,`producto_id`),
  KEY `fk_detalleventa_venta1_idx` (`venta_id`),
  KEY `fk_detalleventa_producto1_idx` (`producto_id`),
  CONSTRAINT `fk_detalleventa_producto1` FOREIGN KEY (`producto_id`) REFERENCES `producto` (`idproducto`),
  CONSTRAINT `fk_detalleventa_venta1` FOREIGN KEY (`venta_id`) REFERENCES `venta` (`idventa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalleventa`
--

LOCK TABLES `detalleventa` WRITE;
/*!40000 ALTER TABLE `detalleventa` DISABLE KEYS */;
INSERT INTO `detalleventa` VALUES (4,1.5,1,1),(3,0.75,1,2),(1,1,1,3),(5,8,1,13),(1,8.99,2,18),(1,15,11,12),(1,4.5,11,17),(1,8.99,11,18),(3,10,19,11),(1,15,19,12),(1,4.5,19,17),(1,8.99,19,18),(6,4.5,24,17),(1,9.99,25,15),(1,12.5,25,16),(1,12,25,20),(1,6,25,21),(1,4.5,30,17),(1,2,31,10),(1,7,31,19),(1,1.2,32,8),(1,1.2,32,9),(1,9.99,32,15),(1,6,32,21),(1,1.2,33,9),(1,2,33,10),(1,10,33,11),(1,9.99,33,15),(1,12.5,33,16),(1,4.5,33,17),(1,2,34,10),(1,10,34,11),(4,12.5,34,16),(1,4.5,34,17),(10,0.5,35,5),(1,4.5,36,17),(2,8.99,36,18),(2,6,36,21),(1,1.2,37,8),(1,10,37,11),(1,15,37,12),(3,4.5,37,17),(1,8.99,37,18),(1,0.5,38,5),(1,0.8,38,6),(2,10,38,11),(1,15,38,12),(1,10,39,11),(1,15,39,12),(1,4.5,39,17),(1,8.99,39,18),(1,0.5,40,5),(1,0.8,40,6),(3,1.2,40,8),(1,1.2,40,9),(1,10,40,11),(2,15,40,12),(1,15,41,12),(1,8.99,43,18),(1,6,43,21);
/*!40000 ALTER TABLE `detalleventa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `oferta`
--

DROP TABLE IF EXISTS `oferta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `oferta` (
  `idoferta` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(50) DEFAULT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime NOT NULL,
  `fichero` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`idoferta`),
  UNIQUE KEY `idoferta_UNIQUE` (`idoferta`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `oferta`
--

LOCK TABLES `oferta` WRITE;
/*!40000 ALTER TABLE `oferta` DISABLE KEYS */;
INSERT INTO `oferta` VALUES (1,'Descuento del 10%','2024-04-01 00:00:00','2024-04-30 00:00:00','descuento10.jpg'),(2,'Descuento del 15%','2024-03-15 00:00:00','2024-03-31 00:00:00','descuento15.jpg'),(3,'Descuento del 20%','2024-03-01 00:00:00','2024-04-01 00:00:00','descuento20.jpg'),(4,'Descuento del 30%','2024-03-01 00:00:00','2024-04-01 00:00:00','descuento30.jpg'),(5,'Descuento del 50%','2024-03-15 00:00:00','2024-03-31 00:00:00','descuento50.jpg'),(6,'Sin Descuento','2024-04-01 00:00:00','2024-04-30 00:00:00','descuento0.jpg');
/*!40000 ALTER TABLE `oferta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permiso`
--

DROP TABLE IF EXISTS `permiso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permiso` (
  `idpermiso` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idpermiso`),
  UNIQUE KEY `idpermiso_UNIQUE` (`idpermiso`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permiso`
--

LOCK TABLES `permiso` WRITE;
/*!40000 ALTER TABLE `permiso` DISABLE KEYS */;
INSERT INTO `permiso` VALUES (1,'Introducir ventas'),(2,'Devolución ventas'),(3,'Introducir productos'),(4,'Modificar/Eliminar productos'),(5,'Campañas de publicidad'),(6,'Gestionar usuarios'),(7,'Edición de permisos'),(8,'Cambio de contraseñas');
/*!40000 ALTER TABLE `permiso` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `persona`
--

DROP TABLE IF EXISTS `persona`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `persona` (
  `idpersona` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(15) NOT NULL,
  `apellidos` varchar(30) DEFAULT NULL,
  `direccion` varchar(50) DEFAULT NULL,
  `poblacion` varchar(25) DEFAULT NULL,
  `cod_postal` varchar(10) DEFAULT NULL,
  `telefono` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`idpersona`),
  UNIQUE KEY `idpersona_UNIQUE` (`idpersona`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `persona`
--

LOCK TABLES `persona` WRITE;
/*!40000 ALTER TABLE `persona` DISABLE KEYS */;
INSERT INTO `persona` VALUES (1,'Kyrie','García','Calle Mayor 123','Valencia','46002','123456789'),(2,'Aru','López','Avenida Libertad 456','Valencia','46002','987654321'),(3,'Sally','Martínez','Plaza España 789','Valencia','46002','654987321'),(6,'Pedro','Díaz','Calle San Pablo 210','Madrid','28002','456789012'),(7,'Ana','Martín','Avenida Libertadores 15','Barcelona','08002','789012345'),(8,'David','Rodríguez','Plaza Independencia 33','Sevilla','41002','901234567'),(9,'Elena','Gutiérrez','Calle Mayor 55','Valencia','46002','685475545'),(10,'Sara','Sánchez','Avenida del Mar 102','Málaga','29002','345678901'),(11,'Genérico','Genérico',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `persona` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `producto`
--

DROP TABLE IF EXISTS `producto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `producto` (
  `idproducto` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(50) DEFAULT NULL,
  `precio` double NOT NULL,
  `ubicacion` varchar(25) DEFAULT NULL,
  `stock` int NOT NULL,
  `iva` double DEFAULT NULL,
  `imagen` varchar(250) DEFAULT NULL,
  `oferta_id` int DEFAULT NULL,
  `tipoproducto_id` int NOT NULL,
  PRIMARY KEY (`idproducto`),
  UNIQUE KEY `idproducto_UNIQUE` (`idproducto`),
  KEY `fk_producto_oferta_idx` (`oferta_id`),
  KEY `fk_producto_tipoproducto1_idx` (`tipoproducto_id`),
  CONSTRAINT `fk_producto_oferta` FOREIGN KEY (`oferta_id`) REFERENCES `oferta` (`idoferta`),
  CONSTRAINT `fk_producto_tipoproducto1` FOREIGN KEY (`tipoproducto_id`) REFERENCES `tipoproducto` (`idtipoproducto`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `producto`
--

LOCK TABLES `producto` WRITE;
/*!40000 ALTER TABLE `producto` DISABLE KEYS */;
INSERT INTO `producto` VALUES (1,'Helado',1.5,'Nevera',47,0.16,'helado.png',6,1),(2,'Nachos',0.75,'Estante A',96,0.16,'nachos.png',6,1),(3,'Pan de Molde',1,'Estante A',28,0.16,'pan-molde.png',6,1),(4,'Papas Fritas',1.2,'Estante A',39,0.16,'papas-fritas.png',6,1),(5,'Bolsa de Chuches',0.5,'Estante A',188,0.16,'chuches.png',6,1),(6,'Agua Mineral',0.8,'Nevera',197,0.16,'agua.png',6,2),(7,'Cerveza Estrella',1.5,'Nevera',79,0.16,'cerveza.png',6,2),(8,'Coca-Cola',1.2,'Nevera',87,0.16,'cocacola.png',6,2),(9,'Vino',1.2,'Nevera',14,0.16,'vino.png',6,2),(10,'Zumo de Naranja',2,'Nevera',26,0.16,'zumo.png',6,2),(11,'Muñeca de Peluche',10,'Almacén',37,0.16,'muneca.png',1,3),(12,'Rompecabezas',15,'Almacén',44,0.16,'rompecabezas.png',6,3),(13,'Balón de Fútbol',8,'Almacén',20,0.16,'pelota.png',2,3),(14,'Herramientas',12,'Almacén',24,0.16,'herramientas.png',6,3),(15,'Libro de Cocina',9.99,'Estante B',26,0.16,'libro-cocina.png',6,4),(16,'Novela de Misterio',12.5,'Estante B',9,0.16,'novela.png',6,4),(17,'Revista Ciencia Ficción',4.5,'Estante B',29,0.16,'revista.png',6,4),(18,'Libro de Poesía',8.99,'Estante B',48,0.16,'poesia.png',3,4),(19,'Aftersun',7,'Estante C',39,0.16,'aftersun.png',6,5),(20,'Protector Solar SPF 50',12,'Estante C',21,0.16,'protector-solar.png',6,5),(21,'Repelente',5.7,'Estante C',4,0.16,'repelente.png',6,5);
/*!40000 ALTER TABLE `producto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rol`
--

DROP TABLE IF EXISTS `rol`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rol` (
  `idrol` int NOT NULL AUTO_INCREMENT,
  `nombre_rol` varchar(15) NOT NULL,
  PRIMARY KEY (`idrol`),
  UNIQUE KEY `idrol_UNIQUE` (`idrol`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rol`
--

LOCK TABLES `rol` WRITE;
/*!40000 ALTER TABLE `rol` DISABLE KEYS */;
INSERT INTO `rol` VALUES (1,'Gerente'),(2,'Encargado'),(3,'Empleado');
/*!40000 ALTER TABLE `rol` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tiene`
--

DROP TABLE IF EXISTS `tiene`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tiene` (
  `permiso_id` int NOT NULL,
  `rol_id` int NOT NULL,
  PRIMARY KEY (`permiso_id`,`rol_id`),
  KEY `fk_permiso_has_rol_rol1_idx` (`rol_id`),
  KEY `fk_permiso_has_rol_permiso1_idx` (`permiso_id`),
  CONSTRAINT `fk_permiso_has_rol_permiso1` FOREIGN KEY (`permiso_id`) REFERENCES `permiso` (`idpermiso`),
  CONSTRAINT `fk_permiso_has_rol_rol1` FOREIGN KEY (`rol_id`) REFERENCES `rol` (`idrol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tiene`
--

LOCK TABLES `tiene` WRITE;
/*!40000 ALTER TABLE `tiene` DISABLE KEYS */;
INSERT INTO `tiene` VALUES (1,1),(2,1),(3,1),(4,1),(5,1),(6,1),(7,1),(8,1),(1,2),(2,2),(3,2),(4,2),(5,2),(8,2),(1,3),(3,3),(8,3);
/*!40000 ALTER TABLE `tiene` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipoproducto`
--

DROP TABLE IF EXISTS `tipoproducto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipoproducto` (
  `idtipoproducto` int NOT NULL AUTO_INCREMENT,
  `categoria` varchar(25) DEFAULT NULL,
  `imagen` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`idtipoproducto`),
  UNIQUE KEY `idproducto_UNIQUE` (`idtipoproducto`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipoproducto`
--

LOCK TABLES `tipoproducto` WRITE;
/*!40000 ALTER TABLE `tipoproducto` DISABLE KEYS */;
INSERT INTO `tipoproducto` VALUES (1,'Comida','comida.png'),(2,'Bebida','bebida.png'),(3,'Juguetes','juguetes.png'),(4,'Lectura','lectura.png'),(5,'Parafarmacia','parafarma.png');
/*!40000 ALTER TABLE `tipoproducto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `idusuario` int NOT NULL AUTO_INCREMENT,
  `username` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `rol_id` int NOT NULL,
  `persona_id` int DEFAULT NULL,
  PRIMARY KEY (`idusuario`),
  UNIQUE KEY `idusuario_UNIQUE` (`idusuario`),
  KEY `fk_usuario_rol1_idx` (`rol_id`),
  KEY `fk_usuario_persona1` (`persona_id`),
  CONSTRAINT `fk_usuario_persona1` FOREIGN KEY (`persona_id`) REFERENCES `persona` (`idpersona`),
  CONSTRAINT `fk_usuario_rol1` FOREIGN KEY (`rol_id`) REFERENCES `rol` (`idrol`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'admin','admin',1,1),(2,'encargado','encargado123',2,2),(3,'empleado','empleado123',3,3);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `venta`
--

DROP TABLE IF EXISTS `venta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `venta` (
  `idventa` int NOT NULL AUTO_INCREMENT,
  `fecha` datetime NOT NULL,
  `total` double NOT NULL,
  `mensaje` varchar(45) DEFAULT NULL,
  `cliente_id` int NOT NULL,
  `usuario_id` int NOT NULL,
  PRIMARY KEY (`idventa`),
  UNIQUE KEY `idventa_UNIQUE` (`idventa`),
  KEY `fk_venta_cliente1_idx` (`cliente_id`),
  KEY `fk_venta_usuario1_idx` (`usuario_id`),
  CONSTRAINT `fk_venta_cliente1` FOREIGN KEY (`cliente_id`) REFERENCES `cliente` (`idcliente`),
  CONSTRAINT `fk_venta_usuario1` FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `venta`
--

LOCK TABLES `venta` WRITE;
/*!40000 ALTER TABLE `venta` DISABLE KEYS */;
INSERT INTO `venta` VALUES (1,'2024-03-10 00:00:00',9.98,'Pase buena tarde!',3,1),(2,'2024-03-15 00:00:00',29.97,': )',1,2),(11,'2024-05-16 00:00:00',28.490000000000002,NULL,6,1),(19,'2024-05-16 00:00:00',58.49,NULL,6,1),(24,'2024-05-21 00:00:00',27,NULL,5,1),(25,'2024-05-21 00:00:00',48.69,NULL,4,1),(30,'2024-05-23 00:00:00',12.5,NULL,6,2),(31,'2024-06-01 00:00:00',9,NULL,6,3),(32,'2024-05-23 00:00:00',22.39,NULL,6,1),(33,'2024-05-27 00:00:00',40.19,NULL,6,1),(34,'2024-05-27 00:00:00',66.5,NULL,6,1),(35,'2024-05-27 00:00:00',5,NULL,5,1),(36,'2024-05-27 00:00:00',50.480000000000004,NULL,4,1),(37,'2024-06-01 00:00:00',48.690000000000005,'',6,1),(38,'2024-06-06 00:00:00',36.3,NULL,3,1),(39,'2024-06-06 00:00:00',38.49,NULL,5,1),(40,'2024-06-06 00:00:00',46.1,NULL,3,1),(41,'2024-06-11 00:00:00',15.5,NULL,6,1),(43,'2024-06-17 00:00:00',14.99,NULL,6,1);
/*!40000 ALTER TABLE `venta` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-10-29  0:48:26
