USE [master]
GO

CREATE DATABASE [pelzerDatabase]; 
GO

USE [pelzerDatabase];
GO
SET QUOTED_IDENTIFIER ON
GO 

CREATE TABLE Users (
    userID int IDENTITY(1,1) PRIMARY KEY,
    userName varchar(255) UNIQUE not null,
    password varchar(255) not null,
    role varchar(255) not null
);

CREATE TABLE Drivers (
    driverID int IDENTITY(1,1) PRIMARY KEY not null,
    fName varchar(255) not null,
    lName varchar(255) not null,
    SSN varchar(9) UNIQUE not null
);

CREATE TABLE Infractions (
    infractionID int IDENTITY(1,1) PRIMARY KEY not null,
    infractionDesc varchar(255) not null,
    infractionCost DECIMAL(19,2) not null
);

CREATE TABLE Vehicles (
    vehicleID int IDENTITY(1,1) PRIMARY KEY,
    make varchar(255) not null,
    model varchar(255) not null,
    liscencePlateNumber varchar(10) not null,
	driverID int not null,
	CONSTRAINT FK_driverID FOREIGN KEY (driverID) REFERENCES Drivers(driverID)
);

CREATE TABLE InfractionDriverLink (
    vehicleID int IDENTITY(1,1) PRIMARY KEY,
    driverID int not null,
	infractionID int not null,
	CONSTRAINT FK_InfractionDriverLink_driverID FOREIGN KEY (driverID) REFERENCES Drivers(driverID),
	CONSTRAINT FK_InfractionDriverLink_infractionID FOREIGN KEY (infractionID) REFERENCES Infractions(infractionID)
);

INSERT INTO Users(userName, password, role)
VALUES ('DMVuser','1234','DMV'),
('policeUser','password','POLICE'),
('johnSmith','v3h!cles','DMV'),
('officer539','p0l!ce','POLICE')
GO

INSERT INTO Infractions(infractionDesc, infractionCost)
VALUES
('Speeding(Over by 10)', 120),
('Speeding(Over by 20)', 250),
('Reckless Driving', 75),
('Driving Under The Influence', 1000)
GO

INSERT INTO Drivers(fName, lName, SSN)
VALUES
('John','Smith','123456789'),
('Jack','Black','234567890'),
('Abby','Stewart','345678901')
GO



INSERT INTO Vehicles(make, model, liscencePlateNumber, driverID)
VALUES
('Dodge','Challenger','QWE464',1),
('Ford','Expidition','DFD191',1),
('Chevy','Silverado','WES365',2)
GO

INSERT INTO InfractionDriverLink(driverID, infractionID)
VALUES
(1, 1),
(1, 2),
(1, 3),
(3, 4)
GO