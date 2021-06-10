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
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `id` varchar(64) NOT NULL,
  `customerid` varchar(64) NOT NULL,
  `order_number` int NOT NULL,
  `order_date` timestamp NOT NULL,
  `address` varchar(255) NOT NULL,
  `order_status` varchar(45) NOT NULL,
  `isConfirmed` tinyint(1) NOT NULL,
  `isPaid` tinyint(1) NOT NULL,
  `delivery_date` timestamp NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fkIdx_32` (`customerid`),
  CONSTRAINT `FK_31` FOREIGN KEY (`customerid`) REFERENCES `customers` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `check_order_status` CHECK ((`order_status` in (_utf8mb4'sent',_utf8mb4'assembly',_utf8mb4'processing',_utf8mb4'delivered')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES ('08f035b1-04d9-414d-848a-121fc583632b','0e6f7274-f188-58c4-1a45-23637c0ab7aa',912856,'2021-05-20 15:43:57','adlcmdslds','processing',0,0,'2021-05-20 15:43:57'),('189635b7-69ab-4846-b091-138d22ef7ccb','0e6f7274-f188-58c4-1a45-23637c0ab7aa',816255,'2021-05-28 08:06:06','zdjkvhkjsdvsd','processing',0,0,'2021-05-28 08:06:06'),('1af895ef-6e45-4a4c-b4e3-d8bd43eba98a','0e6f7274-f188-58c4-1a45-23637c0ab7aa',132659,'2021-05-20 15:34:12','skdmvlkdsmv','processing',0,0,'2021-05-20 21:00:00'),('267002b9-2f0f-40b7-a8df-4913e0566f0b','0e6f7274-f188-58c4-1a45-23637c0ab7aa',919944,'2021-05-21 13:24:10','LLDKLKDMC','processing',0,0,'2021-05-21 21:00:00'),('7512d8e9-4e97-4ddb-8994-aadcaa530cd6','0e6f7274-f188-58c4-1a45-23637c0ab7aa',132659,'2021-05-20 15:30:26','lkmlksmv','processing',0,0,'2021-05-20 21:00:00'),('75ab4f58-bcaf-4b42-b771-67e9df618d44','0e6f7274-f188-58c4-1a45-23637c0ab7aa',717326,'2021-05-20 15:51:26','kjnkjsndv','processing',0,0,'2021-05-20 15:51:26'),('82dbbc03-992d-440e-a670-79bce16334bf','0e6f7274-f188-58c4-1a45-23637c0ab7aa',132659,'2021-05-20 15:36:23','hgjhbjbjbh','processing',0,0,'2021-05-20 21:00:00'),('9ef7c27b-8a72-4baf-a886-60553aa0d383','0e6f7274-f188-58c4-1a45-23637c0ab7aa',646912,'2021-05-22 12:52:57','elkfmlswkmflksdmf','processing',0,0,'2021-05-22 12:52:57'),('b20d4afa-7dbf-47c1-a032-b6994a41a5c1','0e6f7274-f188-58c4-1a45-23637c0ab7aa',421996,'2021-05-24 17:11:52','lsfkvlskmvksv','processing',0,0,'2021-05-24 21:00:00'),('bb9c2a24-ee20-4b62-8ef0-1681e6ccecc7','123',2,'2021-04-30 21:00:00','Moscow','assembly',1,1,'2021-05-02 21:00:00'),('c9ac630c-c342-4389-a5f2-0f65e293ce2d','123',1,'2021-04-30 21:00:00','Moscow','processing',1,1,'2021-05-02 21:00:00'),('e884292f-736e-4f75-a791-c6e9790a7d3d','0e6f7274-f188-58c4-1a45-23637c0ab7aa',650460,'2021-05-24 10:13:01','elkncdanclkadmmds','processing',0,0,'2021-05-24 10:13:01');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
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
