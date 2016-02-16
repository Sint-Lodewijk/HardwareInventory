/*
Created: 13/01/2016
Modified: 26/01/2016
Model: MySQL 5.6
Database: MySQL 5.6
*/


CREATE DATABASE HardwareInventory
-- Create tables section -------------------------------------------------

-- Table hardware

CREATE TABLE `hardware`
(
  `purchaseDate` Date,
  `serialNr` Varchar(40) NOT NULL,
  `internalNr` Varchar(30) NOT NULL,
  `warranty` Varchar(50),
  `extraInfo` Varchar(128),
  `manufacturerName` Varchar(30),
  `addedDate` Date,
  `pictureLocation` Varchar(150),
  `typeNr` Int,
  `attachmentLocation` Varchar(150),
  `eventID` Int,
  `modelNr` Varchar(50)
)
;

CREATE INDEX `IX_Relationship22` ON `hardware` (`typeNr`)
;

CREATE INDEX `hHt` ON `hardware` (`eventID`)
;

ALTER TABLE `hardware` ADD  PRIMARY KEY (`serialNr`,`internalNr`)
;

-- Table people

CREATE TABLE `people`
(
  `nameAD` Varchar(100) NOT NULL,
  `eventID` Int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`eventID`)
)
;

-- Table type

CREATE TABLE `type`
(
  `typeNr` Int NOT NULL AUTO_INCREMENT,
  `type` Varchar(50),
  PRIMARY KEY (`typeNr`)
)
;

-- Table license

CREATE TABLE `license`
(
  `licenseName` Varchar(40),
  `licenseCode` Varchar(150) NOT NULL
)
;

ALTER TABLE `license` ADD  PRIMARY KEY (`licenseCode`)
;

-- Table archive

CREATE TABLE `archive`
(
  `person` Varchar(20),
  `id` Int NOT NULL AUTO_INCREMENT,
  `serialNr` Varchar(40),
  `internalNr` Varchar(30),
  `eventID` Int,
  `assignedDate` Date,
  `returnedDate` Date,
  PRIMARY KEY (`id`),

 UNIQUE `id` (`id`)
)
;

CREATE INDEX `IX_Relationship1` ON `archive` (`serialNr`,`internalNr`)
;

CREATE INDEX `IX_Relationship2` ON `archive` (`eventID`)
;

-- Table licenseHandler

CREATE TABLE `licenseHandler`
(
  `licenseEventID` Int NOT NULL AUTO_INCREMENT,
  `serialNr` Varchar(40),
  `internalNr` Varchar(30),
  `licenseCode` Varchar(150),
  `eventID` Int,
  PRIMARY KEY (`licenseEventID`)
)
;

CREATE INDEX `IX_Relationship1` ON `licenseHandler` (`serialNr`,`internalNr`)
;

CREATE INDEX `IX_Relationship2` ON `licenseHandler` (`licenseCode`)
;

CREATE INDEX `IX_Relationship3` ON `licenseHandler` (`eventID`)
;

-- Create relationships section ------------------------------------------------- 

ALTER TABLE `hardware` ADD CONSTRAINT `hardwareWithType` FOREIGN KEY (`typeNr`) REFERENCES `type` (`typeNr`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `hardware` ADD CONSTRAINT `hHp` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `archive` ADD CONSTRAINT `hardwareArchive` FOREIGN KEY (`serialNr`, `internalNr`) REFERENCES `hardware` (`serialNr`, `internalNr`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `archive` ADD CONSTRAINT `peopleArchive` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `licenseHandler` ADD CONSTRAINT `licenseSaveHardware` FOREIGN KEY (`serialNr`, `internalNr`) REFERENCES `hardware` (`serialNr`, `internalNr`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `licenseHandler` ADD CONSTRAINT `Relationship2` FOREIGN KEY (`licenseCode`) REFERENCES `license` (`licenseCode`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `licenseHandler` ADD CONSTRAINT `licenseSavePeople` FOREIGN KEY (`eventID`) REFERENCES `people` (`eventID`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE type AUTO_INCREMENT = 0;