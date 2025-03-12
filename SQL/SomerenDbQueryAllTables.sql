DROP TABLE DEELNEMER;
DROP TABLE BEGELEIDER;
DROP TABLE ACTIVITEIT;
DROP TABLE BESTELLING;
DROP TABLE DRANKJE;
DROP TABLE DOCENT;
DROP TABLE STUDENT;
DROP TABLE SLAAPKAMER;

--Create tables

CREATE TABLE SLAAPKAMER(
	kamernr nchar(5) NOT NULL PRIMARY KEY,
	aantal_slaapplekken int NOT NULL,
	type_kamer bit NOT NULL
);

CREATE TABLE STUDENT(
	studentennr int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	voornaam varchar(25) NOT NULL,
	achternaam varchar(25) NOT NULL,
	telefoonnr varchar(14) NOT NULL,
	klas varchar(14) NOT NULL,
	kamernr nchar(5) NOT NULL FOREIGN KEY REFERENCES SLAAPKAMER(kamernr)
);

CREATE TABLE DOCENT(
	docentnr int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	voornaam varchar(25) NOT NULL,
	achternaam varchar(25) NOT NULL,
	telefoonnr varchar(14) NOT NULL,
	leeftijd int NOT NULL,
	kamernr nchar(5) NOT NULL FOREIGN KEY REFERENCES SLAAPKAMER(kamernr)
);

CREATE TABLE DRANKJE(
	drankid int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	dranknaam varchar(15) NOT NULL,
	aantal int NOT NULL,
	alcoholisch bit NOT NULL,
	prijs money NOT NULL
);

CREATE TABLE BESTELLING(
	studentennr int NOT NULL FOREIGN KEY REFERENCES STUDENT(studentennr),
	drankid int NOT NULL FOREIGN KEY REFERENCES DRANKJE(drankid),
	aantal int NOT NULL,
	PRIMARY KEY(studentennr, drankid)
);

CREATE TABLE ACTIVITEIT(
	activiteitid int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	naam varchar(25) NOT NULL,
	starttijd datetime NOT NULL,
	eindtijd datetime NOT NULL
);

CREATE TABLE BEGELEIDER(
	docentnr int NOT NULL FOREIGN KEY REFERENCES DOCENT(docentnr),
	activiteitid int NOT NULL FOREIGN KEY REFERENCES ACTIVITEIT(activiteitid),
	PRIMARY KEY(docentnr, activiteitid)
);

CREATE TABLE DEELNEMER(
	studentennr int NOT NULL FOREIGN KEY REFERENCES STUDENT(studentennr),
	activiteitid int NOT NULL FOREIGN KEY REFERENCES ACTIVITEIT(activiteitid),
	PRIMARY KEY(studentennr, activiteitid)
);

