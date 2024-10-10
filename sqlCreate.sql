CREATE TABLE felhasznalok (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nev VARCHAR(128),
    jelszo VARCHAR(255),
    pontok INT,
    szerepkor VARCHAR(255)
);

CREATE TABLE jatekok (
    id INT PRIMARY KEY AUTO_INCREMENT,
    szervezoid INT,
    nev VARCHAR(255),
    alanyokSzama INT,
    status VARCHAR(255)
);

CREATE TABLE alanyok (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nev VARCHAR(128),
    jatekId INT
);

CREATE TABLE esemenyek (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nev INT,
    jatekId INT
);

CREATE TABLE fogadasok (
    id INT PRIMARY KEY AUTO_INCREMENT,
    felhasznaloid INT,
    jatekId INT,
    alanyId INT,
    esemenyId INT,
    tet INT,
    fogadasErteke VARCHAR(128)
);

CREATE TABLE eredmenyek (
    id INT PRIMARY KEY AUTO_INCREMENT,
    jatekId INT,
    alanyId INT,
    esemenyId INT,
    esemenyErteke VARCHAR(128),
    szorzo FLOAT
);

ALTER TABLE jatekok 
    ADD FOREIGN KEY (szervezoid) REFERENCES felhasznalok(id);

ALTER TABLE alanyok 
    ADD FOREIGN KEY (jatekId) REFERENCES jatekok(id);

ALTER TABLE fogadasok 
    ADD FOREIGN KEY (felhasznaloid) REFERENCES felhasznalok(id),
    ADD FOREIGN KEY (jatekId) REFERENCES jatekok(id),
    ADD FOREIGN KEY (alanyId) REFERENCES alanyok(id),
    ADD FOREIGN KEY (esemenyId) REFERENCES esemenyek(id);

ALTER TABLE eredmenyek 
    ADD FOREIGN KEY (jatekId) REFERENCES jatekok(id),
    ADD FOREIGN KEY (alanyId) REFERENCES alanyok(id),
    ADD FOREIGN KEY (esemenyId) REFERENCES esemenyek(id);
