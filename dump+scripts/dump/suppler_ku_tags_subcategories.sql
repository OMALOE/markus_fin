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
-- Table structure for table `tags_subcategories`
--

DROP TABLE IF EXISTS `tags_subcategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tags_subcategories` (
  `tag_id` varchar(64) NOT NULL,
  `subcategory_id` varchar(64) NOT NULL,
  PRIMARY KEY (`tag_id`,`subcategory_id`),
  KEY `fk_tags_has_subcategories_subcategories1_idx` (`subcategory_id`),
  KEY `fk_tags_has_subcategories_tags1_idx` (`tag_id`),
  CONSTRAINT `fk_tags_has_subcategories_subcategories1` FOREIGN KEY (`subcategory_id`) REFERENCES `subcategories` (`id`),
  CONSTRAINT `fk_tags_has_subcategories_tags1` FOREIGN KEY (`tag_id`) REFERENCES `tags` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tags_subcategories`
--

LOCK TABLES `tags_subcategories` WRITE;
/*!40000 ALTER TABLE `tags_subcategories` DISABLE KEYS */;
INSERT INTO `tags_subcategories` VALUES ('1589a28e-6dd7-4152-8b36-9384a84b4961','1'),('dbbf7e22-4587-43cd-8ab4-cad000633c44','1'),('4e170a91-2bbf-44f8-a852-a06640f90f7e','48b2792a-df9e-4119-90d2-09c3d7af0455'),('8adc5861-adeb-463e-a0b0-0d4fe59c171d','48b2792a-df9e-4119-90d2-09c3d7af0455'),('9276edcf-5d1e-4c13-b43f-771d7f4dab19','48b2792a-df9e-4119-90d2-09c3d7af0455'),('ffe40ba1-e8d6-4837-8b17-0763b0a7c1a8','48b2792a-df9e-4119-90d2-09c3d7af0455'),('8adc5861-adeb-463e-a0b0-0d4fe59c171d','d4c4095e-f429-4003-a6b2-d4fe0b6e18a0'),('9276edcf-5d1e-4c13-b43f-771d7f4dab19','d4c4095e-f429-4003-a6b2-d4fe0b6e18a0'),('ca46e445-99ce-45d7-9bf5-92ad04ec3c16','d4c4095e-f429-4003-a6b2-d4fe0b6e18a0');
/*!40000 ALTER TABLE `tags_subcategories` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-10 14:07:20
