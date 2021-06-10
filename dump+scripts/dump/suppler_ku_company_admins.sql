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
-- Table structure for table `company_admins`
--

DROP TABLE IF EXISTS `company_admins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `company_admins` (
  `companyid` varchar(64) NOT NULL,
  `customerid` varchar(64) NOT NULL,
  `role` varchar(45) NOT NULL,
  PRIMARY KEY (`companyid`,`customerid`),
  KEY `fkIdx_289` (`customerid`),
  KEY `fkIdx_77` (`companyid`),
  CONSTRAINT `FK_288` FOREIGN KEY (`customerid`) REFERENCES `customers` (`id`),
  CONSTRAINT `FK_76` FOREIGN KEY (`companyid`) REFERENCES `companies` (`id`),
  CONSTRAINT `check_admin_role` CHECK ((`role` in (_utf8mb4'owner',_utf8mb4'admin')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company_admins`
--

LOCK TABLES `company_admins` WRITE;
/*!40000 ALTER TABLE `company_admins` DISABLE KEYS */;
INSERT INTO `company_admins` VALUES ('120ac51f-e869-4abb-8454-92c97e87a49a','26b6d593-c82f-8407-4d84-09050f85f8b7','owner'),('b871e094-4acf-45ba-a00f-96070a45af39','0500fdb9-e223-794a-67da-0764e10a7be7','owner'),('b9e70e78-e172-4e6f-a33d-f3861892e50c','0500fdb9-e223-794a-67da-0764e10a7be7','owner'),('bc1d2d22-a7c7-4cd8-8d76-d6746085f24c','0599efff-f979-9e3e-4b09-c6cbd19a0a0c','owner');
/*!40000 ALTER TABLE `company_admins` ENABLE KEYS */;
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
