USE master
GO

IF DB_ID('ScrumProject') IS NOT NULL
    DROP DATABASE ScrumProject
GO

--appointments table, no dependencies
CREATE TABLE Appointments (
	appt_id INT PRIMARY KEY IDENTITY,
	req_date DATETIME NOT NULL,
	type VARCHAR(255) NOT NULL,
	description VARCHAR(255) NOT NULL, --should we make optional?
	contact_email VARCHAR(80) NOT NULL,
	contact_phone VARCHAR(10) NOT NULL,
	appt_stat CHAR(1) --' '/null: open, 'C' complete, 'V' void 
);

--users table, no dependencies
CREATE TABLE Users (
	u_id INT PRIMARY KEY IDENTITY, --I think user_id is a reserved word
	user_type CHAR(1) NOT NULL, --'E': employee, 'C': customer, A:admn
	f_name VARCHAR(255) NOT NULL,
	l_name VARCHAR(255) NOT NULL,
	email VARCHAR(80),
	pass VARCHAR(50) NOT NULL --we can do validation in .NET
);

--images table, no dependencies
CREATE TABLE Images (
	img_id INT PRIMARY KEY IDENTITY,
	img_file NVARCHAR(MAX)
);

--jobs table, two dependencies
CREATE TABLE Jobs (
	job_id INT PRIMARY KEY IDENTITY,
	description VARCHAR(255) NOT NULL,
	img_id INT NOT NULL REFERENCES Images(img_id), --fk to images
	u_id INT NOT NULL REFERENCES Users(u_id) --fk to users
);