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
-- Table structure for table `order_products`
--

DROP TABLE IF EXISTS `order_products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order_products` (
  `orderid` varchar(64) NOT NULL,
  `productid` varchar(64) NOT NULL,
  `amount` int NOT NULL,
  `total_price` decimal(10,2) NOT NULL,
  `discount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`orderid`,`productid`),
  KEY `fkIdx_42` (`orderid`),
  KEY `fkIdx_47` (`productid`),
  CONSTRAINT `FK_41` FOREIGN KEY (`orderid`) REFERENCES `orders` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_46` FOREIGN KEY (`productid`) REFERENCES `products` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `check_amount_more_than_zero` CHECK ((`amount` > 0))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_products`
--

LOCK TABLES `order_products` WRITE;
/*!40000 ALTER TABLE `order_products` DISABLE KEYS */;
INSERT INTO `order_products` VALUES ('08f035b1-04d9-414d-848a-121fc583632b','188d293e-41c0-dab3-3b2b-900699dbcc3b',1,2.86,0.00),('189635b7-69ab-4846-b091-138d22ef7ccb','180644b7-6ce7-6254-0333-41182016b489',2,0.00,24.80),('1af895ef-6e45-4a4c-b4e3-d8bd43eba98a','0681ace6-6914-8468-f535-0867539c5e86',2,19.32,0.00),('267002b9-2f0f-40b7-a8df-4913e0566f0b','2ed31806-185f-46d0-aec3-3994ad380e3d',2,358.00,40.00),('7512d8e9-4e97-4ddb-8994-aadcaa530cd6','188d293e-41c0-dab3-3b2b-900699dbcc3b',1,2.86,0.00),('7512d8e9-4e97-4ddb-8994-aadcaa530cd6','2ed31806-185f-46d0-aec3-3994ad380e3d',1,199.00,0.00),('75ab4f58-bcaf-4b42-b771-67e9df618d44','b2e73f83-2587-de6e-edba-daeecb0927d7',1,2.36,0.00),('82dbbc03-992d-440e-a670-79bce16334bf','2ed31806-185f-46d0-aec3-3994ad380e3d',1,199.00,0.00),('82dbbc03-992d-440e-a670-79bce16334bf','93c37a19-e1fa-40d2-b67a-27d6fe2531a5',1,6.99,0.00),('9ef7c27b-8a72-4baf-a886-60553aa0d383','2ed31806-185f-46d0-aec3-3994ad380e3d',3,597.00,0.00),('b20d4afa-7dbf-47c1-a032-b6994a41a5c1','b81b2f9e-2b7b-0fef-0f1a-344940c072de',4,24.92,0.00),('bb9c2a24-ee20-4b62-8ef0-1681e6ccecc7','2ed31806-185f-46d0-aec3-3994ad380e3d',3,300.00,0.00),('c9ac630c-c342-4389-a5f2-0f65e293ce2d','0aed0d1e-6714-40ba-8b90-82e2a4cdf1d9',1,1.00,0.00),('c9ac630c-c342-4389-a5f2-0f65e293ce2d','2ed31806-185f-46d0-aec3-3994ad380e3d',1,100.00,0.00),('e884292f-736e-4f75-a791-c6e9790a7d3d','90bae03d-0351-4c94-8c36-b299ac025d3e',1,1200.00,0.00);
/*!40000 ALTER TABLE `order_products` ENABLE KEYS */;
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
