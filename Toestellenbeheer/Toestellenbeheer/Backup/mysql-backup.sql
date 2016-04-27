-- MySqlBackup.NET 2.0.9.2
-- Dump Time: 2016-04-27 15:41:11
-- --------------------------------------
-- Server version 5.7.11-log MySQL Community Server (GPL)

-- 
-- Create schema hardwareinventory
-- 

CREATE DATABASE IF NOT EXISTS `hardwareinventory` /*!40100 DEFAULT CHARACTER SET utf8 */;
Use `hardwareinventory`;



/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of archive
-- 

DROP TABLE IF EXISTS `archive`;
CREATE TABLE IF NOT EXISTS `archive` (
  `person` varchar(20) DEFAULT NULL,
  `archiveID` int(11) NOT NULL AUTO_INCREMENT,
  `serialNr` varchar(60) DEFAULT NULL,
  `internalNr` varchar(60) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `eventID` int(11) DEFAULT NULL,
  `assignedDate` date DEFAULT NULL,
  `returnedDate` date DEFAULT NULL,
  PRIMARY KEY (`archiveID`),
  KEY `IX_Relationship1` (`serialNr`,`internalNr`),
  KEY `IX_Relationship2` (`eventID`),
  CONSTRAINT `hardwareArchive` FOREIGN KEY (`serialNr`, `internalNr`) REFERENCES `hardware` (`serialNr`, `internalNr`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `peopleArchive` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table archive
-- 

/*!40000 ALTER TABLE `archive` DISABLE KEYS */;

/*!40000 ALTER TABLE `archive` ENABLE KEYS */;

-- 
-- Definition of hardware
-- 

DROP TABLE IF EXISTS `hardware`;
CREATE TABLE IF NOT EXISTS `hardware` (
  `purchaseDate` date DEFAULT NULL,
  `serialNr` varchar(60) NOT NULL,
  `internalNr` varchar(60) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `warranty` varchar(50) DEFAULT NULL,
  `extraInfo` varchar(128) DEFAULT NULL,
  `addedDate` date DEFAULT NULL,
  `pictureLocation` varchar(100) DEFAULT NULL,
  `attachmentLocation` varchar(100) DEFAULT NULL,
  `eventID` int(11) DEFAULT NULL,
  `modelNr` varchar(50) DEFAULT NULL,
  `type` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `manufacturerName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`serialNr`,`internalNr`),
  KEY `IX_Relationship22` (`type`),
  KEY `hHt` (`eventID`),
  KEY `IX_Relationship3` (`manufacturerName`),
  CONSTRAINT `hHp` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`),
  CONSTRAINT `hardwareManufacturer` FOREIGN KEY (`manufacturerName`) REFERENCES `manufacturer` (`manufacturerName`) ON UPDATE CASCADE,
  CONSTRAINT `hardwareWithType` FOREIGN KEY (`type`) REFERENCES `type` (`type`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table hardware
-- 

/*!40000 ALTER TABLE `hardware` DISABLE KEYS */;

/*!40000 ALTER TABLE `hardware` ENABLE KEYS */;

-- 
-- Definition of license
-- 

DROP TABLE IF EXISTS `license`;
CREATE TABLE IF NOT EXISTS `license` (
  `licenseName` varchar(40) DEFAULT NULL,
  `licenseCode` varchar(100) DEFAULT NULL,
  `expireDate` date DEFAULT NULL,
  `licenseFileLocation` varchar(100) DEFAULT NULL,
  `extraInfo` varchar(60) DEFAULT NULL,
  `licenseID` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`licenseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table license
-- 

/*!40000 ALTER TABLE `license` DISABLE KEYS */;

/*!40000 ALTER TABLE `license` ENABLE KEYS */;

-- 
-- Definition of licensehandler
-- 

DROP TABLE IF EXISTS `licensehandler`;
CREATE TABLE IF NOT EXISTS `licensehandler` (
  `licenseEventID` int(11) NOT NULL AUTO_INCREMENT,
  `serialNr` varchar(60) DEFAULT NULL,
  `internalNr` varchar(60) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `eventID` int(11) DEFAULT NULL,
  `licenseID` int(11) DEFAULT NULL,
  PRIMARY KEY (`licenseEventID`),
  KEY `IX_Relationship1` (`serialNr`,`internalNr`),
  KEY `IX_Relationship3` (`eventID`),
  KEY `IX_Relationship2` (`licenseID`),
  CONSTRAINT `licenseHandlerLicense` FOREIGN KEY (`licenseID`) REFERENCES `license` (`licenseID`) ON UPDATE CASCADE,
  CONSTRAINT `licenseSaveHardware` FOREIGN KEY (`serialNr`, `internalNr`) REFERENCES `hardware` (`serialNr`, `internalNr`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `licenseSavePeople` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table licensehandler
-- 

/*!40000 ALTER TABLE `licensehandler` DISABLE KEYS */;

/*!40000 ALTER TABLE `licensehandler` ENABLE KEYS */;

-- 
-- Definition of manufacturer
-- 

DROP TABLE IF EXISTS `manufacturer`;
CREATE TABLE IF NOT EXISTS `manufacturer` (
  `manufacturerName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`manufacturerName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table manufacturer
-- 

/*!40000 ALTER TABLE `manufacturer` DISABLE KEYS */;

/*!40000 ALTER TABLE `manufacturer` ENABLE KEYS */;

-- 
-- Definition of people
-- 

DROP TABLE IF EXISTS `people`;
CREATE TABLE IF NOT EXISTS `people` (
  `nameAD` varchar(40) NOT NULL,
  `eventID` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`eventID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table people
-- 

/*!40000 ALTER TABLE `people` DISABLE KEYS */;

/*!40000 ALTER TABLE `people` ENABLE KEYS */;

-- 
-- Definition of request
-- 

DROP TABLE IF EXISTS `request`;
CREATE TABLE IF NOT EXISTS `request` (
  `requestID` int(11) NOT NULL AUTO_INCREMENT,
  `serialNr` varchar(60) DEFAULT NULL,
  `internalNr` varchar(60) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `eventID` int(11) DEFAULT NULL,
  `requestAccepted` tinyint(1) DEFAULT '0',
  `requestDate` date DEFAULT NULL,
  PRIMARY KEY (`requestID`),
  KEY `IX_Relationship4` (`serialNr`,`internalNr`),
  KEY `IX_Relationship5` (`eventID`),
  CONSTRAINT `hardwareRequest` FOREIGN KEY (`serialNr`, `internalNr`) REFERENCES `hardware` (`serialNr`, `internalNr`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `peopleRequest` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table request
-- 

/*!40000 ALTER TABLE `request` DISABLE KEYS */;

/*!40000 ALTER TABLE `request` ENABLE KEYS */;

-- 
-- Definition of type
-- 

DROP TABLE IF EXISTS `type`;
CREATE TABLE IF NOT EXISTS `type` (
  `type` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table type
-- 

/*!40000 ALTER TABLE `type` DISABLE KEYS */;

/*!40000 ALTER TABLE `type` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2016-04-27 15:41:11
-- Total time: 0:0:0:0:132 (d:h:m:s:ms)
