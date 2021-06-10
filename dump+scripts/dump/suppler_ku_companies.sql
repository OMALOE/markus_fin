-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: suppler_ku
-- ------------------------------------------------------
-- Server version	8.0.23

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
-- Table structure for table `companies`
--

DROP TABLE IF EXISTS `companies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companies` (
  `id` varchar(64) NOT NULL,
  `public_name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `legal_type` varchar(45) NOT NULL,
  `legal_name` varchar(255) NOT NULL,
  `legal_address` varchar(255) NOT NULL,
  `TIN` varchar(25) NOT NULL,
  `TRRC` varchar(45) DEFAULT NULL,
  `PSRN` varchar(45) NOT NULL,
  `country` varchar(45) NOT NULL,
  `city` varchar(255) NOT NULL,
  `delivery_region` varchar(45) NOT NULL,
  `real_address` varchar(255) NOT NULL,
  `delivery_delay` int NOT NULL,
  `work_hours_start` timestamp NOT NULL,
  `work_hours_end` timestamp NOT NULL,
  `logoid` varchar(64) DEFAULT NULL,
  `bannerid` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fkIdx_93` (`logoid`),
  KEY `fkIdx_96` (`bannerid`),
  CONSTRAINT `FK_92` FOREIGN KEY (`logoid`) REFERENCES `images` (`id`),
  CONSTRAINT `FK_95` FOREIGN KEY (`bannerid`) REFERENCES `images` (`id`),
  CONSTRAINT `check_company_type` CHECK ((`legal_type` in (_utf8mb4'ООО',_utf8mb4'ПАО',_utf8mb4'ИП'))),
  CONSTRAINT `check_delivery_region` CHECK ((`delivery_region` in (_utf8mb4'country',_utf8mb4'city',_utf8mb4'global')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companies`
--

LOCK TABLES `companies` WRITE;
/*!40000 ALTER TABLE `companies` DISABLE KEYS */;
INSERT INTO `companies` VALUES ('120ac51f-e869-4abb-8454-92c97e87a49a','big_chungus','Наелся и спит','ООО','Биг Чангус','Россия, Москва, ул. Ордынка, 1','7326724','82379847329874','428798347932874','Палестина','Сектор газа','Country','Россия, Москва, ул. Ордынка, 2',1,'2021-05-19 06:00:00','2021-05-19 16:00:00',NULL,NULL),('b871e094-4acf-45ba-a00f-96070a45af39','Bimba','cool stuff','ООО','Bimba','aaa','12','13','14','Russia','Moscow','Country','aaa',1,'2021-04-21 08:45:38','2021-04-21 18:45:38',NULL,NULL),('b9e70e78-e172-4e6f-a33d-f3861892e50c','abrikosik','Игрушки для детей!','ООО','Абрикосик','Петровка, 38','327298374','48792798374','48279837493','Россия','Москва','Country','Петровка, 38',1,'2021-05-19 07:00:00','2021-05-19 18:00:00',NULL,NULL),('bc1d2d22-a7c7-4cd8-8d76-d6746085f24c','Aboba','Stuff cool','ООО','Aboba','ddd','13','14','15','Russia','Moscow','Country','sss',1,'2021-04-21 08:45:38','2021-04-21 08:45:38',NULL,NULL);
/*!40000 ALTER TABLE `companies` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-10 14:07:22
