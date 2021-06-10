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
-- Table structure for table `variation_options`
--

DROP TABLE IF EXISTS `variation_options`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `variation_options` (
  `id` varchar(64) NOT NULL,
  `product_id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(255) NOT NULL,
  `value` varchar(45) NOT NULL,
  PRIMARY KEY (`id`,`product_id`,`name`),
  KEY `fk_variation_options_products1_idx` (`product_id`),
  CONSTRAINT `fk_variation_options_products1` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `variation_options`
--

LOCK TABLES `variation_options` WRITE;
/*!40000 ALTER TABLE `variation_options` DISABLE KEYS */;
INSERT INTO `variation_options` VALUES ('742b0a8c-c1cf-44eb-b0c5-3ceea054a789','6cab9ea8-75d2-4161-8fc9-05a830693839','acddvsfv','aaaaa'),('7e7cbc45-0e07-48b8-8f4e-4a0149978a74','53bbcde1-fbce-427d-94a5-8f259ba7c712','Размер','10'),('7e7cbc45-0e07-48b8-8f4e-4a0149978a74','53bbcde1-fbce-427d-94a5-8f259ba7c712','Цвет','Зеленый'),('7e7cbc45-0e07-48b8-8f4e-4a0149978a74','e398092b-bfd7-4331-a5c2-b89556fe8d1c','Размер','12'),('7e7cbc45-0e07-48b8-8f4e-4a0149978a74','e398092b-bfd7-4331-a5c2-b89556fe8d1c','Цвет','Зеленый'),('9978c346-19b4-41c3-bded-c1e947e29716','3b91e8d1-2f74-4515-95c8-cb052c4511a5','Цвет','Синий'),('9978c346-19b4-41c3-bded-c1e947e29716','e45b154b-35a2-4a40-a0dc-b4234ce956b0','Цвет','Красный'),('c6f848f6-f1f7-493c-a125-4655945ed27e','b7a0d2da-370f-40d3-9207-b7549b67208c','acasca','aaaa');
/*!40000 ALTER TABLE `variation_options` ENABLE KEYS */;
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
