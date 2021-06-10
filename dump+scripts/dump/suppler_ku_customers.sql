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
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customers` (
  `id` varchar(64) NOT NULL,
  `firstname` varchar(64) NOT NULL,
  `lastname` varchar(64) NOT NULL,
  `phone` varchar(45) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(45) NOT NULL,
  `birthdate` date NOT NULL,
  `avatar` varchar(64) DEFAULT NULL,
  `bio` varchar(64) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_285` (`avatar`),
  CONSTRAINT `FK_285` FOREIGN KEY (`avatar`) REFERENCES `images` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES ('0500fdb9-e223-794a-67da-0764e10a7be7','Mavrodi','Ward','+7 (959) 165-40-93','malesuada.augue.ut@tincidunt.edu','1234567890','2005-02-15',NULL,'quam dignissim pharetra. Nam'),('0599efff-f979-9e3e-4b09-c6cbd19a0a0c','Dara','Arnold','+7 (861) 212-69-51','velit.Aliquam@molestiepharetra.com','1234567890','2020-03-30',NULL,'Phasellus in felis.'),('075d0133-d766-6178-8b1e-57b2b0689a7e','Coby','Morales','+7 (336) 901-77-69','Nulla@consectetueradipiscing.co.uk','1234567890','2014-04-24',NULL,'magnis dis parturient'),('0e6f7274-f188-58c4-1a45-23637c0ab7aa','Uriah','Murphy','+7 (902) 822-64-52','luctus@famesac.edu','1234567890','2010-07-28',NULL,'dolor. Quisque tincidunt pede ac'),('0f50b2ea-19ce-d7c6-ce70-29131aea8ca0','Violet','Powers','+7 (902) 940-32-67','Aliquam.fringilla.cursus@lectus.com','1234567890','2008-12-13',NULL,'nunc ac mattis ornare,'),('120a3a75-b8a0-da20-35c9-ffdca788fc7d','Ria','Kennedy','+7 (302) 421-93-58','Aliquam.erat@et.com','1234567890','1999-09-29',NULL,'tellus eu augue'),('123','Alex','Petrow','123442','123@22.22','123','2001-01-01',NULL,'bio'),('1cb39dfc-b7b4-b554-40bd-48432da81b1c','Glenna','Doyle','+7 (424) 217-91-53','non@Morbi.co.uk','1234567890','1996-05-03',NULL,'dolor quam, elementum'),('26b6d593-c82f-8407-4d84-09050f85f8b7','MacKensie','Jimenez','+7 (900) 398-28-16','neque.sed.sem@sitamet.ca','1234567890','1995-08-24',NULL,'ipsum porta elit, a'),('2713eaaf-9cfc-27dc-0a54-c4af8849385d','Frances','Moran','+7 (846) 413-51-50','pede.et@tempor.edu','1234567890','2017-06-12',NULL,'Lorem ipsum'),('2723d219-d839-6a0d-17dc-78a0a58d11fa','Sydnee','Knox','+7 (865) 756-44-60','non.egestas.a@auctor.com','1234567890','2014-04-16',NULL,'neque tellus, imperdiet'),('2c25b4eb-1233-56f0-5e0c-5bcfb5bb59fc','Wesley','Howard','+7 (415) 953-09-17','Praesent@dictummi.co.uk','1234567890','2006-09-09',NULL,'volutpat.'),('333eae81-de1c-2a2a-1e64-ba9e5969a415','Emi','Mayer','+7 (987) 467-82-01','Nulla@ametorciUt.ca','1234567890','2015-06-19',NULL,'tristique'),('354b9677-68df-5518-abf2-7fc068d4f83b','Oscar','Scott','+7 (932) 282-06-24','arcu.Aliquam.ultrices@senectusetnetus.edu','1234567890','2017-12-09',NULL,'sapien'),('44f78d58-83d2-9ccc-52a2-3c7e9300ebb4','Lewis','Marsh','+7 (812) 379-74-67','sed.est@ullamcorpermagnaSed.edu','1234567890','2015-03-28',NULL,'dolor dapibus'),('4a879941-3a6b-e3a0-f2c0-a41432f67470','Shad','Burks','+7 (982) 150-97-68','vehicula@sit.ca','1234567890','2015-08-11',NULL,'purus gravida sagittis. Duis gravida.'),('4d1940d3-5a4f-9d17-d02b-53a043e7fc27','Melvin','Whitfield','+7 (924) 570-15-77','consequat@Sedeunibh.com','1234567890','2020-11-12',NULL,'sem. Pellentesque'),('4f0d9a5d-2b23-7302-861f-7feb66ee62bf','Tanner','Weber','+7 (990) 627-38-69','in.felis@fermentum.org','1234567890','2012-03-30',NULL,'Mauris magna.'),('551e0932-77a3-2ff1-29ae-4b77a1619ee2','Tarik','Hood','+7 (495) 585-01-82','vel.nisl.Quisque@Suspendissealiquetmolestie.net','1234567890','2017-01-28',NULL,'lectus, a sollicitudin'),('5643b28d-4a61-5964-f7c2-2001acb5b21d','Charity','Bradley','+7 (956) 701-17-97','sapien@tellus.net','1234567890','2013-01-25',NULL,'elit elit fermentum risus, at'),('5a1a4c95-362e-04bd-3545-c659def96917','Cailin','Mccarthy','+7 (953) 715-58-07','lacus@iaculis.org','1234567890','2012-08-06',NULL,'mi'),('5c79d86b-4167-2897-6f82-e6d38c535d57','Quon','Sawyer','+7 (963) 839-16-13','vel@velit.net','1234567890','2011-06-01',NULL,'Nunc laoreet lectus'),('5ef467f7-8fbb-110b-c018-9bc0f5316b2b','Ila','Hansen','+7 (816) 727-17-07','nibh@pedenec.org','1234567890','2021-10-29',NULL,'montes, nascetur ridiculus'),('61d54fba-f9f4-f41c-9d81-4a85f6050b69','Hunter','Beasley','+7 (848) 668-89-02','eget.magna@convallisest.co.uk','1234567890','2018-10-30',NULL,'et magnis dis parturient montes,'),('69928424-3b18-1520-262f-b99cb5f9074b','Martena','Bridges','+7 (966) 996-34-80','nec.euismod@sapienmolestie.co.uk','1234567890','2016-02-19',NULL,'Curabitur massa. Vestibulum'),('6a094b6e-a8b6-15db-246e-0c1ccec484cf','Thomas','Carroll','+7 (401) 974-24-24','ultricies.sem.magna@ridiculusmus.org','1234567890','2008-07-27',NULL,'et risus.'),('6cbc3f54-06e5-a66a-4db6-37bfd5b29c43','Virginia','Foley','+7 (349) 537-23-07','ultrices.a@turpis.org','1234567890','1996-10-02',NULL,'Donec'),('72bb64d5-0e86-c30a-0571-7774d93e7158','Kasper','Stokes','+7 (948) 606-55-96','Mauris.quis.turpis@nulla.co.uk','1234567890','2017-11-08',NULL,'facilisis lorem tristique aliquet. Phasellus'),('74bcd9ce-a390-2b6c-6be1-bd72b3236625','Salvador','Simon','+7 (342) 513-66-50','Cum.sociis.natoque@quis.edu','1234567890','2021-09-21',NULL,'Morbi metus.'),('79216569-96bc-ae30-6593-3e89324b9c9b','Aaron','Sanders','+7 (352) 852-37-61','Curae.Donec.tincidunt@miDuis.org','1234567890','2010-02-15',NULL,'dictum magna. Ut tincidunt orci'),('7b7b7e5d-8532-6136-afbe-944795dffbdb','Sophia','Giles','+7 (491) 214-91-44','bibendum.sed.est@semconsequatnec.ca','1234567890','2016-12-22',NULL,'id, libero. Donec consectetuer mauris'),('7f1331d8-c0b1-e7df-5c22-7d4d191816fb','Ann','Sweet','+7 (943) 844-94-68','nascetur@lobortisaugue.com','1234567890','2001-12-22',NULL,'lectus. Cum sociis'),('83d20c26-ee8c-7173-0e90-0f477965f5e2','Lacota','Hurst','+7 (939) 663-56-65','fermentum@nascetur.edu','1234567890','2021-12-16',NULL,'quis turpis vitae purus gravida'),('95ffd5f0-8641-4bed-43cb-19d6377635e8','Brady','Buchanan','+7 (930) 595-25-47','mauris.elit@Donecfeugiatmetus.com','1234567890','2018-03-12',NULL,'ultrices, mauris'),('98470f9e-b79f-de78-420a-92dde94027e3','Aladdin','Nixon','+7 (909) 134-68-63','arcu.imperdiet.ullamcorper@posuerecubiliaCurae.ca','1234567890','2004-10-30',NULL,'Curabitur ut odio vel'),('a5314d67-a757-6863-b34b-ff2008291d09','Sarah','Rodriguez','+7 (816) 676-48-86','elit.a.feugiat@enimmitempor.co.uk','1234567890','1997-05-08',NULL,'iaculis aliquet diam. Sed diam'),('b3061765-1165-1703-ad4e-f68d1ae3c45e','Adam','Briggs','+7 (901) 184-35-91','mollis.Integer@euismodenim.ca','1234567890','2015-07-02',NULL,'ac turpis egestas. Aliquam fringilla'),('b92e2fb1-4e44-e696-3b9a-59920d14f1eb','Quail','Mooney','+7 (961) 838-06-76','lacus@elit.org','1234567890','1996-01-16',NULL,'in'),('bf641282-fb0a-0e9e-8ca1-993a620f07a8','Karleigh','Johnston','+7 (862) 778-40-14','eget@vulputaterisusa.com','1234567890','2021-11-26',NULL,'ac ipsum.'),('c1244324-21c8-08f2-2143-dfffc45ca188','Merrill','Davenport','+7 (811) 688-12-47','enim.Suspendisse@semelitpharetra.edu','1234567890','2012-02-25',NULL,'sed, facilisis vitae, orci. Phasellus'),('c5417edc-e5ba-a496-386f-8c16a5011b9a','Benjamin','Gross','+7 (491) 466-76-16','dis.parturient.montes@sociis.edu','1234567890','2011-10-25',NULL,'elementum sem, vitae aliquam'),('c78d02b8-3968-4290-6329-4ba54164f0d7','Stuart','Campos','+7 (833) 817-38-94','bibendum.sed.est@etultricesposuere.ca','1234567890','2014-06-15',NULL,'dis parturient montes, nascetur'),('cac44653-caf0-c4a7-d59e-bd05db996f68','Cullen','Jennings','+7 (841) 125-84-06','sed.dui.Fusce@interdumfeugiatSed.com','1234567890','2021-10-13',NULL,'diam. Proin dolor. Nulla semper'),('ccd501d9-e43b-4ed6-a66c-3fb9bcd540b2','Eaton','Mcguire','+7 (817) 144-28-16','et@Inat.net','1234567890','1996-06-16',NULL,'ornare, lectus ante dictum mi,'),('e073c424-9b95-6140-2008-ce6ed4e8634a','Ivy','Kelly','+7 (833) 344-31-72','convallis@Proin.org','1234567890','2002-03-17',NULL,'commodo ipsum. Suspendisse non'),('e20b55e0-95c1-c919-b087-3edf6dc7299a','Calvin','Haney','+7 (411) 939-04-99','orci.lacus@Maecenasmi.ca','1234567890','2004-03-19',NULL,'neque. Sed eget lacus. Mauris'),('e5638b95-dc33-6027-4ddd-d38b5d2b1fc5','Channing','Carney','+7 (426) 644-09-26','dapibus.gravida.Aliquam@nec.ca','1234567890','1997-12-15',NULL,'mus. Donec dignissim magna a'),('eaf83b52-85f9-a309-ce29-babe5d67ab7f','Aladdin','Jefferson','+7 (975) 803-29-13','Lorem@turpisvitae.com','1234567890','1999-08-23',NULL,'erat vel pede blandit congue.'),('f870ca8f-a26a-845b-b0ea-04fb82cbe7cc','Ethan','Welch','+7 (365) 456-12-24','odio.Phasellus@sapien.ca','1234567890','1997-01-20',NULL,'convallis est, vitae'),('fe2c4909-fab8-1876-8af1-ae370c851aaa','Jeanette','Moss','+7 (905) 421-43-75','molestie.in@risusquis.com','1234567890','2019-04-04',NULL,'magna tellus faucibus'),('ffaa0392-29f3-d976-cef5-4e82bf065613','Isaac','Mckinney','+7 (901) 292-36-08','Nullam.enim@luctusfelis.com','1234567890','2018-04-18',NULL,'eu');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
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
