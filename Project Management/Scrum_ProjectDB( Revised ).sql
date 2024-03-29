
USE master
-- Can set up user authentication in the SQL files, but I'm not to familiar with how that looks, 
    -- If anyone wants to take a stab at it. C# Has Some good Authorization
  
DROP DATABASE IF EXISTS ScrumProject;
GO

CREATE DATABASE ScrumProject
GO

-- I don't think 'Customers' should have a login, I don't see why if it's an AutoRepair Shop. 
    -- So, that leaves Technicians, Administrators & General Manager.



USE ScrumProject

CREATE TABLE UserTypes (
    UserTypeID INT PRIMARY KEY IDENTITY  NOT NULL,
    Description TEXT NOT NULL
); -- 'Technician', 'Administrator', 'General Manager'

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
); -- 'New Hire', 'Client'.


CREATE TABLE Appointments(
    AppointmentID INT PRIMARY KEY NOT NULL,
    RequestDate DATETIME NOT NULL,
    AppointmentTypeID INT NOT NULL,
    FOREIGN KEY (AppointmentTypeID) REFERENCES AppointmentType(AppointmentTypeID),
);

-- Option #1 Depending on how many images we plan on adding, thought having a tag name for a set of photos would make querying images easier.
    -- That is if the plan is to store the image files in SQL VARBINARY.
-- Option #2 Perhaps a Resources Directory, then do it HTML style in Visual Studio might suffice,the pictures i think of dont seem confidential. 
CREATE TABLE ImageTags(
    ImageTagID INT PRIMARY KEY IDENTITY NOT NULL,
    ImageTag VARCHAR(15) NOT NULL
);

CREATE TABLE Images(
    ImageID INTEGER PRIMARY KEY IDENTITY NOT NULL,
    ImageSrc VARBINARY(MAX) NOT NULL,
    ImageAlt VARCHAR(MAX) NOT NULL,
    ImageCaption VARCHAR(MAX) -- ALLOW NULL: May be unecessary because not all images will require a caption, unless someone disagrees, then just add NOT NULL.
); 



CREATE TABLE Jobs (
    JobID INT PRIMARY KEY IDENTITY NOT NULL,
    TypeDescription VARCHAR(255) NOT NULL,
    ImageID INT NOT NULL,
);

-- DML Statments --
GO
USE ScrumProject;

-- I think having an ImageTags Table would make querying much easier, 
    -- On the other hand, i can't see the sight having too many images. 
    -- Example psuedocode: Where (i => i.Tag == 'Technician' OR 'General Manager' OR 'Administration')

ALTER TABLE Images  
    ADD ImageTagID INT NOT NULL
    REFERENCES ImageTags(ImageTagID);

ALTER TABLE Users
    ADD UserTypeID INT NOT NULL
    REFERENCES UserTypes(UserTypeID);

INSERT INTO ImageTags (ImageTag)
VALUES ('Technician'), ('Administrator'), ('Manager'), ('Workshop');

INSERT INTO AppointmentType (Description)
VALUES ('New Hire'), ('Client');

USE ScrumProject;
INSERT INTO UserTypes (Description) 
VALUES ('Technician'), ( 'Administration'), ('General Manager');






