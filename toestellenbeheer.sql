/*
Created: 13/01/2016
Modified: 21/01/2016
Model: MySQL 5.6
Database: MySQL 5.6
*/


-- Create tables section -------------------------------------------------

-- Table hardware

CREATE TABLE `hardware`
(
  `purchaseDate` Date,
  `serialNr` Varchar(30) NOT NULL,
  `internalNr` Varchar(30) NOT NULL,
  `warranty` Char(20) NOT NULL,
  `extraInfo` Varchar(128),
  `manufacturerName` Varchar(30),
  `addedDate` Date,
  `pictureLocation` Varchar(150),
  `typeNr` Int,
  `attachmentLocation` Varchar(150),
  `licenseCode` Varchar(150)
)
;

CREATE INDEX `IX_Relationship22` ON `hardware` (`typeNr`)
;

CREATE INDEX `IX_Relationship1` ON `hardware` (`licenseCode`)
;

ALTER TABLE `hardware` ADD  PRIMARY KEY (`serialNr`,`internalNr`)
;

-- Table people

CREATE TABLE `people`
(
  `nameAD` Varchar(100) NOT NULL,
  `serialNr` Varchar(30),
  `internalNr` Varchar(30),
  `licenseCode` Varchar(150)
)
;

CREATE INDEX `IX_Relationship29` ON `people` (`serialNr`,`internalNr`)
;

CREATE INDEX `IX_Relationship7` ON `people` (`licenseCode`)
;

ALTER TABLE `people` ADD  PRIMARY KEY (`nameAD`)
;

-- Table type

CREATE TABLE `type`
(
  `typeNr` Int NOT NULL,
  `type` Varchar(50)
)
;

ALTER TABLE `type` ADD  PRIMARY KEY (`typeNr`)
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
  `internaNr` Varchar(30) NOT NULL,
  `serialNr` Varchar(20) NOT NULL,
  `id` Int NOT NULL
)
;

ALTER TABLE `archive` ADD  PRIMARY KEY (`id`)
;

ALTER TABLE `archive` ADD UNIQUE `id` (`id`)
;

-- Create relationships section ------------------------------------------------- 

ALTER TABLE `hardware` ADD CONSTRAINT `hHT` FOREIGN KEY (`typeNr`) REFERENCES `type` (`typeNr`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `people` ADD CONSTRAINT `hWp` FOREIGN KEY (`serialNr`, `internalNr`) REFERENCES `hardware` (`serialNr`, `internalNr`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `people` ADD CONSTRAINT `peopleLicenseCode` FOREIGN KEY (`licenseCode`) REFERENCES `license` (`licenseCode`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

ALTER TABLE `hardware` ADD CONSTRAINT `hardwareLicense` FOREIGN KEY (`licenseCode`) REFERENCES `license` (`licenseCode`) ON DELETE RESTRICT ON UPDATE RESTRICT
;

