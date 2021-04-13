CREATE TABLE Customer 
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Nama VARCHAR(32) NOT NULL,
	Email VARCHAR(32) NOT NULL,
	Alamat VARCHAR(255) NOT NULL,
	NoHandphone VARCHAR(32) NOT NULL,
	[Password] VARCHAR(10) NOT NULL,
	FotoProfile VARCHAR(255)
)

INSERT INTO Customer VALUES
('Putu', 'putu.duta@gmail.com', 'Jakarta', '088888', 'Putu123#', 'putud.jpg')

