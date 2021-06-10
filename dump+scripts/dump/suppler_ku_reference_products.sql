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
-- Table structure for table `reference_products`
--

DROP TABLE IF EXISTS `reference_products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reference_products` (
  `id` varchar(64) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `options` text NOT NULL,
  `tagid` varchar(64) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fkIdx_273` (`tagid`),
  CONSTRAINT `FK_272` FOREIGN KEY (`tagid`) REFERENCES `tags` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reference_products`
--

LOCK TABLES `reference_products` WRITE;
/*!40000 ALTER TABLE `reference_products` DISABLE KEYS */;
INSERT INTO `reference_products` VALUES ('a32fcdbc-5eb2-4b5e-9753-9365c870323e','Плюшевая игрушка БИГ ЧАНГУС','выдлмьвьыдмьдьм','Размер=20х20|Цвет=Серый','d69aaad2-2874-4675-be73-9fe022f9e095'),('bf716b5c-2dd6-4f2b-a816-9353e716d389','IPhone 12','Cool phone super good','display=OLED|size=50cm','1589a28e-6dd7-4152-8b36-9384a84b4961'),('e117a6e2-48fa-4c35-880f-deae1c7cbdde','Mkeke Compatible with iPhone XR Screen Protector','Cool IPhone XR screen protector','model=IPhone XR','dbbf7e22-4587-43cd-8ab4-cad000633c44'),('e15d6da9-96b5-489a-a922-a2e08728b741','Кулон синий','иииии','','1589a28e-6dd7-4152-8b36-9384a84b4961');
/*!40000 ALTER TABLE `reference_products` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-10 14:07:21
