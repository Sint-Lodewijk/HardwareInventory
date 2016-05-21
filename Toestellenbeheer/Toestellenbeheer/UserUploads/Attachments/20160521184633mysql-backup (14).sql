-- MySqlBackup.NET 2.0.9.2
-- Dump Time: 2016-05-16 13:56:28
-- --------------------------------------
-- Server version 5.7.11-log MySQL Community Server (GPL)

-- 
-- Create schema HardwareInventory
-- 

CREATE DATABASE IF NOT EXISTS `HardwareInventory` /*!40100 DEFAULT CHARACTER SET latin1 */;
Use `HardwareInventory`;



/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES latin1 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of DBAccount
-- 

DROP TABLE IF EXISTS `DBAccount`;
CREATE TABLE IF NOT EXISTS `DBAccount` (
  `UserName` varchar(30) CHARACTER SET latin1 NOT NULL,
  `PassHash` varchar(100) CHARACTER SET latin1 NOT NULL,
  `UserGroup` varchar(30) CHARACTER SET latin1 NOT NULL DEFAULT 'gg_hardware_user',
  `ADAccount` varchar(50) CHARACTER SET latin1 DEFAULT NULL,
  PRIMARY KEY (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- 
-- Dumping data for table DBAccount
-- 

/*!40000 ALTER TABLE `DBAccount` DISABLE KEYS */;
INSERT INTO `DBAccount`(`UserName`,`PassHash`,`UserGroup`,`ADAccount`) VALUES
('jhli','ef51306214d9a6361ee1d5b452e6d2bb70dc7ebb85bf9e02c3d4747fb57d6bec','gg_hardware_user','jhli'),
('Jianing','e2e3650e7e61d661007d536f94f56ed065170ec303d2160407b3d7664f226c6a','gg_hardware_admin','jli'),
('jli','ef51306214d9a6361ee1d5b452e6d2bb70dc7ebb85bf9e02c3d4747fb57d6bec','gg_hardware_user','jli');
/*!40000 ALTER TABLE `DBAccount` ENABLE KEYS */;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table license
-- 

/*!40000 ALTER TABLE `license` DISABLE KEYS */;

/*!40000 ALTER TABLE `license` ENABLE KEYS */;

-- 
-- Definition of licenseHandler
-- 

DROP TABLE IF EXISTS `licenseHandler`;
CREATE TABLE IF NOT EXISTS `licenseHandler` (
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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 
-- Dumping data for table licenseHandler
-- 

/*!40000 ALTER TABLE `licenseHandler` DISABLE KEYS */;

/*!40000 ALTER TABLE `licenseHandler` ENABLE KEYS */;

-- 
-- Definition of manufacturer
-- 

DROP TABLE IF EXISTS `manufacturer`;
CREATE TABLE IF NOT EXISTS `manufacturer` (
  `manufacturerName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`manufacturerName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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


-- Dump completed on 2016-05-16 13:56:38
-- Total time: 0:0:0:10:68 (d:h:m:s:ms)


<!DOCTYPE html>
<html lang="en">
<head><meta charset="utf-8" /><meta name="viewport" content="width=device-width, initial-scale=1.0" /><meta name="theme-color" content="#F8F8F8" /><title>
	Manage database | Hardware inventory management software
</title><script src="/Scripts/modernizr-2.8.3.js"></script>
<link href="/Content/bootstrap.css" rel="stylesheet"/>
<link href="/Content/Site.css" rel="stylesheet"/>
<link href="../favicon.ico" rel="shortcut icon" type="image/x-icon" /></head>
<body>
    <script src='/Scripts/jquery-2.2.3.min.js'></script>
    <script src='/Scripts/jquery-ui.js'></script>
    <script src='/Scripts/bootstrap.min.js'></script>
    <script src='/Scripts/jquery.tablesorter.min.js'></script>
    <script src='/Scripts/bootstrap-datepicker.min.js'></script>
    <script src='/Scripts/jquery.tablesorter.widgets.js'></script>
    <script src='/Scripts/loading.min.js'></script>
    <link rel="stylesheet" href='/Scripts/jquery-ui.css' />
    <link rel="stylesheet" href='/Content/bootstrap-datepicker3.min.css' />

    <form method="post" action="./manage-database" id="ctl01" enctype="multipart/form-data">
<div class="aspNetHidden">
<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="SNWipNjHKCPdHsC33x/x0mxrVZ0OVwKbEWrTaFTahOn3O52Ke5S1ttb0pwAah9CWXRt5WOkTFOyKLIOIBw6FMMbMpcorLFVYF2CfuwdRnCjGDRevEquav4iNwcvbG89oveQVR1exc5RDqK6zOTd7sdXLIfO8Km1VX/tTWGhgXBTYi+1cIyzOVchn75FIHX6vohWCSj1cjHtfoLMDK/FdU3I6RseTSKO4ylYQ0lbkpDYIzl3MNXCYl7CdVldgczTi2Q0oV0bhR5P3PQcr7ADTeANtAs9211IqYI0h1vyaud32JcNHeKYtDr6wN9i5jEonJct2+vVWih82CJxtXZTb1OHWWMzuvsNCpy13eh1qL9/pQhZ7C+ASHDNfc29pFMMYdD7Lw0jscPhS8k8Rz4lJPQ==" />
</div>

<script type="text/javascript">
//<![CDATA[
var theForm = document.forms['ctl01'];
if (!theForm) {
    theForm = document.ctl01;
}
function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}
//]]>
</script>



<script src="/bundles/MsAjaxJs?v=D6VN0fHlwFSIWjbVzi6mZyE9Ls-4LNrSSYVGRU46XF81" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[
if (typeof(Sys) === 'undefined') throw new Error('ASP.NET Ajax client-side framework failed to load.');
//]]>
</script>

<script src="../Scripts/respond.js" type="text/javascript"></script>
<script src="/bundles/WebFormsJs?v=N8tymL9KraMLGAMFuPycfH3pXe6uUlRXdhtYv8A_jUU1" type="text/javascript"></script>
        <script type="text/javascript">
//<![CDATA[
Sys.WebForms.PageRequestManager._initialize('ctl00$ctl09', 'ctl01', ['tctl00$MainContent$udpSuccess','MainContent_udpSuccess'], [], [], 90, 'ctl00');
//]]>
</script>

        <script type="text/javascript">
            $(function () {
                function stripTrailingSlash(str) {
                    if (str.substr(-1) == '/') {
                        return str.substr(0, str.length - 1);
                    }
                    return str;
                }
                var url = window.location.pathname;
                var activePage = ".." + stripTrailingSlash(url);
                $('.nav li a').each(function () {
                    var currentPage = stripTrailingSlash($(this).attr('href'));
                    if (activePage == currentPage) {
                        $(this).parent().addClass('active');
                    }
                });
            });
        </script>
        <div class="navbar navbar-default navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a href="../Default.aspx" class="pull-left">
                        <img src="../Images/brand-image.png" alt="Hardware Inventory" class="brand-image" />
                    </a>
                </div>
                <div class="navbar-collapse collapse">
                    
                                    <ul class="nav navbar-nav">
                                        <li class="dropdown">
                                            <a href="../Overview" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Overview <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li class="text-center"><a href='/Overview/hardware-overview'>Hardware Overview</a></li>
                                                <li class="text-center"><a href='/Overview/license-overview'>License Overview</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Overview'>Overview</a></li>
                                            </ul>
                                        </li>
                                        <li class="dropdown">
                                            <a href="../Add" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Add <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li class="text-center"><a href='/Add/add-hardware'>Add hardware</a></li>
                                                <li class="text-center"><a href='/Add/add-license'>Add license</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Add/add-person'>Add AD User</a></li>
                                                <li class="text-center"><a href='/Add/add-db-user'>Add DB User</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Add'>Overview</a></li>
                                            </ul>
                                        </li>
                                        <li class="dropdown">
                                            <a href="../Archive" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Archive <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li class="text-center"><a href='/Archive/hardware-history'>Hardware history</a></li>
                                                <li class="text-center"><a href='/Archive/people-history'>People history</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Archive'>Overview</a></li>
                                            </ul>
                                        </li>
                                        <li class="dropdown">
                                            <a href="../User" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">MySpace <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li class="text-center"><a href='/User/my-hardware'>My hardware</a></li>
                                                <li class="text-center"><a href='/User/my-license'>My license</a></li>
                                                <li class="text-center"><a href='/User/request-hardware'>Request hardware</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/User'>Overview</a></li>
                                            </ul>
                                        </li>
                                    </ul>
                                    <ul class="nav navbar-nav navbar-right">
                                        <li><a href="manage-requests" class="OpenRequest"><span class="badge">
                                            <span id="login_lblOpenRequest">0</span></span> &nbsp;requests
                                        </a></li>
                                        <li class="dropdown">
                                            <a href='/Manage' class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Manage, jli ! <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li class="text-center"><a href='/Manage/assign-hardware'>Assign hardware</a></li>
                                                <li class="text-center"><a href='/Manage/assign-license'>Assign license</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Manage/manage-manufacturer'>Manage manufacturer</a></li>
                                                <li class="text-center"><a href='/Manage/manage-type'>Manage type</a></li>
                                                <li class="text-center"><a href='/Manage/manage-requests'>Manage requests</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Manage/return-hardware'>Return hardware</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Manage/manage-database'>Manage database</a></li>
                                                <li role="separator" class="divider"></li>
                                                <li class="text-center"><a href='/Manage'>Overview</a></li>
                                            </ul>
                                        </li>
                                        <li>
                                            <a href="javascript:__doPostBack(&#39;ctl00$login$ctl06$ctl00&#39;,&#39;&#39;)">Log off</a>
                                        </li>
                                    </ul>
                                
                </div>
            </div>
        </div>

        

        <div class="container body-content">
            <script src='/Scripts/alert.js'></script>
            <div id="alert_placeholder" style="z-index: 9999999;"></div>
            
    <div id="MainContent_udpSuccess">
	
            <div class="alert alert-danger fade in" id="successMessageAlert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <span id="MainContent_lblAlert">Please note! This feature is still experimental, the restore function may not working proberly.</span>
            </div>
        
</div>
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#backup" aria-controls="backup" role="tab" data-toggle="tab">Backup database</a></li>
        <li role="presentation"><a href="#restore" aria-controls="restore" role="tab" data-toggle="tab">Restore database</a></li>
        <li role="presentation"><a href="#destroy" aria-controls="destroy" role="tab" data-toggle="tab">Destroy database</a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="backup">
            <h3>Backup</h3>
            <p>Back up the database regularly on preventing data lose.</p>
            <input type="submit" name="ctl00$MainContent$btnBackup" value="Backup" id="MainContent_btnBackup" class="btn btn-primary" />
        </div>
        <div role="tabpanel" class="tab-pane" id="restore">
            <h3>Restore</h3>
            <p>Restore the database from the sql file.</p>
            <div class="input-group">
                <input type="file" name="ctl00$MainContent$fileRestore" id="MainContent_fileRestore" class="form-control" />
                <span class="input-group-btn">
                    <input type="submit" name="ctl00$MainContent$btnRestore" value="Restore" id="MainContent_btnRestore" class="btn btn-primary" />
                </span>
            </div>
            <!-- /input-group -->
        </div>
        <div role="tabpanel" class="tab-pane" id="destroy">
            <h3>Truncate database</h3>
            <p>Truncate all content in the database!</p>
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#confirmTruncateModal">
                Truncate database
            </button>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="confirmTruncateModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="confirmTruncateTitle">Confirm truncate</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <p>Are you sure to truncate the database? This action is not reversible, but you can still restore the previous database with the restore function.</p>
                        <p>Continue to proceed?</p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <input type="submit" name="ctl00$MainContent$btnTruncate" value="Truncate all tables" id="MainContent_btnTruncate" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>

            <hr />
            <footer class="footer">
                <p>Hardware inventory management software - Jianing - 2016 </p>
            </footer>
        </div>
    
<div class="aspNetHidden">

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="+A9JT3adsYscmdSfCrRJa9wYffXNrTPokR2KTvXvTLo5SZTCfU7kF/ObLGm0cTetwsr4ivQqXQoMxiVVIWkg/AzqdKx9ymaqFz48WZFj/JiFU46wO33J9dwV0gIhF8AIOFLW7o6iBHC88i74qLKneVLNELJqujBQNfSCLHOIqpEdyCURnCY2ElY8+Jgv4OPe" />
</div>

<script type="text/javascript">
//<![CDATA[
$(function () { showalert('Backup successfully','alert-success'); });//]]>
</script>
</form>
</body>
</html>
