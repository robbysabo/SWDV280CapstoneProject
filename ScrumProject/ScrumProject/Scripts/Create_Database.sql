
use master;
GO
  
DROP DATABASE IF EXISTS ScrumProject;
GO

CREATE DATABASE ScrumProject
GO

SET ANSI_NULLS ON

USE ScrumProject
select * from WorkerImages;
CREATE TABLE UserTypes (
    UserTypeID INT PRIMARY KEY IDENTITY  NOT NULL,
    Description TEXT NOT NULL
); 

CREATE TABLE Users (
    UserID INT PRIMARY KEY NOT NULL,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Email VARCHAR(80) NOT NULL,
    Password VARCHAR(50) NOT NULL,
);

CREATE TABLE AppointmentType (
    AppointmentTypeID INT PRIMARY KEY IDENTITY NOT NULL,
    Description TEXT NOT NULL,
); 


CREATE TABLE Appointments(
    AppointmentID INT PRIMARY KEY NOT NULL,
    RequestDate DATETIME NOT NULL,
    AppointmentTypeID INT NOT NULL,
    FOREIGN KEY (AppointmentTypeID) REFERENCES AppointmentType(AppointmentTypeID),
);

CREATE TABLE WorkerImages(
    ImageID INTEGER PRIMARY KEY IDENTITY NOT NULL,
    ImageSrc VARCHAR(MAX) NOT NULL,
	EmployeeName VARCHAR(MAX),
    ImageAlt VARCHAR(MAX) NOT NULL,
    Description VARCHAR(MAX)
);

INSERT INTO WorkerImages (ImageSrc, EmployeeName, ImageAlt, Description)
Values('images/owner.jpg', 'Scott', 'Shop owner', 'Founder & Master Craftsman With a lifelong passion for automotive repair, Mr. Rockefeller brings unparalleled expertise and a creative vision to Scotts auto repair . From conceptualization to execution, He oversees every project with precision and finesse, setting the standard for excellence in our industry.'),
('images/tech1.jpg', 'Abby', 'Repair technician', 'Repair Tech As a seasoned diagnosis, repair and maintenance technician with foundational technical expertise. Natalie brings a unique perspective to our team. With a keen attention to detail and a passion for innovation, she plays a crucial role in ensuring that every project is executed flawlessly.'),
('images/tech2.jpg', 'John', 'Lead technician', 'Lead Tech Joe has 30 years of experience in automotive repair. Whether its oil change, brake service, rotation and balance, engine tune-up, and cooling system service. Joe approaches every task with unwavering dedication and a commitment to exceeding expectations.');

CREATE TABLE Jobs (
    JobID INT PRIMARY KEY IDENTITY NOT NULL,
    TypeDescription VARCHAR(255) NOT NULL,
    ImageID INT NOT NULL,
);
GO
-- DML
USE ScrumProject;

ALTER TABLE Users
    ADD UserTypeID INT NOT NULL
    REFERENCES UserTypes(UserTypeID);


INSERT INTO AppointmentType (Description)
VALUES ('New Hire'), ('Client');

USE ScrumProject;
INSERT INTO UserTypes (Description) 
VALUES ('Technician'), ( 'Administration'), ('General Manager');


